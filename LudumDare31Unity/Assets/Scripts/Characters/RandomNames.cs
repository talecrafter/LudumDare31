﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Core;

public class RandomNames {

	private static List<string> _maleForeNames = new List<string>();
	private static List<string> _femaleForeNames = new List<string>();
	private static List<string> _sureNames = new List<string>();

	private static bool _wasInit = false;

	public static string GetUniqueName(Gender gender)
	{
		if (!_wasInit)
			Init();

		return GetName(gender);
	}

	private static string GetName(Gender gender)
	{
		string foreName = string.Empty;

		if (gender == Gender.Male)
		{
			foreName = _maleForeNames.PickRandom();
		}
		else
		{
			foreName = _femaleForeNames.PickRandom();
		}

		string sureName = _sureNames.PickRandom();

		return foreName + " " + sureName;
	}

	private static void Init()
	{
		_maleForeNames.Add("John");
		_maleForeNames.Add("Walter");
		_maleForeNames.Add("Carl");
		_maleForeNames.Add("Joseph");
		_maleForeNames.Add("Steve");


		_femaleForeNames.Add("Claire");
		_femaleForeNames.Add("Clara");
		_femaleForeNames.Add("Martha");
		_femaleForeNames.Add("Missandra");
		_femaleForeNames.Add("Margaret");
		_femaleForeNames.Add("Susan");

		_sureNames.Add("Rainborough");
		_sureNames.Add("Hummer");
		_sureNames.Add("Swisson");
		_sureNames.Add("Bolton");
		_sureNames.Add("Sureton");
		_sureNames.Add("Cains");
		_sureNames.Add("Cockton");
		_sureNames.Add("Burlough");

	}
    
}