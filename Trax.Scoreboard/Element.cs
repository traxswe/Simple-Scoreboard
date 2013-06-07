using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Trax.Scoreboard.Annotations;

namespace Trax.Scoreboard
{
    public class Element : INotifyPropertyChanged
    {
        private int _fontSize;
        private Brush _color;
        private string _fontName;
        private bool _bold;
        private bool _italic;

        public bool Bold
        {
            get
            {
                return _bold;
            }
            set
            {
                _bold = value;
                OnPropertyChanged();
            }
        }
        public bool Italic
        {
            get
            {
                return _italic;
            }
            set
            {
                _italic = value;
                OnPropertyChanged();
            }
        }
        public string FontName
        {
            get
            {
                return _fontName;
            }
            set
            {
                _fontName = value;
                OnPropertyChanged();
            }
        }
        public int FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                _fontSize = value;
                OnPropertyChanged();
            }
        }
        public Brush Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
