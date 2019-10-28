using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UMA;
using UMA.CharacterSystem;


public class GameManager : MonoBehaviour

{
    public GameObject loadingObject;
    public GameObject genderSwitch;
    public GameObject avatar;

    void Start()
    {
        if (loadingObject)
        {
            loadingObject.SetActive(false);
        }

        if (genderSwitch)
        {            
            Debug.Log(genderSwitch.GetComponent<bool>());
        }        
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

    public void ARSwitcherButton(GameObject obj)
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
}
