using ApiPubg.Models.SquadModel;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;

namespace ApiPubg.Popups;

public partial class InforPartidaPopup : Popup
{

	public InforPartidaPopup(Jogador jogador)
	{
		InitializeComponent();
		BindingContext = jogador;
	}
    //public JogadorPopup()
    //{
    //}

    private void Button_Clicked(object sender, EventArgs e)
    {
		//var popup = new JogadorPopup();
		var page = Application.Current?.Windows.Count > 0 ? Application.Current.Windows[0].Page : null;

		if (page != null)
		{
			page.ClosePopupAsync();
		}
    }
}