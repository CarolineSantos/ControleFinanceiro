using ControleFinanceiro.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Services
{
    public class UsuarioService
    {
        FirebaseClient firebase;

        public UsuarioService() 
        {
            //chamar banco
            firebase = new FirebaseClient("https://controlefinanceiro-cf392-default-rtdb.firebaseio.com/");
        }

        public async Task<bool> IsUserExists(string login)
        {
            var user = (await firebase.Child("Usuarios")
                .OnceAsync<Usuario>())
                .Where(u => u.Object.Login == login)
                .FirstOrDefault();

            return (user != null);//usuario diferente  de nul true
        }

        public async Task<bool> Registrar(int idUsuario,string login, string senha)
        {
            //Se usuario não existe ele registra usuário
            if (await IsUserExists(login) == false)
            {
                await firebase.Child("Usuarios")
                    .PostAsync(new Usuario()
                    {
                        IdUsuario = idUsuario,
                        Login = login,
                        Senha = senha
                    });
                return true;
            }
            else
                return false;
        }

        public async Task<bool> Login(string login, string senha)
        {
            var user = (await firebase.Child("Usuarios")
                .OnceAsync<Usuario>())
                .Where(u => u.Object.Login == login)
                .Where(u => u.Object.Senha == senha)
                .FirstOrDefault();

            return (user != null);
        }
    }
}
