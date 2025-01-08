
namespace TriviaPLay
{
    public partial class MainPage : ContentPage
    {
       

      
        

        public MainPage()
        {
            InitializeComponent();
        }

        //navigates to start game
        private async void StartButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StartMenu());
        }

        //navigates to options
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuOption());
        }
    }
}

