using Cleared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleared.ViewModels
{
    public class SquareDataViewModel
    {
        TouchState touchState;

        public bool Fixed { get; set; }
        public string Text { get; set; }
        public int PaletteIndex { get; set; }

        public TouchState TouchState
        {
            get { return touchState; }
            set
            {
                if (touchState == value) return;
                touchState = value;
                View.Update(false);
            }
        }

        public GameDefinition.GameLine GameLine { get; set; }

        public bool MarkerVisible { get; set; }

        public IUpdateable View { get; set; }

        public override string ToString()
        {
            return $"{Text}-{TouchState}:{GameLine}";
        }
    }

}
