using System.Text;


namespace Business
{
    /// <summary>
    /// Gestion des déplacements des pièces
    /// </summary>
    public class Moves
    {
        /// <summary>
        /// Vrai, si un déplacement a été effectué  
        /// </summary>
        public bool Any() => Items.Any();

        /// <summary>
        /// Liste des déplacements des pièces
        /// </summary>
        public List<Move> Items { get; private set; }

        /// <summary>
        /// Référence du mouvement
        /// </summary>
        private int Index;

        public Moves() 
        {
            Items = new List<Move>();
            Index = 0;
        }

        /// <summary>
        /// Nouveau mouvement
        /// </summary>
        public Move Add(Square from, Square to)
        {
            Index++;
            var move = new Move(Index, from, to);
            Items.Add(move);
            return move;
        }

        /// <summary>
        /// Dernier move
        /// </summary>
        public Move? Last()
        {
            Move? result = null;
            if (Items.Any())
            {
                int number = Items.Max(_ => _.Number);
                result = Items.FirstOrDefault(_=>_.Number == number);
            }
            return result;
        }

        /// <summary>
        /// Clef identifiant ce mouvement
        /// </summary>
        public string Key
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var item in Items.OrderBy(_ => _.Number))
                {
                    sb.Append(item.ToString());
                }
                return sb.ToString();
            }
        }
    }
}
