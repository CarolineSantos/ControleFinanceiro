﻿using ControleFinanceiro.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ControleFinanceiro
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
