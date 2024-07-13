using GameOfLife.Domain.Models;

namespace GameOfLife.Domain.Interfaces
{
    public interface IBoardService
    {
        public int[,] NextGeneration(int[,] grid);
    }
}
