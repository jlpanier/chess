using Chess.ViewModels;
using static Business.Piece;

namespace Chess.Views;

/// <summary>
/// Choix d'une promotion d'un pion
/// </summary>
public partial class PromotionPopup : CommunityToolkit.Maui.Views.Popup
{
    /// <summary>
    /// Case de départ du pion à promouvoir
    /// </summary>
    private readonly SquareViewModel From;

    /// <summary>
    /// Case de fin du pion à promouvoir
    /// </summary>
    readonly SquareViewModel To;

    /// <summary>
    /// callback fonction à exécuter après le choix de la promotion
    /// </summary>
    Action<SquareViewModel, SquareViewModel, PieceType> _action { get; set; }

    public PromotionPopup(SquareViewModel from, SquareViewModel to, Action<SquareViewModel, SquareViewModel, PieceType> action)
    {
        From = from;
        To = to;
        _action = action;
        InitializeComponent();
        BindingContext = new PromotionPopupViewModel(ClosePopup);
    }

    /// <summary>
    /// callback fonction à exécuter après le choix de la promotion
    /// </summary>
    public void ClosePopup(PieceType result)
    {
        _action.Invoke(From, To, result);
        CloseAsync(); // ferme la popup et renvoie la valeur
    }
}