using GameOfLife.Domain.Dtos;
using GameOfLife.Domain.Interfaces;
using GameOfLife.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GameOfLife.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoardController : ControllerBase
    {
        public Board Board;

        private readonly ILogger<BoardController> _logger;
        private readonly IBoardService _boardService;
        private readonly IRedisService _redisService;

        public BoardController(ILogger<BoardController> logger, IBoardService boardService, IRedisService redisService)
        {
            _logger = logger;
            _boardService = boardService;
            _redisService = redisService;
        }

        [HttpGet]
        public string Welcome()
        {
            return "Welcome to Conway's game of life :)";
        }

        [HttpPost]
        public Guid CreateBoard([FromBody] BoardDto board)
        {
            _logger.LogTrace("Instantiating new Board");

            var id = Guid.NewGuid();

            _redisService.SetKeyValue(id, new Board(id, board.Cells) { });

            return id;
        }

        [HttpGet("GetNextState")]
        public Board GetNextState([FromQuery] Guid Id)
        {
            var board = _redisService.GetKeyValue(Id);

            var newCells = _boardService.NextGeneration(board);

            return new Board(Board.Id, null);
        }

        [HttpGet("GetFinalState")]
        public Board GetFinalState([FromQuery] Guid Id)
        {
            var board = Board;
            var newCells = _redisService.GetKeyValue(Id);

            for (int i = 0; i < 10; i++)
            {
                newCells = _boardService.NextGeneration(board.Cells);
            }

            return new Board(Board.Id, newCells);
        }
    }
}
