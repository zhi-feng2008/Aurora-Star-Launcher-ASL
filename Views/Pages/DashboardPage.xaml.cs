// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Aurora_Star_Launcher.ViewModels.Pages;
using Wpf.Ui.Controls;
using StarLight_Core.Launch;
using StarLight_Core.Authentication;
using StarLight_Core.Models;
using StarLight_Core.Utilities;
using StarLight_Core.Enum;


namespace Aurora_Star_Launcher.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardViewModel ViewModel { get; }

        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

        // 启动&登录 S

        // a Microsoft S
        // a-1 登录 S
        private void Microsoft_Login_Click(object sender, RoutedEventArgs e)
        {

        }
        // a-1 登录 E

        // a-2 启动 S
        private void Microsoft_Start_Click(object sender, RoutedEventArgs e)
        {

        }
        // a-2 启动 E
        // a Microsoft E

        // 启动%登录 E

    }
}
