using ControleFinanceiro.Services;
using ControleFinanceiro.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ControleFinanceiro.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private int _IdUsuario;
        private string _Login;
        private string _Senha;
        private bool _Result;
        private bool _IsBusy;

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

        public string Login
        {
            set
            {
                this._Login = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Login;
            }
        }

        public string Senha
        {
            set
            {
                this._Senha = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Senha;
            }
        }

        //login ou registro se foi realizado com sucesso
        public bool Result
        {
            set
            {
                this._IsBusy = value;
                OnPropertyChanged();
            }
            get
            {
                return this._IsBusy;
            }
        }

        //verifica se login foi realizado para evitar concorrencia
        public bool IsBusy
        {
            set
            {
                this._IsBusy = value;
                OnPropertyChanged();
            }
            get
            {
                return this._IsBusy;
            }
        }

        public Command LoginCommand { get; set; }
        public Command RegisterCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginCommandAsync());
            RegisterCommand = new Command(async () => await RegisterCommandAsync());
        }

        private async Task RegisterCommandAsync()
        {
            try
            {
                IsBusy = true;
                Result = false;
                var userService = new UsuarioService();

                if (!string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Senha))
                    Result = await userService.Registrar(IdUsuario, Login, Senha);

                if (Result)
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Usuário Registrado", "Ok");
                else
                    await Application.Current.MainPage.DisplayAlert("Erro", "Falha ao registrar usuário", "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoginCommandAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var userService = new UsuarioService();//cria instancia
                Result = await userService.Login(Login, Senha);

                if (Result)
                {
                    Preferences.Set("Login", Login);
                    await Application.Current.MainPage.Navigation.PushAsync(new MenuView());
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Erro", "Usuário/Senha inválidos", "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
