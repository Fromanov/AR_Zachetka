using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsHandler : MonoBehaviour
{
	/*public GameObject rulesButton;
	public GameObject termsButton;*/
	public GameObject rulesPanel;
	public GameObject singUpPanel;	
	
    
	public void TermsOfUseButton()
	{
		Application.OpenURL("https://www.iubenda.com/privacy-policy/37730946/full-legal");
	}

	public void RulesOfLabButton()
	{
		singUpPanel.SetActive(false);
		rulesPanel.SetActive(true);
	}

	public void BackButton()
	{
		singUpPanel.SetActive(true);
		rulesPanel.SetActive(false);
	}
}
