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
				_printEpoch();

				if (_checkShouldStop())
					break;

				_calculateNextEpoch();
				Thread.Sleep(250);
			}
		}

		#region utility
		// checks if every cell in grid is dead
		private static bool _checkShouldStop()
		{
			foreach (int i in grid)
				if (i == 1)
					return false;
			return true;
		}

		// reads base state from input.txt
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

		// prints the current generation over the last one
		private static void _printEpoch()
		{
			Console.SetCursorPosition(0, 0);
			for (int i = 0; i < WIDTH; ++i)
			{
				for (int j = 0; j < HEIGHT; ++j)
					Console.Write(grid[i, j] == 0 ? " ." : " #");
				Console.Write('\n');
			}
		}
		#endregion

		#region logic
		// calculates the entire next generation
		private static void _calculateNextEpoch()
		{
			var next = new int[WIDTH, HEIGHT];
			for (int i = 0; i < WIDTH; ++i)
				for (int j = 0; j < HEIGHT; ++j)
					next[i, j] = _calculateCellLife(i, j) ? 1 : 0;
			grid = next;
		}

		// calculates if the cell at <i, j> will live in the next generation
		private static bool _calculateCellLife(int i, int j)
		{
			int numNeighbours = 0;
			for (int x = i - 1; x <= i + 1; ++x)
			{
				if (x < 0 || x > WIDTH - 1) continue; // index out of bounds
				for (int y = j - 1; y <= j + 1; ++y)
				{
					if (y < 0 || y > HEIGHT - 1) continue; // index out of bounds
					if (x == i && y == j) continue; // don't check yourself
					if (grid[x, y] == 1) ++numNeighbours;
				}
			}

			// a dead cell can only be revived with exactly 3 neighbours
			if (grid[i, j] == 0)
				return numNeighbours == 3;
			// a living cell will only continue living if it has 2 or 3 neighbours
			else
				return numNeighbours == 2 || numNeighbours == 3;
		}
		#endregion
	}
}