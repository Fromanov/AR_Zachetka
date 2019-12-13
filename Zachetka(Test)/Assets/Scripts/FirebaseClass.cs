using Dobro.Text.RegularExpressions;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Unity.Editor;
using FullSerializer;
using Michsky.UI.CCUI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Apxfly.Verificator;


public class FirebaseClass : MonoBehaviour
{

	private string TAG = "FIrebaseAuth";
	private Firebase.Auth.FirebaseAuth Auth;
	private Firebase.Auth.FirebaseUser User;
	private PhoneAuthProvider Provider;
	DatabaseReference Reference;
	private bool Loading = false;

	private const string projectId = "zachetka-d22be"; // You can find this in your Firebase project settings
	private static readonly string databaseURL = $"https://{projectId}.firebaseio.com/";


	[SerializeField]
	private InputField email_login;
	[SerializeField]
	private InputField password_login;

	[SerializeField]
	private InputField email_register;
	[SerializeField]
	private InputField password_register;
	[SerializeField]
	private InputField phone_register;
	[SerializeField]
	private InputField name_register;
	[SerializeField]
	private InputField lastName_register;
	[SerializeField]
	private InputField patronymic_register;
	[SerializeField]
	private Dropdown[] birthday_register ;

	[SerializeField]
	private InputField phone_SMS_code;

	private GameManager gameManager;
	public delegate void MethodContainer();

	public event MethodContainer onCount;

	private string Email;
	private string Password;
	private string Phone_Number;
	private string Name;
	private string LastName;
	private string Patronymic;
	private string Birthday;

	private fsSerializer serializer = new fsSerializer();

	private UserStatsFromFirebase User_Stats = new UserStatsFromFirebase();
	private ProfileHandler profileHandler;

	private string verificationId;
	private string verificationCode;

	public GameObject rememberMeToggle;
	public GameObject tremsOfUseToggle;
	public GameObject labaRulesToggle;



	private void Start()
	{
		InitializeFirebase();
		DontDestroyOnLoad(this);

		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		if (gameManager == null)
			Debug.Log(TAG + "Game Manager is NULL");

		Auth.StateChanged += AuthStateChanged;
		AuthStateChanged(this, null);

		SceneManager.activeSceneChanged += OnSceneLoaded;

		email_login.text = PlayerPrefs.GetString(PrefsKey.RememberEmail) != null ? PlayerPrefs.GetString(PrefsKey.RememberEmail) : "";
		password_login.text = PlayerPrefs.GetString(PrefsKey.RememberPass) != null ? PlayerPrefs.GetString(PrefsKey.RememberPass) : "";

	}

	void OnSceneLoaded(Scene current, Scene next)
	{
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		if (gameManager == null)
		{
			Debug.Log(TAG + "Game Manager is NULL");
		}

		if (next.name.Equals("MainMenu"))
		{
			profileHandler = GameObject.Find("Profie Panel").GetComponent<ProfileHandler>();

		}
		else if (!next.name.Equals("MainMenu"))
		{
		}
	}



	public void Signin()
	{
		Signin(email_login.text, password_login.text);
	}


