using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
	class AI
	{
		private int depth;
		private Node root;

		public AI(int depth, Node root)
		{
			this.depth = depth;
			this.root = root;
		}

		public int getDecision()
		{
			return root.constructTree(depth);
			//int score = root.constructTree(depth);
			
			//int score = minimax(root, 0, true, Int32.MinValue, Int32.MaxValue);
			
			//GameController.log($"Score = {score}");
			
			/*foreach (Node child in root.childs)
			{
				GameController.log($"child.score = {child.score}");
				if (child.score == score)
				{
					root.decision = child.decision;
					break;
				}
			}*/
			
			//return root.decision;
			//Random r = new Random();
			//return r.Next() % 7;
		}
		/*
		public int minimax(Node node, int depth, bool isMaximizingPlayer, int alpha, int beta)
		{
			// Leaf node
			if (node.childs.Count == 0 || depth == this.depth )
			{
				int s = node.board.calculateScore();
				node.score = s;
				return s;
			}

			if (isMaximizingPlayer)
			{
				int bestVal = Int32.MinValue;
				
				foreach(Node child in node.childs)
				{
					int value = minimax(child, depth + 1, false, alpha, beta);
					bestVal = Math.Max(bestVal, value);
					alpha = Math.Max(alpha, bestVal);

					if (beta <= alpha)
						break;

				}
				node.score = bestVal;
				return bestVal;
			}
			else
			{
				int bestVal = Int32.MaxValue;

				foreach (Node child in node.childs)
				{
					int value = minimax(child, depth + 1, true, alpha, beta);
					bestVal = Math.Min(bestVal, value);
					beta = Math.Min(beta, bestVal);

					if (beta <= alpha)
						break;

				}
				node.score = bestVal;
				return bestVal;
			}
		}
		*/
	}
}
