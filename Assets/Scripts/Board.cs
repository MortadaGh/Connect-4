﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
	private Piece[,] pieces;
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
		string s = "\n";
		for (int i = 0; i < 6; i++)
		{
			for (int j = 0; j < 7; j++)
			{
				s += pieces[j, i] + (pieces[j,i] == Piece.YELLOW ? "\t" : "\t\t");
			}
			s += "\n";
		}
		return s;
	}

	public void play(int col)
	{
		if (!won && !draw)
		{
			int a = r[col]--;
			if (a < 0)
			{
				return;
			}
			pieces[col, a] = turn;
			
			won = checkWin(col, a);
			if (!won) won = checkWinOblique1(col, a);
			if (!won) won = checkWinOblique2(col, a);
			if (won) won = true;

			draw = checkDraw();
			if (draw && !won) draw = true;

			if (turn == Piece.RED) turn = Piece.YELLOW;
			else if (turn == Piece.YELLOW) turn = Piece.RED;
		}
	}

	public bool checkWin(int i, int j)
	{
		//Vertical
		for (int k = 0; k < 3; k++)
		{
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

		return false;
	}

	public bool checkWinOblique1(int i, int j)
	{
		int x = 0, y = 0, t = 0;

		//Oblique 1
		x = i + j;
		if (x == 3) y = 1;
		else if (x == 8) { y = 3; x = 6; t = 2; }
		else if (x == 4) y = 2;
		else if (x == 7) { y = 3; x = 6; t = 1; }
		else if (x == 5 || x == 6) y = 3;
		else
		{
			 return false;
		}

		for (int k = t; k < y; k++)
		{
			Piece p1 = pieces[x, k];
			Piece p2 = pieces[x - 1, k + 1];
			Piece p3 = pieces[x - 2, k + 2];
			Piece p4 = pieces[x - 3, k + 3];
			if (p1 == turn
				&& p2 == turn
				&& p3 == turn
				&& p4 == turn)
			{
				return true;
			}
			x--;
		}
		return false;
	}

	public bool checkWinOblique2(int i, int j)
	{
		//Oblique 2
		int x = 0, y = 0, t = 0;
		x = j - i;

		if (x == 3) y = 1;
		else if (x == 2) y = 2;
		else if (x == 1) y = 3;
		else if (x == 0) y = 3;
		else if (x == -1) { y = 3; x = 0; t = 1; }
		else if (x == -2) { y = 3; x = 0; t = 2; }
		else return false;

		for (int k = t; k < y; k++)
		{
			Piece p1 = pieces[x, k];
			Piece p2 = pieces[x + 1, k + 1];
			Piece p3 = pieces[x + 2, k + 2];
			Piece p4 = pieces[x + 3, k + 3];

			if (p1 == turn && p2 == turn && p3 == turn && p4 == turn)
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
				return false;
		}
		return true;
	}

	public Board clone()
	{
		Board clone = new Board
		{
			pieces = new Piece[7, 6]
		};

		for (int i = 0; i < 7; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				clone.pieces[i, j] = pieces[i, j];
			}
		}

		clone.r = new int[7];
		for(int i = 0; i < r.Length; i++)
			clone.r[i] = r[i];

		clone.turn = turn;
		clone.won = won;
		clone.draw = draw;
		return clone;
	}

	public bool checkWin(Piece turn)
	{
		//Vertical
		for(int j = 0; j < 7; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				Piece p1 = pieces[j, k];
				Piece p2 = pieces[j, k + 1];
				Piece p3 = pieces[j, k + 2];
				Piece p4 = pieces[j, k + 3];
				if (p1 == turn && p2 == turn && p3 == turn && p4 == turn)
					return true;
			}
		}

		//Horizontal
		for (int i = 0; i < 6; i++)
		{
			for (int k = 0; k < 4; k++)
			{
				Piece p1 = pieces[k, i];
				Piece p2 = pieces[k + 1, i];
				Piece p3 = pieces[k + 2, i];
				Piece p4 = pieces[k + 3, i];
				if (p1 == turn && p2 == turn && p3 == turn && p4 == turn)
					return true;
			}
		}

		return false;
	}
	public bool checkWinOblique1(Piece turn)
	{
		//Oblique 1
		int x = 0, y = 0, t = 0;
		for (int j = 0; j < 8; j++)
		{
			x = j;
			if (x == 3) y = 1;
			else if (x == 8) { y = 3; x = 6; t = 2; }
			else if (x == 4) y = 2;
			else if (x == 7) { y = 3; x = 6; t = 1; }
			else if (x == 5 || x == 6) y = 3;
			else return false;

			for (int k = t; k < y; k++)
			{
				Piece p1 = pieces[x, k];
				Piece p2 = pieces[x - 1, k + 1];
				Piece p3 = pieces[x - 2, k + 2];
				Piece p4 = pieces[x - 3, k + 3];
				if (p1 == turn && p2 == turn && p3 == turn && p4 == turn)
					return true;
				x--;
			}
		}

		return false;
	}

	public bool checkWinOblique2(Piece turn)
	{
		int x = 0, k = 0, t = 0, y = 0;

		for (int j = 0; j < 6; j++)
		{
			x = j;
			if (x == 3) y = 1;
			else if (x == 2) y = 2;
			else if (x == 1) y = 3;
			else if (x == 0) y = 3;
			else if (x == 4) { y = 3; x = 0; t = 1; }
			else if (x == 5) { y = 3; x = 0; t = 2; }
			else return false;

			for (k = t; k < y; k++)
			{
				Piece p1 = pieces[x, k];
				Piece p2 = pieces[x + 1, k + 1];
				Piece p3 = pieces[x + 2, k + 2];
				Piece p4 = pieces[x + 3, k + 3];
				if (p1 == turn && p2 == turn && p3 == turn && p4 == turn)
					return true;
				x++;
			}
		}
		return false;
	}

	public int calculateScore(Piece turn)
	{
		int score = 0;
		Piece otherTurn = turn;

		if (turn == Piece.YELLOW) otherTurn = Piece.RED;
		else if (turn == Piece.RED) otherTurn = Piece.YELLOW;

		bool turnTrue = checkWin(turn) || checkWinOblique1(turn) || checkWinOblique2(turn);
		bool otherTurnTrue = checkWin(otherTurn) || checkWinOblique1(otherTurn) || checkWinOblique2(otherTurn);

		score += turnTrue ? 1000 : otherTurnTrue ? -700 : 0;
	
		score += threePieces(turn);
		score -= threePieces(otherTurn);
		
		score += twoPieces(turn);
		score -= twoPieces(otherTurn);

		return score;
	}

	private int twoPieces(Piece turn)
	{
		int score = 0;

        //check for PPEE || EPPE || EEPP (vertical and horizontal) score += 15 (score+=6)
        //check for PEPE || EPEP (vertical and horizontal) score += 10 (score+=4)
        //check for PEEP (vertical and horizontal) score += 5 (score+=2)
        for (int j = 0; j < 7; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				Piece p1 = pieces[j, k];
				Piece p2 = pieces[j, k + 1];
				Piece p3 = pieces[j, k + 2];
				Piece p4 = pieces[j, k + 3];
				if (p1 == turn && p2 == turn && p3 == Piece.EMPTY && p4 == Piece.EMPTY
					|| p1 == Piece.EMPTY && p2 == turn && p3 == turn && p4 == Piece.EMPTY
					|| p1 == Piece.EMPTY && p2 == Piece.EMPTY && p3 == turn && p4 == turn)
				{
                    //score += 15;
                    score += 6;
				}
				else if (p1 == turn && p2 == Piece.EMPTY && p3 == turn && p4 == Piece.EMPTY
					|| p1 == Piece.EMPTY && p2 == turn && p3 == Piece.EMPTY && p4 == turn)
				{
                    //score += 10;
                    score += 4;
				}
				else if (p1 == turn && p2 == Piece.EMPTY && p3 == Piece.EMPTY && p4 == turn)
				{
                    //score += 5;
                    score += 2;
				}
			}
		}

		//Horizontal
		for (int i = 0; i < 6; i++)
		{
			for (int k = 0; k < 4; k++)
			{
				Piece p1 = pieces[k, i];
				Piece p2 = pieces[k + 1, i];
				Piece p3 = pieces[k + 2, i];
				Piece p4 = pieces[k + 3, i];
				if (p1 == turn && p2 == turn && p3 == Piece.EMPTY && p4 == Piece.EMPTY
					|| p1 == Piece.EMPTY && p2 == turn && p3 == turn && p4 == Piece.EMPTY
					|| p1 == Piece.EMPTY && p2 == Piece.EMPTY && p3 == turn && p4 == turn)
				{
                    //score += 15;
                    score += 6;
				}
				else if (p1 == turn && p2 == Piece.EMPTY && p3 == turn && p4 == Piece.EMPTY
					|| p1 == Piece.EMPTY && p2 == turn && p3 == Piece.EMPTY && p4 == turn)
				{
                    //score += 10;
                    score += 4;
                }
				else if (p1 == turn && p2 == Piece.EMPTY && p3 == Piece.EMPTY && p4 == turn)
				{
                    //score += 5;
                    score += 2;
                }
			}
		}
		score += check_2pieces_Oblique1(turn);
		score += check_2pieces_Oblique2(turn);
		return score;
	}

	public int check_3pieces_Oblique1(Piece turn)
	{
		int score = 0;
		//Oblique 1
		//check for PPPE (oblique 1) score += 5
		int x = 0, y = 0, t = 0;

		for (int j = 0; j < 8; j++)
		{
		
			x = j;
			if (x == 3) y = 1;
			else if (x == 8) { y = 3; x = 6; t = 2; }
			else if (x == 4) y = 2;
			else if (x == 7) { y = 3; x = 6; t = 1; }
			else if (x == 5 || x == 6) y = 3;
			else break;

			for (int k = t; k < y; k++)
			{
				//Debug.Log($"i={i},j={j},k={k},y={y}");
				Piece p1 = pieces[x, k];
				Piece p2 = pieces[x - 1, k + 1];
				Piece p3 = pieces[x - 2, k + 2];
				Piece p4 = pieces[x - 3, k + 3];
				if (p1 == turn && p2 == turn && p3 == Piece.EMPTY && p4 == turn
					|| p1 == turn && p2 == Piece.EMPTY && p3 == turn && p4 == turn)
				{
                    //score+=30;
                    score += 8;
                }
				else if(p1 == turn && p2 == turn && p3 == turn && p4 == Piece.EMPTY
					|| p1 == Piece.EMPTY && p2 == turn && p3 == turn && p4 == turn)
				{
                    //score += 50;
                    score += 10;
                }
				x--;
			}
		}
		
		return score;
	}

	public int check_2pieces_Oblique1(Piece turn)
	{
		int score = 0;
		//Oblique 1
		//check for PPEE || EEPP || EPPE (oblique 1) score += 15
		//check for PEPE || EPEP (oblique 1) score += 10
		//check for PEEP (oblique 1) score += 5
		int x = 0, y = 0, t = 0;

		for (int j = 0; j < 8; j++)
		{
			x = j;
			if (x == 3) y = 1;
			else if (x == 8) { y = 3; x = 6; t = 2; }
			else if (x == 4) y = 2;
			else if (x == 7) { y = 3; x = 6; t = 1; }
			else if (x == 5 || x == 6) y = 3;
			else return score;

			for (int k = t; k < y; k++)
			{
				Piece p1 = pieces[x, k];
				Piece p2 = pieces[x - 1, k + 1];
				Piece p3 = pieces[x - 2, k + 2];
				Piece p4 = pieces[x - 3, k + 3];
				if (p1 == turn && p2 == turn && p3 == Piece.EMPTY && p4 == Piece.EMPTY
					|| p1 == Piece.EMPTY && p2 == Piece.EMPTY && p3 == turn && p4 == turn
					|| p1 == Piece.EMPTY && p2 == turn && p3 == turn && p4 == Piece.EMPTY)
				{
                    //score += 15;
                    score += 6;
                }
				//check for PEPE || EPEP (oblique 1) score += 10
				else if (p1 == turn && p2 == Piece.EMPTY && p3 == turn && p4 == Piece.EMPTY
					|| p1 == Piece.EMPTY && p2 ==turn && p3 == Piece.EMPTY && p4 == turn)
				{
                    //score += 10;
                    score += 4;
                }
				else if (p1 == turn && p2 == Piece.EMPTY && p3 == Piece.EMPTY && p4 == turn)
				{
                    //score += 5;
                    score += 2;
                }
				x--;
			}
		}

		return score;
	}

	public int check_3pieces_Oblique2(Piece turn)
	{
		int score = 0;

		//Oblique 1
		//check for PPPE (oblique 1) score += 5
		int x = 0, y = 0, t = 0;

		for (int j = 0; j < 6; j++)
		{

			x = j;

			if (x == 3) y = 1;
			else if (x == 2) y = 2;
			else if (x == 1) y = 3;
			else if (x == 0) y = 3;
			else if (x == 4) { y = 3; x = 0; t = 1; }
			else if (x == 5) { y = 3; x = 0; t = 2; }
			else break;

			for (int k = t; k < y; k++)
			{
				//Debug.Log($"i={i},j={j},k={k},y={y}");
				Piece p1 = pieces[x, k];
				Piece p2 = pieces[x + 1, k + 1];
				Piece p3 = pieces[x + 2, k + 2];
				Piece p4 = pieces[x + 3, k + 3];
				if (p1 == turn && p2 == turn && p3 == Piece.EMPTY && p4 == turn
					|| p1 == turn && p2 == Piece.EMPTY && p3 == turn && p4 == turn)
				{
                    //score += 30;
                    score += 8;
                }
				else if (p1 == turn && p2 == turn && p3 == turn && p4 == Piece.EMPTY
					|| p1 == Piece.EMPTY && p2 == turn && p3 == turn && p4 == turn)
				{
                    //score += 50;
                    score += 10;
                }
				x++;
			}
		}

		return score;
	}

	public int check_2pieces_Oblique2(Piece turn)
	{
		int score = 0;

		//Oblique 1
		//check for PPEE || EEPP || EPPE (oblique 1) score += 15
		//check for PEPE || EPEP (oblique 1) score += 10
		//check for PEEP (oblique 1) score += 5
		int x = 0, y = 0, t = 0;

		for (int j = 0; j < 6; j++)
		{
			x = j;

			if (x == 3) y = 1;
			else if (x == 2) y = 2;
			else if (x == 1) y = 3;
			else if (x == 0) y = 3;
			else if (x == 4) { y = 3; x = 0; t = 1; }
			else if (x == 5) { y = 3; x = 0; t = 2; }
			else break;

			for (int k = t; k < y; k++)
			{
				Piece p1 = pieces[x, k];
				Piece p2 = pieces[x + 1, k + 1];
				Piece p3 = pieces[x + 2, k + 2];
				Piece p4 = pieces[x + 3, k + 3];
				if (p1 == turn && p2 == turn && p3 == Piece.EMPTY && p4 == Piece.EMPTY
					|| p1 == Piece.EMPTY && p2 == Piece.EMPTY && p3 == turn && p4 == turn
					|| p1 == Piece.EMPTY && p2 == turn && p3 == turn && p4 == Piece.EMPTY)
				{
					//score += 15;
                    score += 6;
                }
				//check for PEPE || EPEP (oblique 1) score += 10
				else if (p1 == turn && p2 == Piece.EMPTY && p3 == turn && p4 == Piece.EMPTY
					|| p1 == Piece.EMPTY && p2 == turn && p3 == Piece.EMPTY && p4 == turn)
				{
                    //score += 10;
                    score += 4;
                }
				else if (p1 == turn && p2 == Piece.EMPTY && p3 == Piece.EMPTY && p4 == turn)
				{
                    //score += 5;
                    score += 2;
                }
				x++;
			}
		}

		return score;
	}


	private int threePieces(Piece turn)
	{
		int score = 0;

		//check for PPPE || EPPP (vertical and horizontal) score += 50
		//check for PPEP || PEPP (vertical and horizontal) score += 30
		//Vertical
		for (int j = 0; j < 7; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				Piece p1 = pieces[j, k];
				Piece p2 = pieces[j, k + 1];
				Piece p3 = pieces[j, k + 2];
				Piece p4 = pieces[j, k + 3];
				if (p1 == turn && p2 == turn && p3 == turn && p4 == Piece.EMPTY
					|| p1 == Piece.EMPTY && p2 == turn && p3 == turn && p4 == turn)
				{
                    //score += 50;
                    score += 10;
                } 
				else if (p1 == turn && p2 == turn && p3 == Piece.EMPTY && p4 == turn
					|| p1 == turn && p2 == Piece.EMPTY && p3 == turn && p4 == turn)
				{
                    //score += 30;
                    score += 8;
                }
			}
		}

		//Horizontal
		for (int i = 0; i < 6; i++)
		{
			for (int k = 0; k < 4; k++)
			{
				Piece p1 = pieces[k, i];
				Piece p2 = pieces[k + 1, i];
				Piece p3 = pieces[k + 2, i];
				Piece p4 = pieces[k + 3, i];
				if (p1 == turn && p2 == turn && p3 == turn && p4 == Piece.EMPTY
					|| p1 == Piece.EMPTY && p2 == turn && p3 == turn && p4 == turn)
				{
                    //score += 50;
                    score += 10;
                }
				else if (p1 == turn && p2 == turn && p3 == Piece.EMPTY && p4 == turn
					|| p1 == turn && p2 == Piece.EMPTY && p3 == turn && p4 == turn)
				{
                    //score += 30;
                    score += 8;
                }
			}
		}

		score += check_3pieces_Oblique1(turn);
		score += check_3pieces_Oblique2(turn);
		return score;
	}
}



public enum Piece
{
	EMPTY, RED, YELLOW
}