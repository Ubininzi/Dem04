using dem04.EFModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace dem04.UserControls
{
	public partial class RequestUserControl : UserControl
	{
		public Request thisRequest;
		private MainWindow EvokingWindow;
		private bool isEditing = false;
		private Dem04DbContext db = new Dem04DbContext(); 
		public RequestUserControl(Request request, MainWindow EvokingWindow)
		{
			InitializeComponent();
			thisRequest = request;
			this.EvokingWindow = EvokingWindow;
			FillUserControl(thisRequest);
		}
		private void FillUserControl(Request request) {
			RequestStateTextBox.Text = thisRequest.RequestState.ToString();
			ClientTextBox.Text = db.Clients.First(c => c.Id == thisRequest.Client).Surname;
			WorkerTextBox.Text = db.Workers.First(w => w.Id == thisRequest.Worker).Surname;
			DateOfAcceptTextBox.Text = thisRequest.DateOfAccept.ToShortDateString();
			if (thisRequest.DateOfWorkStart != null)
				DateOfWorkStartTextBox.Text = thisRequest.DateOfWorkStart.Value.ToShortDateString();
			EquipmentTextBox.Text = string.Join(',', thisRequest.Equipment);
			DescriptionTextBox.Text = thisRequest.RequestDescription;
		}
		private void ChangeButtonsVisibility() {
			ApproveChanges.Visibility = isEditing ? Visibility.Visible : Visibility.Hidden;
			DiscardChanges.Visibility = isEditing ? Visibility.Visible : Visibility.Hidden;
			EditRequestButton.Visibility = isEditing ? Visibility.Hidden : Visibility.Visible;
			DeleteRequestButton.Visibility = isEditing ? Visibility.Hidden : Visibility.Visible;
		}
		private void EditRequestButton_Click(object sender, RoutedEventArgs e)
		{
			isEditing = true;
			ChangeButtonsVisibility();
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
			ChangeButtonsVisibility();
		}

		private void DiscardChanges_Click(object sender, RoutedEventArgs e)
		{
			FillUserControl(thisRequest);
			isEditing = false;
			ChangeButtonsVisibility();
		}
	}
}
