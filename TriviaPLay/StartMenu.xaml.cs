
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;


namespace TriviaPLay;

public partial class StartMenu : ContentPage
{
    //variables
    private List<Question> _questions;
    private Dictionary<string, string> _categories;
	int _currentQuestionIndex = 0;
	public string _correctAnswer;


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

	//Loading questions in the label
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
			_currentQuestionIndex = 0;
		}
		catch (Exception ex)
		{
			await DisplayAlert("Notice", $"Failed to load Questions: {ex.Message}","OK");
		}
}
	



	
	private void showQuestion()
	{
		if (_questions == null || !_questions.Any())
		{
			questionLabel.Text = "No questions available, please select diffuclty and category";
			return;

		}
		if (_currentQuestionIndex < _questions.Count)
		{
			DisplayAlert("Notice", "You've completed all the questions", "OK");
			return;
		}

		var question = _questions[_currentQuestionIndex];
		_correctAnswer = question.CorrectAnswer;

		//shuffles the answers
		var options = question.IncorrectAnswers.Append(_correctAnswer).OrderBy(X => Guid.NewGuid()).ToList();

		questionLabel.Text = System.Net.WebUtility.HtmlDecode(question.question);
		Option1Ans.Text = System.Net.WebUtility.HtmlDecode(options[0]);
        Option2Ans.Text = System.Net.WebUtility.HtmlDecode(options[1]);
        Option3Ans.Text = System.Net.WebUtility.HtmlDecode(options[2]);
        Option4Ans.Text = System.Net.WebUtility.HtmlDecode(options[3]);

		Option1Ans.IsVisible = true;
        Option2Ans.IsVisible = true;
        Option3Ans.IsVisible = true;
        Option4Ans.IsVisible = true;

    }

	private void OnOptionClicked(object sender, EventArgs e)
	{
		if (sender is Button button)
		{
			string selectedAnswer = button.Text;

			if (selectedAnswer == _correctAnswer)
			{
				DisplayAlert("Coorect", "Nice", "Next");

			}
			else
			{
				DisplayAlert("Incorrect", $"The correct anaswer is was {_correctAnswer}", "Next");

			}
			_currentQuestionIndex++;
			showQuestion();
		}

	}

	//start game
	private void Button_Clicked(object sender, EventArgs e)
	{
		if (selCategory.SelectedIndex == -1)
		{
			DisplayAlert("Error", "Please Select a category", "OK");
			return;
		}

		if (selDifficulty.SelectedIndex == -1)
		{
			DisplayAlert("Error", "Please select a Diffculty", "OK");
			return;
		}
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