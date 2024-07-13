using GameOfLife.Domain.Interfaces;
using GameOfLife.Domain.Models;
using GameOfLife.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameOfLife.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoardController : ControllerBase
    {
        public Board Board;

        private readonly ILogger<BoardController> _logger;
        private readonly IBoardService _boardService;

        public BoardController(ILogger<BoardController> logger, IBoardService boardService)
        {
            _logger = logger;
            _boardService = boardService;
        }

        [HttpGet]
        public string Welcome()
        {
            return "Welcome to Conway's game of life :)";
        }

        [HttpPost]
        public Guid CreateBoard([FromBody] Board board)
        {
            // Initializing the grid.
            int[,] grid = {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 1, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 1, 0, 0, 0, 0, 0 },
                { 0, 0, 1, 1, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };

            board = new Board(Guid.NewGuid(), grid);



            return board.Id;
        }

        [HttpGet("GetNextState")]
        public Board GetNextState([FromQuery] Guid Id)
        {
            //get board by id
            //do it here
            var board = Board;

            var newCells = _boardService.NextGeneration(board.Cells);

            return new Board(Board.Id, newCells);
        }
    }
}
