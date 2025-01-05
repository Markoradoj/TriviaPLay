namespace TriviaPLay
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void StartButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StartMenu());
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuOption());
        }
    }
}

