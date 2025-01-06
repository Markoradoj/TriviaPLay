
using Newtonsoft.Json;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Nodes;

namespace TriviaPLay;

public partial class StartMenu : ContentPage
{
	//variables
	private List<Question> _questions;
	private Dictionary<string, string> _categories;
	int currentQuestionIndex = 0;
	private string correctAnswer;


	public StartMenu()
	{
		InitializeComponent();
		LoadCatagory();

	}

	//Loading categories via API
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


	private async void LoadQuestions(string categoryID, string difficulty)
	{
		try
		{
			string url = $"https://opentdb.com/api.php?amount=10&type=multiple";
			if (!string.IsNullOrEmpty(categoryID))
				url += $"&category={categoryID}";
			if (!string.IsNullOrEmpty(difficulty))
				url += $"&difficulty={difficulty}";

			using var client = new HttpClient();
			string response = await client.GetStringAsync(url);
			var triviaResponse = JsonConvert.DeserializeObject<TriviaResponse>(response);

			_questions = triviaResponse.Results;
			currentQuestionIndex = 0;
		}
		catch (Exception ex)
		{
			await DisplayAlert("Notice", $"Failed to load Questions: {ex.Message}","OK");
		}
}
	//start button
	private void Button_Clicked(object sender, EventArgs e)
		{

		}



	//Helper class for deserialization
	public class CategoryResponse
	{
		[JsonProperty("trivia_categories")]
		public List<Category> TriviaCategories { get; set; }
	}

	public class Category
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}

	public class TriviaResponse
	{
		[JsonProperty("results")]
		public List<Question> Results { get; set; }
	}
}