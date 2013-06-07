using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using Trax.Control.TextElement;

namespace Trax.Scoreboard
{
    /// <summary>
    /// Interaction logic for ControlWindow.xaml
    /// </summary>
    public partial class ControlWindow : Window
    {
        private IScoreboardData _scoreboardData;
        private W1024x768 _scoreboardWindow;

        private ImageSource _teamOneLogo;
        private ImageSource _teamTwoLogo;

        private ImageSource _displayImage;

        private Timer _timer;

        public ControlWindow()
        {
            InitializeComponent();
            _scoreboardData = ScoreboardContainer.Container.GetInstance<IScoreboardData>();
            _scoreboardData.ScoreboardBoxFont.FontSize = 150;

            for (int i = 100; i >= 0; i--)
            {
                ScoreboardBoxOpacity.Items.Add(String.Format("{0}%", i));
                ScoreboardBoxBorderOpacity.Items.Add(String.Format("{0}%", i));                
            }
            ScoreboardBoxOpacity.SelectedIndex = 0;
            ScoreboardBoxBorderOpacity.SelectedIndex = 0;
            ScoreFont.SetFontSizes(20, 300);
            ScoreFont.FontSize = 150;
            ClockFont.SetFontSizes(20, 300);
            ClockFont.FontSize = 80;
            Title.SetFontSizes(20, 300);
            Title.FontSize = 100;

            Title.OnSetButtonClicked += Title_OnSetButtonClicked;
            ScoreFont.OnSetButtonClicked += ScoreFont_OnSetButtonClicked;
            ClockFont.OnSetButtonClicked += ClockFont_OnSetButtonClicked;
            TeamOneName.OnSetButtonClicked += TeamOneName_OnSetButtonClicked;
            TeamTwoName.OnSetButtonClicked += TeamTwoName_OnSetButtonClicked;
        }

        void ClockFont_OnSetButtonClicked(object sender, TextElementEventArgs e)
        {
            _scoreboardData.ClockFont.Bold = e.Bold;
            _scoreboardData.ClockFont.Color = e.Color;
            _scoreboardData.ClockFont.FontName = e.FontName;
            _scoreboardData.ClockFont.FontSize = e.FontSize;
            _scoreboardData.ClockFont.Italic = e.Italic;
        }

        void TeamTwoName_OnSetButtonClicked(object sender, TextElementEventArgs e)
        {
            _scoreboardData.TeamOneName.Bold = e.Bold;
            _scoreboardData.TeamOneName.Color = e.Color;
            _scoreboardData.TeamOneName.FontName = e.FontName;
            _scoreboardData.TeamOneName.FontSize = e.FontSize;
            _scoreboardData.TeamOneName.Italic = e.Italic;
            _scoreboardData.TeamOneName.Text = e.Text;
        }

        void TeamOneName_OnSetButtonClicked(object sender, TextElementEventArgs e)
        {
            _scoreboardData.TeamTwoName.Bold = e.Bold;
            _scoreboardData.TeamTwoName.Color = e.Color;
            _scoreboardData.TeamTwoName.FontName = e.FontName;
            _scoreboardData.TeamTwoName.FontSize = e.FontSize;
            _scoreboardData.TeamTwoName.Italic = e.Italic;
            _scoreboardData.TeamTwoName.Text = e.Text;
        }

        void ScoreFont_OnSetButtonClicked(object sender, TextElementEventArgs e)
        {
            _scoreboardData.ScoreboardBoxFont.Bold = e.Bold;
            _scoreboardData.ScoreboardBoxFont.Color = e.Color;
            _scoreboardData.ScoreboardBoxFont.FontName = e.FontName;
            _scoreboardData.ScoreboardBoxFont.FontSize = e.FontSize;
            _scoreboardData.ScoreboardBoxFont.Italic = e.Italic;
        }

        private void Title_OnSetButtonClicked(object sender, TextElementEventArgs e)
        {
            _scoreboardData.Title.Bold = e.Bold;
            _scoreboardData.Title.Color = e.Color;
            _scoreboardData.Title.FontName = e.FontName;
            _scoreboardData.Title.FontSize = e.FontSize;
            _scoreboardData.Title.Italic = e.Italic;
            _scoreboardData.Title.Text = e.Text;
        }

        private void ShowUi(object sender, RoutedEventArgs e)
        {
            _scoreboardWindow = new W1024x768();
            _scoreboardWindow.Show();
        }

        private void SetWindowSize(object sender, RoutedEventArgs e)
        {
            int width, height;
            if (!Int32.TryParse(WindowWidth.Text, out width))
                width = 1024;
            if (!Int32.TryParse(WindowHeight.Text, out height))
                height = 768;

            if (width < 500 || width > 6000)
            {
                MessageBox.Show("Invalid width");
                return;
            }
            if (height < 500 || height > 2000)
            {
                MessageBox.Show("Invalid height");
                return;
            }

            _scoreboardData.SetWindowSize(width, height);
        }

        private void SetBackgroundColor(object sender, RoutedEventArgs e)
        {
            _scoreboardData.BackgroundColor = new SolidColorBrush(BackgroundColorPicker.SelectedColor);
        }

        private void AddPointToTeamOne(object sender, RoutedEventArgs e)
        {
            _scoreboardData.TeamOneScore++;
        }

        private void SubtractPointFromTeamOne(object sender, RoutedEventArgs e)
        {
            if (_scoreboardData.TeamOneScore > 0)
                _scoreboardData.TeamOneScore--;
        }

        private void AddPointToTeamTwo(object sender, RoutedEventArgs e)
        {
            _scoreboardData.TeamTwoScore++;
        }

