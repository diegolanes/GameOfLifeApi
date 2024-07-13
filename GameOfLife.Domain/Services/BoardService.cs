using GameOfLife.Domain.Interfaces;

namespace GameOfLife.Domain.Services
{
    public class BoardService : IBoardService
    {
        public BoardService() { }

        public int[,] NextGeneration(int[,] grid)
        {
            var rowsCount = grid.GetLength(0);
            var colsCount = grid.GetLength(1);
            int[,] future = new int[rowsCount, colsCount];

            // Loop through every cell
            for (int l = 1; l < rowsCount - 1; l++)
            {
                for (int m = 1; m < colsCount - 1; m++)
                {

                    int liveNeighbours = GetLiveNeighbours(l, m, grid);

                    // Implementing the Rules of Life

                    // Cell is lonely and dies
                    if ((grid[l, m] == 1) &&
                                (liveNeighbours < 2))
                        future[l, m] = 0;

                    // Cell dies due to over population
                    else if ((grid[l, m] == 1) &&
                                 (liveNeighbours > 3))
                        future[l, m] = 0;

                    // A new cell is born
                    else if ((grid[l, m] == 0) &&
                                (liveNeighbours == 3))
                        future[l, m] = 1;

                    // Remains the same
                    else
                        future[l, m] = grid[l, m];
                }
            }

            return future;
        }

        private int GetLiveNeighbours(int x, int y, int[,] board)
        {
            int liveNeighbours = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (x + i < 0 || x + i >= board.GetLength(0)) //Avoid out of range exception
                        continue;
                    if (y + j < 0 || y + j >= board.GetLength(1)) //Avoid out of range exception
                        continue;
                    if (x + i == x && y + j == y) //Avoid the element itself
                        continue;

                    liveNeighbours += board[x + i, y + j];
                }
            }

            return liveNeighbours;
        }
    }
}