	private void Signin(string email, string pass)
	{

		if (!TestEmail.IsEmail(email_login.text))
		{
			Debug.Log("Неверно введен Email");
			return;
		}
		if (password_login.text.Length < 6)
		{
			Debug.Log("Пароль должен быть больше 6-ти символов");
			return;
		}
		gameManager.ShowLoading(true);
		Debug.Log("we are here");
		Email = email;
		Password = pass;
		Auth.SignInWithEmailAndPasswordAsync(email, pass).ContinueWithOnMainThread(authTask =>
		{
			if (authTask.IsCanceled)
			{
				Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
				gameManager.ShowLoading(false);
				return;
			}
			if (authTask.IsFaulted)
			{
				Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + authTask.Exception);
				gameManager.ShowLoading(false);
				return;
			}
			if (authTask.IsCompleted && authTask.Result != null)
			{
				User = authTask.Result;
			}
		});
	}


	private void HandleValueChanged(object sender, ChildChangedEventArgs args)
	{

		if (args.DatabaseError != null)
		{
			Debug.LogError(args.DatabaseError.Message);
			return;
		}

		Debug.Log("Changed");
		switch (args.Snapshot.Key)
		{
			case "PreviousHighScore":
				string PrevHS = args.Snapshot.GetRawJsonValue();
				User_Stats.PreviousHighScore = Convert.ToInt32(PrevHS);
				PlayerPrefs.SetInt(PrefsKey.OldHS, Convert.ToInt32(PrevHS));
				return;
			case "HighScore":
				string NewHS = args.Snapshot.GetRawJsonValue();
				User_Stats.HighScore = Convert.ToInt32(NewHS);
				PlayerPrefs.SetInt(PrefsKey.NewHS, Convert.ToInt32(NewHS));
				return;
			case "Hours":
				Debug.Log("Changed hours");
				string dataHours = args.Snapshot.GetRawJsonValue();
				PlayerPrefs.SetInt(PrefsKey.Hours, Convert.ToInt32(dataHours));
				profileHandler.UpdateUserStats();

				return;
			case "Coin":
				Debug.Log("Changed coin");
				string dataCoins = args.Snapshot.GetRawJsonValue();
				PlayerPrefs.SetInt(PrefsKey.Coin, Convert.ToInt32(dataCoins));
				profileHandler.UpdateUserStats();
				return;
		}


	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log(birthday_register[2].options[birthday_register[2].value].text);
			
			
		}
	}

	public void NewRegister()
	{
		if (tremsOfUseToggle.GetComponentInChildren<Toggle>().isOn && labaRulesToggle.GetComponentInChildren<Toggle>().isOn)
		{
			string number = Verificator.IsValidPhoneNumber(phone_register.text);
			if (number == null)
			{
				Debug.Log("Неверно введен номер телефона");
				return;
			}
			if (!TestEmail.IsEmail(email_register.text))
			{
				Debug.Log("Неверно введен Email");
				return;
			}
			if (password_register.text.Trim().Length < 6)
			{
				Debug.Log("Пароль должен быть больше 6-ти символов");
				return;
			}
			if (name_register.text.Trim().Length < 2)
			{
				Debug.Log("Введите Имя");
				return;
			}
			if (lastName_register.text.Trim().Length < 1)
			{
				Debug.Log("Введите Фамилию");
				return;
			}
			if (patronymic_register.text.Trim().Length < 2)
			{
				Debug.Log("Введите Отчество");
				return;
			}
			

			Email = email_register.text;
			Password = password_register.text;
			Phone_Number = number;
			Name = name_register.text;
			LastName = lastName_register.text;
			Patronymic = patronymic_register.text;
			Birthday = "" + birthday_register[0].options[birthday_register[0].value].text + "/" 
				+ birthday_register[1].options[birthday_register[1].value].text + "/" 
				+ birthday_register[2].options[birthday_register[2].value].text;
			gameManager.ShowLoading(true);

			uint phoneAuthTimeoutMs = 60 * 1000;
			Provider = PhoneAuthProvider.GetInstance(Auth);
			Provider.VerifyPhoneNumber(Phone_Number, phoneAuthTimeoutMs, null,
				verificationCompleted: (credential) =>
				{
					Debug.Log("Completed");
					SignInAndUpdate(credential);
				},
				verificationFailed: (error) =>
				{
					Debug.Log("error");
					Debug.Log(error);
					gameManager.ShowLoading(false);

				},
				codeSent: (id, token) =>
				{
					Debug.Log(id);
					verificationId = id;
					gameManager.Panels[1].GetComponent<Animator>().Play("SSCR Fade-out");
					gameManager.Panels[2].GetComponent<Animator>().Play("SSCR Fade-in");
					gameManager.ShowLoading(false);

				},
				codeAutoRetrievalTimeOut: (id) =>
				{
					Debug.Log("Phone Auth, auto-verification timed out");
					gameManager.ShowLoading(false);

				});
		}
	}

	public void SubmitSMSCode()
	{
		verificationCode = phone_SMS_code.text;

		Credential credential = Provider.GetCredential(verificationId, verificationCode);
		SignInAndUpdate(credential);

	}

	private void SignInAndUpdate(Credential credential)
	{
		Debug.Log("We are here");
		gameManager.ShowLoading(true);
		Auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
		{
			if (task.IsFaulted)
			{
				Debug.LogError("SignInWithCredentialAsync encountered an error: " +
							   task.Exception);
				return;
			}

			User = task.Result;

			Debug.Log("User signed in successfully");
			Firebase.Auth.Credential credentialUpdate = Firebase.Auth.EmailAuthProvider.GetCredential(Email, Password);
			User.LinkWithCredentialAsync(credentialUpdate).ContinueWith(UpdateCredentialTask =>
			{
				if (UpdateCredentialTask.IsCanceled)
				{
					Debug.LogError("LinkWithCredentialAsync was canceled.");
					return;
				}
				if (UpdateCredentialTask.IsFaulted)
				{
					Debug.LogError("LinkWithCredentialAsync encountered an error: " + UpdateCredentialTask.Exception);
					return;
				}

				User = UpdateCredentialTask.Result;
				Debug.LogFormat("Credentials successfully linked to Firebase user: {0} ({1})",
					User.Email);
			});

			IDictionary<string, object> updatedFields = new Dictionary<string, object>{
				{"Email", Email},
				{"Password", Password},
				{"Phone Number", Phone_Number},
				{"Id", User.UserId},
				{"Name", Name},
				{"LastName", LastName},
				{"Patronymic", Patronymic},
				{"Birthday", Birthday},
					   		};

			Reference.Child("users").Child(User.UserId).UpdateChildrenAsync(updatedFields).ContinueWithOnMainThread(UpdateTask =>
			{
				if (UpdateTask.IsFaulted)
				{
					Debug.Log("Error " + UpdateTask.Exception);

				}
				else if (UpdateTask.IsCompleted)
				{

					gameManager.Panels[2].GetComponent<Animator>().Play("SSCR Fade-out");

					Signin(Email, Password);
					Debug.Log("User's data was successfully updated");
					PlayerPrefs.SetString(PrefsKey.RememberEmail, Email);
					PlayerPrefs.SetString(PrefsKey.RememberPass, Password);
					Reference.Child("users").Child(User.UserId).ChildChanged += HandleValueChanged;

					GetData();
				}
			});

		});
		gameManager.ShowLoading(false);
	}

	// Track state changes of the auth object.
	void AuthStateChanged(object sender, System.EventArgs eventArgs)
	{
		if (Auth.CurrentUser != User)
		{
			bool signedIn = User != Auth.CurrentUser && Auth.CurrentUser != null;
			if (!signedIn && User != null)
			{
				Debug.Log("Signed out " + User.UserId);
				Reference.Child("users").Child(User.UserId).ChildChanged -= HandleValueChanged;
				User = null;
				gameManager.LoadLevel("LoginRoom");
			}
			User = Auth.CurrentUser;
			if (signedIn)
			{
				Debug.Log("Signed in " + User.UserId);

				if (tremsOfUseToggle.GetComponentInChildren<Toggle>().isOn)
				{
					PlayerPrefs.SetString(PrefsKey.RememberEmail, Email);
					PlayerPrefs.SetString(PrefsKey.RememberPass, Password);
				}
				else
				{
					PlayerPrefs.SetString(PrefsKey.RememberEmail, "");
					PlayerPrefs.SetString(PrefsKey.RememberPass, "");
				}

				gameManager.LoadLevel("MainMenu");
				Reference.Child("users").Child(User.UserId).ChildChanged += HandleValueChanged;

				GetData();
			}
		}
	}

	private void InitializeFirebase()
	{
		Debug.Log("Setting up Firebase Auth");
		Auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(databaseURL);
		Reference = FirebaseDatabase.DefaultInstance.RootReference;
	}

	public void WriteDataInDB(string avatarData)
	{
		Debug.Log(avatarData);
		Reference.Child("users").Child(User.UserId).Child("Avatar").SetRawJsonValueAsync(avatarData);

	}

	public void GetData()
	{
		Reference.Child("users").Child(User.UserId).GetValueAsync().ContinueWithOnMainThread(dataTask =>
		{
			if (dataTask.IsCanceled)
			{
				Debug.LogError("Loading data from Firebase was canceled.");
				return;
			}
			if (dataTask.IsFaulted)
			{
				Debug.LogError("Loading data from Firebase encountered an error: " + dataTask.Exception);
				return;
			}
			if (dataTask.IsCompleted && dataTask.Result != null)
			{
				string json = dataTask.Result.GetRawJsonValue();

				Debug.Log(json);

				var data = fsJsonParser.Parse(json);
				object deserialized = null;
				serializer.TryDeserialize(data, typeof(UserStatsFromFirebase), ref deserialized);

				User_Stats = deserialized as UserStatsFromFirebase;

				PlayerPrefs.SetInt(PrefsKey.Hours, Convert.ToInt32(User_Stats.Hours));
				PlayerPrefs.SetInt(PrefsKey.Coin, Convert.ToInt32(User_Stats.Coin));
				PlayerPrefs.SetInt(PrefsKey.OldHS, Convert.ToInt32(User_Stats.PreviousHighScore));
				PlayerPrefs.SetInt(PrefsKey.NewHS, Convert.ToInt32(User_Stats.HighScore));
				PlayerPrefs.SetString(PrefsKey.Name, User_Stats.Name);
				PlayerPrefs.SetString(PrefsKey.LastName, User_Stats.LastName);
				PlayerPrefs.SetString(PrefsKey.Patronymic, User_Stats.Patronymic);
				PlayerPrefs.SetString(PrefsKey.Birthday, User_Stats.Birthday);
				
			}
		});
	}

	public void GetRecipeFromDB()
	{
		Reference.Child("users").Child(User.UserId).Child("Avatar").GetValueAsync().ContinueWithOnMainThread(dataTask =>
		{
			if (dataTask.IsCanceled)
			{
				Debug.LogError("Loading data from Firebase was canceled.");
				return;
			}
			if (dataTask.IsFaulted)
			{
				Debug.LogError("Loading data from Firebase encountered an error: " + dataTask.Exception);
				return;
			}
			if (dataTask.IsCompleted && dataTask.Result != null)
			{
				Debug.Log(dataTask.Result.GetRawJsonValue());
				GameObject.Find("UMA Character Avatar").GetComponent<UMACustomizer>().LoadAvatar(dataTask.Result.GetRawJsonValue());
			}
		});
	}


	void OnDestroy()
	{
		Auth.StateChanged -= AuthStateChanged;
		Reference.Child("users").Child(User.UserId).ChildChanged -= HandleValueChanged;
		//Auth.SignOut();
		Auth = null;
	}


	public void SignOut()
	{
		Auth.SignOut();
	}


	public void SignInWithCredit(string googleIdToken, string googleAccessToken)
	{
		Firebase.Auth.Credential credential =
	Firebase.Auth.GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);
		Auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
		{
			if (task.IsCanceled)
			{
				Debug.LogError("SignInWithCredentialAsync was canceled.");
				return;
			}
			if (task.IsFaulted)
			{
				Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
				return;
			}

			User = task.Result;
			Debug.LogFormat("User signed in successfully: {0} ({1})",
				User.DisplayName, User.UserId);
		});
	}


}