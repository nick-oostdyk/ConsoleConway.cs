namespace cyxx.Examples
{
	class Program
	{
		const int WIDTH = 10;
		const int HEIGHT = 10;

		private static int[,] grid = new int[WIDTH, HEIGHT];

		// Simulates Conways `Game of Life`
		// Simulation stops if all cells are dead
		public static void Main()
		{
			_readInputFile();

			Console.CursorVisible = false;
			while (true)
			{
				Console.SetCursorPosition(0, 0);
				_printIteration();

				bool stop = true;
				foreach (int i in grid)
				{
					if (i == 0) continue;
					stop = false;
					break;
				}
				if (stop) break;

				_calculateNextEpoch();
				Thread.Sleep(250);
			}
		}

		#region utility
		private static void _readInputFile()
		{
			var path = "../../../input.txt";

			if (!File.Exists(path))
				return;

			var input = File.ReadAllText(path);
			var lines = input.Split("\n");
			for (int i = 0; i < WIDTH; ++i)
				for (int j = 0; j < HEIGHT; ++j)
					grid[i, j] = int.Parse(lines[i][j].ToString());
		}

		private static void _printIteration()
		{
			for (int i = 0; i < WIDTH; ++i)
			{
				for (int j = 0; j < HEIGHT; ++j)
					Console.Write(grid[i, j] == 0 ? " ." : " #");
				Console.Write('\n');
			}
		}
		#endregion

		#region logic
		private static void _calculateNextEpoch()
		{
			var next = new int[WIDTH, HEIGHT];
			for (int i = 0; i < WIDTH; ++i)
				for (int j = 0; j < HEIGHT; ++j)
					next[i, j] = _calculateCellLife(i, j) ? 1 : 0;
			grid = next;
		}

		private static bool _calculateCellLife(int i, int j)
		{
			int numNeighbours = 0;
			for (int x = i - 1; x <= i + 1; ++x)
			{
				if (x < 0 || x > WIDTH - 1) continue;
				for (int y = j - 1; y <= j + 1; ++y)
				{
					if (y < 0 || y > HEIGHT - 1) continue;
					if (x == i && y == j) continue;
					if (grid[x, y] == 1) ++numNeighbours;
				}
			}

			if (grid[i, j] == 0)
				return numNeighbours == 3;
			else
				return numNeighbours > 1 && numNeighbours < 4;
		}
		#endregion
	}
}