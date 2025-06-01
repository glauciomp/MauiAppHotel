using MauiAppHotel.Models;
using Microsoft.VisualBasic;

namespace MauiAppHotel.Views;

public partial class ContratacaoHospedagem : ContentPage
{
    App PropriedadesApp; // definindo propriedades para o app

	public ContratacaoHospedagem()
	{
		InitializeComponent();

        PropriedadesApp = (App)Application.Current; // trazendo a propriedade para o aplicativo atual
        pck_quarto.ItemsSource = PropriedadesApp.lista_quartos; // indo buscar as propriedades que foram criadas para os 4 quartos em app.xaml.cs

        dtpck_checkin.MinimumDate = DateTime.Now; // setando a data m�nima do checkin sempre para a data ataul (hoje)
        dtpck_checkin.MaximumDate = DateTime.Now.AddMonths(1); 
        // definindo o m�nimo de dias que o h�spede pode ficar

        dtpck_checkout.MinimumDate = dtpck_checkin.Date.AddDays(1); // travar o checkout para no m�nimo + 1 dia, para n�o ser poss�vel escolher data no passado;
        dtpck_checkout.MaximumDate = dtpck_checkin.Date.AddMonths(6); // travar o checkou para no m�ximo 6 meses, n�o permitindo escolher mais que isso;
	}

    private async void OnSobreButtonClicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new SobreHospedagem());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao navegar: {ex.Message}");
            await DisplayAlert("Erro", "Ocorreu um erro ao navegar para a p�gina Sobre N�s.", "OK");
        }
                
    }

    private async void Button_Clicked(Object sender, EventArgs e)
    { // definido o m�todo como assincrono para executar na ordem
        try
        {
            Hospedagem h = new Hospedagem
            {
                QuartoSelecionado = (Quarto)pck_quarto.SelectedItem,
                // ctrl + espa�o para trazer os campos da nossa classe
                QntAdultos = Convert.ToInt32(stp_adultos.Value),
                // convertendo o valor do stepper para inteiro pois o usu�rio � quem est� digitando
                QntCriancas = Convert.ToInt32(stp_criancas.Value),
                DataCheckIn = dtpck_checkin.Date,
                DataCheckOut = dtpck_checkout.Date,

                // assim est� feito o nosso objeto, nossa model inteira e j� preenchida e pronta para enviar
                // para hospedagem contratada
            };
            await Navigation.PushAsync(new HospedagemContratada()
            {
                BindingContext = h // passando o contexto de dados para a p�gina HospedagemContratada
            });
            // quando colocamos como assincrono, precisamos tamb�m fazer a navega��o esperar, ent�o
            // colocamos o awayt no try, tanto na nagga��o quanto no display alert


        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Ok");

        }
    }
    
    private void dtpck_checkin_DateSelected(object sender, DateChangedEventArgs e)
    {
        DatePicker elemento = sender as DatePicker; // criando mais um elemento para tratar (este � o disparador)
                                                    // como o h�spede seleciona a data no checkin
                                                    // para ele n�o selecionar o checkout no passado
                                                    // tem que colocar um evendo no datepicker do Contratacao
                                                    // Hospedatem colcando DateSelected="dtpck_chekin_DateSelected que � um evento
                                                    // que ser� disparado toda vez que � selecionado a data

        DateTime data_selecionada_checkin = elemento.Date; // variavel datetime

        dtpck_checkout.MinimumDate = data_selecionada_checkin.AddDays(1); // chamar a data do checkout pegando a data m�nima
                                                                          // onde a data selecionada ser� + 1 dia
        dtpck_checkout.MaximumDate = data_selecionada_checkin.AddMonths(6); // aqui no m�ximo 6 meses de acordo com a data do checkin
    }
}