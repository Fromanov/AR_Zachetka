using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Michsky.UI.CCUI
{
    public class ColorPickerWheel : MonoBehaviour
    {
        [Header("RESOURCES")]
        public Michsky.UI.CCUI.ColorSelector mainColorSelector;
        public Image thumb;
        public Image colorPalette;

        [Header("SETTINGS")]
        public Color colorValue = Color.white;
        public UnityEvent selectEvent;

        public void OnPress()
        {
            UpdateThumbPosition();
        }

        public void OnDrag()
        {
            UpdateThumbPosition();
        }

        private Color GetColor()
        {
            Vector2 spectrumScreenPosition = colorPalette.transform.position;
            Vector2 ThumbScreenPosition = thumb.transform.position;
            Vector2 SpectrumXY = new Vector2(colorPalette.GetComponent<RectTransform>().rect.width, colorPalette.GetComponent<RectTransform>().rect.height);
            Vector2 position = ThumbScreenPosition - spectrumScreenPosition + SpectrumXY * 0.5f;

            Texture2D texture = colorPalette.mainTexture as Texture2D;

            position = new Vector2((position.x / (colorPalette.GetComponent<RectTransform>().rect.width * colorPalette.transform.localScale.x)),
                                    position.y / (colorPalette.GetComponent<RectTransform>().rect.height * colorPalette.transform.localScale.y));

            Color SelectedColor = texture.GetPixelBilinear(position.x, position.y);
            SelectedColor.a = 1;
            return SelectedColor;
        }

        private void UpdateThumbPosition()
        {
            if (colorPalette.GetComponent<CircleCollider2D>())
            {
                Vector2 center = transform.position;
                Vector2 position = Input.mousePosition;
                Vector2 offset = position - center;
                Vector2 Set = Vector2.ClampMagnitude(offset, (colorPalette.GetComponent<CircleCollider2D>().radius));
                Vector2 newPos = center + Set;

                if (thumb.rectTransform.anchoredPosition != newPos)
                {
                    thumb.transform.position = newPos;
                    colorValue = GetColor();
                    mainColorSelector.mainColor = colorValue;
                    selectEvent.Invoke();
                }
            }  
        }
    }
}