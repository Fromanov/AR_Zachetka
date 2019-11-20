using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelsPauseFail : MonoBehaviour
{
	public GameObject panelpause;
	public GameObject panelfail;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void ButtonsStack(GameObject obj)
	{
		string curState = obj.GetComponent<Button>().name;
		switch(curState)
		{
			case "Back Button":
				Time.timeScale = 0;
				panelpause.SetActive(true);
				break;
			case "Menu Button":
				SceneManager.LoadScene("MainMenu");
				break;
			case "Retry Button":
				SceneManager.LoadScene("StackRoom");
				break;
			case "Continue Button":
				//
				panelpause.SetActive(false);
				panelfail.SetActive(false);
				Time.timeScale = 1;
				break;

		}


	}



}
