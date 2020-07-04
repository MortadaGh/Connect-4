using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
	private Piece[,] pieces;  //2 dimensions array
	public int[] r;
	private Piece turn;

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

	}
}

public enum Piece
{
	EMPTY, RED, YELLOW
}