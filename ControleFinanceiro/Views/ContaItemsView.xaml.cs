using ControleFinanceiro.Models;
using ControleFinanceiro.Services;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ControleFinanceiro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContaItemsView : ContentPage
    {
        ContaItemService service;
        Conta conta = new Conta();
        
        public ContaItemsView()
        {
            conta = (Conta)Application.Current.Properties["ContaSelecionada"];
            InitializeComponent();

            if (conta.IdConta != 0 && conta.Nome != null)
            {
                Application.Current.Properties["Conta"] = conta;
            }
            else
                conta = (Conta)Application.Current.Properties["Conta"];
            
            Title = conta.Nome + " = R$ " + conta.Valor;
        }

        protected override void OnAppearing()
        {//OnAppearing é diparado sempre que a página for exebida
            ExibirContaItems();
            //this.IdConta.Text = conta.IdConta.ToString();
        }

        private async void ExibirContaItems() 
        {
            service = new ContaItemService();

            var listContaItems = await service.GetContaItems();

            listaContas.ItemsSource = listContaItems;
        }        

        private async void btnNovoItem_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync((PopupPage)Activator.CreateInstance(typeof(ContaItemAddView)));
        }

        private void btnRecalcular_Clicked(object sender, EventArgs e)
        {

        }
    }
}