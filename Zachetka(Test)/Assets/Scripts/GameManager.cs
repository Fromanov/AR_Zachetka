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
	public GameObject playerProfile;

	[Header("Uma stuff")]
	public GameObject genderSwitch;
	public GameObject avatar;
	public string recipeFilePath;


	[SerializeField]
	public List<GameObject> Panels;

	private FirebaseClass firebase;

	void Awake()
	{

		recipeFilePath = Application.persistentDataPath + "/CharacterRecipes";

		PlayerData playerData = new PlayerData();

		if (!Directory.Exists(recipeFilePath))
		{			
			Directory.CreateDirectory(recipeFilePath);
		}		

		if (!File.Exists(recipeFilePath + '/' + "MyCharSet.txt") && avatar != null)
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
		firebase = GameObject.Find("Firebase").GetComponent<FirebaseClass>();
		LunarConsolePlugin.LunarConsole.Show();
		ShowLoading(false);
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
				//loadingObject.SetActive(true);
				ShowLoading(true);
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
				menuPanelObject.SetActive(false);
				//loadingObject.SetActive(true);
				ShowLoading(true);
				LoadLevel("gyroRoom2");
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

	public void SocialButtons(GameObject obj)
	{
		string curState = obj.GetComponent<Button>().name;
		switch (curState)
		{
			case "Review Button":
				Application.OpenURL("https://www.vl.ru/laboratoriya-virtualnoj-realnosti-vrlab-dv");
				break;

			case "Youtube Button":
				Application.OpenURL("https://www.youtube.com/channel/UCpzY0Au0ZNkA6wvYxdvE0Zw");
				break;

			case "Tripadvisor Button":
				Application.OpenURL("https://www.tripadvisor.ru/Attraction_Review-g298496-d12324222-Reviews-Laboratory_of_Virtual_Reality_VRLABDV-Vladivostok_Primorsky_Krai_Far_Eastern_Dis.html");
				break;

			case "Facebook Button":
				Application.OpenURL("https://www.facebook.com/vrlab.play/");
				break;

			case "VK Button":
				Application.OpenURL("https://vk.com/vrlabdv");
				break;

			case "Insta play Button":
				Application.OpenURL("https://www.instagram.com/vrlab.play/");
				break;

			case "Insta dev Button ":
				Application.OpenURL("https://www.instagram.com/vrlab.dev/?igshid=1kypfzjhf5cyk");
				break;
		}
	}

	public void ProfileAfatarDisabler()
	{
		if(playerProfile.activeSelf)
		{
			playerProfile.SetActive(false);
		}
		else
		{
			playerProfile.SetActive(true);
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

	public void ShowLoading(bool show)
	{		
		if (show == true)
		{
			loadingObject.SetActive(true);
			Debug.Log("Loading");
		} else
		{
			loadingObject.SetActive(false);
		}
	}

	
	//void Update()
	//{
	//	//Debug.Log("efef");

	//	if (Input.GetKeyDown(KeyCode.Space))
	//	{

	//		PlayerPrefs.SetInt("prevMax", 10);
	//		PlayerPrefs.SetInt("max", 26);
	//		UpdateScore();
	//	}
	//}

	public void SignOut()
	{
		if (firebase == null)
			firebase = GameObject.Find("Firebase").GetComponent<FirebaseClass>();
		firebase.SignOut();
	}

	//Stacker Logic

	//public void UpdateScore()
	//{
	//	Debug.Log("Update score");
	//	firebase.UpdateScore();
	//}

}