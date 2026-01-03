using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Converter
{
    public class SquareGrid : Grid
    {
        protected override Size MeasureOverride(double widthConstraint, double heightConstraint)
        {
            double size = Math.Min(widthConstraint, heightConstraint);
            return base.MeasureOverride(size, size);
        }

        protected override Size ArrangeOverride(Rect bounds)
        {
            double size = Math.Min(bounds.Width, bounds.Height);
            var square = new Rect(bounds.X, bounds.Y, size, size);
            return base.ArrangeOverride(square);
        }
    }
}
