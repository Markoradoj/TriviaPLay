
using Newtonsoft.Json;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Nodes;

namespace TriviaPLay;

public partial class StartMenu : ContentPage
{
	private List<Question> _questions;
	private Dictionary<string, string> _categories;


    public StartMenu()
	{
		InitializeComponent();

	}

	private async void LoadCatagory()
	{
		try
		{
			using var client = new HttpClient();
			string response = await client.GetStringAsync("https://opentdb.com/api_category.php");
			var categoryResponse = JsonConvert.DeserializeObject<CategoryResponse>(response);

			_categories = categoryResponse.TriviaCategories.ToDictionary(c => c.Name, c => c.Id.ToString());

			foreach (var category in _categories.Keys)
			{
				selCategory.Items.Add(category);
			}
		}

		catch (Exception ex)
		{
			await DisplayAlert("Notice", $"Failed to load catagories: {ex.Message}", "OK");
		}
	}
    private void Button_Clicked(object sender, EventArgs e)
    {

    }


}