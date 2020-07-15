using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			// Beginner plays randomly
			if (depth == 0)
			{
				List<int> decisions = new List<int>();
				for(int i=0; i<7; i++){
                    //GameController.log($"root.board.r[i] = {root.board.r[i]}");
                    if (root.board.r[i] >= 0){
						decisions.Add(i);
                    }
				}

				Random r = new Random();
                int d = r.Next();

				//GameController.log($"decisions.Count = {decisions.Count}");

				return decisions[d % decisions.Count];
			}
			return root.constructTree(depth);
		}
	}
}
