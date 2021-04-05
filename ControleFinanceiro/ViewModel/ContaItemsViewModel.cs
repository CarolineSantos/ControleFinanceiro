using ControleFinanceiro.Models;
using ControleFinanceiro.Services;
using ControleFinanceiro.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ControleFinanceiro.ViewModel
{
    public class ContaItemsViewModel : BaseViewModel
    {
        ContaItemService itemService;
        private int _IdConta;
        private string _Nome;
        private string _Valor;
        private string _Competencia;
        private int _Parcelas;
                
        public int IdConta
        {
            set
            {
                this._IdConta = value;
                OnPropertyChanged();
            }
            get
            {
                return this._IdConta;
            }
        }

        public string Nome
        {
            set
            {
                this._Nome = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Nome;
            }
        }

        public string Valor
        {
            set
            {
                this._Valor = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Valor;
            }
        }

        public string Competencia
        {
            set
            {
                this._Competencia = DateTime.Now.ToString();
                OnPropertyChanged();
            }
            get
            {
                return this._Competencia;
            }
        }

        public int Parcelas
        {
            set
            {
                this._Parcelas = 1;
                OnPropertyChanged();
            }
            get
            {
                return this._Parcelas;
            }
        }

        public ICommand InserirContaItemCommand { get; set; }
        public ICommand AtualizarContaItemCommand { get; set; }
        public ICommand DeletarContaItemCommand { get; set; }
        public ICommand GetContaItemsCommand { get; set; }

        public ContaItemsViewModel() 
        {
            InserirContaItemCommand = new Command(async () => await InserirContaItemCommandAsync());
            AtualizarContaItemCommand = new Command(async () => await AtualizarContaItemCommandAsync());
            DeletarContaItemCommand = new Command(async () => await DeletarContaItemCommandAsync());
            GetContaItemsCommand = new Command(async () => await GetContaItemsCommandAsync());
        }

        private async Task<List<ContaItem>> GetContaItemsCommandAsync()
        {
            var contaItems = new List<ContaItem>();

            try
            {
                itemService = new ContaItemService();
                contaItems = await itemService.GetContaItems();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Item da conta não encontrada : " + ex.Message, "Ok");
            }

            return contaItems;
        }

        private Task DeletarContaItemCommandAsync()
        {
            throw new NotImplementedException();
        }

        private async Task InserirContaItemCommandAsync()
        {
            try
            {
                Conta conta = new Conta();

                if (IdConta == 0)
                {
                    conta = (Conta)Application.Current.Properties["Conta"];
                    IdConta = conta.IdConta;
                }

                itemService = new ContaItemService();
                var contaItem = await itemService.GetContaItemNome(IdConta, Nome);

                if (contaItem == null)
                    await itemService.AddContaItem(IdConta, Nome, Valor, Competencia, Parcelas);
                else
                    await itemService.UpdateContaItem(IdConta, Nome, Valor);

                //Aletar valor de  conta pai
                decimal valorSomar = Convert.ToDecimal(conta.Valor.Replace("R$ ",""));
                valorSomar += Convert.ToDecimal(Valor.Replace("R$ ", ""));
                conta.Valor = "R$ " + valorSomar.ToString();
                ContaService contaService = new ContaService();
                contaService.UpdateConta(conta.IdConta, conta.Nome, conta.Valor);

                conta.IdConta = IdConta;
                Valor = "R$ 0,00";
                Nome = string.Empty;                

                await Application.Current.MainPage.DisplayAlert("Sucess", "Item incluído com sucesso", "Ok");
                await Application.Current.MainPage.Navigation.PushAsync(new ContaItemsView(conta));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Item não encontrada : " + ex.Message, "Ok");
            }
        }

        private Task AtualizarContaItemCommandAsync()
        {
            throw new NotImplementedException();
        }
    }
}
