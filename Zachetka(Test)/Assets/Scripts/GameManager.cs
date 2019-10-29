using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UMA;
using UMA.CharacterSystem;
using Michsky.UI.CCUI;

public class GameManager : MonoBehaviour

{
    public GameObject loadingObject;
    public GameObject genderSwitch;
    public GameObject avatar;
    public string recipeFilePath;

    void Awake()
    {
        PlayerData playerData = new PlayerData();

        string json = File.ReadAllText(recipeFilePath + "/MyCharSet.txt");
        playerData = JsonUtility.FromJson<PlayerData>(json);
        //Debug.Log(playerData.race);

        if (playerData.race == "HumanMaleDCS")
        {
            genderSwitch.GetComponentInChildren<SwitchManager>().isOn = true;
            avatar.GetComponent<DynamicCharacterAvatar>().LoadFromTextFile(recipeFilePath + "\\MyCharSet.txt");
        }
        else
        {
            genderSwitch.GetComponentInChildren<SwitchManager>().isOn = false;
            avatar.GetComponent<DynamicCharacterAvatar>().LoadFromTextFile(recipeFilePath + "\\MyCharSet.txt");
        }
    }

    void Start()
    {
        if (loadingObject)
        {
            loadingObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    public void LoginButton(GameObject obj)
    {
        string curText = obj.GetComponent<Button>().name;

        switch (curText)
        {
            case "Exit Button":
                Debug.Log("Exit works!");
                Application.Quit();
                break;

            case "Login Button":
                Debug.Log("Start");
                SceneManager.LoadScene("MainMenu");
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

            case "Profile Button":
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
            //Debug.Log(progress);

            yield return null;
        }
    }

    private class PlayerData
    {
        public string race;
    }
}
