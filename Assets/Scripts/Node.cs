using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public List<Node> childs;
	public Board board;
	public int score;
	public int level;
	public int decision;

	public Node(Board board, int level)
	{
		this.board = board;
		this.level = level;
		childs = new List<Node>();
	}

	public override string ToString()
	{
		string s = "[";
		childs.ForEach(e =>
		{
			s += e.decision + " : " + e.score + " | ";
		});
		s += "]";
		return s;
	}

	public int constructTree(int depth)
	{
		int score = constructTree2(this, depth, true, Int32.MinValue, Int32.MaxValue);
		return this.decision;
	}


	public int constructTree2(Node node, int depth, bool isMaximizingPlayer, int alpha, int beta)
	{
		// Leaf node
		if (depth == 0)
		{
			Piece turn = isMaximizingPlayer ? Piece.YELLOW : Piece.RED;
			int s = node.board.calculateScore(turn);
			node.score = isMaximizingPlayer ? s : -s;
			return isMaximizingPlayer ? s : -s;
		}

		if (isMaximizingPlayer)
		{
			int bestVal = Int32.MinValue;

			int nodeDecision = 0;
			for(int i = 0; i < 7; i++)
			{
				if (board.r[i] >= 0)
				{
					Board newBoard = board.clone();
					newBoard.play(i);

					Node childNode = new Node(newBoard.clone(), level + 1);
					childNode.decision = i;
					childs.Add(childNode);
					
					int value = childNode.constructTree2(childNode, depth - 1, false, alpha, beta);

					if (value > bestVal)
					{
						bestVal = value;
						nodeDecision = i;
					}
					bestVal = Math.Max(bestVal, value);
					alpha = Math.Max(alpha, bestVal);

					if (beta <= alpha)
						break;
				}
			}
			node.score = bestVal;
			node.decision = nodeDecision;
			return bestVal;
		}
		else
		{
			int bestVal = Int32.MaxValue;

			int nodeDecision = 0;
			for (int i = 0; i < 7; i++)
			{
				if (board.r[i] >= 0)
				{
					Board newBoard = board.clone();
					newBoard.play(i);

					Node childNode = new Node(newBoard.clone(), level + 1);
					childNode.decision = i;
					childs.Add(childNode);

					int value = childNode.constructTree2(childNode, depth - 1, true, alpha, beta);
					if (value < bestVal)
					{
						bestVal = value;
						nodeDecision = i;
					}
					bestVal = Math.Min(bestVal, value);
					beta = Math.Min(beta, bestVal);

					if (beta <= alpha)
						break;
				}
			}
			node.score = bestVal;
			node.decision = nodeDecision;
			return bestVal;
		}
		
	}
}
