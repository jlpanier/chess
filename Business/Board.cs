using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Board
    {
        private readonly Piece?[] _squares = new Piece?[64];

        public Piece? GetPiece(int index) => _squares[index];
        public void SetPiece(int index, Piece? piece) => _squares[index] = piece;

        public void SetupInitialPosition() { /* place pieces */ }

        public IEnumerable<Move> GetLegalMoves(int fromIndex)
        {
            // generate pseudo-legal moves, filter by king safety
            yield break;
        }

        public void ApplyMove(Move move)
        {
            // handle captures, promotion, castling, en passant
        }

        public bool IsCheck(Color color) { return false; }

        public bool IsCheckmate(Color color) { return false; }
    }

}
