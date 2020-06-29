using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables
{
	[Range(3, 8)]
	public static int numRows = 6;

	[Range(3, 8)]
	public static int numColumns = 7;

	[Range(3, 8)]
	public static int numPiecesToWin = 4;

	public static float dropTime = 4f;

	public static bool allowDiagonal = true;

	public static Difficulty difficulty = Difficulty.BEGINNER;
	
	public enum Difficulty
	{
		BEGINNER, INTERMEDIATE, EXPERT
	}
}
