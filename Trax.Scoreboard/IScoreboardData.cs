using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Trax.Scoreboard
{
    public interface IScoreboardData : IBaseWindow, INotifyPropertyChanged
    {
        TextElement Title { get; set; }
        TextElement TeamOneName { get; set; }
        TextElement TeamTwoName { get; set; }
        int TeamOneScore { get; set; }
        int TeamTwoScore { get; set; }
        Visibility IsTeamOneLogoSet { get; }
        Visibility IsTeamTwoLogoSet { get; }
        ImageSource TeamOneLogo { get; set; }
        ImageSource TeamTwoLogo { get; set; }

        Element ScoreboardBoxFont { get; set; }
        Element ClockFont { get; set; }
        Brush ScoreboardBoxBorderColor { get; set; }
        Brush ScoreboardBoxColor { get; set; }
        
        string Clock { get; }
        int ClockSecondsLeft { get; set; }

        string FreeText { get; set; }
        ImageSource DisplayImage { get; set; }
    }
}
