using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MicrosoftNews.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsDetailsPage : ContentPage
    {
        public NewsDetailsPage()
        {
            InitializeComponent();
            NewsDescriptionWebView.Navigating += (sender, e) =>
            {
                e.Cancel = true;
                Device.OpenUri(new System.Uri(e.Url));
            };
        }
    }
}
