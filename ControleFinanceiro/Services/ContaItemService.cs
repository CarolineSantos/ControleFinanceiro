using ControleFinanceiro.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ControleFinanceiro.Services
{
    public class ContaItemService
    {
        FirebaseClient firebase;
        ContaService contaService = new ContaService();

        public ContaItemService()
        {
            //chamar banco
            firebase = new FirebaseClient("https://controlefinanceiro-cf392-default-rtdb.firebaseio.com/");
        }

        public async Task AddContaItem(int IdConta, string Nome, string Valor, DateTime Competencia, int Parcelas, int idUsuario)
        {
            await firebase.Child("ContaItems")
                .PostAsync(
                    new ContaItem() { 
                        IdConta = IdConta,
                        Nome = Nome,
                        Valor = Valor,
                        Competencia = Competencia,
                        Parcelas = Parcelas,
                        IdUsuario = idUsuario 
                    });
        }

        public async Task<List<ContaItem>> GetContaItems()
        {
            Conta conta = new Conta();
            conta = (Conta)Application.Current.Properties["Conta"];

            return (await firebase
                .Child("ContaItems")
                .OnceAsync<ContaItem>()).Select(item => new ContaItem
                {
                    IdConta = item.Object.IdConta,
                    Nome = item.Object.Nome,
                    Valor = item.Object.Valor,
                    Competencia = item.Object.Competencia
                })
                .Where(a => a.IdUsuario == Convert.ToInt32(Application.Current.Properties["IDUsuario"]) && a.IdConta == conta.IdConta)
                .ToList();
        }

        public async Task<ContaItem> GetContaItem(string Nome)
        {
            try
            {
                var conta = (await firebase
                    .Child("ContaItems")
                    .OnceAsync<ContaItem>())
                    .Where(a => a.Object.Nome == Nome).FirstOrDefault();

                return await firebase.Child("ContaItems")
                    .Child(conta.Key).OnceSingleAsync<ContaItem>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateContaItem(int idConta, string nome, string valor)
        {
            try
            {
                var toUpdateConta = (await firebase
                    .Child("ContaItems")
                    .OnceAsync<ContaItem>())
                    .Where(a => a.Object.Nome == nome).FirstOrDefault();

                await firebase.Child("ContaItems")
                    .Child(toUpdateConta.Key)
                    .PutAsync(new ContaItem() { IdConta = idConta, Nome = nome, Valor = valor });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteContaItem(string nome)
        {
            try
            {
                var toUpdateConta = (await firebase
                    .Child("ContaItems")
                    .OnceAsync<ContaItem>())
                    .Where(a => a.Object.Nome == nome).FirstOrDefault();

                await firebase.Child("ContaItems")
                    .Child(toUpdateConta.Key)
                    .DeleteAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ContaItem> GetContaItemNome(int idContaItem, string nome)
        {
            try
            {
                var conta = (await firebase
                    .Child("ContaItems")
                    .OnceAsync<ContaItem>())
                    .Where(a => a.Object.Nome == nome).FirstOrDefault();

                if (conta != null)
                    return await firebase.Child("ContaItems")
                        .Child(conta.Key).OnceSingleAsync<ContaItem>();
                else return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ContaItem>> GetContaItemsPorAno(DateTime MesAno)
        {
            List<Conta> listaConta = new List<Conta>();
            
            return (await firebase
                .Child("ContaItems")
                .OnceAsync<ContaItem>()).Select(item => new ContaItem
                {
                    IdConta = item.Object.IdConta,
                    Nome = item.Object.Nome,
                    Valor = item.Object.Valor,
                    Competencia = item.Object.Competencia
                })
                .Where(a => a.IdUsuario == Convert.ToInt32(Application.Current.Properties["IDUsuario"]) 
                        && a.Competencia.Year == MesAno.Year && a.Competencia.Month == MesAno.Month)
                .ToList();
        }

        public async Task<Decimal> CalcularTotalConta() 
        {            
            try
            {
                decimal total = 0;
                Conta conta = new Conta();
                conta = (Conta)Application.Current.Properties["Conta"];
                total = (await firebase
                    .Child("ContaItems")
                    .OnceAsync<ContaItem>())                    
                    .Where(b => b.Object.IdConta == conta.IdConta)
                    .Sum(a => Convert.ToDecimal(a.Object.Valor.Replace("R$ ","").Replace(",", ".")));

                return total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
