using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace EventoApp.Models;

// Model que armazena os dados do evento e calcula duração e custo total.
public class Evento : INotifyPropertyChanged
{
    private string _nome = string.Empty;
    private DateTime _dataInicio = DateTime.Today;
    private DateTime _dataTermino = DateTime.Today.AddDays(1);
    private int _numeroParticipantes = 1;
    private string _local = string.Empty;
    private decimal _custoPorParticipante;

    private static readonly CultureInfo PtBr = new("pt-BR");

    public string Nome
    {
        get => _nome;
        set => SetProperty(ref _nome, value);
    }

    public DateTime DataInicio
    {
        get => _dataInicio;
        set
        {
            if (SetProperty(ref _dataInicio, value))
                NotificarCalculos();
        }
    }

    public DateTime DataTermino
    {
        get => _dataTermino;
        set
        {
            if (SetProperty(ref _dataTermino, value))
                NotificarCalculos();
        }
    }

    public int NumeroParticipantes
    {
        get => _numeroParticipantes;
        set
        {
            if (SetProperty(ref _numeroParticipantes, value))
                OnPropertyChanged(nameof(CustoTotalTexto));
        }
    }

    public string Local
    {
        get => _local;
        set => SetProperty(ref _local, value);
    }

    public decimal CustoPorParticipante
    {
        get => _custoPorParticipante;
        set
        {
            if (SetProperty(ref _custoPorParticipante, value))
            {
                OnPropertyChanged(nameof(CustoPorParticipanteTexto));
                OnPropertyChanged(nameof(CustoTotalTexto));
            }
        }
    }

    // Propriedade auxiliar para receber o valor digitado no Entry.
    public string CustoPorParticipanteTexto
    {
        get => _custoPorParticipante.ToString("F2", PtBr);
        set
        {
            if (decimal.TryParse(value, NumberStyles.Number, PtBr, out var valor) && valor >= 0)
                CustoPorParticipante = valor;
            else if (string.IsNullOrWhiteSpace(value))
                CustoPorParticipante = 0;
        }
    }

    // O período é contado incluindo o primeiro e o último dia do evento.
    public TimeSpan Duracao
    {
        get
        {
            DateTime inicio = DataInicio.Date;
            DateTime termino = DataTermino.Date;
            return termino >= inicio
                ? (termino - inicio).Add(TimeSpan.FromDays(1))
                : TimeSpan.Zero;
        }
    }

    public decimal CustoTotal => NumeroParticipantes * CustoPorParticipante;

    public string DuracaoTexto => $"{Duracao.Days} {(Duracao.Days == 1 ? "dia" : "dias")}";
    public string DataInicioFormatada => DataInicio.ToString("dd/MM/yyyy", PtBr);
    public string DataTerminoFormatada => DataTermino.ToString("dd/MM/yyyy", PtBr);
    public string CustoPorParticipanteFormatado => CustoPorParticipante.ToString("C2", PtBr);
    public string CustoTotalTexto => CustoTotal.ToString("C2", PtBr);

    public event PropertyChangedEventHandler? PropertyChanged;

    private bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    private void NotificarCalculos()
    {
        OnPropertyChanged(nameof(Duracao));
        OnPropertyChanged(nameof(DuracaoTexto));
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
