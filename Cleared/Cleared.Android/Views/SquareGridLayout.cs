using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Graphics;

namespace Cleared.Droid.Views
{
    public class SquareGridLayout : GridLayout
    {
        int cellSize;

        public SquareGridLayout(Context context) : this(context, null)
        {  }

        public SquareGridLayout(Context context, IAttributeSet attrs) : this(context, attrs, 0)
        {  }

        public SquareGridLayout(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            var smallMargin = Resources.GetDimensionPixelSize(Resource.Dimension.margin_small);

            var widthMode = MeasureSpec.GetMode(widthMeasureSpec);
            var heightMode = MeasureSpec.GetMode(heightMeasureSpec);

            var widthSize = MeasureSpec.GetSize(widthMeasureSpec);
            var heightSize = MeasureSpec.GetSize(heightMeasureSpec);

            widthSize -= PaddingStart + PaddingEnd;
            heightSize -= PaddingTop + PaddingBottom;

            var isSquare = RowCount == ColumnCount;
            if (isSquare)
            {
                if (widthMode == MeasureSpecMode.Exactly && (heightMode == MeasureSpecMode.AtMost || heightMode == MeasureSpecMode.Unspecified))
                {
                    cellSize = widthSize;
                }
                else if (heightMode == MeasureSpecMode.Exactly && (widthMode == MeasureSpecMode.AtMost || widthMode == MeasureSpecMode.Unspecified))
                {
                    cellSize = heightSize;
                }
                else
                {
                    cellSize = Math.Min(widthSize, heightSize);
                }

                SetMeasuredDimension(cellSize, cellSize);
            }
            else
            {
                if ((RowCount == 0) || (ColumnCount == 0))
                    return;

                if (ColumnCount < 2)
                {
                    SetMeasuredDimension(widthSize, heightSize);
                    return;
                }

                var cellWidth = widthSize / ColumnCount;
                var cellHeight = heightSize / RowCount;
                cellSize = Math.Min(cellWidth, cellHeight);

                var cellsWidth = cellSize * ColumnCount;
                if (cellsWidth < widthSize)
                {
                    /*
                    var sidePadding = (widthSize - cellsWidth) / 2;
                    var remaining = widthSize - sidePadding;
                    if (PaddingLeft != sidePadding)     // prevent re-measure
                        SetPadding(sidePadding, PaddingTop, PaddingRight, PaddingBottom);
                        */
                }

                SetMeasuredDimension(
                    PaddingStart + PaddingEnd + (cellSize * ColumnCount),
                    PaddingTop + PaddingBottom + (cellSize * RowCount));
            }

        }
    }
}