using Repository.Dbo;
using Repository.Entities;
using System.Runtime.InteropServices;
using System.Text;
using static Business.Piece;

namespace Business
{
    /// <summary>
    /// Gestion du plateau d'échecs 
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Taille de l'échiquier (8x8)
        /// </summary>
        const int Size = 8;

        /// <summary>
        /// Définition de chaque case de l'échiquier
        /// </summary>
        public Square[] Squares { get; private set; }

        /// <summary>
        /// Désigne la couleur du joueur qui doit jouer
        /// </summary>
        public PieceColor Playing { get; private set; }

        /// <summary>
        /// Retourne la case à l'index spécifié
        /// </summary>
        public Square GetSquare(int index) => Squares[index];

        /// <summary>
        /// Piéces prises
        /// </summary>
        public List<Piece> Takens { get; private set; } = new List<Piece>();

        public Board()
        {
            Squares = new Square[0];
        }

        public Moves Moves { get; private set; } = new Moves();

        /// <summary>
        /// Rafraichir le plateau pour une nouvelle partie
        /// </summary>
        public void NewGame()
        {
            Playing = PieceColor.White;
            Squares = new Square[]
            {
                new Square(0,new Piece(PieceType.Rook, PieceColor.White)),
                new Square(1,new Piece(PieceType.Knight, PieceColor.White)),
                new Square(2,new Piece(PieceType.Bishop, PieceColor.White)),
                new Square(3,new Piece(PieceType.King, PieceColor.White)),
                new Square(4,new Piece(PieceType.Queen, PieceColor.White)),
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
            SetInfo();
        }

        public void Unselect()
        {
            foreach (var item in Squares)
            {
                item.Unselect();
            }

        }

        /// <summary>
        /// Liste des déplacements possibles pour une pièce sur cette case donnée
        /// </summary>
        public void Select(Square square)
        {
            if (square.Piece != null && Playing == square.Piece.Color)
            {
                var moves = AuthorizedMoves(square);
                foreach (var item in Squares)
                {
                    item.Unselect();
                    item.IsAuthorizedMove = moves.Any(_ => _.Index == item.Index);
                }
                square.Select();
            }
            SetInfo();
        }

        /// <summary>
        /// Liste des mouvements autorisés pour une pièce sur cette case donnée
        /// </summary>
        /// <param name="square"></param>
        /// <returns></returns>
        private List<Square> AuthorizedMoves(Square square)
        {
            var result = new List<Square>();
            if (square.Piece != null && square.Piece.Color == Playing)
            {
                switch (square.Piece.Type)
                {
                    case Piece.PieceType.King:
                        for (int r = square.Row - 1; r <= square.Row + 1; r++)
                        {
                            for (int c = square.Column - 1; c <= square.Column + 1; c++)
                            {
                                if (r >= 0 && r < 8 && c >= 0 && c < 8)
                                {
                                    var index = r * 8 + c;
                                    var to = Squares[index];
                                    if (to.Piece == null)
                                    {
                                        // sauf si echec
                                        result.Add(to);
                                    }
                                    else if (to.Piece.Color != square.Piece.Color)
                                    {
                                        // sauf si echec
                                        result.Add(to);
                                    }
                                }
                            }
                        }
                        break;
                    case Piece.PieceType.Queen:
                        int[][] queenDirections =
                        {
                            new[] { -1, -1 }, // haut-gauche
                            new[] { -1,  1 }, // haut-droite
                            new[] {  1, -1 }, // bas-gauche
                            new[] {  1,  1 }, // bas-droite

                            new[] { -1,  0 }, // haut
                            new[] {  1,  0 }, // bas
                            new[] {  0, -1 }, // gauche
                            new[] {  0,  1 }  // droite
                        };
                        foreach (var dir in queenDirections)
                        {
                            int row = square.Row + dir[0];
                            int col = square.Column + dir[1];

                            while (row >= 0 && row < Size && col >= 0 && col < Size)
                            {
                                var target = Squares.Single(_ => _.Row == row && _.Column == col);
                                if (target.Piece == null)
                                {
                                    result.Add(target); // case vide
                                }
                                else
                                {
                                    if (target.Piece.Color != square.Piece.Color) result.Add(target); // capture possible
                                    break; // on s'arrête dans tous les cas
                                }
                                row += dir[0];
                                col += dir[1];
                            }
                        }
                        break;
                    case Piece.PieceType.Rook:
                        int[][] rookDdirections =
                        {
                            new[] { -1,  0 }, // haut
                            new[] {  1,  0 }, // bas
                            new[] {  0, -1 }, // gauche
                            new[] {  0,  1 }  // droite
                        };


                        foreach (var dir in rookDdirections)
                        {
                            int row = square.Row + dir[0];
                            int col = square.Column + dir[1];

                            while (row >= 0 && row < Size && col >= 0 && col < Size)
                            {
                                var target = Squares.Single(_ => _.Row == row && _.Column == col);
                                if (target.Piece == null)
                                {
                                    result.Add(target); // case vide
                                }
                                else
                                {
                                    if (target.Piece.Color != square.Piece.Color) result.Add(target); // capture possible
                                    break; // on s'arrête dans tous les cas
                                }
                                row += dir[0];
                                col += dir[1];
                            }
                        }
                        break;
                    case Piece.PieceType.Bishop:
                        int[][] bishopDirections =
                        {
                            new[] { -1, -1 }, // haut-gauche
                            new[] { -1,  1 }, // haut-droite
                            new[] {  1, -1 }, // bas-gauche
                            new[] {  1,  1 }  // bas-droite
                        };

                        foreach (var dir in bishopDirections)
                        {
                            int row = square.Row + dir[0];
                            int col = square.Column + dir[1];

                            while (row >= 0 && row < Size && col >= 0 && col < Size)
                            {
                                var target = Squares.Single(_ => _.Row == row && _.Column == col);
                                if (target.Piece == null)
                                {
                                    result.Add(target); // case vide
                                }
                                else
                                {
                                    if (target.Piece.Color != square.Piece.Color) result.Add(target); // capture possible
                                    break; // on s'arrête dans tous les cas
                                }
                                row += dir[0];
                                col += dir[1];
                            }
                        }
                        break;
                    case Piece.PieceType.Knight:
                        int[] knightMoves = { -17, -15, -10, -6, 6, 10, 15, 17 };
                        foreach (var move in knightMoves)
                        {
                            int targetIndex = square.Index + move;
                            if (targetIndex >= 0 && targetIndex < 64)
                            {
                                int targetRow = targetIndex / 8;
                                int targetCol = targetIndex % 8;
                                int rowDiff = Math.Abs(targetRow - square.Row);
                                int colDiff = Math.Abs(targetCol - square.Column);
                                if ((rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2))
                                {
                                    var targetSquare = Squares[targetIndex];
                                    if (targetSquare.Piece == null || targetSquare.Piece.Color != square.Piece.Color)
                                    {
                                        result.Add(targetSquare);
                                    }
                                }
                            }
                        }
                        break;
                    case Piece.PieceType.Pawn:
                        int forwardIndex;
                        int captureLeftIndex;
                        int captureRightIndex;
                        switch (square.Piece.Color)
                        {
                            case PieceColor.White:
                                forwardIndex = square.Index + 8;
                                if (forwardIndex < 64 && Squares[forwardIndex].Piece == null)
                                {
                                    result.Add(Squares[forwardIndex]);
                                    if (square.Row == 1)
                                    {
                                        int doubleForwardIndex = square.Index + 16;
                                        if (Squares[doubleForwardIndex].Piece == null)
                                        {
                                            result.Add(Squares[doubleForwardIndex]);
                                        }
                                    }
                                }
                                captureLeftIndex = square.Index + 7;
                                if (square.Column > 0 && captureLeftIndex < 64 && Squares[captureLeftIndex].Piece != null && Squares[captureLeftIndex].Piece!.Color == PieceColor.Black)
                                {
                                    result.Add(Squares[captureLeftIndex]);
                                }
                                captureRightIndex = square.Index + 9;
                                if (square.Column < 7 && captureRightIndex < 64 && Squares[captureRightIndex].Piece != null && Squares[captureRightIndex].Piece!.Color == PieceColor.Black)
                                {
                                    result.Add(Squares[captureRightIndex]);
                                }
                                break;
                            case PieceColor.Black:
                                forwardIndex = square.Index - 8;
                                if (forwardIndex >= 0 && Squares[forwardIndex].Piece == null)
                                {
                                    result.Add(Squares[forwardIndex]);
                                    if (square.Row == 6)
                                    {
                                        int doubleForwardIndex = square.Index - 16;
                                        if (Squares[doubleForwardIndex].Piece == null)
                                        {
                                            result.Add(Squares[doubleForwardIndex]);
                                        }
                                    }
                                }
                                captureLeftIndex = square.Index - 9;
                                if (square.Column > 0 && captureLeftIndex >= 0 && Squares[captureLeftIndex].Piece != null && Squares[captureLeftIndex].Piece!.Color == PieceColor.White)
                                {
                                    result.Add(Squares[captureLeftIndex]);
                                }
                                captureRightIndex = square.Index - 7;
                                if (square.Column < 7 && captureRightIndex >= 0 && Squares[captureRightIndex].Piece != null && Squares[captureRightIndex].Piece!.Color == PieceColor.White)
                                {
                                    result.Add(Squares[captureRightIndex]);
                                }
                                break;
                        }
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// Déplacement d'une pièce d'une case à une autre
        /// </summary>
        public bool Move(Square from, Square to)
        {
            bool result = false;
            if (from != null && to != null)
            {
                var moves = AuthorizedMoves(from);
                if (moves.Any(_ => _.Index == to.Index))
                {
                    Moves.Add(from, to);

                    PositionDbo.Instance.Save(new PositionEntity()
                    {
                        ID = Position,
                        BAD = Bad,
                        BEST = Best,
                        DATEMAJ = DateTime.Now,
                        WARN = Warn,
                    });

                    if (to.Piece != null)
                    {
                        Takens.Add(to.Piece);
                    }
                    to.Piece = from.Piece;
                    from.Piece = null;
                    Playing = Playing == PieceColor.White ? PieceColor.Black : PieceColor.White;

                    foreach (var item in Squares)
                    {
                        item.Unselect();
                    }
                    SetInfo();

                    result = true;
                }
            }
            return result;
        }

        private void SetInfo()
        {
            var positions = PositionDbo.Instance.GetByPosition(Position);
            if (positions.Any())
            {
                foreach (var position in positions) // only one
                {
                    if (!string.IsNullOrEmpty(position.BAD))
                    {
                        foreach (var data in position.BAD.Split(' '))
                        {
                            if (int.TryParse(data, out var index) && index >= 0 && index < 64)
                            {
                                Squares[index].IsBadMove = true;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(position.WARN))
                    {
                        foreach (var data in position.WARN.Split(' '))
                        {
                            if (int.TryParse(data, out var index) && index >= 0 && index < 64)
                            {
                                Squares[index].IsWarning = true;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(position.BEST))
                    {
                        foreach (var data in position.BEST.Split(' '))
                        {
                            if (int.TryParse(data, out var index) && index >= 0 && index < 64)
                            {
                                Squares[index].IsBestMove = true;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gestion du double clic sur une case : sélection de la pièce, affichage des déplacements possibles, indication du meilleur coup, etc.
        /// </summary>
        /// <param name="square"></param>
        public void DoubleClick(Square square)
        {
            var selected = Squares.FirstOrDefault(_ => _.IsSelected);
            if (selected == null) // Aucune piece sélectionnée
            {
                if (square.Piece != null && Playing == square.Piece.Color)
                {
                    var moves = AuthorizedMoves(square);
                    foreach (var item in Squares)
                    {
                        item.Unselect();
                        item.IsAuthorizedMove = moves.Any(_ => _.Index == item.Index);
                        item.IsBestMove = square.Index == item.Index && square.Piece != null && square.Piece.Color == Playing;
                    }
                    square.Select();
                }
            }
            else
            {
                var moves = AuthorizedMoves(selected);
                var bestmove = moves.FirstOrDefault(_ => _.IsBestMove);
                if (bestmove == null)
                {
                    moves.ForEach(_ => _.IsBestMove = _.Index == square.Index);
                }
                else if (bestmove.Index == square.Index)
                {
                    bestmove.IsBestMove = false;
                }
                else if (moves.Any(_ => _.Index == square.Index))
                {
                    square.IsBadMove = !square.IsBadMove;
                }
                else
                {
                    square.IsWarning = !square.IsWarning;
                }

            }
        }

        private string Position
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var square in Squares.ToList().Where(_ => _.Piece != null && _.Piece.IsWhite).OrderBy(_ => _.Piece!.Type))
                {
                    System.Diagnostics.Debug.Assert(square.Piece != null);
                    sb.Append($"{square.Piece.ToLetterPiece()}{square.Position}");
                }
                foreach (var square in Squares.ToList().Where(_ => _.Piece != null && _.Piece.IsBlack).OrderBy(_ => _.Piece!.Type))
                {
                    System.Diagnostics.Debug.Assert(square.Piece != null);
                    sb.Append($"{square.Piece.ToLetterPiece()}{square.Position}");
                }
                return sb.ToString();
            }
        }

        private string Bad
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var square in Squares.Where(_ => _.IsBadMove))
                {
                    sb.Append($"{square.Index} ");
                }
                return sb.ToString().Trim();
            }
        }

        private string Best
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var square in Squares.Where(_ => _.IsBestMove))
                {
                    sb.Append($"{square.Index} ");
                }
                return sb.ToString().Trim();

            }
        }

        private string Warn
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var square in Squares.Where(_ => _.IsWarning))
                {
                    sb.Append($"{square.Index} ");
                }
                return sb.ToString().Trim();
            }
        }
    }
}
