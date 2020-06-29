using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
	public TextMeshProUGUI difficultyText;
	public Slider rowsSlider;
	public Slider columnsSlider;
	public Slider piecesSlider;
	public Toggle allowDiagonalToggle;

	[Range(3, 8)]
	public int numRows = 6;
	[Range(3, 8)]
	public int numColumns = 7;
	[Range(3, 8)]
	public int numPiecesToWin = 4;

	public float dropTime = 4f;
	public bool allowDiagonal = true;
	public GlobalVariables.Difficulty difficulty = GlobalVariables.Difficulty.BEGINNER;

	public void HandleInputData(int value)
    {
		//Manager manager = GameObject.Find("Manager").GetComponent<Manager>();
		UpdateDifficulty(value);
		string text = difficulty.ToString();
		Debug.Log("Text = " + text);
		difficultyText.text = text;
	}

	public void UpdateDifficulty(int d)
	{
		//Manager manager = GameObject.Find("Manager").GetComponent<Manager>();
		Enum.TryParse<GlobalVariables.Difficulty>(d + "", out GlobalVariables.Difficulty diff);
		difficulty = diff;
	}

	public void SaveOptions()
	{
		Debug.Log("Save action called!");
		/*
		numRows = (int) rowsSlider.value;
		numColumns = (int) columnsSlider.value;
		numPiecesToWin = (int) piecesSlider.value;
		numRows = (int) rowsSlider.value;
		allowDiagonal = allowDiagonalToggle.isOn;
		*/
		UpdateGlobalVariables();
		Debug.Log("Saved!");
	}

	public void UpdateGlobalVariables()
	{
		GlobalVariables.numRows = this.numRows;
		GlobalVariables.numColumns = this.numColumns;
		GlobalVariables.numPiecesToWin = this.numPiecesToWin;
		GlobalVariables.allowDiagonal = this.allowDiagonal;
		GlobalVariables.difficulty = this.difficulty;
	}
}
