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
        private DateTime _Competencia;
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

        public DateTime Competencia
        {
            set
            {
                this._Competencia = DateTime.Now;
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
                this._Parcelas = value;
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
                var dataAtual = DateTime.Now;
                                
                for (int i = 1; i <= Parcelas; i++)
                {
                    if (i == 1)
                        dataAtual = Competencia;

                    await itemService.AddContaItem(IdConta, Nome, Valor, dataAtual, Parcelas, Convert.ToInt32(Application.Current.Properties["IDUsuario"]));

                    dataAtual = dataAtual.AddMonths(1);
                }
                //await itemService.UpdateContaItem(IdConta, Nome, Valor);

                //Aletar valor de  conta pai
                decimal valorSomar = Convert.ToDecimal(conta.Valor.Replace("R$ ","").Replace(",","."));
                valorSomar += Convert.ToDecimal(Valor.Replace("R$ ", "").Replace(",", "."));
                conta.Valor = "R$ " + valorSomar.ToString();
                ContaService contaService = new ContaService();
                await contaService.UpdateConta(conta.IdConta, conta.Nome, conta.Valor);

                conta.IdConta = IdConta;
                Valor = "R$ 0,00";
                Nome = string.Empty;                

                await Application.Current.MainPage.DisplayAlert("Sucess", "Item incluído com sucesso", "Ok");
                await Application.Current.MainPage.Navigation.PopAsync();
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

        private void CarregarMeses() 
        {
            //var contaItem = await itemService.GetContaItems();
        }
    }
}
