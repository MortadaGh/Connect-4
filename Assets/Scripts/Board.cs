using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
	private Piece[,] pieces;  //2 dimensions array
	public int[] r;
	public Piece turn;

	public bool won, draw;

	public Board(Piece turn)
	{
		this.turn = turn;
		pieces = new Piece[7, 6];
		
		for (int i = 0; i < 7; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				pieces[i, j] = Piece.EMPTY;
			}
		}

		r = new int[] {5, 5, 5, 5, 5, 5, 5};
	}
	
	public Board()
	{
		pieces = new Piece[7, 6];

		for (int i = 0; i < 7; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				pieces[i, j] = Piece.EMPTY;
			}
		}

		r = new int[] { 5, 5, 5, 5, 5, 5, 5 };
	}

	public override string ToString()
	{
		string s = "\n[ ";
		for (int i = 0; i < 6; i++)
		{
			for (int j = 0; j < 7; j++)
			{
				s += pieces[j, i] + " ";
			}
			s += "\n";
		}
		s += "]";
		return s;
	}

	public void play(int col)
	{
		if (!won && !draw)
		{
			int a = r[col]--;
			if (a < 0)
			{
				///Debug.Log($"column {col} is full!");
				return;
			}
			//Piece p = pieces[col, a];
			//p = turn;
			pieces[col, a] = turn;

			//won = checkWin(a, col);
			won = checkWin(col, a);
			if (won)
			{
				///Debug.Log(turn == Piece.BLUE ? "Blue Won!!!!!!!!!!!!!!!!!!!!" : "Yellow Won!!!!!!!!!!!!!!!");
			}

			draw = checkDraw();
			if (draw && !won)
			{
				///Debug.Log("Draw!!!!");
			}

			if (turn == Piece.BLUE)
				turn = Piece.YELLOW;
			else if (turn == Piece.YELLOW)
				turn = Piece.BLUE;
		}
	}

	public bool checkWin(int i, int j)
	{
		/////Debug.Log($"i = {i} / j = {j}");
		//Color color = isPlayerTurn ? Color.blue : Color.yellow;

		//Vertical
		for (int k = 0; k < 3; k++)
		{
			/////Debug.Log($"k = {k}");
			Piece p1 = pieces[i,k];
			Piece p2 = pieces[i,k + 1];
			Piece p3 = pieces[i,k + 2];
			Piece p4 = pieces[i,k + 3];
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
			/////Debug.Log($"k = {k}");
			Piece p1 = pieces[k,j];
			Piece p2 = pieces[k + 1,j];
			Piece p3 = pieces[k + 2,j];
			Piece p4 = pieces[k + 3,j];
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

		for (int i = 0; i < 7; i++)
		{
			for (int j = 0; j < 6; j++)
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

	public bool checkWin(Piece turn)
	{
		/////Debug.Log($"i = {i} / j = {j}");
		//Color color = isPlayerTurn ? Color.blue : Color.yellow;

		//Vertical
		for(int j = 0; j < 7; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				/////Debug.Log($"k = {k}");
				Piece p1 = pieces[j, k];
				Piece p2 = pieces[j, k + 1];
				Piece p3 = pieces[j, k + 2];
				Piece p4 = pieces[j, k + 3];
				if (p1 == turn
					&& p2 == turn
					&& p3 == turn
					&& p4 == turn)
				{
					return true;
				}
			}
		}

		//Horizontal
		for (int i=0; i < 6; i++)
		{
			for (int k = 0; k < 4; k++)
			{
				/////Debug.Log($"k = {k}");
				Piece p1 = pieces[k, i];
				Piece p2 = pieces[k + 1, i];
				Piece p3 = pieces[k + 2, i];
				Piece p4 = pieces[k + 3, i];
				if (p1 == turn
					&& p2 == turn
					&& p3 == turn
					&& p4 == turn)
				{
					return true;
				}
			}
		}

		//Oblique 1

		//Oblique 2

		return false;
	}

	//TODO
	public int calculateScore(Piece turn)
	{
		int score = 0;
		Piece otherTurn = turn;

		if (turn == Piece.YELLOW)
			otherTurn = Piece.BLUE;
		else if (turn == Piece.BLUE)
			otherTurn = Piece.YELLOW;

		bool turnTrue = checkWin(turn);
		bool otherTurnTrue = checkWin(otherTurn);

		/////Debug.Log($"turnTrue = {turnTrue}");
		/////Debug.Log($"otherTurnTrue = {otherTurnTrue}");

		score += turnTrue ? 1000 : otherTurnTrue ? -700 : 0;

		score += this.GetHashCode() % 100;

		return score;
		//Random r = new Random();

		//score = this.GetHashCode() % 100;
		//score = (int) Random.Range(0, 100);

		//Horizontal
		//check for win +1000
		//check for loss -700
		
		//check for PPPE or EPPP +50
		//check for PPEP or PEPP +40
		//check for PPEE or EEPP +10
		//check for PEPE or EPEP +7
		//return score;
	}
}

public enum Piece
{
	EMPTY, RED, YELLOW
}