using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skidki : MonoBehaviour
{
	public GameObject ten;
	public GameObject twenty;
	public GameObject thirty;
	public GameObject forty;
	public GameObject fifty;
	public GameObject PanalOpisanieSkidok;
	public GameObject Text;
	public GameObject Button;
	public GameObject PanelSkidok;
	// Start is called before the first frame update
	void Start()
    {
        if(PlayerPrefs.GetInt("max") >= 18)
		{
			ten.SetActive(true);
		}
		if (PlayerPrefs.GetInt("max") >= 37)
		{
			twenty.SetActive(true);
		}
		if (PlayerPrefs.GetInt("max") >= 56)
		{
			thirty.SetActive(true);
		}
		if (PlayerPrefs.GetInt("max") >= 72)
		{
			forty.SetActive(true);
		}
		if (PlayerPrefs.GetInt("max") >= 87)
		{
			fifty.SetActive(true);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }




	public void PanelSkidokOn()
	{
		PanalOpisanieSkidok.SetActive(true);
		Text.SetActive(true);
		Button.SetActive(true);
		PanelSkidok.SetActive(false);
	}

	public void PanelSkidokOFF()
	{
		PanalOpisanieSkidok.SetActive(false);
		Text.SetActive(false);
		Button.SetActive(false);
		PanelSkidok.SetActive(true);
	}

	public void Menu()
	{
		SceneManager.LoadScene(0);
	}


}
