using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public TextMeshProUGUI output;

    void Start()
    {
		output.text = "Difficulty Level : " + GlobalVariables.difficulty.ToString();
    }
	
}
