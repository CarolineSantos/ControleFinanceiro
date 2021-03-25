using ControleFinanceiro.Models;
using ControleFinanceiro.Services;
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
        
        public ContaItemsView(Conta _conta)
        {
            InitializeComponent();
            conta = _conta;
            Title = conta.Nome;
        }

        protected async override void OnAppearing()
        {//OnAppearing é diparado sempre que a página for exebida
            ExibirContaItems();
            this.IdConta.Text = conta.IdConta.ToString();
        }

        private async void ExibirContaItems() 
        {
            service = new ContaItemService();

            var listContaItems = await service.GetContaItems();

            listaContas.ItemsSource = listContaItems.Where(a => a.IdConta == conta.IdConta);
        }

        private async void btnFechar_Clicked(object sender, EventArgs e)
        {
            await Task.WhenAny<bool>
                  (
                    this.popuplayout.FadeTo(0, 200, Easing.SinInOut)
                  );

            this.popuplayout.IsVisible = !this.popuplayout.IsVisible;
            btnNovoItem.IsVisible = true;
        }

        private async void btnNovoItem_Clicked(object sender, EventArgs e)
        {
            if (!this.popuplayout.IsVisible)
            {
                this.popuplayout.IsVisible = !this.popuplayout.IsVisible;
                this.popuplayout.AnchorX = 1;
                this.popuplayout.AnchorY = 1;

                Animation scaleAnimation = new Animation(
                    f => this.popuplayout.Scale = f,
                    0.5,
                    1,
                    Easing.SinInOut);

                Animation fadeAnimation = new Animation(
                    f => this.popuplayout.Opacity = f,
                    0.2,
                    1,
                    Easing.SinInOut);

                scaleAnimation.Commit(this.popuplayout, "popupScaleAnimation", 250);
                fadeAnimation.Commit(this.popuplayout, "popupFadeAnimation", 250);
                btnNovoItem.IsVisible = false;
            }
            else
            {
                await Task.WhenAny<bool>
                  (
                    this.popuplayout.FadeTo(0, 200, Easing.SinInOut)
                  );

                this.popuplayout.IsVisible = !this.popuplayout.IsVisible;
            }
        }
    }
}