using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Michsky.UI.CCUI
{
    public class UMADnaSlider : MonoBehaviour
    {
        [Header("RESOURCES")]
        public UMACustomizer customizerScript;
        public Slider mainSlider;

        [Header("SETTINGS")]
        public string DNATag = "height";

        IEnumerator GetSliderValue()
        {
            yield return new WaitForSeconds(0.1f);
            mainSlider.value = customizerScript.dna[DNATag].Get();
            StopCoroutine("GetSliderValue");
        }

        void Start()
        {
            StartCoroutine("GetSliderValue");
        }

        void OnEnable()
        {
            mainSlider.onValueChanged.AddListener(ChangeDNA);
        }

        void OnDisable()
        {
            mainSlider.onValueChanged.RemoveListener(ChangeDNA);
        }

        public void ChangeDNA(float value)
        {
            customizerScript.dna[DNATag].Set(value);
            customizerScript.avatar.BuildCharacter();
        }
    }
}