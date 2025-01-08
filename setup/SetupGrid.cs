using Raylib_cs;

class SetupGrid
{
	public int[,] Grid { get; private set; }
	public const int Rows = 20;
	public const int Columns = 10;
	public const int CellSize = 30;

	public SetupGrid()
	{
		Grid = new int[Rows, Columns];
		InitializeGrid();
	}

	private void InitializeGrid()
	{
		for (int row = 0; row < Rows; row++)
		{
			for (int col = 0; col < Columns; col++)
			{
				Grid[row, col] = 0;
			}
		}
	}

	public void DrawGrid()
	{
		for (int row = 0; row < Rows; row++)
		{
			for (int col = 0; col < Columns; col++)
			{
				if (Grid[row, col] != 0)
				{
					Raylib.DrawRectangle(col * CellSize, row * CellSize, CellSize, CellSize, Color.DARKGRAY);
				}
				Raylib.DrawRectangleLines(col * CellSize, row * CellSize, CellSize, CellSize, Color.LIGHTGRAY);
			}
		}
	}

    public int ClearFullLines()
    {
        int linesCleared = 0;

        for (int row = 0; row < Rows; row++)
        {
            bool isFullLine = true;

            for (int col = 0; col < Columns; col++)
            {
                if (Grid[row, col] == 0)
                {
                    isFullLine = false;
                    break;
                }
            }

            if (isFullLine)
            {
                RemoveLine(row);
                linesCleared++;
            }
        }

        return linesCleared;
    }


    private void RemoveLine(int line)
	{
		for (int row = line; row > 0; row--)
		{
			for (int col = 0; col < Columns; col++)
			{
				Grid[row, col] = Grid[row - 1, col];
			}
		}

		for (int col = 0; col < Columns; col++)
		{
			Grid[0, col] = 0;
		}
	}
}
