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
    public partial class YellowPressAdd : Window, INotifyPropertyChanged
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


        public YellowPressAdd(Singer singer)
        {
            InitializeComponent();
            yellowPress = new();

            yellowPress.IdSinger = singer.Id;
            Ids.Text = singer.Id.ToString();
        }

        private void SaveAnBack(object sender, RoutedEventArgs e)
        {
            using (var db = new _1135New2024Context())
            {
                yellowPress.TitleArticle = Zag.Text;
                yellowPress.Description = Des.Text;
                db.Add(yellowPress);
                db.SaveChanges();
                Close();
            }
        }

        void Signal([CallerMemberName] string prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
