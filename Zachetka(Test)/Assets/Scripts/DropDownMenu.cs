using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

public class DropDownMenu : MonoBehaviour
{
	#region Variables
	[SerializeField]
	private Dropdown day;
	[SerializeField]
	private Dropdown month;
	[SerializeField]
	private Dropdown year;
	#endregion

	private void Start()
	{
		InitilizeMenu();
		year.onValueChanged.AddListener(delegate {
			OnYearChanged(year);
		});
		month.onValueChanged.AddListener(delegate {
			OnYearChanged(month);
		});
	}

	private void OnYearChanged(Dropdown change)
	{

	}

	private void InitilizeMenu()
	{
		var days = new List<string>();
			

		for (int i = 1; i<=31; i++)
		{
			days.Add(i.ToString());
		}
		day.AddOptions(days);

		var months = new List<string>();

		for (int i = 1; i <= 12; i++)
		{
			months.Add(i.ToString());
		}
		month.AddOptions(months);

		var years = new List<string>();

		for (int i = 1900; i <= System.Convert.ToInt32(DateTime.Today.ToString("yyyy")); i++)
		{
			years.Add(i.ToString());
		}

		year.AddOptions(years);
		
	}
}