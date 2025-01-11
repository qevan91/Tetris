using Raylib_cs;

class SetupGrid
{
    public int[,] Grid { get; set; }
	public const int Rows = 20;
	public const int Columns = 10;
	public const int CellSize = 30;

	public SetupGrid()
	{
		Grid = new int[Rows, Columns];
		InitializeGrid();
	}

    public void InitializeGrid()
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
        int screenWidth = 1920;
        int screenHeight = 1080;

        int gridWidth = Columns * CellSize;
        int gridHeight = Rows * CellSize;

        int offsetX = (screenWidth - gridWidth) / 2;
        int offsetY = (screenHeight - gridHeight) / 2;

        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                int x = offsetX + col * CellSize;
                int y = offsetY + row * CellSize;

                if (Grid[row, col] != 0)
                {
                    Raylib.DrawRectangle(x, y, CellSize, CellSize, Color.DARKGRAY);
                }
                Raylib.DrawRectangleLines(x, y, CellSize, CellSize, Color.LIGHTGRAY);
            }
        }
    }

    public void DrawGrid1()
    {
        int screenWidth = 1920;
        int screenHeight = 1080;

        int gridWidth = Columns * CellSize;
        int gridHeight = Rows * CellSize;

        int totalGridWidth = 2 * gridWidth;
        int availableSpace = screenWidth - totalGridWidth;

        int offsetX = availableSpace / 3;
        int offsetY = (screenHeight - gridHeight) / 2;

        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                int x = offsetX + col * CellSize;
                int y = offsetY + row * CellSize;

                if (Grid[row, col] != 0)
                {
                    Raylib.DrawRectangle(x, y, CellSize, CellSize, Color.DARKGRAY);
                }
                Raylib.DrawRectangleLines(x, y, CellSize, CellSize, Color.LIGHTGRAY);
            }
        }
    }
    
    public void DrawGrid2()
    {
        int screenWidth = 1920;
        int screenHeight = 1080;

        int gridWidth = Columns * CellSize;
        int gridHeight = Rows * CellSize;

        int totalGridWidth = 2 * gridWidth;
        int availableSpace = screenWidth - totalGridWidth;

        int spaceBetweenGrids = 20;

        int offsetX = (availableSpace / 2) + gridWidth + spaceBetweenGrids;
        int offsetY = (screenHeight - gridHeight) / 2;

        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                int x = offsetX + col * CellSize;
                int y = offsetY + row * CellSize;

                if (Grid[row, col] != 0)
                {
                    Raylib.DrawRectangle(x, y, CellSize, CellSize, Color.DARKGRAY);
                }
                Raylib.DrawRectangleLines(x, y, CellSize, CellSize, Color.LIGHTGRAY);
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
