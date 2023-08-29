using _5330038.Data;
using _5330038.Models;
using System.Collections.ObjectModel;

namespace _5330038.Views;

public partial class SurveysView : ContentPage
{

	SurveyDatabase database;

    public ObservableCollection<Survey> Surveys { get; set; } = new();

  
    public SurveysView(SurveyDatabase sDatabase)
	{
		InitializeComponent();
		database = sDatabase;
		BindingContext = this;
	}


	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{

        base.OnNavigatedTo(args);

		var items = await database.GetTodoItemsAsync();

        MainThread.BeginInvokeOnMainThread(() =>
        {

            items.Clear();
		foreach (var item in items)
                Surveys.Add(item);
	
		});
	}

    async void ItemAdded_Clicked(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync(nameof(SurveyDetailsView), true, new Dictionary<string, object>
		{
			["survey"] = new Survey()
		});
    }
	private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Survey item)
			return;

		await Shell.Current.GoToAsync(nameof(SurveyDetailsView), true, new Dictionary<string, object>{
			["survey"] = item
		});
    }
}