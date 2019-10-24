using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Michsky.UI.CCUI
{
    public class RadioAnimManager : MonoBehaviour
    {
        [Header("BUTTON LIST")]
        public List<GameObject> buttons = new List<GameObject>();

        // [Header("PANEL ANIMS")]
        private string buttonFadeIn = "Hover to Pressed";
        private string buttonFadeOut = "Pressed to Normal";

        private GameObject currentButton;
        private GameObject nextButton;

        [Header("SETTINGS")]
        public int currentButtonIndex = 0;
        public bool enableAtStart = false;

        private Animator currentButtonAnimator;
        private Animator nextButtonAnimator;

        void Start()
        {
            if(enableAtStart == true)
            {
                currentButton = buttons[currentButtonIndex];
                currentButtonAnimator = currentButton.GetComponent<Animator>();
                currentButtonAnimator.Play(buttonFadeIn);
            }
        }

        public void PanelAnim(int newPanel)
        {
            if (newPanel != currentButtonIndex)
            {
                currentButton = buttons[currentButtonIndex];

                currentButtonIndex = newPanel;
                nextButton = buttons[currentButtonIndex];

                currentButtonAnimator = currentButton.GetComponent<Animator>();
                nextButtonAnimator = nextButton.GetComponent<Animator>();

                currentButtonAnimator.Play(buttonFadeOut);
                nextButtonAnimator.Play(buttonFadeIn);
            }
        }
    }
}