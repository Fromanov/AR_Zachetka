using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UMA;
using UMA.CharacterSystem;
using Michsky.UI.CCUI;
using Michsky.UI.FieldCompleteMainMenu;

public class GameManager : MonoBehaviour

{
    public GameObject loadingObject;
    public GameObject genderSwitch;
    public GameObject avatar;
	public GameObject menuManager;
    public string recipeFilePath;
	public bool logged = true;

    void Awake()
    {
		UserSettings playerData = new UserSettings();

		//recipeFilePath = Application.persistentDataPath;

		if (recipeFilePath != null)
		{
			string json = File.ReadAllText(recipeFilePath + '/' + "MyCharSet.txt");
			playerData = JsonUtility.FromJson<UserSettings>(json);			

			if (playerData.race == "HumanMaleDCS")
			{
				genderSwitch.GetComponentInChildren<Michsky.UI.CCUI.SwitchManager>().isOn = true;
				Debug.Log(genderSwitch.GetComponentInChildren<Michsky.UI.CCUI.SwitchManager>().isOn);
				avatar.GetComponent<DynamicCharacterAvatar>().LoadFromTextFile(recipeFilePath + "/MyCharSet.txt");
			}
			else if (playerData.race == "HumanFemaleDCS")
			{
				genderSwitch.GetComponentInChildren<Michsky.UI.CCUI.SwitchManager>().isOn = false;
				Debug.Log(genderSwitch.GetComponentInChildren<Michsky.UI.CCUI.SwitchManager>().isOn);
				avatar.GetComponent<DynamicCharacterAvatar>().LoadFromTextFile(recipeFilePath + "/MyCharSet.txt");
			}
		}

		if (recipeFilePath != null)
		{
			recipeFilePath = Application.persistentDataPath;
		}

	}

    void Start()
    {
        if (loadingObject)
        {
            loadingObject.SetActive(false);
        }

		if (genderSwitch)
		{
			genderSwitch.SetActive(true);
		}
		
    }

    void Update()
    {
        
    }

    public void LoginButton(GameObject obj)
    {
        string curText = obj.name;

		Debug.Log("Pressed");

		switch (curText)
        {
            case "Exit Button":                
                Application.Quit();
                break;

            case "Login Button":
				//menuManager.gameObject.GetComponent<SplashScreenManager>().isLoggedIn = true;
				Debug.Log(menuManager.gameObject.GetComponent<SplashScreenManager>().isLoggedIn);
                break;
        }
    }

    public void MainMenuButton(GameObject obj)
    {
        string curState = obj.GetComponent<Button>().name;

        Debug.Log(curState);

        switch (curState)
        {
            case "Play Button":
                //SceneManager.LoadScene("");
                break;

            case "Character Button":
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

    public void LoadLevel (string sceneName)
    {
        StartCoroutine(LoadAsynchtonously(sceneName));        
    }

    IEnumerator LoadAsynchtonously (string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            yield return null;
        }
    }      
}
