using _5330038.Data;
using _5330038.Models;

namespace _5330038.Views;


//se que indica que esta página puede recibir un parámetro
//llamado Item cuando se navega a ella usando Shell.

[QueryProperty("survey", "survey")]
public partial class SurveyDetailsView : ContentPage
{
    Survey item;
    private readonly string[] teams =
    {
        "Alianza Lima",
        "América",
        "Boca Juniors",
        "Caracas FC",
        "Colo-Colo",
        "Peñarol",
        "Real Madrid",
        "Saprissa"
    };


    public Survey Surveys
    {
    
        get => BindingContext as Survey;
        set => BindingContext = value;
    }

    SurveyDatabase database;

    public SurveyDetailsView(SurveyDatabase todoItemDatabase)
	{
		InitializeComponent();
        database = todoItemDatabase;

	}

    async void Save_Clicked(object sender, EventArgs e)
    {
      
        if (string.IsNullOrWhiteSpace(Surveys.Name))
        {
            await DisplayAlert("Name Required", "Please enter a name for the todo item.", "OK");
            return;
        }
        Surveys.Birthdate = BirthdatePicker.Date;
        Surveys.FavoriteTeam = FavoriteTeamLabel.Text;
        await database.SaveItemAsync(Surveys);
        await Shell.Current.GoToAsync("..");

    }

    async void Delete_Clicked(object sender, EventArgs e)
    {
 
        if (Surveys.Id == 0)
            return;
        await database.DeleteItemAsync(Surveys);
        await Shell.Current.GoToAsync("..");

    }
    async void Cancel_Clicked(object sender, EventArgs e)
    {
        //con este metodo nada más se llama a la siguiente funcion
        await Shell.Current.GoToAsync("..");
    }

    private async void FavoriteTeam_ClickedAsync(object sender, EventArgs e)
    {
        var favoriteteam = await DisplayActionSheet(Literals.FavoriteTeamTitle, null, null, teams);
        if (!string.IsNullOrWhiteSpace(favoriteteam))
        {
            FavoriteTeamLabel.Text = favoriteteam;
        }
    }
}