using dem04.EFModel;
using System.Windows;

namespace dem04
{
    public partial class MainWindow : Window
    {
        public Worker worker;
        public MainWindow(Worker worker)
        {
            InitializeComponent();
            this.worker = worker;
        }
    }
}
