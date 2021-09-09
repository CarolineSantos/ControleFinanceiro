using ControleFinanceiro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ControleFinanceiro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MesesView : ContentPage
    {
        public ContaViewModel contaModel = new ContaViewModel();

        public MesesView()
        {
            InitializeComponent();

            List<int> anos = new List<int>();
            DateTime hoje = DateTime.Now;
            int cincoAtras = hoje.Year - 2;
            int cincoFrente = hoje.Year + 5;
            for (int i = cincoAtras; i <= cincoFrente; i++)
            {
                anos.Add(i);
            }
            listaAnos.ItemsSource = anos;

            List<int> meses = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                meses.Add(i);
            }

            listaMeses.ItemsSource = meses;
        }

        public async void ExibeContas()
        {
            var conta = await contaModel.GetContas();
            listaContas.ItemsSource = conta;
        }

        private void Ano_SelectedIndexChanged(object sender, EventArgs e)
        {
            var itemSelecionado = listaAnos.Items[listaAnos.SelectedIndex];
            DisplayAlert(itemSelecionado, "Foi o item Selecionado", "OK");
        }

        private void Mes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var itemSelecionado = listaMeses.Items[listaMeses.SelectedIndex];
            DisplayAlert(itemSelecionado, "Foi o item Selecionado", "OK");
        }

        private void listaContaItens_Changed(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}