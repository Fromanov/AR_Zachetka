using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Threading;
using Firebase.Extensions;
using UMA;
using UMA.CharacterSystem;
using Michsky.UI.CCUI;

public class FirebaseClass : MonoBehaviour
{

	private string TAG = "FIrebaseAuth";
	private Firebase.Auth.FirebaseAuth auth;
	private Firebase.Auth.FirebaseUser user;
	private Firebase.Auth.FirebaseUser newUser;
	DatabaseReference reference;
	private bool Loading = false;


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

	private GameManager gameManager;
	public delegate void MethodContainer();

	public event MethodContainer onCount;

	public string dataHoursJson;
	public string dataCoinsJson;

	public void Awake()
	{
		InitializeFirebase();
		DontDestroyOnLoad(this);		
		
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		if (gameManager == null)
		{
			Debug.Log(TAG + "Game Manager is NULL");
		}
	}

	//public void Register()
	//{
	//	auth.CreateUserWithEmailAndPasswordAsync(email_register.text, password_register.text).ContinueWith(task =>
	//	{
	//		if (task.IsCanceled)
	//		{
	//			Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
	//			return;
	//		}
	//		if (task.IsFaulted)
	//		{
	//			Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
	//			return;
	//		}

	//		// Firebase user has been created.
	//		Firebase.Auth.FirebaseUser newUser = task.Result;
	//		Debug.LogFormat("Firebase user created successfully: {0} ({1})",
	//			newUser.DisplayName, newUser.UserId);
	//	});
	//}

	public void Register()
	{
		if (phone_register.text.Length != 11)
		{
			Debug.Log("Неверно введен номер телефона");
			return;
		}

		User user = new User();
		user.Email = email_register.text;
		user.Password = password_register.text;
		user.Phone_number = phone_register.text;
		Debug.Log(email_register.text);
		Debug.Log(password_register.text);

		auth.CreateUserWithEmailAndPasswordAsync(email_register.text, password_register.text).ContinueWith(createUserTask =>
		{
			if (createUserTask.IsCanceled)
			{
				Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
				return;
			}
			if (createUserTask.IsFaulted)
			{
				Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + createUserTask.Exception);
				return;
			}

			// Firebase user has been created.
			Firebase.Auth.FirebaseUser newUser = createUserTask.Result;
			Debug.LogFormat("Firebase user created successfully: {0} ({1})",
				newUser.DisplayName, newUser.UserId);



			IDictionary<string, object> updatedFields = new Dictionary<string, object>{
				{"Email", user.Email},
				{"Password", user.Password},
				{"Phone number", user.Phone_number},
				{"Id", newUser.UserId}
					   		};

			reference.Child("users").Child(newUser.UserId).UpdateChildrenAsync(updatedFields).ContinueWithOnMainThread(task =>
			{
				if (task.IsFaulted)
				{
					Debug.Log("Error " + task.Exception);

				}
				else if (task.IsCompleted)
				{
					Debug.Log("User's data was successfully updated");
				}
			});

		});


	}

	public void Signin()
	{
		auth.SignInWithEmailAndPasswordAsync(email_login.text, password_login.text).ContinueWithOnMainThread(authTask =>
		{
			if (authTask.IsCanceled)
			{
				Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
				return;
			}
			if (authTask.IsFaulted)
			{
				Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + authTask.Exception);
				return;
			}
			if (authTask.IsCompleted && authTask.Result != null)
			{
				newUser = authTask.Result;
				gameManager.LoadLevel("MainMenu");
				GetHoursDataFromDB();
				GetCoinsDataFromDB();
			}
		});
	}

	// Track state changes of the auth object.
	void AuthStateChanged(object sender, System.EventArgs eventArgs)
	{
		if (auth.CurrentUser != user)
		{
			bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
			if (!signedIn && user != null)
			{
				Debug.Log("Signed out " + user.UserId);
			}
			user = auth.CurrentUser;
			if (signedIn)
			{
				Debug.Log("Signed in " + user.UserId);
			}
		}
	}

	private void InitializeFirebase()
	{
		Debug.Log("Setting up Firebase Auth");
		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		auth.StateChanged += AuthStateChanged;
		AuthStateChanged(this, null);
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://zachetka-d22be.firebaseio.com/");
		reference = FirebaseDatabase.DefaultInstance.RootReference;
	}

	public void WriteDataInDB(string avatarData)
	{
		Debug.Log(avatarData);
		reference.Child("users").Child(user.UserId).Child("Avatar").SetRawJsonValueAsync(avatarData);
		//reference.Child("users").Child(user.UserId).Child("hoursinvr").SetRawJsonValueAsync(data); 
		//reference.Child("users").Child(user.UserId).Child("vrcoin").SetRawJsonValueAsync(data);
	}

	public void GetDataFromDB()
	{
		reference.Child("users").Child(user.UserId).Child("Avatar").GetValueAsync().ContinueWithOnMainThread(dataTask =>
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

	public void GetHoursDataFromDB()
	{
		reference.Child("users").Child(user.UserId).Child("hoursinvr").GetValueAsync().ContinueWithOnMainThread(dataTask =>
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
				dataHoursJson = dataTask.Result.GetRawJsonValue();
			}
		});		
	}

	public void GetCoinsDataFromDB()
	{
		reference.Child("users").Child(user.UserId).Child("vrcoin").GetValueAsync().ContinueWithOnMainThread(dataTask =>
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
				dataCoinsJson = dataTask.Result.GetRawJsonValue();				
			}
		});
	}

	void OnDestroy()
	{
		auth.StateChanged -= AuthStateChanged;
		auth.SignOut();
		auth = null;
	}

	private void OnApplicationQuit()
	{
		if (auth != null)
		{
			auth.SignOut();
		}
	}
}