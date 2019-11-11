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

public class LoginManager : MonoBehaviour
{
	[SerializeField]
	private Text Error_Message;
	[SerializeField]
	private Text username_login;
	[SerializeField]
	private Text password_login;

	[SerializeField]
	private Text firstname_register;
	[SerializeField]
	private Text lastname_register;
	[SerializeField]
	private Text dob_register;

	[SerializeField]
	private Text email_register;
	[SerializeField]
	private Text password_register;
	[SerializeField]
	private Text confirm_password_register;

	private GameManager gameManager;

	private string TAG = "LoginManager";

	public static LoginManager instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		if (gameManager == null)
		{
			Debug.Log(TAG + "Game Manager is NULL");
		}
	}

	public void Login()
	{
		string username = username_login.text;
		string pass = password_login.text;

		
		if (username.Length == 0 || pass.Length == 0)
		{
			StartCoroutine(instance.ShowError("Все поля должны быть заполнены"));
			return;
		}


		UserData authData = new UserData(username, pass);
		string jsonAuth = JsonUtility.ToJson(authData);
		Debug.Log(jsonAuth);
		instance.StartCoroutine(instance.LogIn(jsonAuth));
	}

	public void Register()
	{
		string firstname = firstname_register.text;
		string lastname = lastname_register.text;
		string dob = dob_register.text;
		string username = email_register.text;
		string pass = password_register.text;
		string confirm_pass = confirm_password_register.text;

		
		if (firstname.Length == 0 || lastname.Length == 0 || dob.Length == 0 || username.Length == 0 || pass.Length == 0 || confirm_pass.Length == 0)
		{
			StartCoroutine(instance.ShowError("Все поля должны быть заполнены"));
			
			return;
		}
		else if (!pass.Equals(confirm_pass))
		{
			StartCoroutine(instance.ShowError("Пароли должны совпадать"));
			
			return;

		}

		UserData registerData = new UserData(firstname, lastname, dob, username, pass);
		string registerDataJson = JsonUtility.ToJson(registerData);
		StartCoroutine(Register(registerDataJson));
	}


	IEnumerator LogIn(string json)
	{
		UnityWebRequest request = UnityWebRequest.Put("http://10.128.125.10/Api/autification/login.php", json);
		request.SetRequestHeader("Content_type", "application/json");
		yield return request.SendWebRequest();
		if (request.isNetworkError)
		{
			Debug.Log(request.error);

		}
		else
		{
			Debug.Log(request.downloadHandler.text);
			LoginResponseObject response = JsonUtility.FromJson<LoginResponseObject>(request.downloadHandler.text);
			if (response.jwt != null && response.jwt.Length > 200)
			{
				gameManager.LoadLevel("Main Menu");
			}
			else
			{
				Debug.Log("Token is not right");
			}
		}

	}

	IEnumerator Register(string json)
	{
		UnityWebRequest request = UnityWebRequest.Put("http://10.128.125.10/Api/autification/create_user.php", json);
		request.SetRequestHeader("Content_type", "application/json");
		yield return request.SendWebRequest();
		if (request.isNetworkError)
		{
			Debug.Log(request.error);

		}
		else
		{
			Debug.Log(request.downloadHandler.text);
			RegisterResponseObject response = JsonUtility.FromJson<RegisterResponseObject>(request.downloadHandler.text);
			if (response.message != null && response.message.Equals("User was created."))
			{
	//			Login_Canvas.SetActive(true);
//				Register_Canvas.SetActive(false);

			}
			else
			{
				Debug.Log(response.message);
			}
		}

	}

	public class UserData
	{
		public string firstname;
		public string lastname;
		public string dob;
		public string email;
		public string password;

		public UserData(string firstname, string lastname, string dob, string email, string password)
		{
			this.email = email;
			this.password = password;
			this.firstname = firstname;
			this.lastname = lastname;
			this.dob = dob;
		}

		public UserData(string email, string password)
		{
			this.email = email;
			this.password = password;

		}

	}

	public class LoginResponseObject
	{
		public string message;
		public string jwt;
	}

	public class RegisterResponseObject
	{
		public string message;
	}



	IEnumerator ShowError(string msg)
	{
		Error_Message.gameObject.SetActive(true);
		Error_Message.text = msg;
		yield return new WaitForSeconds(5f);
		Error_Message.gameObject.SetActive(false);
		Error_Message.text = "";
	}
}
