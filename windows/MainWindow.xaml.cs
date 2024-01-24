using dem04.EFModel;
using dem04.UserControls;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace dem04
{
	public partial class MainWindow : Window
	{
        public Worker thisWorker;
        public Dem04DbContext db = new Dem04DbContext();
        public MainWindow(Worker worker)
        {
            InitializeComponent();
			FillMainGrid();
            this.thisWorker = worker;
            LoginLabel.Content = $"Вы вошли как: {db.Roles.First(r => r.Id == thisWorker.Role).Rolename} {thisWorker.Name} {thisWorker.Surname}";
        }
        public void FillMainGrid() { 
			int i = 0, j = 0;
			foreach (Request request in db.Requests.Include(r => r.WorkerNavigation).Include(r => r.ClientNavigation))
			{
				RequestUserControl newRequest = new RequestUserControl(request,this);
				Grid.SetColumn(newRequest, i);
				Grid.SetRow(newRequest, j);
				MainGrid.Children.Add(newRequest);
				i++;
				if (i >= 4)
				{
					MainGrid.RowDefinitions.Add(new RowDefinition());
					i = 0; j++;
                }
			}
		}
        public void RefreshData() {
            MainGrid.Children.Clear();
            FillMainGrid();
        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void CreateRequestButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
