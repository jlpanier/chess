using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.ViewModels
{
    public partial class BoardSquareViewModel : ObservableObject
    {
        [ObservableProperty] int index;
        [ObservableProperty] string? pieceSymbol; // or image key
        [ObservableProperty] bool isSelected;
        [ObservableProperty] bool isHighlighted;
    }

}
