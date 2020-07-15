using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject Result;
    public GameObject PlayAgain;
	public GameObject BackButton;
	public GameObject DarkBackground;

	private List<GameObject[]> c;

	public bool isPlayerTurn, won, draw;

	public int[] r;

	private Board currentBoard;

	private Node root;
	private AI ai;
	private int depth;

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
		won = false;
		draw = false;

		currentBoard = new Board(Piece.RED);
		
        switch (GlobalVariables.difficulty)
        {
            case GlobalVariables.Difficulty.BEGINNER:
                depth = 0;
                break;
            case GlobalVariables.Difficulty.INTERMEDIATE:
                depth = 2;
                break;
            case GlobalVariables.Difficulty.EXPERT:
                depth = 4;
                break;
        }

		isPlayerTurn = true;

		if (!isPlayerTurn) aiMove();
	}

	private void aiMove()
	{
		root = new Node(currentBoard.clone(), 0);
		ai = new AI(depth, root);
		int ai_move = ai.getDecision();
		dropPiece(ai_move);
	}

	public void dropPiece(int col) 
	{
		if (!won && !draw)
		{
			currentBoard.play(col);
			int a = r[col]--;

			//Column full
			if(a < 0)
			{
				return;
			}
			GameObject p = c[col][a];
			Image m = p.GetComponent<Image>();
			m.color = isPlayerTurn ? Color.red : Color.yellow;
			won = checkWin(a, col);
			if (!won) won = checkWinOblique1(a, col);
			if (!won) won = checkWinOblique2(a, col);

			if (won)
			{
				won = true;
				if (isPlayerTurn) Result.GetComponent<TextMeshProUGUI> ().text = "You Won!";
                else Result.GetComponent<TextMeshProUGUI>().text = "You Lost!";
				Result.SetActive(true);
				DarkBackground.SetActive(true);
				PlayAgain.SetActive(true);
            }

			draw = checkDraw();
			if (draw && !won)
			{
				draw = true;
                Result.GetComponent<TextMeshProUGUI>().text = "Draw!";
            }

			isPlayerTurn = !isPlayerTurn;

            if (!isPlayerTurn)
			{
   				aiMove();
			}
		}
	}

	public bool checkWin(int i, int j)
	{
		Color color = isPlayerTurn ? Color.red : Color.yellow;
        int k = 0;

		//Vertical
		for (k = 0; k < 3; k++)
		{
			Image p1 = c[j][k].GetComponent<Image>();
			Image p2 = c[j][k + 1].GetComponent<Image>();
			Image p3 = c[j][k + 2].GetComponent<Image>();
			Image p4 = c[j][k + 3].GetComponent<Image>();
			if (p1.color == color && p2.color == color && p3.color == color && p4.color == color)
				return true;
		}
       
		//Horizontal
		for (k = 0; k < 4; k++)
		{
			Image p1 = c[k][i].GetComponent<Image>();
			Image p2 = c[k + 1][i].GetComponent<Image>();
			Image p3 = c[k + 2][i].GetComponent<Image>();
			Image p4 = c[k + 3][i].GetComponent<Image>();
			if (p1.color == color && p2.color == color && p3.color == color && p4.color == color)
				return true;
		}

		return false;
	}

	public bool checkWinOblique1(int i, int j)
	{
		Color color = isPlayerTurn ? Color.red : Color.yellow;

		int x = 0, k = 0, t = 0, y = 0;
		
		//Oblique 1
		x = i + j;

		if (x == 3) y = 1;
		else if (x == 8) { y = 3; x = 6; t = 2; }
		else if (x == 4) y = 2;
		else if (x == 7) { y = 3; x = 6; t = 1; }
		else if (x == 5 || x == 6) y = 3;
		else checkWinOblique2(i, j);

		for (k = t; k < y; k++)
		{
			Image p1 = c[x][k].GetComponent<Image>();
			Image p2 = c[x - 1][k + 1].GetComponent<Image>();
			Image p3 = c[x - 2][k + 2].GetComponent<Image>();
			Image p4 = c[x - 3][k + 3].GetComponent<Image>();
			if (p1.color == color && p2.color == color && p3.color == color && p4.color == color)
				return true;
			x--;
		}

		return false;

	}

	public bool checkWinOblique2(int i, int j)
	{
		Color color = isPlayerTurn ? Color.red : Color.yellow;
		int x = 0, k = 0, t = 0, y = 0;
		x = j - i;

		if (x == 3) y = 1;
		else if (x == 2) y = 2;
		else if (x == 1) y = 3;
		else if (x == 0) y = 3;
		else if (x == -1) { y = 3; x = 0; t = 1; }
		else if (x == -2) { y = 3; x = 0; t = 2; }
		else return false;

		for (k = t; k < y; k++)
		{
			Image p1 = c[x][k].GetComponent<Image>();
			Image p2 = c[x + 1][k + 1].GetComponent<Image>();
			Image p3 = c[x + 2][k + 2].GetComponent<Image>();
			Image p4 = c[x + 3][k + 3].GetComponent<Image>();
			if (p1.color == color && p2.color == color && p3.color == color && p4.color == color)
				return true;
			x++;
		}

		return false;
	}

	private bool checkDraw()
	{
		for (int i = 0; i < r.Length; i++)
		{
			if (r[i] >= 0)
			{
				return false;
			}
		}
		return true;
	}

	public void playAgain()
	{
		PlayAgain.SetActive(false);
		Result.SetActive(false);
		DarkBackground.SetActive(false);

		resetGame();
	}

	private void resetGame()
	{
		won = false;
		draw = false;
		isPlayerTurn = true;

		currentBoard = new Board(Piece.RED);

		r = new int[] { 5, 5, 5, 5, 5, 5, 5 };

		for (int i=0; i< c.Count; i++)
		{
			for (int j = 0; j<c[i].Length; j++)
			{
				Image p = c[i][j].GetComponent<Image>();
				p.color = Color.white;
			}
		}
	}

	public void backToMainMenu()
	{

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}

	public static void log(String m)
	{
		//Debug.Log($"{m}");
	}
}
