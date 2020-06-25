using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
	//public TextMeshProUGUI output;

    public void HandleInputData(int value)
    {
		UpdateDifficulty(value);
		String text = Manager.difficulty.ToString();
		Debug.Log("Text = " + text);
		//output.text = text;
	}

	public void UpdateDifficulty(int d)
	{
		Enum.TryParse<Manager.Difficulty>(d + "", out Manager.Difficulty diff);
		Manager.difficulty = diff;
	}
}
