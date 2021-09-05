using ControleFinanceiro.Models;
using ControleFinanceiro.Services;
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
    public partial class ContaListagemView : ContentPage
    {
        public ContaService contaService = new ContaService();
        public List<Conta> Contas = new List<Conta>();
        
        public ContaListagemView()
        {
            InitializeComponent();
            ExibeContas();
        }

        protected async override void OnAppearing()
        {//OnAppearing é diparado sempre que a página for exebida
            ExibeContas();
        }

        public async void ExibeContas()
        {
            var conta = await contaService.GetContas();
            listaContas.ItemsSource = conta;
        }        

        private void btnNovo_Clicked(object sender, EventArgs e)
        {
            var conta = new Conta();
            OnActionSheetCancelDeleteClicked(sender, e);
        }

        async void OnActionSheetCancelDeleteClicked(object sender, EventArgs e)
        {
            string nomeConta = await DisplayPromptAsync("Nova Conta", "por exemplo Cartão A");
            if(!String.IsNullOrEmpty(nomeConta))
                contaService.AddConta(1,Convert.ToInt32(Application.Current.Properties["IDUsuario"]),nomeConta, "R$ 0,00");
        }

        private void listaContaItens_Changed(object sender, SelectionChangedEventArgs e)
        {
            var conta = (Conta)e.CurrentSelection.FirstOrDefault();
            Navigation.PushAsync(new ContaItemsView(conta));
        }
    }
}