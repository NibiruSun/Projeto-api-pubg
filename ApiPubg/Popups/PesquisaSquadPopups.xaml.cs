using ApiPubg.ViewModel;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Views;

namespace ApiPubg.Popups;

public partial class PesquisaSquadPopups : Popup
{
    public PesquisaSquadPopups(ConnectionApiPubg vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

}