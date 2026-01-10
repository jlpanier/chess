using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business.Piece;

namespace Business
{
    public class Board
    {
        public Square[]? Squares;

        public Board() 
        {
            NewGame();
        }

        public void NewGame()
        {
            Squares = new Square[]
            {
                new Square(0,new Piece(PieceType.Rook, PieceColor.White)),
                new Square(1,new Piece(PieceType.Knight, PieceColor.White)),
                new Square(2,new Piece(PieceType.Bishop, PieceColor.White)),
                new Square(3,new Piece(PieceType.Queen, PieceColor.White)),
                new Square(4,new Piece(PieceType.King, PieceColor.White)),
                new Square(5,new Piece(PieceType.Bishop, PieceColor.White)),
                new Square(6,new Piece(PieceType.Knight, PieceColor.White)),
                new Square(7,new Piece(PieceType.Rook, PieceColor.White)),
                new Square(8,new Piece(PieceType.Pawn, PieceColor.White)),
                new Square(9,new Piece(PieceType.Pawn, PieceColor.White)),
                new Square(10,new Piece(PieceType.Pawn, PieceColor.White)),
                new Square(11,new Piece(PieceType.Pawn, PieceColor.White)),
                new Square(12,new Piece(PieceType.Pawn, PieceColor.White)),
                new Square(13,new Piece(PieceType.Pawn, PieceColor.White)),
                new Square(14,new Piece(PieceType.Pawn, PieceColor.White)),
                new Square(15,new Piece(PieceType.Pawn, PieceColor.White)),
                new Square(16),
                new Square(17),
                new Square(18),
                new Square(19),
                new Square(20),
                new Square(21),
                new Square(22),
                new Square(23),
                new Square(24),
                new Square(25),
                new Square(26),
                new Square(27),
                new Square(28),
                new Square(29),
                new Square(30),
                new Square(31),
                new Square(32),
                new Square(33),
                new Square(34),
                new Square(35),
                new Square(36),
                new Square(37),
                new Square(38),
                new Square(39),
                new Square(40),
                new Square(41),
                new Square(42),
                new Square(43),
                new Square(44),
                new Square(45),
                new Square(46),
                new Square(47),
                new Square(48, new Piece(PieceType.Pawn, PieceColor.Black)),
                new Square(49, new Piece(PieceType.Pawn, PieceColor.Black)),
                new Square(50, new Piece(PieceType.Pawn, PieceColor.Black)),
                new Square(51, new Piece(PieceType.Pawn, PieceColor.Black)),
                new Square(52, new Piece(PieceType.Pawn, PieceColor.Black)),
                new Square(53, new Piece(PieceType.Pawn, PieceColor.Black)),
                new Square(54, new Piece(PieceType.Pawn, PieceColor.Black)),
                new Square(55, new Piece(PieceType.Pawn, PieceColor.Black)),
                new Square(56, new Piece(PieceType.Rook, PieceColor.Black)),
                new Square(57, new Piece(PieceType.Knight, PieceColor.Black)),
                new Square(58, new Piece(PieceType.Bishop, PieceColor.Black)),
                new Square(59, new Piece(PieceType.King, PieceColor.Black)),
                new Square(60, new Piece(PieceType.Queen, PieceColor.Black)),
                new Square(61, new Piece(PieceType.Bishop, PieceColor.Black)),
                new Square(62, new Piece(PieceType.Knight, PieceColor.Black)),
                new Square(63, new Piece(PieceType.Rook, PieceColor.Black))
            };
        }

        public Piece? GetPiece(int index) => Squares == null ? null : Squares[index].Piece;

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
