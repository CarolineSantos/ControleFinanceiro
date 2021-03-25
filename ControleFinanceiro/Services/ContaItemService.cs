using ControleFinanceiro.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Services
{
    public class ContaItemService
    {
        FirebaseClient firebase;

        public ContaItemService()
        {
            //chamar banco
            firebase = new FirebaseClient("https://controlefinanceiro-cf392-default-rtdb.firebaseio.com/");
        }

        public async Task AddContaItem(int id, int idConta, string nome, string valor)
        {
            await firebase.Child("ContaItems")
                .PostAsync(
                    new ContaItem() { IdConta = idConta, IdContaItem = id, Nome = nome, Valor = valor });
        }

        public async Task<List<ContaItem>> GetContaItems()
        {
            return (await firebase
                .Child("ContaItems")
                .OnceAsync<ContaItem>()).Select(item => new ContaItem
                {
                    IdConta = item.Object.IdConta,
                    Nome = item.Object.Nome,
                    Valor = item.Object.Valor
                }).ToList();
        }

        public async Task<ContaItem> GetContaItem(int idContaItem)
        {
            try
            {
                var conta = (await firebase
                    .Child("ContaItems")
                    .OnceAsync<ContaItem>())
                    .Where(a => a.Object.IdContaItem == idContaItem).FirstOrDefault();

                return await firebase.Child("ContaItems")
                    .Child(conta.Key).OnceSingleAsync<ContaItem>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateContaItem(int IdContaItem ,int idConta, string nome, string valor)
        {
            try
            {
                var toUpdateConta = (await firebase
                    .Child("ContaItems")
                    .OnceAsync<ContaItem>())
                    .Where(a => a.Object.IdContaItem == IdContaItem).FirstOrDefault();

                await firebase.Child("ContaItems")
                    .Child(toUpdateConta.Key)
                    .PutAsync(new ContaItem() { IdConta = idConta, Nome = nome, Valor = valor });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteContaItem(int idContaItem)
        {
            try
            {
                var toUpdateConta = (await firebase
                    .Child("ContaItems")
                    .OnceAsync<ContaItem>())
                    .Where(a => a.Object.IdContaItem == idContaItem).FirstOrDefault();

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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
