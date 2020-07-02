using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
	private Piece[,] pieces;
	public int[] r;
	private Piece turn;

	public bool won, draw;

	public Board(Piece turn)
	{
		this.turn = turn;
		pieces = new Piece[7,6];
		
		for (int i = 0; i<pieces.GetLength(0); i++)
		{
			for (int j = 0; j < pieces.GetLength(1); j++)
			{
				pieces[i, j] = Piece.EMPTY;
			}
		}

		r = new int[] {5, 5, 5, 5, 5, 5, 5};
	}
	
	public Board()
	{
		pieces = new Piece[7, 6];

		for (int i = 0; i < pieces.GetLength(0); i++)
		{
			for (int j = 0; j < pieces.GetLength(1); j++)
			{
				pieces[i, j] = Piece.EMPTY;
			}
		}

		r = new int[] { 5, 5, 5, 5, 5, 5, 5 };
	}

	public void play(int col)
	{
		if (!won && !draw)
		{
			int a = r[col]--;
			if (a < 0)
			{
				Debug.Log($"column {col} is full!");
				return;
			}
			Piece p = pieces[col, a];
			p = turn;

			won = checkWin(a, col);
			if (won)
			{
				Debug.Log(turn == Piece.BLUE ? "Blue Won!" : "Yellow Won!");
			}

			draw = checkDraw();
			if (draw && !won)
			{
				Debug.Log("Draw!");
			}

			if (turn == Piece.BLUE)
				turn = Piece.YELLOW;
			if (turn == Piece.YELLOW)
				turn = Piece.BLUE;
		}
	}

	public bool checkWin(int i, int j)
	{
		//Debug.Log($"i = {i} / j = {j}");
		//Color color = isPlayerTurn ? Color.blue : Color.yellow;

		//Vertical
		for (int k = 0; k < 3; k++)
		{
			//Debug.Log($"k = {k}");
			Piece p1 = pieces[j,k];
			Piece p2 = pieces[j,k + 1];
			Piece p3 = pieces[j,k + 2];
			Piece p4 = pieces[j,k + 3];
			if (p1 == turn
				&& p2 == turn
				&& p3 == turn
				&& p4 == turn)
			{
				return true;
			}
		}

		//Horizontal
		for (int k = 0; k < 4; k++)
		{
			//Debug.Log($"k = {k}");
			Piece p1 = pieces[k,i];
			Piece p2 = pieces[k + 1,i];
			Piece p3 = pieces[k + 2,i];
			Piece p4 = pieces[k + 3,i];
			if (p1 == turn
				&& p2 == turn
				&& p3 == turn
				&& p4 == turn)
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
		for (int i = 0; i < r.Length; i++)
		{
			if (r[i] >= 0)
			{
				return false;
			}
		}
		return true;
	}

	public Board clone()
	{
		Board clone = new Board();
		clone.pieces = new Piece[7, 6];

		for (int i = 0; i < pieces.GetLength(0); i++)
		{
			for (int j = 0; j < pieces.GetLength(1); j++)
			{
				clone.pieces[i, j] = pieces[i, j];
			}
		}

		clone.r = new int[7];
		for(int i = 0; i < r.Length; i++)
		{
			clone.r[i] = r[i];
		}

		clone.turn = turn;
		clone.won = won;
		clone.draw = draw;
		return clone;
	}

	public int calculateScore()
	{
		int score = 0;

		//TODO
		System.Random r = new System.Random();
		score += r.Next() % 100;
		score -= r.Next() % 50;

		//Horizontal
		//check for win +1000
		//check for loss -700
		//check for PPPE or EPPP +50

		//check for PPEP or PEPP +40
		//check for PPEE or EEPP +10
		//check for PEPE or EPEP +7
		return score;
	}
}

public enum Piece
{
	EMPTY, BLUE, YELLOW
}