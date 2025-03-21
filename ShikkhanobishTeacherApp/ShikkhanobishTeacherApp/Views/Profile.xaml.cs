﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace ShikkhanobishTeacherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        public async Task EndOrBackBtn()
        {
            var result = await MaterialDialog.Instance.ConfirmAsync(message: "Do you want to close app?",
                                  confirmingText: "Yes",
                                  dismissiveText: "No");
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                Navigation.RemovePage(page);
            }
            Application.Current.Quit();
        }
        protected override bool OnBackButtonPressed()
        {
            EndOrBackBtn();
            return true;
        }
    }
}