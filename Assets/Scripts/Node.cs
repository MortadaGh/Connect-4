using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	private Node[] childs;
	private Board board;
	private int score;
	private int level;
	private int maxdepth;
	private NodeType type;

	public Node(Board board, int level, int maxdepth)
	{
		this.board = board;
		this.level = level;
		this.maxdepth = maxdepth;
		type = level == maxdepth? NodeType.Terminal : level % 2 == 0 ? NodeType.Max : NodeType.Min;
	}

	public void constructDecisionTree()
	{

	}
}

public enum NodeType
{
	Min, Max, Terminal
}