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
        private List<YellowPress> yps;

        public List<YellowPress> Yps
        {
            get => yps;
            set
            {
                yps = value;
                Signal();
            }
        }

        public YellowPress SelectedYP { get; set; }

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

        private _1135New2024Context db = new();

        private void UpdateList()
        {
            Singers = db.Singers.
                Include(s => s.YellowPresses).
                ToList();

            Yps = db.YellowPresses.ToList();
        }

        private void DelYP(object sender, RoutedEventArgs e)
        {
            if (SelectedYP != null)
            {
                switch (MessageBox.Show("Удалить?", "Предупреждение!!", MessageBoxButton.YesNo))
                {
                    case MessageBoxResult.Yes:
                        Yps = db.YellowPresses.ToList();
                        db.YellowPresses.Remove(SelectedYP);
                        db.SaveChanges();
                        UpdateList();
                        break;

                    case MessageBoxResult.No:
                        MessageBox.Show("Действие отменено!");
                        break;
                }
            }
        }

        private void YTFull(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (SelectedYP == null)
                return;

            new WinYellowPressFull(SelectedYP).ShowDialog();
            UpdateList();
        }
    }
}
