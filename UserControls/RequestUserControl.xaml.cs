using dem04.EFModel;
using System.Linq;
using System.Windows.Controls;

namespace dem04.UserControls
{
    public partial class RequestUserControl : UserControl
    {
        public Request thisRequest;
        public RequestUserControl(Request request)
        {
            InitializeComponent();
            thisRequest = request;
            RequestStateLabel.Content = thisRequest.RequestState;
            ClientLabel.Content = thisRequest.Client;
            WorkerLabel.Content = thisRequest.Worker;
            DateOfAcceptLabel.Content = thisRequest.DateOfAccept;
            DateOfWorkStartLabel.Content = thisRequest.DateOfWorkStart;
            EquipmentLabel.Content = string.Join(',',thisRequest.Equipment);
            DescriptionLabel.Content = thisRequest.RequestDescription;

        }
    }
}
