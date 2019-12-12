using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;





public class FirebaseSms : MonoBehaviour
{

	protected Firebase.Auth.FirebaseAuth auth;
	protected Firebase.Auth.FirebaseAuth otherAuth;
	protected Dictionary<string, Firebase.Auth.FirebaseUser> userByAuth =
	new Dictionary<string, Firebase.Auth.FirebaseUser>();



	private void Start()
	{
		NewSignIn();
	}

	public void NewSignIn()
	{

		auth = FirebaseAuth.DefaultInstance;
		string phoneNumber = "+79244225416";
		uint phoneAuthTimeoutMs = 100000;
		PhoneAuthProvider provider = PhoneAuthProvider.GetInstance(auth);
		
		provider.VerifyPhoneNumber(phoneNumber, phoneAuthTimeoutMs, null,
			verificationCompleted: (credential) =>
			{
				Debug.Log("Completed");
			},
			verificationFailed: (error) =>
			{
				Debug.Log("error");
			},
			codeSent: (id, token) =>
			{
				Debug.Log(id);
			},
			codeAutoRetrievalTimeOut: (id) =>
			{
				Debug.Log("Phone Auth, auto-verification timed out");

			});



	}





}