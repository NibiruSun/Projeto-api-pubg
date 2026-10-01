using ApiPubg.Popups;
using ApiPubg.Views;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
using Microsoft.Maui.Controls.Shapes;

namespace ApiPubg
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(Painel), typeof(Painel));
        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();

        //    var popup = new JogadorPopup();

        //    var result = await this.ShowPopupAsync(popup);
        //    //var result = await this.ShowPopupAsync(popup, new PopupOptions
        //    //{
        //    //    CanBeDismissedByTappingOutsideOfPopup = false,
        //    //    Shape = new RoundRectangle
        //    //    {
        //    //        CornerRadius = new CornerRadius(20),
        //    //        Stroke = new SolidColorBrush(Colors.AliceBlue),
        //    //        StrokeThickness = 0,
        //    //    },
        //    //});

        //    // Supondo que JogadorPopup retorna o nome via uma propriedade customizada, por exemplo "Nome"
        //    // Ajuste conforme a implementação real do JogadorPopup e IPopupResult
        //    await Shell.Current.GoToAsync($"{nameof(Painel)}? nome={result}");
        //}
    }
}
