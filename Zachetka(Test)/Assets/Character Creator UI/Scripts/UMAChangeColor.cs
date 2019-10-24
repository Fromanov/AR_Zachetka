using UnityEngine;

namespace Michsky.UI.CCUI
{
    public class UMAChangeColor : MonoBehaviour
    {
        [Header("RESOURCES")]
        public UMACustomizer customizerScript;

        [Header("SETTINGS")]
        public Color selectedColor;

        public void ChangeSkinColor()
        {
            customizerScript.avatar.SetColor("Skin", selectedColor);
            customizerScript.avatar.UpdateColors(true);
        }

        public void ChangeEyesColor()
        {
            customizerScript.avatar.SetColor("Eyes", selectedColor);
            customizerScript.avatar.UpdateColors(true);
        }

        public void ChangeEarsColor()
        {
            customizerScript.avatar.SetColor("Ears", selectedColor);
            customizerScript.avatar.UpdateColors(true);
        }

        public void ChangeHairColor()
        {
            customizerScript.avatar.SetColor("Hair", selectedColor);
            customizerScript.avatar.UpdateColors(true);
        }

        public void ChangeEyebrowsColor()
        {
            customizerScript.avatar.SetColor("Eyebrows", selectedColor);
            customizerScript.avatar.UpdateColors(true);
        }

        public void ChangeBeardColor()
        {
            customizerScript.avatar.SetColor("Beard", selectedColor);
            customizerScript.avatar.UpdateColors(true);
        }

        public void ChangeTorsoColor()
        {
            customizerScript.avatar.SetColor("Shirt", selectedColor);
            customizerScript.avatar.SetColor("Shirt1", selectedColor);
            customizerScript.avatar.SetColor("ShirtAccent", selectedColor);
            customizerScript.avatar.UpdateColors(true);
        }

        public void ChangeHandsColor()
        {
            customizerScript.avatar.SetColor("Hands", selectedColor);
            customizerScript.avatar.SetColor("AlternateHands", selectedColor);
            customizerScript.avatar.UpdateColors(true);
        }

        public void ChangeBottomColor()
        {
            customizerScript.avatar.SetColor("Legs", selectedColor);
            customizerScript.avatar.SetColor("Pants", selectedColor);
            customizerScript.avatar.SetColor("Pants1", selectedColor);
            customizerScript.avatar.UpdateColors(true);
        }

        public void ChangeFeetColor()
        {
            customizerScript.avatar.SetColor("Feet", selectedColor);
            customizerScript.avatar.SetColor("Shoes", selectedColor);
            customizerScript.avatar.UpdateColors(true);
        }

        public void ChangeUnderwearColor()
        {
            customizerScript.avatar.SetColor("Underwear", selectedColor);
            customizerScript.avatar.UpdateColors(true);
        }
    }
}