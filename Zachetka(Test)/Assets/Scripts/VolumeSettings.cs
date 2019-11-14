using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;

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
		currentSettings = new Settings();
		SAVE_PATH = Application.persistentDataPath + "/VolumeSettings.txt";

		masterSlider.onValueChanged.AddListener(delegate { OnSliderChange(mMaster); });
		soundFxSlider.onValueChanged.AddListener(delegate { OnSliderChange(mSound); });
		musicSlider.onValueChanged.AddListener(delegate { OnSliderChange(mMusic); });

		Settings defaultSettings = new Settings(100f, 50f, 50f);


		if (File.Exists(SAVE_PATH))
		{

			currentSettings = JsonUtility.FromJson<Settings>(File.ReadAllText(SAVE_PATH));
			Debug.Log(currentSettings);

		}
		else
		{
			Debug.Log("NOT EXIST");
			currentSettings = defaultSettings;
			string jsonSettings = JsonUtility.ToJson(defaultSettings);

			File.CreateText(SAVE_PATH).Dispose();
			using (TextWriter writer = new StreamWriter(SAVE_PATH, false))
			{
				writer.Write(jsonSettings);
				writer.Close();
			}


		}
		masterSlider.value = currentSettings.Master_Value;
		soundFxSlider.value = currentSettings.SoundFx_Value;
		musicSlider.value = currentSettings.Music_Value;
		


	}

	private void Start()
	{
		Master.audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterSlider.value / 100) * 20);
		Music.audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value / 100) * 20);
		SoundFx.audioMixer.SetFloat("SoundFxVolume", Mathf.Log10(soundFxSlider.value / 100) * 20);
		SceneManager.activeSceneChanged += ChangedActiveScene;

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


	private void ChangedActiveScene(Scene current, Scene next)
	{
		SaveSettings();
		
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

		public Settings() { }
	}

	

	private void OnApplicationQuit()
	{
		SaveSettings();
		
	}


	private void SaveSettings()
	{
		Debug.Log(SAVE_PATH);
		string jsonSettings = JsonUtility.ToJson(currentSettings);

		using (TextWriter writer = new StreamWriter(SAVE_PATH, false))
		{
			writer.Write(jsonSettings);
			writer.Close();
		}
	}
}