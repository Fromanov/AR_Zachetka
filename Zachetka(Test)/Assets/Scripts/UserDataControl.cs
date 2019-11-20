using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class UserDataControl
{	
	public string email;                // User email
	public string password;             // User password
	public string avatarUMA;            // User UMA recepie
	public string userToken;            // User token	
	public float rangTime;              // User rang time
	public float skillPoints;           // User skill points

	private string userJson;

	public void GetUserDataFromServer()
	{

	}

	public void SaveUserDataToServer(string avatar, float rang, float skill)
	{
		WWWForm form = new WWWForm();
		form.AddField("avatarUMA", avatar);
		form.AddField("rangTime", rang.ToString());
		form.AddField("skillPoints", skill.ToString());

		UnityWebRequest www = UnityWebRequest.Post("http://www.my-server.com/myform", form);
	}

	public void LoginUser(string email, string password)
	{
		WWWForm form = new WWWForm();
		form.AddField("email", email);
		form.AddField("password", password);

		UnityWebRequest www = UnityWebRequest.Post("http://www.my-server.com/myform", form);
	}

	public void RegisterUser(string email, string password)
	{
		WWWForm form = new WWWForm();
		form.AddField("email", email);
		form.AddField("password", password);

		UnityWebRequest www = UnityWebRequest.Post("http://www.my-server.com/myform", form);		
	}
}
