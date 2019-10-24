using UnityEngine;
using System.Collections;

public class GyroControl : MonoBehaviour 
{
	private bool gyroBool;
	private Gyroscope gyro;
	private Quaternion rotFix;

	void Start() 
	{
		GameObject camParent = GameObject.Find("gyroscopeCamera");
		gyroBool = SystemInfo.supportsGyroscope;
		
		if (gyroBool) 
		{
			gyro = Input.gyro;
			gyro.enabled = true;
			//Making it work for landscape
			camParent.transform.eulerAngles = new Vector3(90,180,0);
			rotFix = new Quaternion(0,0,1,0);
		} 
	}
	
	void Update () 
	{
		if (gyroBool) 
		{
			Quaternion camRot = gyro.attitude * rotFix;
			transform.localRotation = camRot;
		}
	}
}