        private void SubtractPointFromTeamTwo(object sender, RoutedEventArgs e)
        {
            if (_scoreboardData.TeamTwoScore > 0)
                _scoreboardData.TeamTwoScore--;
        }

        private void UpdateTeamOneProperties(object sender, RoutedEventArgs e)
        {
            _scoreboardData.TeamOneName.Text = TeamOneName.Text;

            if (_teamOneLogo != null)
            {
                _scoreboardData.TeamOneLogo = _teamOneLogo;
            }
            else
            {
                _scoreboardData.TeamOneLogo = null;
            }
        }

        private void UpdateTeamTwoProperties(object sender, RoutedEventArgs e)
        {
            _scoreboardData.TeamTwoName.Text = TeamTwoName.Text;

            if (_teamTwoLogo != null)
            {
                _scoreboardData.TeamTwoLogo = _teamTwoLogo;
            }
            else
            {
                _scoreboardData.TeamTwoLogo = null;
            }
        }

        private void SelectTeamOneImageFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                _teamOneLogo = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void SelectTeamTwoImageFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                _teamTwoLogo = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void ClearTeamOneImage(object sender, RoutedEventArgs e)
        {
            _teamOneLogo = null;
        }

        private void ClearTeamTwoImage(object sender, RoutedEventArgs e)
        {
            _teamTwoLogo = null;
        }

        private void UpdateTeamOneData(object sender, RoutedEventArgs e)
        {
            try
            {
                var teamOneScore = Int32.Parse(TeamOneScore.Text);
                if (teamOneScore >= 0)
                    _scoreboardData.TeamOneScore = teamOneScore;
                else if (teamOneScore < 0)
                    _scoreboardData.TeamOneScore = 0;
            }
            catch
            {
            }
        }

        private void UpdateTeamTwoData(object sender, RoutedEventArgs e)
        {
            try
            {
                var teamTwoScore = Int32.Parse(TeamTwoScore.Text);
                if (teamTwoScore >= 0)
                    _scoreboardData.TeamTwoScore = teamTwoScore;
                else if (teamTwoScore < 0)
                    _scoreboardData.TeamTwoScore = 0;
            }
            catch
            {
            }
        }

        private void SelectDisplayImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                _displayImage = new BitmapImage(new Uri(op.FileName));
                DisplayImagePreview.Source = _displayImage;
            }
        }

        private void DisplayTextSet(object sender, RoutedEventArgs e)
        {
            if (FreeText.Text == string.Empty)
                _scoreboardData.FreeText = null;
            else
                _scoreboardData.FreeText = FreeText.Text;
        }

        private void DisplayImageSet(object sender, RoutedEventArgs e)
        {
            _scoreboardData.DisplayImage = _displayImage;
        }

        private void ClearDisplayImage(object sender, RoutedEventArgs e)
        {
            _displayImage = null;
            DisplayImagePreview.Source = null;
        }

        private void ClearBackgroundImage(object sender, RoutedEventArgs e)
        {
            _scoreboardData.BackgroundImage = null;
        }

        private void SetBackgroundImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                _scoreboardData.BackgroundImage = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void SetClock(object sender, RoutedEventArgs e)
        {
            int secondsLeft;
            if (!Int32.TryParse(TimeInSeconds.Text, out secondsLeft))
                secondsLeft = 0;

            if (_timer != null) _timer.Stop();

            _timer = new Timer(1000);
            _timer.Elapsed += _timer_Elapsed;
            _scoreboardData.ClockSecondsLeft = secondsLeft;
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _scoreboardData.ClockSecondsLeft--;
            if (_scoreboardData.ClockSecondsLeft == 0)
                _timer.Stop();
        }

        private void StartClock(object sender, RoutedEventArgs e)
        {
            if (_timer == null) return;
            if (_scoreboardData.ClockSecondsLeft == 0) return;
            _timer.Start();
        }

        private void PauseClock(object sender, RoutedEventArgs e)
        {
            if (_timer == null) return;
            _timer.Stop();
        }

        private void SetScoreboardBoxColor(object sender, RoutedEventArgs e)
        {
            var color = new SolidColorBrush(ScoreboardBoxColorPicker.SelectedColor);
            color.Opacity = ConvertPercentToOpacity(ScoreboardBoxOpacity.SelectedValue.ToString());
            _scoreboardData.ScoreboardBoxColor = color;
        }

        /// <summary>
        /// Converts a string % value to double to be used for opacity
        /// </summary>
        /// <param name="valueToConvert">Expected a value like: 100% or 5%</param>
        /// <returns>1 or 0.05</returns>
        private double ConvertPercentToOpacity(string valueToConvert)
        {
            valueToConvert = valueToConvert.Replace("%", "");
            int intValue;
            if (!Int32.TryParse(valueToConvert, out intValue))
                return 1;

            if (intValue == 100)
                return 1;
            else if (intValue == 0)
                return 0;

            string stringValue = "0.";
            if (intValue < 10)
                stringValue += "0";
            stringValue += intValue.ToString();

            decimal output = Decimal.Parse(stringValue, CultureInfo.InvariantCulture);
            return (double)output;
        }

        private void SetScoreboardBoxBorderColor(object sender, RoutedEventArgs e)
        {
            var color = new SolidColorBrush(ScoreboardBoxBorderColorPicker.SelectedColor);
            color.Opacity = ConvertPercentToOpacity(ScoreboardBoxBorderOpacity.SelectedValue.ToString());
            _scoreboardData.ScoreboardBoxBorderColor = color;
        }
    }
}
