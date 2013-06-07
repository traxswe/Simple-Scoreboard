using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Trax.Scoreboard
{
    public interface IBaseWindow
    {
        int WindowWidth { get; }
        int WindowHeight { get; }
        int WindowResized { get; }
        BitmapImage BackgroundImage { get; set; }
        Brush BackgroundColor { get; set; }

        void SetWindowSize(int width, int height);
    }
}
