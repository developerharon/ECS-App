﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClockPageMaster : ContentPage
    {
        public ListView ListView;

        public ClockPageMaster()
        {
            InitializeComponent();

            BindingContext = new ClockPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class ClockPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<ClockPageMasterMenuItem> MenuItems { get; set; }

            public ClockPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<ClockPageMasterMenuItem>(new[]
                {
                    new ClockPageMasterMenuItem { Id = 0, Title = "Page 1" },
                    new ClockPageMasterMenuItem { Id = 1, Title = "Page 2" },
                    new ClockPageMasterMenuItem { Id = 2, Title = "Page 3" },
                    new ClockPageMasterMenuItem { Id = 3, Title = "Page 4" },
                    new ClockPageMasterMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}