using ApiPubg.Models;
using ApiPubg.ViewModel;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Maui.Extensions;
using ApiPubg.Models.Painel;

namespace ApiPubg.Popups;

public partial class JogadorPopup : Popup<NickPainel>
{
	NickPainel nick;
	public JogadorPopup()
	{
		InitializeComponent();

		BindingContext = nick = new NickPainel();

    }

    private void Button_Clicked(object sender, EventArgs e)
	{
		CloseAsync(nick);
    }
}