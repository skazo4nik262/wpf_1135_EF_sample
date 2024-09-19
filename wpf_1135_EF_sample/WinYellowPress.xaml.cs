using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;

namespace wpf_1135_EF_sample
{
    public partial class WinYellowPress : Window, INotifyPropertyChanged
    {
        private List<Singer> singers;

        public List<Singer> Singers
        {
            get => singers;
            set
            {
                singers = value;
                Signal();
            }
        }

        public Singer SelectedSinger { get; set; }

        public WinYellowPress()
        {
            InitializeComponent();
            DataContext = this;

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateList();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private void UpdateList()
        {
            using (var db = new _1135New2024Context())
            {
                Singers = db.Singers.
                    Include(s => s.YellowPresses).
                    ToList();
            }
        }

        private void DelYP(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Удалить?", "Предупреждение!!", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    break;

                case MessageBoxResult.No:
                    MessageBox.Show("Действие отменено!");
                    break;
            }

        }
    }
}
