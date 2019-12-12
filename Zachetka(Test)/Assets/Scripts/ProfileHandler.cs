using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileHandler : MonoBehaviour
{

	public GameObject userHours;
	public GameObject userRank;
	public GameObject userCoinValue;
	public GameObject userRankProfileAvatar;
	public GameObject userCoinValueProfileAvatar;
	public static UserProfileData userProfileData = new UserProfileData();

	private FirebaseClass firebase;	

	public void Avake()
	{
		firebase = GameObject.Find("Firebase").GetComponent<FirebaseClass>();
		//firebase.GetDataFromDB();
	}

	public void Start()
	{
		firebase = GameObject.Find("Firebase").GetComponent<FirebaseClass>();			

		UpdateUserStats();			
	}

	public string GetRank(int hours)
	{
		if(hours >= 0 && hours < 2)            //Абитуриент
		{
			return("Абитуриент");
		}
		else if(hours >= 2 && hours < 5)       //Студент 1к.
		{
			return ("Студент 1 курса");
		}
		else if (hours >= 5 && hours < 10)     //Студент 2к.
		{
			return ("Студент 2 курса");
		}
		else if (hours >= 10 && hours < 15)    //Студент 3к.
		{
			return ("Студент 3 курса");
		}
		else if (hours >= 15 && hours < 20)    //Студент 4к.
		{
			return ("Студент 4 курса");
		}
		else if (hours >= 20 && hours < 30)    //Студент 5к.
		{
			return ("Студент 5 курса");
		}
		else if (hours >= 30 && hours < 35)    //Аспирант.
		{
			return ("Аспирант");
		}
		else if (hours >= 35 && hours < 40)    //МНС.
		{
			return ("Младший научный сотрудник");
		}
		else if (hours >= 40 && hours < 45)    //ВНС.
		{
			return ("Ведущий научный сотрудник");
		}
		else if (hours >= 45 && hours < 50)    //ГНС.
		{
			return ("Главный научный сотрудник");
		}
		else if (hours >= 50 && hours < 70)    //Кандидат наук.
		{
			return ("Кандидат наук");
		}
		else if (hours >= 70 && hours < 85)    //Доцент.
		{
			return ("Доцент");
		}
		else if (hours >= 85 && hours < 100)    //Доктор наук.
		{
			return ("Доктор наук");
		}

		return null;
	}	

	public class UserProfileData
	{
		public int hoursInVR { get; set; }
		public int vrCoin;
	}




	public void UpdateUserStats()
	{
		userRank.GetComponent<UnityEngine.UI.Text>().text = GetRank(PlayerPrefs.GetInt(PrefsKey.Hours));
		userCoinValueProfileAvatar.GetComponent<UnityEngine.UI.Text>().text = PlayerPrefs.GetInt(PrefsKey.Coin).ToString();

		userRankProfileAvatar.GetComponent<UnityEngine.UI.Text>().text = GetRank(PlayerPrefs.GetInt(PrefsKey.Hours));
		userCoinValue.GetComponent<UnityEngine.UI.Text>().text = PlayerPrefs.GetInt(PrefsKey.Coin).ToString();
		userHours.GetComponent<UnityEngine.UI.Text>().text = PlayerPrefs.GetInt(PrefsKey.Hours) + " ч.";
	}
}
