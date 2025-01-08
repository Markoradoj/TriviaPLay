using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Maui.Audio;
using System.Net.Http;
using System.Linq;
using System.Linq.Expressions;




namespace TriviaPLay;

public partial class StartMenu : ContentPage
{
	//variables
	private List<Question> _questions;
	private Dictionary<string, string> _categories;
	int _currentQuestionIndex = 0;
	public string _correctAnswer;
	private IAudioManager audioManager;

	public StartMenu()
	{
		InitializeComponent();
		LoadCatagory();


	}

	//Loading categories via API
	private async void LoadCatagory()
	{
		try
		{	//getting the client from the response
			using var client = new HttpClient();
			string response = await client.GetStringAsync("https://opentdb.com/api_category.php");
			var categoryResponse = JsonConvert.DeserializeObject<CategoryResponse>(response);

			_categories = categoryResponse.TriviaCategories.ToDictionary(c => c.Name, c => c.Id.ToString());

			//category select
			foreach (var category in _categories.Keys)
			{
				selCategory.Items.Add(category);
			}
		}


		//error message if the api fails to catch
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
			string url = $"https://opentdb.com/api.php?amount=10&type=multiple"; //loads questions from multiple type section
			if (!string.IsNullOrEmpty(categoryID))
				url += $"&category={categoryID}";
			if (!string.IsNullOrEmpty(difficulty))
				url += $"&difficulty={difficulty}";

			using var client = new HttpClient();
			string response = await client.GetStringAsync(url);
			var triviaResponse = JsonConvert.DeserializeObject<TriviaResponse>(response);

			_questions = triviaResponse.Results;
			_currentQuestionIndex = 0;
			showQuestion();
		}
		catch (Exception ex)
		{
			await DisplayAlert("Notice", $"Failed to load Questions: {ex.Message}", "OK");
		}
	}




	//shows when no questions selected or completed all questions
	private void showQuestion()
	{
		if (_questions == null || !_questions.Any())
		{
			questionLabel.Text = "No questions available, please select diffuclty and category";
			return;

		}
		if (_currentQuestionIndex >= _questions.Count)
		{
			DisplayAlert("Notice", "You've completed all the questions", "OK");
			return;
		}




		var question = _questions[_currentQuestionIndex];

		//shuffle answers
		var answers = new List<string>(question.incorrect_answers) { _correctAnswer };
		Random rand = new Random();
		for (int i = answers.Count - 1; i >= 0; i--)
		{
			int j = rand.Next(i + 1);
			(answers[i], answers[j]) = (answers[j], answers[i]);
		}



		//answer lael to buttons
		questionLabel.Text = System.Net.WebUtility.HtmlDecode(question.question);
		Option1Ans.Text = System.Net.WebUtility.HtmlDecode(answers[0]);
		Option2Ans.Text = System.Net.WebUtility.HtmlDecode(answers[1]);
		Option3Ans.Text = System.Net.WebUtility.HtmlDecode(answers[2]);
		Option4Ans.Text = System.Net.WebUtility.HtmlDecode(answers[3]);

		//ensure buttons are visible
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
				
				AnimateText();
				correctSound();
			}
			else
			{
				DisplayAlert("Incorrect", $"The correct anaswer is was {_correctAnswer}", "Next");
				AnimateTextWrong();
				wrongSound();
			}
			_currentQuestionIndex++;
			showQuestion();
			//show next question or end game
			if (_currentQuestionIndex < _questions.Count)
			{
				showQuestion();
			}
			else
			{
				DisplayAlert("Notice", "You've completed all questions", "OK");
			}
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
		string selectedCategory = selCategory.SelectedItem.ToString();
		string selectedDifficulty = selDifficulty.SelectedItem.ToString();

		if (_categories.TryGetValue(selectedCategory, out var categoryID))
		{
			LoadQuestions(categoryID, selectedDifficulty);
		}

	}


	//animation text
	private async void AnimateText()
	{
       

		CorrectWord.Opacity = 0;
		await CorrectWord.FadeTo(1, 500);
		await Task.Delay(1000);
		await CorrectWord.FadeTo(0, 500);
	}

    private async void AnimateTextWrong()
    {
		WrongWord.Opacity = 0;
		await WrongWord.FadeTo(1, 500);
		await Task.Delay(1000);
		await WrongWord.FadeTo(0, 500);

    }

	//plays sound when correct
	private async void correctSound()
	{

		var audioplayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("correct.mp3"));
		audioplayer.Play();
		//audio stops once it pkays
		audioplayer.Dispose();
	}

	//plays sound when incorrect
    private async void wrongSound()
    {

        var audioplayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("incorrect.mp3"));
        audioplayer.Play();
		audioplayer.Dispose();
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
