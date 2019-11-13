using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.IO;
using System;

public class VolumeSettings : MonoBehaviour
{

	[SerializeField]
	private AudioMixerGroup Master;
	[SerializeField]
	private AudioMixerGroup SoundFx;
	[SerializeField]
	private AudioMixerGroup Music;

	[SerializeField]
	private Slider masterSlider;
	[SerializeField]
	private Slider soundFxSlider;
	[SerializeField]
	private Slider musicSlider;
	private string mMaster = "master";
	private string mMusic = "music";
	private string mSound = "sound";
	private Settings currentSettings;



	private string SAVE_PATH;

	private void Awake()
	{
		currentSettings = new Settings(masterSlider.value, musicSlider.value, soundFxSlider.value);
		SAVE_PATH = Application.dataPath + "/VolumeSettings.txt";

		masterSlider.onValueChanged.AddListener(delegate { OnSliderChange(mMaster); });
		soundFxSlider.onValueChanged.AddListener(delegate { OnSliderChange(mSound); });
		musicSlider.onValueChanged.AddListener(delegate { OnSliderChange(mMusic); });

		Settings defaultSettings = new Settings(100f, 50f, 50f);


		if (File.Exists(SAVE_PATH))
		{

			defaultSettings = JsonUtility.FromJson<Settings>(File.ReadAllText(SAVE_PATH));

		}
		else
		{
			string jsonSettings = JsonUtility.ToJson(defaultSettings);

			File.CreateText(SAVE_PATH).Dispose();
			using (TextWriter writer = new StreamWriter(SAVE_PATH, false))
			{
				writer.Write(jsonSettings);
				writer.Close();
			}


		}
		masterSlider.value = defaultSettings.Master_Value;
		soundFxSlider.value = defaultSettings.SoundFx_Value;
		musicSlider.value = defaultSettings.Music_Value;
		Master.audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterSlider.value / 100) * 20);
		Music.audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value / 100) * 20);
		SoundFx.audioMixer.SetFloat("SoundFxVolume", Mathf.Log10(soundFxSlider.value / 100) * 20);
	}

	private void OnSliderChange(string type)
	{

		switch (type)
		{
			case "sound":
				SoundFx.audioMixer.SetFloat("SoundFxVolume", Mathf.Log10(soundFxSlider.value / 100) * 20);
				currentSettings.SoundFx_Value = soundFxSlider.value;
				break;
			case "master":
				Master.audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterSlider.value / 100) * 20);
				currentSettings.Master_Value = masterSlider.value;
				break;
			case "music":
				Music.audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value / 100) * 20);
				currentSettings.Music_Value = musicSlider.value;
				break;


		}

	}





	public class Settings
	{
		public float Master_Value;
		public float Music_Value;
		public float SoundFx_Value;

		public Settings(float master, float music, float soundFx)
		{
			Master_Value = master;
			Music_Value = music;
			SoundFx_Value = soundFx;
		}
	}

	private void OnApplicationQuit()
	{
		

		string jsonSettings = JsonUtility.ToJson(currentSettings);

		using (TextWriter writer = new StreamWriter(SAVE_PATH, false))
		{
			writer.Write(jsonSettings);
			writer.Close();
		}
	}
}


