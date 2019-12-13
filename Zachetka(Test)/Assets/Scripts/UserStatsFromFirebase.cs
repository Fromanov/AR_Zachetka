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
	public string Name { get; set; }
	public string LastName { get; set; }
	public string Patronymic { get; set; }
	public string Birthday { get; set; }

}
