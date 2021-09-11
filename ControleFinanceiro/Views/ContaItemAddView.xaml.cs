using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
    public partial class ContaItemAddView : PopupPage
    {
        public ContaItemAddView()
        {
            InitializeComponent();
        }

        protected void Fechar_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }
    }
}