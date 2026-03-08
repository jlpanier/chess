using System.Windows.Input;
using static Business.Piece;

/// <summary>
/// Promotion d'un pion : choix de la pièce à promouvoir
/// </summary>
public class PromotionPopupViewModel
{
    /// <summary>
    /// Commande de sélection de la pièce à promouvoir
    /// </summary>
    public ICommand SelectCommand { get; }

    /// <summary>
    ///callback fonction à exécuter après le choix de la promotion
    /// </summary>
    private readonly Action<PieceType> _close;

    /// <summary>
    ///callback fonction à exécuter après le choix de la promotion
    /// </summary>
    public PromotionPopupViewModel(Action<PieceType> close)
    {
        _close = close;
        SelectCommand = new Command<string>(OnPromotionSelect);
    }

    /// <summary>
    /// Sélection de la pièce : valeur = CommandParameter 
    /// </summary>
    private void OnPromotionSelect(string piece)
    {
        switch (piece)
        {
            case "Queen":
                _close.Invoke(PieceType.Queen);
                break;
            case "Rook":
                _close.Invoke(PieceType.Rook);
                break;
            case "Bishop":
                _close.Invoke(PieceType.Bishop);
                break;
            case "Knight":
                _close.Invoke(PieceType.Knight);
                break;
        }
    }
}