using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class BikeWizardSelectBikePage : ContentPage
	{
		bool IsAnswerd = false;
		int questionNumber = 0;
		string option = string.Empty;
		QuestionAnswerModel questionAnswerModel = null;
		WizardResultModel resultModel = null;
		List<string> answerArray = null;
		public BikeWizardSelectBikePage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			btnBack.Clicked += BtnBack_Clicked;
			btnNext.Clicked += BtnNext_Clicked;

			PrepareUI();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			questionNumber = 0;
			IsAnswerd = false;
			answerArray  = new List<string>();
			GetQuestionAnswers().Wait();
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			questionNumber = 0;
		}
		private void PrepareUI()
		{
			try
			{
				if (StaticMethods.IsIpad())
				{
					lblQuestion.FontSize = 30;
					btnBack.HeightRequest = 70;
					btnNext.HeightRequest = 70;
					btnBack.FontSize = 30;
					btnNext.FontSize = 30;
					lblA.FontSize = 30;
					lblB.FontSize = 30;
					lblC.FontSize = 30;
					_slButton.Margin = new Thickness(0, 30, 0, 0);
					_slA.Margin = new Thickness(0, 30, 0, 0);
				}
			}
			catch (Exception ex)
			{

			}
		}
		async void back_Tapped(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
		void BtnBack_Clicked(object sender, EventArgs e)
		{
			IsAnswerd = false;
			imgA.IsVisible = false;
			imgB.IsVisible = false;
			imgC.IsVisible = false;

			if (questionNumber <= 1)
			{
				Navigation.PopModalAsync();
			}
			else
			{
				questionNumber = questionNumber - 1;
				LoadQuestion(questionNumber);
			}
		}

		void BtnNext_Clicked(object sender, EventArgs e)
		{
			imgA.IsVisible = false;
			imgB.IsVisible = false;
			imgC.IsVisible = false;

			if (IsAnswerd)
			{
				
				if (questionNumber <= 6)
				{
					answerArray.Add(option);
					LoadQuestion(questionNumber);

					if(questionNumber==6)
						GetWizardResult().Wait();

					questionNumber = questionNumber + 1;
				}
				else
				{
					

				}

			}
			IsAnswerd = false;
		}
		void optionATapped(object sender, EventArgs e)
		{
			imgA.IsVisible = true;
			imgB.IsVisible = false;
			imgC.IsVisible = false;

			IsAnswerd = true;
			option = "a";
		}

		void optionBTapped(object sender, EventArgs e)
		{
			imgA.IsVisible = false;
			imgB.IsVisible = true;
			imgC.IsVisible = false;

			IsAnswerd = true;
			option = "b";
		}

		void optionCTapped(object sender, EventArgs e)
		{
			imgA.IsVisible = false;
			imgB.IsVisible = false;
			imgC.IsVisible = true;
			IsAnswerd = true;
			option = "c";
		}
		private void LoadQuestion(int position)
		{
			try
			{
				_slC.IsVisible = true;
				if (questionAnswerModel != null)
				{

					lblQuestion.Text = questionAnswerModel.data[position].question;
					lblA.Text = questionAnswerModel.data[position].ans1;
					lblB.Text = questionAnswerModel.data[position].ans2;
					if (!string.IsNullOrEmpty(questionAnswerModel.data[position].ans3))
						lblC.Text = questionAnswerModel.data[position].ans3;
					else
						_slC.IsVisible = false;

				}
			}
			catch (Exception ex)
			{

			}
		}
		private async Task GetQuestionAnswers()
		{

			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						questionAnswerModel = WebService.GetWizardQuestionAnswers();


					}).ContinueWith(async
					t =>
					{

						Device.BeginInvokeOnMainThread(async () =>
						{
							LoadQuestion(questionNumber);
							questionNumber++;
						});

						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
		private async Task GetWizardResult()
		{

			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

				resultModel = WebService.GetWizardResult(answerArray);


					}).ContinueWith(async
					t =>
					{

						Device.BeginInvokeOnMainThread(async () =>
						{
							if (resultModel != null)
							{
						Navigation.PushModalAsync(new BikeWizardFinishPage(resultModel));
					}
						});

						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
	}
}
