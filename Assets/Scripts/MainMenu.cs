using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GlobalVariables.Difficulty difficulty = GlobalVariables.Difficulty.BEGINNER;
    //private int difficulty=0;

    public void Play()
    {
        Debug.Log($"Difficulty={difficulty.ToString()}");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void HandleInputData(int value)
    {
        Debug.Log($"value={value}");
        //Manager manager = GameObject.Find("Manager").GetComponent<Manager>();
        UpdateDifficulty(value);
        string text = difficulty.ToString();
        //string text = difficulty;
        Debug.Log("Text = " + text);
        //difficulty.text = text;
    }

    public void UpdateDifficulty(int d)
    {
        //Manager manager = GameObject.Find("Manager").GetComponent<Manager>();
        Enum.TryParse<GlobalVariables.Difficulty>(d + "", out GlobalVariables.Difficulty diff);
        difficulty = diff;
        GlobalVariables.difficulty = this.difficulty;
    }
}
