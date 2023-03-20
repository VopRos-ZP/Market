using Xamarin.Forms;

namespace Market
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new OfferContent((Offer)e.SelectedItem));
        }
    }
}

