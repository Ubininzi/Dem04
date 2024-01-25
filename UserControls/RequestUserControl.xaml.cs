using dem04.EFModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace dem04.UserControls
{
	public partial class RequestUserControl : UserControl
	{
		public Request thisRequest;
		private MainWindow EvokingWindow;
		private bool isEditing = false;
		private Dem04DbContext db = new Dem04DbContext(); 
		private List<SolidColorBrush> gamma = new List<SolidColorBrush>() { 
			new SolidColorBrush(Colors.LightGreen),
			new SolidColorBrush(Colors.LightSeaGreen)
		};
		public RequestUserControl(Request request, MainWindow EvokingWindow)
		{
			InitializeComponent();
			thisRequest = request;
			this.EvokingWindow = EvokingWindow;
			FillUserControl(thisRequest);
		}
		private void FillUserControl(Request request) {
            MainBorder.Background = gamma[thisRequest.RequestPriority];
			//RequestStateTextBox.Text = thisRequest.RequestState.ToString();
			ClientTextBox.Text = db.Clients.First(c => c.Id == thisRequest.Client).Surname;
			WorkerTextBox.Text = db.Workers.First(w => w.Id == thisRequest.Worker).Surname;
			DateOfAcceptTextBox.Text = thisRequest.DateOfAccept.ToShortDateString();
			if (thisRequest.DateOfWorkStart != null)
				DateOfWorkStartTextBox.Text = thisRequest.DateOfWorkStart.Value.ToShortDateString();
			EquipmentTextBox.Text = string.Join(',', thisRequest.Equipment);
			DescriptionTextBox.Text = thisRequest.RequestDescription;
		}
		private void ChangeEditingMode() {
			ApproveChanges.Visibility = isEditing ? Visibility.Visible : Visibility.Hidden;		//change button visibility
			DiscardChanges.Visibility = isEditing ? Visibility.Visible : Visibility.Hidden;
			EditRequestButton.Visibility = isEditing ? Visibility.Hidden : Visibility.Visible;
			DeleteRequestButton.Visibility = isEditing ? Visibility.Hidden : Visibility.Visible;

			//RequestStateTextBox.IsEnabled = isEditing;		//добавить проверку - если админ меняет все поля, если работник - только статус и описание
            WorkerTextBox.IsEnabled = isEditing;			//change texbox editing mode
            DescriptionTextBox.IsEnabled = isEditing;

		}
		private void EditRequestButton_Click(object sender, RoutedEventArgs e)
		{
			isEditing = true;
			ChangeEditingMode();
		}

		private void DeleteRequestButton_Click(object sender, RoutedEventArgs e)
		{
            EvokingWindow.MainGrid.Children.Remove(this);
            EvokingWindow.db.Requests.Remove(thisRequest);
            EvokingWindow.db.SaveChanges();
            EvokingWindow.RefreshData();
        }

		private void ApproveChanges_Click(object sender, RoutedEventArgs e)
		{
            isEditing = false;
			ChangeEditingMode();
		}

		private void DiscardChanges_Click(object sender, RoutedEventArgs e)
		{
			FillUserControl(thisRequest);
			isEditing = false;
			ChangeEditingMode();
		}
	}
}
