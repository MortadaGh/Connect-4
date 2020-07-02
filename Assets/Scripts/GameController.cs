using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public GameObject[] c1;
	public GameObject[] c2;
	public GameObject[] c3;
	public GameObject[] c4;
	public GameObject[] c5;
	public GameObject[] c6;
	public GameObject[] c7;

	private List<GameObject[]> c;

	public bool isPlayerTurn, won, draw;

	public int[] r;

	private AI ai;

	public void Start()
	{
		c = new List<GameObject[]>();
		c.Add(c1);
		c.Add(c2);
		c.Add(c3);
		c.Add(c4);
		c.Add(c5);
		c.Add(c6);
		c.Add(c7);

		isPlayerTurn = true;
		won = false;
		draw = false;

		ai = new AI(5);
	}

	public void dropPiece(int col)
	{
		if (!won && !draw)
		{
			int a = r[col]--;
			if(a < 0)
			{
				Debug.Log($"column {col} is full!");
				return;
			}
			GameObject p = c[col][a];
			Image m = p.GetComponent<Image>();
			m.color = isPlayerTurn ? Color.blue : Color.yellow;

			won = checkWin(a, col);
			if (won)
			{
				Debug.Log(isPlayerTurn ? "Blue Won!" : "Yellow Won!");
			}

			draw = checkDraw();
			if (draw && !won)
			{
				Debug.Log("Draw!");
			}

			isPlayerTurn = !isPlayerTurn;			
		}
	}
	
	public bool checkWin(int i, int j)
	{
		//Debug.Log($"i = {i} / j = {j}");
		Color color = isPlayerTurn ? Color.blue : Color.yellow;

		//Vertical
		for(int k=0; k < 3; k++)
		{
			//Debug.Log($"k = {k}");
			Image p1 = c[j][k].GetComponent<Image>();
			Image p2 = c[j][k+1].GetComponent<Image>();
			Image p3 = c[j][k+2].GetComponent<Image>();
			Image p4 = c[j][k+3].GetComponent<Image>();
			if (p1.color == color
				&& p2.color == color
				&& p3.color == color
				&& p4.color == color)
			{
				return true;
			}
		}

		//Horizontal
		for (int k = 0; k < 4; k++)
		{
			//Debug.Log($"k = {k}");
			Image p1 = c[k][i].GetComponent<Image>();
			Image p2 = c[k + 1][i].GetComponent<Image>();
			Image p3 = c[k + 2][i].GetComponent<Image>();
			Image p4 = c[k + 3][i].GetComponent<Image>();
			if (p1.color == color
				&& p2.color == color
				&& p3.color == color
				&& p4.color == color)
			{
				return true;
			}
		}

		//Oblique 1

		//Oblique 2

		return false;
	}

	private bool checkDraw()
	{
		for(int i=0; i < r.Length; i++)
		{
			if (r[i] >= 0)
			{
				return false;
			}
		}
		return true;
	}
}
