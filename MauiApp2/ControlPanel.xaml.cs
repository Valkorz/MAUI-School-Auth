using System.Diagnostics;
using System;
using MauiApp2.Data;
using Microsoft.EntityFrameworkCore;
using MauiApp2.Pages;

namespace MauiApp2
{
    public partial class ControlPanel : ContentPage
    {
        private readonly UserControl _usrControl;
        public ControlPanel(UserControl usrControl)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

#if !DEBUG
            if ((App.ActiveUser?.Permissions & User.UserPermissions.Administrator) != User.UserPermissions.Administrator)
            {
                //disable admin panel if user does not contain the privileges.
                admin_panel.IsEnabled = false;
                admin_panel.IsVisible = false;
            }
#endif
            //int selector = Welcome.Text.
            _usrControl = usrControl;
        }

        public async void OnDbControlClicked(object sender, EventArgs e)
        {
#if !DEBUG
            //Deny access if user is not administrator
            bool? permissionState = App.ActiveUser?.VerifyPermissions(User.UserPermissions.Administrator);
            if (permissionState.HasValue && !permissionState.Value)
            {
                await DisplayAlert("Erro", "Acesso Negado.", "OK");
                return;
            }
#endif

            await Shell.Current.GoToAsync(nameof(ControleAcad));
        }

        public async void OnSignOut(object? sender, EventArgs e)
        {
            bool ans = await DisplayAlert("Sign Out", "Gostaria de desconectar-se?", "Sim", "N�o");
            if (ans)
            {
                App.ActiveUser = null;
                await Shell.Current.GoToAsync("///MainPage");
            }
        }
    }
}