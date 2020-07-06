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
		string s = "[ ";
		childs.ForEach(e =>
		{
			s += e.decision + " : " + e.score + " ; ";
		});
		s += "]";
		return s;
	}

	public int constructTree(int depth)
	{
		/*
		GameController.log($"board.r.Length = {board.r.Length}");
		for(int i=0; i< board.r.Length; i++)
		{
			GameController.log($"board.r[{i}] = {board.r[i]}");
			if (board.r[i] > 0)
			{
				Board newBoard = board.clone();
				newBoard.play(i);
				Node childNode = new Node(newBoard, level + 1);
				childNode.decision = i;
				childNode.constructTree(depth - 1);
				childs.Add(childNode);
			}
		}
		GameController.log($"childs.Count = {childs.Count}");
		*/

		int score = constructTree2(this, depth, true, Int32.MinValue, Int32.MaxValue);
		///Debug.Log($"score = {score}");
		return this.decision;
	}


	public int constructTree2(Node node, int depth, bool isMaximizingPlayer, int alpha, int beta)
	{
		// Leaf node
		if (depth == 0)
		{
			/////Debug.Log($"turn = {board.turn}");
			Piece turn = isMaximizingPlayer ? Piece.YELLOW : Piece.BLUE;
			/////Debug.Log($"turn = {turn}");
			int s = node.board.calculateScore(turn);
			node.score = s;
			return s;
		}

		if (isMaximizingPlayer)
		{
			///Debug.Log("Maximizing player");
			int bestVal = Int32.MinValue;

			int nodeDecision = 0;
			for(int i = 0; i < 7; i++)
			{
				if (board.r[i] > 0)
				{
					///Debug.Log($"i = {i}");
					/////Debug.Log($"board: {board}");
					Board newBoard = board.clone();
					/////Debug.Log($"board turn before play: {newBoard.turn}");
					newBoard.play(i);
					/////Debug.Log($"board turn after play: {newBoard.turn}");
					///Debug.Log($"new board: {newBoard}");

					Node childNode = new Node(newBoard.clone(), level + 1);
					childNode.decision = i;
					//childNode.constructTree(depth - 1);
					childs.Add(childNode);


					int value = childNode.constructTree2(childNode, depth - 1, false, alpha, beta);
					///Debug.Log($"value: {value}");
					///Debug.Log($"childNode.decision: {childNode.decision}");
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
			Debug.Log($"maximizing node: {node}");
			return bestVal;
		}
		else
		{
			///Debug.Log("Minimizing player");
			int bestVal = Int32.MaxValue;

			int nodeDecision = 0;
			for (int i = 0; i < 7; i++)
			{
				if (board.r[i] > 0)
				{
					///Debug.Log($"i = {i}");
					/////Debug.Log($"board: {board}");
					Board newBoard = board.clone();
					/////Debug.Log($"board turn before play: {newBoard.turn}");
					newBoard.play(i);
					/////Debug.Log($"board turn after play: {newBoard.turn}");
					///Debug.Log($"new board: {newBoard}");

					Node childNode = new Node(newBoard.clone(), level + 1);
					childNode.decision = i;
					//childNode.constructTree(depth - 1);
					childs.Add(childNode);


					int value = childNode.constructTree2(childNode, depth - 1, true, alpha, beta);
					///Debug.Log($"value: {value}");
					///Debug.Log($"childNode.decision: {childNode.decision}");
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
			Debug.Log($"minimizing node: {node}");
			return bestVal;
		}
		
	}
}
