using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UserStatsFromFirebase
{
	public string Avatar { get; set; }
	public int Hours { get; set; }
	public int Coin { get; set; }
	public int HighScore { get; set; }
	public int PreviousHighScore { get; set; }

}
