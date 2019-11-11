using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Text scoreText;
    public GameObject Main;
    public GameObject Skid;
    public GameObject Skin;
    public GameObject InstrSkin;
    public GameObject InstrSkid;

    private void Start(){
		scoreText.text = PlayerPrefs.GetInt ("max").ToString();
	}

	public void ToGame(){
		SceneManager.LoadScene (1);
	}

	public void Exit()
	{
		Application.Quit();
	}


	public void Skidki()
	{
        Skid.SetActive(true);
        Main.SetActive(false);
        Skin.SetActive(false);
    }



	public void Skini()
	{
        Skid.SetActive(false);
        Main.SetActive(false);
        Skin.SetActive(true);
    }

    public void  Back()
    {
        Skid.SetActive(false);
        Main.SetActive(true);
        Skin.SetActive(false);
    }
       
    public void InstrucSkinOn()
    {
        InstrSkin.SetActive(true);
    }

    public void InstrucSkidOn()
    {
        InstrSkid.SetActive(true);
    }
    public void InstrucSkinOf()
    {
        InstrSkin.SetActive(false);
    }

    public void InstrucSkidOf()
    {
        InstrSkid.SetActive(false);
    }
}
