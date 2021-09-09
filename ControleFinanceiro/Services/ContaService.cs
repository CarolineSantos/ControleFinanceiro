using ControleFinanceiro.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ControleFinanceiro.Services
{
    public class ContaService
    {
        FirebaseClient firebase;
        UsuarioService usuarioService = new UsuarioService();

        public ContaService()
        {
            //chamar banco
            firebase = new FirebaseClient("https://controlefinanceiro-cf392-default-rtdb.firebaseio.com/");
        }

        public async Task AddConta(int id, int idUsuario, string nome, string valor)
        {
            await firebase.Child("Contas")
                .PostAsync(
                    new Conta() { IdConta = id, IdUsuario = idUsuario, Nome = nome, Valor = valor });
        }

        public async Task<List<Conta>> GetContas()
        {
            return (await firebase
                .Child("Contas")
                .OnceAsync<Conta>()).Select(item => new Conta
                {
                    IdConta = item.Object.IdConta,
                    Nome = item.Object.Nome,
                    Valor = item.Object.Valor,
                    IdUsuario = item.Object.IdUsuario
                })
                .Where(a => a.IdUsuario == Convert.ToInt32(Application.Current.Properties["IDUsuario"]))
                .ToList();
        }

        public async Task<List<Conta>> GetContas(int ano, int mes)
        {
            return (await firebase
                .Child("Contas")
                .OnceAsync<Conta>()).Select(item => new Conta
                {
                    IdConta = item.Object.IdConta,
                    Nome = item.Object.Nome,
                    Valor = item.Object.Valor,
                    IdUsuario = item.Object.IdUsuario
                })
                .Where(a => a.IdUsuario == Convert.ToInt32(Application.Current.Properties["IDUsuario"]))
                .ToList();
        }

        public async Task<Conta> GetConta(int idConta)
        {
            try
            {
                var conta = (await firebase
                    .Child("Contas")
                    .OnceAsync<Conta>())
                    .Where(a => a.Object.IdConta == idConta).FirstOrDefault();

                return await firebase.Child("Contas")
                    .Child(conta.Key).OnceSingleAsync<Conta>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateConta(int idConta, string nome, string valor)
        {
            try
            {
                var toUpdateConta = (await firebase
                    .Child("Contas")
                    .OnceAsync<Conta>())
                    .Where(a => a.Object.IdConta == idConta).FirstOrDefault();

                await firebase.Child("Contas")
                    .Child(toUpdateConta.Key)
                    .PutAsync(new Conta() { IdConta = idConta, Nome = nome, Valor = valor });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteConta(int idConta)
        {
            try
            {
                var toUpdateConta = (await firebase
                    .Child("Contas")
                    .OnceAsync<Conta>())
                    .Where(a => a.Object.IdConta == idConta).FirstOrDefault();

                await firebase.Child("Contas")
                    .Child(toUpdateConta.Key)
                    .DeleteAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Conta> GetContaNome(int idUsuario, string nome)
        {
            try
            {
                var conta = (await firebase
                    .Child("Contas")
                    .OnceAsync<Conta>())
                    .Where(a => a.Object.Nome == nome && a.Object.IdUsuario == idUsuario).FirstOrDefault();

                return await firebase.Child("Contas")
                    .Child(conta.Key).OnceSingleAsync<Conta>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
