using dem04.EFModel;
using dem04.UserControls;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace dem04
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			MainGrid = FillMainGrid();
		}
		public Grid FillMainGrid() { 
			Grid grid = new Grid();
			Dem04DbContext db = new Dem04DbContext();
			int i = 0, j = 0;
			foreach (Request request in db.Requests.Include(r => r.Worker).Include(r => r.Client).Include(r => r.Equipment))
			{
				RequestUserControl newRequest = new RequestUserControl(request);

				Grid.SetColumn(newRequest, i);
				Grid.SetRow(newRequest, j);
				grid.Children.Add(newRequest);

				i++;
				if (i >= 5)
				{
					RowDefinition row = new RowDefinition();
					grid.RowDefinitions.Add(row);
					i = 0; j++;
				}
			}
			return grid;
		}
	}
}
