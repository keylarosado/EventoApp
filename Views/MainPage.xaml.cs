using EventoApp.Models;

namespace EventoApp.Views;

public partial class MainPage : ContentPage
{
    private readonly Evento _evento;

    public MainPage()
    {
        InitializeComponent();
        _evento = new Evento();
        BindingContext = _evento;
    }

    private async void OnCadastrarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_evento.Nome))
        {
            await DisplayAlert("Campo obrigatório", "Informe o nome do evento.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(_evento.Local))
        {
            await DisplayAlert("Campo obrigatório", "Informe o local do evento.", "OK");
            return;
        }

        if (_evento.NumeroParticipantes <= 0)
        {
            await DisplayAlert("Valor inválido", "O número de participantes deve ser maior que zero.", "OK");
            return;
        }

        if (_evento.CustoPorParticipante < 0)
        {
            await DisplayAlert("Valor inválido", "O custo por participante não pode ser negativo.", "OK");
            return;
        }

        if (_evento.DataTermino.Date < _evento.DataInicio.Date)
        {
            await DisplayAlert("Datas inválidas", "A data de término deve ser igual ou posterior à data de início.", "OK");
            return;
        }

        await Navigation.PushAsync(new ResumoPage(_evento));
    }
}
