using ControleFinanceiro.Models;
using ControleFinanceiro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ControleFinanceiro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuView : MasterDetailPage
    {
        MenuViewModel menuViewModel = new MenuViewModel();

        public MenuView()
        {
            InitializeComponent();
            paginaMestreList.ItemsSource = menuViewModel.CarregarMenu();
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ContaListagemView)));
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MenuModel)e.SelectedItem;
            Type page = item.Pagina;
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
    }
}