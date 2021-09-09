using ControleFinanceiro.Models;
using ControleFinanceiro.Services;
using ControleFinanceiro.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ControleFinanceiro.ViewModel
{
    public class ContaViewModel : BaseViewModel
    {
        public List<Conta> Contas = new List<Conta>();
        ContaService contaService;

        #region Propridades 
        private int _IdConta;
        private int _IdUsuario;
        private string _Nome;
        private decimal _Valor;

               
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

        public int IdUsuario
        {
            set
            {
                this._IdUsuario = value;
                OnPropertyChanged();
            }
            get
            {
                return this._IdUsuario;
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
                this._Valor = Convert.ToDecimal(value);
                OnPropertyChanged();
            }
            get
            {
                return "R$ " + this._Valor.ToString().Replace(".", ",");
            }
        }
        #endregion

        #region Commands
        public ICommand ExibirContaCommand { get; set; }
        public ICommand InserirContaCommand { get; set; }
        public ICommand AtualizarContaCommand { get; set; }
        public ICommand DeletarContaCommand { get; set; }
        #endregion

        public ContaViewModel()
        {
            ExibirContaCommand = new Command(async () => await ExibirContaCommandAsync());
            AtualizarContaCommand = new Command(async () => await AtualizarContaCommandAsync());
            DeletarContaCommand = new Command(async () => await DeletarContaCommandAsync());
        }

        private async Task ExibirContaCommandAsync()
        {
            var conta = new Conta();
            contaService = new ContaService();
            if (IdConta <= 0)
                await Application.Current.MainPage.DisplayAlert("Erro", "ID da conta inválido", "Ok");
            else
            {
                try
                {
                    conta = await contaService.GetConta(IdConta);

                    if (conta != null)
                    {
                        IdConta = conta.IdConta;
                        Nome = conta.Nome;
                        var num = conta.Valor.ToString();
                        Valor = "R$ " + conta.Valor.ToString().Replace(".", ",");  //Convert.ToDecimal(num.Replace(",","."));
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Erro", "Não existe conta com esse ID", "Ok");
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Conta não encontrada : " + ex.Message, "Ok");
                }
            }
            // return conta;
        }

        public async Task InserirConta(string nomeConta)
        {
            try
            {
                contaService = new ContaService();
                var conta = await contaService.GetContas();
                //var conta = await contaService.GetContaNome(IdUsuario, Nome);
                                
                    IdConta = 1+conta.Select(c => c.IdConta).LastOrDefault();
                    await contaService.AddConta(IdConta, IdUsuario, nomeConta, Valor);
               
                    //await contaService.UpdateConta(Convert.ToInt32(IdConta), Nome, Valor);

                IdConta = 0;
                Valor = "0";
                Nome = string.Empty;

                await Application.Current.MainPage.DisplayAlert("Sucess", "Contato incluído com sucesso", "Ok");
                //await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Conta não encontrada : " + ex.Message, "Ok");
            }
        }

        private async Task AtualizarContaCommandAsync()
        {
            if (IdConta <= 0)
                await Application.Current.MainPage.DisplayAlert("Erro", "ID da conta inválido", "Ok");
            else
            {
                try
                {
                    contaService = new ContaService();
                    await contaService.UpdateConta(Convert.ToInt32(IdConta), Nome, Valor);
                    IdConta = 0;
                    Nome = string.Empty;
                    Valor = "0";

                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Contato atualizado com sucesso", "Ok");

                    await Application.Current.MainPage.Navigation.PushAsync(new ContaListagemView());
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Conta não encontrada : " + ex.Message, "Ok");
                }
            }
        }

        private async Task DeletarContaCommandAsync()
        {
            if (IdConta <= 0)
                await Application.Current.MainPage.DisplayAlert("Erro", "ID da conta inválido", "Ok");
            else
            {
                try
                {
                    contaService = new ContaService();
                    await contaService.DeleteConta(Convert.ToInt32(IdConta));
                    IdConta = 0;
                    Nome = string.Empty;
                    Valor = "0";

                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Contato deletado com sucesso", "Ok");

                    await Application.Current.MainPage.Navigation.PushAsync(new ContaListagemView());
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Conta não encontrada : " + ex.Message, "Ok");
                }
            }
        }

        public async Task<List<Conta>> GetContas()
        {
            var conta = new List<Conta>();

            try
            {
                contaService = new ContaService();
                conta = await contaService.GetContas();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Conta não encontrada : " + ex.Message, "Ok");
            }

            return conta;
        }

        public async Task<List<Conta>> GetContas(int ano, int mes)
        {
            var conta = new List<Conta>();

            try
            {
                contaService = new ContaService();

                conta = await contaService.GetContas(ano, mes);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Conta não encontrada : " + ex.Message, "Ok");
            }

            return conta;
        }
    }
}
