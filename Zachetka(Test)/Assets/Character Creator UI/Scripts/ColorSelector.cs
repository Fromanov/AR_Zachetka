using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Michsky.UI.CCUI
{
    public class ColorSelector : MonoBehaviour
    {
        [Header("RESOURCES")]
        public TextMeshProUGUI colorTextLight;
        public TextMeshProUGUI colorTextDark;
        public Image colorIconLight;
        public Image colorIconDark;

        [Header("SETTINGS")]
        public Color mainColor;
        public bool isCustom;
        public bool UMAEnabled;

        private HorizontalLayoutGroup hlpLight;
        private HorizontalLayoutGroup hlpDark;
        private Michsky.UI.CCUI.UMAChangeColor umaColorScript;

        void Start()
        {
            hlpLight = colorTextLight.GetComponentInParent<HorizontalLayoutGroup>();
            hlpDark = colorTextDark.GetComponentInParent<HorizontalLayoutGroup>();
            umaColorScript = this.GetComponent<UMAChangeColor>();
        }

        public void SetColor(string colorName)
        {
            colorTextLight.text = colorName;
            colorTextDark.text = colorName;
            colorIconLight.color = mainColor;
            colorIconDark.color = mainColor;

            if (UMAEnabled == true)
            {
                umaColorScript.selectedColor = mainColor;
            }

            // BECAUSE UNITY UI IS BUGGY AND NEEDS REFRESHING :P
            StartCoroutine(ExecuteAfterTime(0.01f));
        }

        IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);

            hlpLight.enabled = false;
            hlpLight.enabled = true;
            hlpDark.enabled = false;
            hlpDark.enabled = true;

            StopCoroutine(ExecuteAfterTime(0.01f));
        }
    }
}