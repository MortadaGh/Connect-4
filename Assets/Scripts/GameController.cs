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

	public void dropPiece(int col) //drop the piece in column col ?! 
	{
		if (!won && !draw)
		{
			int a = r[col]--; //r[0]=5, r[0]--=4, a=4
            //Debug.Log($"r[col]={r[col]}, col={col} , a={a}"); // col 0 , a 5 (first col and row)..
            if (a < 0)
			{
				Debug.Log($"column {col} is full!");
				return;
			}
			GameObject p = c[col][a];
			Image m = p.GetComponent<Image>();  //get an image
			m.color = isPlayerTurn ? Color.red : Color.yellow;  //color the image according to the turn
            //Check if a player wins
			won = checkWin(a, col);
			if (won)
			{
				Debug.Log(isPlayerTurn ? "Red Won!" : "Yellow Won!");
			}

			draw = checkDraw();
			if (draw && !won)
			{
				Debug.Log("Draw!");
			}

			isPlayerTurn = !isPlayerTurn;	//Change the turn		
		}
	}

    public bool checkWin(int i, int j)
    {
        //Debug.Log($"i = {i} / j = {j}");
        Color color = isPlayerTurn ? Color.red : Color.yellow;
        int k = 0, x = 0, y = 0, t = 0;
        //Vertical
        for (k=0; k < 3; k++)
		{
            //Debug.Log($"k = {k}");
            Debug.Log($"j={j}, k={k}");
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
		for (k = 0; k < 4; k++)
		{
            //Debug.Log($"k = {k}");
            Debug.Log($"i={i}, k={k}");
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
        x = i + j;

        if (x == 3) y = 1;
        else if (x == 8) { y = 3; x = 6; t = 2; }
        else if (x == 4) y = 2;
        else if (x == 7) { y = 3; x = 6; t = 1; }
        else if (x == 5 || x == 6) y = 3;
        else { Debug.Log($"No Diagonal"); return false; }

        for (k = t; k < y; k++)
        {
            Debug.Log($"i={i},j={j},k={k},y={y}");
            Image p1 = c[x][k].GetComponent<Image>();
            Image p2 = c[x - 1][k + 1].GetComponent<Image>();
            Image p3 = c[x - 2][k + 2].GetComponent<Image>();
            Image p4 = c[x - 3][k + 3].GetComponent<Image>();
            if (p1.color == color
                && p2.color == color
                && p3.color == color
                && p4.color == color)
            {
                return true;
            }
            x--;
        }

        //Oblique 2
        x = j - i;

        if (x == 3) y = 1;
        else if (x == 2) y = 2;
        else if (x == 1) y = 3;
        else if (x == 0) y = 3; 
        //; x = 6; t = 1; }
        else if (x == -1) { y = 3; x = 0; t = 1; }
        else if (x == -2) { y = 3; x = 0; t = 2; }
        else { Debug.Log($"No Diagonal"); return false; }

        for (k = t; k < y; k++)
        {
            Debug.Log($"i={i},j={j},k={k},y={y}");
            Image p1 = c[x][k].GetComponent<Image>();
            Image p2 = c[x + 1][k + 1].GetComponent<Image>();
            Image p3 = c[x + 2][k + 2].GetComponent<Image>();
            Image p4 = c[x + 3][k + 3].GetComponent<Image>();
            if (p1.color == color
                && p2.color == color
                && p3.color == color
                && p4.color == color)
            {
                return true;
            }
            x++;
        }

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
