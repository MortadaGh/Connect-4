using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
	public TextMeshProUGUI output;

    void Start()
    {
		output.text = "Difficulty Level : " + Manager.difficulty.ToString();
    }
	
}
