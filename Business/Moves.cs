using System.Text;


namespace Business
{
    public class Moves
    {
        public List<Move> Items { get; private set; }
        
        public int Index { get; private set; }
        public Moves() 
        {
            Items = new List<Move>();
            Index = 0;
        }

        public void Add(Square from, Square to)
        {
            Index++;
            Items.Add(new Move(Index, from, to));
        }

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
