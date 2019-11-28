using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UMA;
using UMA.CharacterSystem;
using Michsky.UI.CCUI;
using UnityEngine.Networking;


public class GameManager : MonoBehaviour

{
	[Header("Login stuff")]
	[SerializeField]
	private GameObject Login_Canvas;
	[SerializeField]
	private GameObject Register_Canvas;

	[Header("Main menu")]
	public GameObject loadingObject;
	public GameObject menuPanelObject;
	public GameObject mainStacker;
	public GameObject skinsStacker;
	public GameObject instructionStacker;	

	[Header("Uma stuff")]
	public GameObject genderSwitch;
	public GameObject avatar;
	public string recipeFilePath;


	void Awake()
	{

		recipeFilePath = Application.persistentDataPath + "/CharacterRecipes";

		PlayerData playerData = new PlayerData();

		if (!Directory.Exists(recipeFilePath))
		{			
			Directory.CreateDirectory(recipeFilePath);
		}		

		if (!File.Exists(recipeFilePath + '/' + "MyCharSet.txt"))
		{			
			avatar.GetComponent<UMACustomizer>().ResetClick();
		}
		else
		{
			string json = File.ReadAllText(recipeFilePath + '/' + "MyCharSet.txt");
			playerData = JsonUtility.FromJson<PlayerData>(json);

			if (playerData.race == "HumanMaleDCS")
			{				
				if (genderSwitch)
				{
					genderSwitch.GetComponentInChildren<SwitchManager>().isOn = true;
					Debug.Log(genderSwitch.GetComponentInChildren<SwitchManager>().isOn);
					avatar.GetComponent<UMACustomizer>().LoadRecipe();
					genderSwitch.SetActive(true);
				}
				else
				{
					if (avatar)
					{
						avatar.GetComponent<UMACustomizer>().LoadRecipe();
					}
				}
			}
			else if (playerData.race == "HumanFemaleDCS")
			{				
				if (genderSwitch)
				{
					genderSwitch.GetComponentInChildren<SwitchManager>().isOn = false;
					Debug.Log(genderSwitch.GetComponentInChildren<SwitchManager>().isOn);
					avatar.GetComponent<UMACustomizer>().LoadRecipe();
					genderSwitch.SetActive(true);
				}
				else
				{
					if(avatar)
					{
						avatar.GetComponent<UMACustomizer>().LoadRecipe();
					}					
				}
			}
		}		
	}

	void Start()
	{
		if (loadingObject)
		{
			loadingObject.SetActive(false);
		}

		LunarConsolePlugin.LunarConsole.Show();
	}


	public void Button(GameObject obj)
	{
		string tag = obj.tag;

		switch (tag)
		{
			case "Exit Button":
				Debug.Log("Exit works!");
				Application.Quit();
				break;
		}
	}

	public void MainMenuButton(GameObject obj)
	{
		string curState = obj.GetComponent<Button>().name;

		Debug.Log(curState);

		switch (curState)
		{
			case "Character Button":
				menuPanelObject.SetActive(false);
				loadingObject.SetActive(true);				
				LoadLevel("CharacterRoom");
				break;

			case "Back":
				SceneManager.LoadScene("MainMenu");
				break;
		}
	}

	public void ARSwitcherButton(GameObject obj) // AR switcher method
	{
		string curState = obj.GetComponentInChildren<Button>().name;

		switch (curState)
		{
			case "Off":
				SceneManager.LoadScene("GyroRoom");
				break;

			case "On":
				SceneManager.LoadScene("MainMenu");
				break;
		}
	}

	public void ARStackerButtons(GameObject obj) // AR switcher method
	{
		string curState = obj.GetComponentInChildren<Button>().name;

		switch (curState)
		{
			case "Play Button":
				SceneManager.LoadScene("StackRoom");
				break;

			case "Skins Button":				
				mainStacker.SetActive(false);
				skinsStacker.SetActive(true);
				instructionStacker.SetActive(false);	
				break;

			case "Sales Button":				
				mainStacker.SetActive(false);
				skinsStacker.SetActive(false);
				instructionStacker.SetActive(false);
				break;

			case "Instruction Button":				
				mainStacker.SetActive(false);
				skinsStacker.SetActive(false);
				instructionStacker.SetActive(true);
				break;

			case "AR Stack Game":				
				mainStacker.SetActive(true);
				skinsStacker.SetActive(false);
				instructionStacker.SetActive(false);				
				break;

			case "Back Button":				
				mainStacker.SetActive(true);
				skinsStacker.SetActive(false);
				instructionStacker.SetActive(false);				
				break;
		}
	}
	public void ArStackerPause(GameObject obj)
	{
		string curState = obj.GetComponent<Button>().name;
		switch (curState)
		{
			case "Back Button":
				Debug.Log("Paues");
				Time.timeScale = 0;
				
				break;
			case "Continue Button":
				Debug.Log("Continue");
				Time.timeScale = 1;
				break;
		}
	}


	public void LoadLevel(string sceneName)
	{
		StartCoroutine(LoadAsynchtonously(sceneName));
	}

	IEnumerator LoadAsynchtonously(string sceneName)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / .9f);
			//Debug.Log(progress);

			yield return null;
		}
	}


	public void SaveRecepie()
	{
		avatar.GetComponent<UMACustomizer>().SaveRecipe();
	}

	private class PlayerData
	{
		public string race;
	}
}