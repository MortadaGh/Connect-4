using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GlobalVariables.Difficulty difficulty;

	public void Start()
	{
		difficulty = GlobalVariables.Difficulty.BEGINNER;
		GlobalVariables.difficulty = difficulty;
	}

    public void Play()
    {
        //Debug.Log($"Difficulty={difficulty.ToString()}");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        //Debug.Log("QUIT!");
        Application.Quit();
    }

    public void HandleInputData(int value)
    {
        //Debug.Log($"value={value}");
        UpdateDifficulty(value);
        string text = difficulty.ToString();
        //Debug.Log("Text = " + text);
    }

    public void UpdateDifficulty(int d)
    {
        //Manager manager = GameObject.Find("Manager").GetComponent<Manager>();
        Enum.TryParse<GlobalVariables.Difficulty>(d + "", out GlobalVariables.Difficulty diff);
        difficulty = diff;
        GlobalVariables.difficulty = this.difficulty;
    }
}
