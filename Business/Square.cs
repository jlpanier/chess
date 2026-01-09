using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Square
    {
        public readonly int Index;

        public bool IsSelected { get; private set; }

        public Piece? Piece { get; set; }

        public Square(int index)
        {
            Index = index;
            Piece = null;
        }

        public Square(int index, Piece piece)
        {
            Index = index;
            Piece = piece;
        }

        public void Selected()
        {
            IsSelected = true;
        }

        public void Unselected()
        {
            IsSelected = false;
        }
    }
}
