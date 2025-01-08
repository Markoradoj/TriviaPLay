namespace TriviaPLay;

public partial class MenuOption : ContentPage
{
	public MenuOption()
	{
		InitializeComponent();
	}

    //Sets the color buttons globally
	private void SetBlueButtonClicked(object sender , EventArgs e)
	{
		Application.Current.Resources["ButtonBackgroundColor"] = Colors.Blue;
	}

    private void SetRedButtonClicked(object sender, EventArgs e)
    {
        Application.Current.Resources["ButtonBackgroundColor"] = Colors.Red;
    }

    private void SetGreenButtonClicked(object sender, EventArgs e)
    {
        Application.Current.Resources["ButtonBackgroundColor"] = Colors.Green;
    }

    //goes back to menu
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}