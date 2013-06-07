using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Trax.Scoreboard.Annotations;

namespace Trax.Scoreboard
{
    class ScoreboardData : IScoreboardData
    {
        public TextElement Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        public TextElement TeamOneName
        {
            get { return _teamOneName; }
            set { _teamOneName = value; OnPropertyChanged(); }
        }

        public TextElement TeamTwoName
        {
            get { return _teamTwoName; }
            set { _teamTwoName = value; OnPropertyChanged(); }
        }

        public int TeamOneScore
        {
            get { return _teamOneScore; }
            set { _teamOneScore = value; OnPropertyChanged(); }
        }

        public int TeamTwoScore
        {
            get { return _teamTwoScore; }
            set { _teamTwoScore = value; OnPropertyChanged(); }
        }

        public Visibility IsTeamOneLogoSet
        {
            get { return _isTeamOneLogoSet; }
        }

        public Visibility IsTeamTwoLogoSet
        {
            get { return _isTeamTwoLogoSet; }
        }

        public ImageSource TeamOneLogo
        {
            get { return _teamOneLogo; }
            set
            {
                _teamOneLogo = value;
                if (value == null)
                    _isTeamOneLogoSet = Visibility.Hidden;

                OnPropertyChanged("IsTeamOneLogoSet");
                OnPropertyChanged();
            }
        }

        public ImageSource TeamTwoLogo
        {
            get { return _teamTwoLogo; }
            set
            {
                _teamTwoLogo = value;
                if (value == null)
                    _isTeamTwoLogoSet = Visibility.Hidden;

                OnPropertyChanged("IsTeamTwoLogoSet");
                OnPropertyChanged();
            }
        }

        public Element ScoreboardBoxFont
        {
            get { return _scoreboardBoxFont; }
            set { _scoreboardBoxFont = value; OnPropertyChanged(); }
        }

        public Element ClockFont
        {
            get { return _clockFont; }
            set { _clockFont = value; OnPropertyChanged(); }
        }

        public Brush ScoreboardBoxBorderColor
        {
            get { return _scoreboardBoxBorderColor; }
            set { _scoreboardBoxBorderColor = value; OnPropertyChanged(); }
        }

        public Brush ScoreboardBoxColor
        {
            get { return _scoreboardBoxColor; }
            set { _scoreboardBoxColor = value; OnPropertyChanged(); }
        }

        public int WindowWidth
        {
            get { return _windowWidth; }
        }

        public int WindowHeight
        {
            get { return _windowHeight; }
        }

        public int WindowResized
        {
            get { return _windowResized; }
        }

        public BitmapImage BackgroundImage
        {
            get { return _backgroundImage; }
            set { _backgroundImage = value; OnPropertyChanged(); }
        }

        public Brush BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; OnPropertyChanged(); }
        }

        public void SetWindowSize(int width, int height)
        {
            _windowWidth = width;
            _windowHeight = height;
            OnPropertyChanged("WindowResized");
        }

        public string Clock
        {
            get { return GenerateCorrectClockTime(); }
        }

        public int ClockSecondsLeft
        {
            get { return _clockSecondsLeft; }
            set
            {
                _clockSecondsLeft = value;
                if (_clockSecondsLeft < 0)
                    _clockSecondsLeft = 0;
                if (_clockSecondsLeft > 5999)
                    _clockSecondsLeft = 5999;
                OnPropertyChanged("Clock");
            }
        }

        public string FreeText
        {
            get { return _freeText; }
            set { _freeText = value; OnPropertyChanged(); }
        }

        public ImageSource DisplayImage
        {
            get { return _displayImage; }
            set { _displayImage = value; OnPropertyChanged(); }
        }


        private TextElement _title;
        private TextElement _teamOneName;
        private TextElement _teamTwoName;
        private int _teamOneScore;
        private int _teamTwoScore;
        private Visibility _isTeamOneLogoSet;
        private Visibility _isTeamTwoLogoSet;
        private ImageSource _teamOneLogo;
        private ImageSource _teamTwoLogo;
        private string _freeText;
        private ImageSource _displayImage;
        private BitmapImage _backgroundImage;
        private int _clockSecondsLeft;
        private int _windowWidth;
        private int _windowHeight;
        private int _windowResized;
        private Brush _backgroundColor;
        private Brush _scoreboardBoxColor;
        private Element _scoreboardBoxFont;
        private Brush _scoreboardBoxBorderColor;
        private Element _clockFont;

        public ScoreboardData()
        {
            _isTeamOneLogoSet = Visibility.Hidden;
            _isTeamTwoLogoSet = Visibility.Hidden;

            _teamOneScore = 0;
            _teamTwoScore = 0;

            Title = new TextElement();
            TeamOneName = new TextElement();
            TeamTwoName = new TextElement();
            ScoreboardBoxFont = new Element();
            ClockFont = new Element();
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private string GenerateCorrectClockTime()
        {
            if (_clockSecondsLeft < 1)
                return "00:00";

            int secondsLeft = _clockSecondsLeft;
            int minutesLeft = 0;
            while (secondsLeft > 59)
            {
                minutesLeft++;
                secondsLeft -= 60;
            }

            string minuteString = "00";
            string secondString = "00";
            if (minutesLeft > 9)
                minuteString = minutesLeft.ToString();
            else if (minutesLeft > 0 && minutesLeft < 10)
                minuteString = string.Format("0{0}", minutesLeft);

            if (secondsLeft > 9)
                secondString = secondsLeft.ToString();
            else if (secondsLeft > 0 && secondsLeft < 10)
                secondString = string.Format("0{0}", secondsLeft);

            return string.Format("{0}:{1}", minuteString, secondString);
        }
    }
}
