using Raylib_cs;
using System;

class Tetrimino
{
    public int[,] Shape { get; private set; }
    public Color Color { get; private set; }
    public int X { get; set; }
    public int Y { get; set; }
    public const int CellSize = 30;

    public Tetrimino()
    {
        Random random = new Random();
        int pieceType = random.Next(7);
        switch (pieceType)
        {
            case 0:
                Shape = new int[,] { { 1, 1, 1, 1 } };
                Color = Color.SKYBLUE;
                break;
            case 1:
                Shape = new int[,] { { 1, 1 }, { 1, 1 } };
                Color = Color.YELLOW;
                break;
            case 2:
                Shape = new int[,] { { 0, 1, 0 }, { 1, 1, 1 } };
                Color = Color.PURPLE;
                break;
            case 3:
                Shape = new int[,] { { 1, 0, 0 }, { 1, 1, 1 } };
                Color = Color.ORANGE;
                break;
            case 4:
                Shape = new int[,] { { 0, 0, 1 }, { 1, 1, 1 } };
                Color = Color.BLUE;
                break;
            case 5:
                Shape = new int[,] { { 0, 1, 1 }, { 1, 1, 0 } };
                Color = Color.GREEN;
                break;
            case 6:
                Shape = new int[,] { { 1, 1, 0 }, { 0, 1, 1 } };
                Color = Color.RED;
                break;
        }

        X = 3;
        Y = 0;
    }

    public void DrawTetrimino()
    {
        for (int row = 0; row < Shape.GetLength(0); row++)
        {
            for (int col = 0; col < Shape.GetLength(1); col++)
            {
                if (Shape[row, col] == 1)
                {
                    Raylib.DrawRectangle((X + col) * CellSize, (Y + row) * CellSize, CellSize, CellSize, Color);
                    Raylib.DrawRectangleLines((X + col) * CellSize, (Y + row) * CellSize, CellSize, CellSize, Color.BLACK);
                }
            }
        }
    }

    public void MoveLeft(SetupGrid grid)
    {
        if (CanMove(-1, 0, grid))
        {
            X--;
        }
    }

    public void MoveRight(SetupGrid grid)
    {
        if (CanMove(1, 0, grid))
        {
            X++;
        }
    }

    public bool MoveDown(SetupGrid grid)
    {
        if (CanMove(0, 1, grid))
        {
            Y++;
            return false;
        }
        else
        {
            LockToGrid(grid);
            return true;
        }
    }

    public void Rotate(SetupGrid grid)
    {
        int[,] rotatedShape = new int[Shape.GetLength(1), Shape.GetLength(0)];

        for (int row = 0; row < Shape.GetLength(0); row++)
        {
            for (int col = 0; col < Shape.GetLength(1); col++)
            {
                rotatedShape[col, Shape.GetLength(0) - row - 1] = Shape[row, col];
            }
        }

        if (CanRotate(rotatedShape, grid))
        {
            Shape = rotatedShape;
        }
    }

    private bool CanRotate(int[,] rotatedShape, SetupGrid grid)
    {
        for (int row = 0; row < rotatedShape.GetLength(0); row++)
        {
            for (int col = 0; col < rotatedShape.GetLength(1); col++)
            {
                if (rotatedShape[row, col] == 1)
                {
                    int newX = X + col;
                    int newY = Y + row;

                    if (newX < 0 || newX >= SetupGrid.Columns || newY < 0 || newY >= SetupGrid.Rows)
                    {
                        return false;
                    }

                    if (grid.Grid[newY, newX] != 0)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    private bool CanMove(int offsetX, int offsetY, SetupGrid grid)
    {
        for (int row = 0; row < Shape.GetLength(0); row++)
        {
            for (int col = 0; col < Shape.GetLength(1); col++)
            {
                if (Shape[row, col] == 1)
                {
                    int newX = X + col + offsetX;
                    int newY = Y + row + offsetY;

                    if (newX < 0 || newX >= SetupGrid.Columns || newY >= SetupGrid.Rows)
                    {
                        return false;
                    }

                    if (grid.Grid[newY, newX] != 0)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    private void LockToGrid(SetupGrid grid)
    {
        for (int row = 0; row < Shape.GetLength(0); row++)
        {
            for (int col = 0; col < Shape.GetLength(1); col++)
            {
                if (Shape[row, col] == 1)
                {
                    grid.Grid[Y + row, X + col] = 1;
                }
            }
        }
    }
}

