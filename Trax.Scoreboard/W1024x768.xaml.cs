using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Trax.Scoreboard
{
    public partial class W1024x768 : Window
    {
        public IScoreboardData _scoreboardData;

        public W1024x768()
        {
            InitializeComponent();

            _scoreboardData = ScoreboardContainer.Container.GetInstance<IScoreboardData>();
            _scoreboardData.PropertyChanged += ScoreboardDataOnPropertyChanged;
            DataContext = _scoreboardData;

            _scoreboardData.Title.PropertyChanged += delegate(object sender, PropertyChangedEventArgs args)
            {
                if (args.PropertyName.Equals("Text"))
                {
                    if (string.IsNullOrEmpty(_scoreboardData.Title.Text))
                        WindowTitle.Visibility = Visibility.Collapsed;
                    else
                        WindowTitle.Visibility = Visibility.Visible;
                    this.InvalidateVisual();
                }
            };
        }

        private void ScoreboardDataOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FreeText")
            {
                DisplayImage.Visibility = Visibility.Hidden;
                FreeText.Content = _scoreboardData.FreeText == null ? string.Empty : _scoreboardData.FreeText;
                FreeText.Visibility = Visibility.Visible;
            }
            if (e.PropertyName == "DisplayImage")
            {
                FreeText.Visibility = Visibility.Hidden;
                DisplayImage.Source = _scoreboardData.DisplayImage;
                DisplayImage.Visibility = Visibility.Visible;
            }
            if (e.PropertyName.Equals("BackgroundImage"))
            {
                if (_scoreboardData.BackgroundImage != null)
                    this.Background = new ImageBrush(_scoreboardData.BackgroundImage);
                else
                    this.Background = _scoreboardData.BackgroundColor;
            }
            if (e.PropertyName.Equals("BackgroundColor"))
            {
                this.Background = _scoreboardData.BackgroundColor;
            }
            if (e.PropertyName.Equals("WindowResized"))
            {
                this.Width = _scoreboardData.WindowWidth;
                this.Height = _scoreboardData.WindowHeight;
                InvalidateVisual();
            }
        }

        private void DWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
