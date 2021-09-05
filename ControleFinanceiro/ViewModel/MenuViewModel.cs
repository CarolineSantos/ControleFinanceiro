using System;
using System.Collections.Generic;
using System.Text;
using ControleFinanceiro.Models;
using ControleFinanceiro.Views;

namespace ControleFinanceiro.ViewModel
{
    public class MenuViewModel
    {
        public List<MenuModel> ListaMenu = new List<MenuModel>();

        public MenuViewModel() 
        {
                      
        }

        public List<MenuModel> CarregarMenu() 
        {
            ListaMenu.Add(new MenuModel() { Titulo = "Contas", Icone="", Pagina= typeof(ContaListagemView) });
            ListaMenu.Add(new MenuModel() { Titulo = "Meses", Icone = "", Pagina = typeof(MesesView) });
            //ListaMenu.Add(new MenuModel() { Titulo = "Logout", Icone = "", Pagina = typeof(LogoutView) });
            return ListaMenu;
        }
    }
}
