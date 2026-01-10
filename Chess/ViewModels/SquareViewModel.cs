using Business;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chess.ViewModels
{
    public partial class SquareViewModel : INotifyPropertyChanged
    {
        #region PropertyChange

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Properties

        private Board _board;

        public Square Square { get; private set; }

        public int Index => Square.Index;

        public bool HasPiece => Square.Piece != null;

        public Color PieceColor
        {
            get
            {
                if (Square.Piece != null)
                {
                    return Square.Piece.IsWhite ? Colors.White : Colors.Black;
                }
                return Colors.Black;
            }
        }

        public string PieceSymbol => PieceSymbols.ToSymbol(Square?.Piece);

        public bool IsSelected => Square.IsSelected;

        #endregion

        public SquareViewModel(Board board, Square square)
        {
            Square = square;
            _board = board;
        }

        public void Selected()
        {
            Square.Selected();
            NotifyPropertyChanged(nameof(IsSelected));
        }

        public void Unselected()
        {
            Square.Unselected();
            NotifyPropertyChanged(nameof(IsSelected));
        }
    }
}
