namespace MauiAppHotel.Models
{
    public class Hospedagem
    {
        public Quarto QuartoSelecionado { get; set; }
        public int QntAdultos { get; set; }
        public int QntCriancas { get; set; }
        public DateTime DataCheckIn { get; set; }
        public DateTime DataCheckOut { get; set; }
        public int Estadia
        {
            get => DataCheckOut.Subtract(DataCheckIn).Days;
            // estou encapsulando a regra de negócio em Estadia;
        }
         public double ValorTotal
        { // este vai calcular a forma de hospedagem
            get
            {
                double valor_adultos = QntAdultos * QuartoSelecionado.ValorDiariaAdulto;
                // aqui já descobri quando vai custar para os adultos os quartos selecionados;
                double valor_criancas = QntCriancas * QuartoSelecionado.ValorDiariaCrianca;
                // aqui para as crianças;

                double total = (valor_adultos + valor_criancas) * Estadia;
                // aqui trazer o resultado (fazer a formula)
                return total;
                // aqui o retorno do valor total da hospedagem
            }
        }
    }
}
