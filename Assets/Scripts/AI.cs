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
			//TODO use Minimax
			Random r = new Random();
			return r.Next() % 7;
		}
	}
}
