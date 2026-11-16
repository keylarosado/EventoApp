namespace EventoApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new Views.MainPage())
        {
            BarBackgroundColor = Color.FromArgb("#245B52"),
            BarTextColor = Colors.White
        };
    }
}
