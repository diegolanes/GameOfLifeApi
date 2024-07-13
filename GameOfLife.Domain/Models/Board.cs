namespace GameOfLife.Domain.Models
{
    public class Board
    {
        public Guid Id { get; private set; }
        public int[,] Cells { get; set; }

        public Board(Guid id, int[,] cells)
        {
            Id = id;
            Cells = cells;
        }
    }
}
