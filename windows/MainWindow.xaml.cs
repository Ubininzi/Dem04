using dem04.EFModel;
using dem04.UserControls;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace dem04
{
	public partial class MainWindow : Window
	{
        public Worker worker;
        public MainWindow(Worker worker)
        {
            InitializeComponent();
			FillMainGrid();
            this.worker = worker;
        }
        public void FillMainGrid() { 
			MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
			MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
			MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
			MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            MainGrid.RowDefinitions.Add(new RowDefinition());
            MainGrid.RowDefinitions.Add(new RowDefinition());
            Dem04DbContext db = new Dem04DbContext();
			int i = 0, j = 0;
			foreach (Request request in db.Requests.Include(r => r.WorkerNavigation).Include(r => r.ClientNavigation))
			{
				RequestUserControl newRequest = new RequestUserControl(request);
				Grid.SetColumn(newRequest, i);
				Grid.SetRow(newRequest, j);
				MainGrid.Children.Add(newRequest);

				i++; j = i / 4;
				if (i >= 5)
				{
					MainGrid.RowDefinitions.Add(new RowDefinition());
					i = 0;
				}
			}
		}
	}
}
