

namespace TriviaPLay;

public partial class StartMenu : ContentPage
{


    string[] diffuclty = { "Mix", "Easy", "Medium", "Hard" };

    //to recognize the key is loaded properly within the api
    Dictionary<short, string> responses = new Dictionary<short, string>()
    {
        {0, "Success" },
        {1, "No Results" },
        {2, "Invalid Paremeter" },
        {3, "Token not Found" },
        {4, "Token session empty" }


    };

    //get a list of valid options for the category of API
    Dictionary<short, string> categoryType = new Dictionary<short, string>()
	{
		{0, "Mixed" },
		{9, "General Knowledge"},
		{10, "Books"},
		{11, "Film" },
		{12, "Music" },
		{13, "Musicals & Theatres"},
		{14, "Televsion" },
		{15, "Video Games" },
		{16, "Board Games" },
		{17, "Nature" },
		{18, "Computers" },
		{19, "Mathematics" },
		{20, "Mythology" },
		{21, "Sports" },
		{22, "Geography" },
		{23, "History" },
		{24, "Politics" },
		{25, "Art" },
		{26, "Celebrities" },
		{27, "Animals" },
		{28, "Vehicles" },
		{29, "Comics" },
		{30, "Gadgets" },
		{31, "Anime & Manga" },
		{32, "Cartoons & Animations" }

	};

	public StartMenu()
	{
		InitializeComponent();

		//picker setups
		selCategory.ItemsSource = categoryType.Keys.ToList();
		selDifficulty.ItemsSource = diffuclty;

		selCategory.SelectedIndex = 0;
		selDifficulty.SelectedIndex = 0;

	}

    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}