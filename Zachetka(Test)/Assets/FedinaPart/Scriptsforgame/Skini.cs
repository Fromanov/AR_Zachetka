using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Skini : MonoBehaviour
{

	public GameObject obj;
	public GameObject objtwo;
	public GameObject Zachetka;
	public GameObject Ground;
	public GameObject Grass;
	public GameObject brick;
	public GameObject Rock;
	public Toggle Nonet;
	public Toggle Zachetkat;
	public Toggle Groundt;
	public Toggle Grasst;
	public Toggle brickt;
	public Toggle Rockt;
	// Start is called before the first frame update
	void Start()
    {
		if (PlayerPrefs.GetInt("max") >= 18)
		{
			Zachetka.SetActive(true);
		}
		if (PlayerPrefs.GetInt("max") >= 37)
		{
			Ground.SetActive(true);
		}
		if (PlayerPrefs.GetInt("max") >= 56)
		{
			Grass.SetActive(true);
		}
		if (PlayerPrefs.GetInt("max") >= 72)
		{
			brick.SetActive(true);
		}
		if (PlayerPrefs.GetInt("max") >= 87)
		{
			Rock.SetActive(true);
		}


		if (PlayerPrefs.GetInt("Textur") == 0)
		{
			Nonet.isOn = true;
		}
		if (PlayerPrefs.GetInt("Textur") == 1)
		{
			Zachetkat.isOn = true;
		}
		if (PlayerPrefs.GetInt("Textur") == 2)
		{
			Groundt.isOn = true;
		}
		if (PlayerPrefs.GetInt("Textur") == 3)
		{
			Grasst.isOn = true;
		}
		if (PlayerPrefs.GetInt("Textur") == 4)
		{
			brickt.isOn = true;
		}
		if (PlayerPrefs.GetInt("Textur") == 5)
		{
			Rockt.isOn = true;
		}

	}

    // Update is called once per frame
    void Update()
    {

    }

	public void Menu()
	{
		SceneManager.LoadScene(0);
	}


	public void Instruc()
	{
		obj.SetActive(true);
		objtwo.SetActive(false);
	}

	public void Back()
	{
		obj.SetActive(false);
		objtwo.SetActive(true);
	}


	public void None(bool a)
	{
		if(a)
		{
			PlayerPrefs.SetInt("Textur", 0);
		}
	}


	public void Zach(bool a)
	{
		if (a)
		{
			PlayerPrefs.SetInt("Textur", 1);
		}
	}


	public void Groun(bool a)
	{
		if (a)
		{
			PlayerPrefs.SetInt("Textur", 2);
		}
	}

	public void Gras(bool a)
	{
		if (a)
		{
			PlayerPrefs.SetInt("Textur", 3);
		}
	}

	public void Brik(bool a)
	{
		if (a)
		{
			PlayerPrefs.SetInt("Textur", 4);
		}
	}

	public void Rok(bool a)
	{
		if (a)
		{
			PlayerPrefs.SetInt("Textur", 5);
		}
	}



}
