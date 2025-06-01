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

        dtpck_checkin.MinimumDate = DateTime.Now; // setando a data mínima do checkin sempre para a data ataul (hoje)
        dtpck_checkin.MaximumDate = DateTime.Now.AddMonths(1); 
        // definindo o mínimo de dias que o hóspede pode ficar

        dtpck_checkout.MinimumDate = dtpck_checkin.Date.AddDays(1); // travar o checkout para no mínimo + 1 dia, para não ser possível escolher data no passado;
        dtpck_checkout.MaximumDate = dtpck_checkin.Date.AddMonths(6); // travar o checkou para no máximo 6 meses, não permitindo escolher mais que isso;
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
            await DisplayAlert("Erro", "Ocorreu um erro ao navegar para a página Sobre Nós.", "OK");
        }
                
    }

    private async void Button_Clicked(Object sender, EventArgs e)
    { // definido o método como assincrono para executar na ordem
        try
        {
            Hospedagem h = new Hospedagem
            {
                QuartoSelecionado = (Quarto)pck_quarto.SelectedItem,
                // ctrl + espaço para trazer os campos da nossa classe
                QntAdultos = Convert.ToInt32(stp_adultos.Value),
                // convertendo o valor do stepper para inteiro pois o usuário é quem está digitando
                QntCriancas = Convert.ToInt32(stp_criancas.Value),
                DataCheckIn = dtpck_checkin.Date,
                DataCheckOut = dtpck_checkout.Date,

                // assim está feito o nosso objeto, nossa model inteira e já preenchida e pronta para enviar
                // para hospedagem contratada
            };
            await Navigation.PushAsync(new HospedagemContratada()
            {
                BindingContext = h // passando o contexto de dados para a página HospedagemContratada
            });
            // quando colocamos como assincrono, precisamos também fazer a navegação esperar, então
            // colocamos o awayt no try, tanto na naggação quanto no display alert


        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Ok");

        }
    }
    
    private void dtpck_checkin_DateSelected(object sender, DateChangedEventArgs e)
    {
        DatePicker elemento = sender as DatePicker; // criando mais um elemento para tratar (este é o disparador)
                                                    // como o hóspede seleciona a data no checkin
                                                    // para ele não selecionar o checkout no passado
                                                    // tem que colocar um evendo no datepicker do Contratacao
                                                    // Hospedatem colcando DateSelected="dtpck_chekin_DateSelected que é um evento
                                                    // que será disparado toda vez que é selecionado a data

        DateTime data_selecionada_checkin = elemento.Date; // variavel datetime

        dtpck_checkout.MinimumDate = data_selecionada_checkin.AddDays(1); // chamar a data do checkout pegando a data mínima
                                                                          // onde a data selecionada será + 1 dia
        dtpck_checkout.MaximumDate = data_selecionada_checkin.AddMonths(6); // aqui no máximo 6 meses de acordo com a data do checkin
    }
}