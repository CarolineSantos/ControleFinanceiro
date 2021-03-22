using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.Services
{
    public class UsuarioService
    {
        FirebaseClient firebase;

        public UsuarioService() 
        {
            //chamar banco
            firebase = new FirebaseClient("");
        }
    }
}
