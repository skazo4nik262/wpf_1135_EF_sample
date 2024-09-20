using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace wpf_1135_EF_sample
{
    public partial class WinYellowPressFull : Window, INotifyPropertyChanged
    {
        public YellowPress YP
        {
            get => yellowPress;
            set
            {
                yellowPress = value;
                Signal();
            }
        }
        public YellowPress yellowPress;

        public WinYellowPressFull(YellowPress yp)
        {
            InitializeComponent();
            YP = yp;
            DataContext = this;
        }

        void Signal([CallerMemberName] string prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}