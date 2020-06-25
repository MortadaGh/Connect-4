using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
	public enum Difficulty
	{
		BEGINNER, INTERMEDIATE, EXPERT
	}

	public static Difficulty difficulty { get; set; } 

	// Start is called before the first frame update
	void Start()
    {
		difficulty = Difficulty.BEGINNER;
	}
}
