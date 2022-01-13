﻿using ShikkhanobishTeacherApp.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShikkhanobishTeacherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CallingPage : ContentPage
    {
        public CallingPage(string thisID)
        {
            InitializeComponent();
            BindingContext = new CallingPageViewModel(thisID);
        }
    }
}