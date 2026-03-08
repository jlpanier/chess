using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Rendered
{
    public class SquareContentView : ContentView
    {
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width <= 0 || height <= 0)
                return;

            double size = Math.Min(width, height);

            // On force le contrôle à être carré
            WidthRequest = size;
            HeightRequest = size;
        }
    }
}
