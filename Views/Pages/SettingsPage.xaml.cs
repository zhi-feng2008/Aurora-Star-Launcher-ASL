using Aurora_Star_Launcher.ViewModels.Pages;
using Wpf.Ui.Controls;
using StarLight_Core.Utilities;
using System.Windows.Controls;

namespace Aurora_Star_Launcher.Views.Pages
{
    public partial class SettingsPage : INavigableView<SettingsViewModel>
    {
        public SettingsViewModel ViewModel { get; }

        public static ComboBox JavaList { get; set; } = new ComboBox();
        public static System.Windows.Controls.TextBox MemoryBox { get; set; } = new System.Windows.Controls.TextBox();

        public static System.Windows.Controls.TextBox GameH { get; set; } = new System.Windows.Controls.TextBox();
        public static System.Windows.Controls.TextBox GameW { get; set; } = new System.Windows.Controls.TextBox();

        public SettingsPage(SettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            JavaList = Java;
            MemoryBox = MemoryText;
            GameH = Game_Window_Height;
            GameW = Game_Window_Width;

            // 自动寻找Java
            var javaInfo = JavaUtil.GetJavas();
            string javaPath = javaInfo.First().JavaPath;
            JavaList.DisplayMemberPath = "JavaLibraryPath";
            JavaList.SelectedValuePath = "JavaLibraryPath";
            JavaList.ItemsSource = javaInfo;
            JavaList.SelectedItem = 1;
        }

        // 设置 S
        private void JavaList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MemoryText_1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void MemoryText_2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        // 设置 E

        // 关于 S
        private void GitHub_Click(object sender, RoutedEventArgs e)
        {
           System.Diagnostics.Process.Start("explorer.exe", "https://github.com/Aurora-Studio-Team/Aurora-Minecarft-Launcher-AMCL");
        }

        private void Gitee_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://gitee.com/THZtx/Aurora-Minecarft-Launcher-AMCL");
        }

        private void Website_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://amcl.thzstudent.top");
        }

        private void Sponsor_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://afdian.net/a/thzstudent");
        }

        private void ULA_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://amcl.thzstudent.top/doc/ula.html");
        }

        // 关于 E
    }
}
