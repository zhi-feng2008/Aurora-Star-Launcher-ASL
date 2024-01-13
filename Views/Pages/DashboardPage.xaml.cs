using Aurora_Star_Launcher.ViewModels.Pages;
using Wpf.Ui.Controls;
using MinecraftLaunch.Classes.Models.Auth;
using MinecraftLaunch.Classes.Models.Launch;
using KMCCC.Launcher;
using StarLight_Core.Authentication;
using System.Diagnostics;
using MessageBox = System.Windows.MessageBox;

namespace Aurora_Star_Launcher.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public static LaunchConfig LaunchConfig { get; } = new LaunchConfig();
        public Account UserInfo { get; private set; }

        public static LauncherCore Core = LauncherCore.Create();
        private string versionURL;

        public DashboardViewModel ViewModel { get; }

        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            // 自动寻找版本
            var versions = Core.GetVersions().ToArray();
            version.ItemsSource = versions;//绑定数据源
            version.DisplayMemberPath = "Id";//设置comboBox显示的为版本Id

            // 初始选择
            version.SelectedItem = 1;

        }

        // 启动&登录 S

        // a Microsoft S
        // a-1 登录 S
        private async void Microsoft_Login_Click(object sender, RoutedEventArgs e)
        {
            var auth = new MicrosoftAuthentication("a0ceb477-0738-47fa-8c93-52d892aa866a");
            var deviceCodeInfo = await auth.RetrieveDeviceCodeInfo();
            Process.Start("explorer.exe", deviceCodeInfo.VerificationUri);
            MessageBox.Show("请在浏览器中输入您的用户验证代码：" + deviceCodeInfo.UserCode, "Microsoft验证");
            var tokenInfo = await auth.GetTokenResponse(deviceCodeInfo);
            var userInfo = await auth.MicrosoftAuthAsync(tokenInfo, x =>
            {
                Console.WriteLine(x);
            });
            MessageBox.Show("欢迎回来！" + userInfo.Name, "欢迎");
            Microsoft_User_Name.Text = userInfo.Name;
        }
        // a-1 登录 E

        // a-2 启动 S
        private void Microsoft_Start_Click(object sender, RoutedEventArgs e)
        {

        }
        // a-2 启动 E
        // a Microsoft E

        // b 离线 S
        private void Offine_Start_Click (object sender, RoutedEventArgs e)
        {
            if (Offine_User_Name.Text != string.Empty && SettingsPage.JavaList.Text != string.Empty && version.Text != string.Empty && SettingsPage.MemoryBox.Text != string.Empty)
            {
                try
                {
                    Core.JavaPath = SettingsPage.JavaList.SelectedValue + "\\javaw.exe";
                    var ver = (KMCCC.Launcher.Version)version.SelectedItem;
                    var result = Core.Launch(new LaunchOptions
                    {
                        Version = ver,
                        MaxMemory = Convert.ToInt32(SettingsPage.MemoryBox.Text),
                        Authenticator = new KMCCC.Authentication.OfflineAuthenticator(Offine_User_Name.Text),
                        Mode = LaunchMode.MCLauncher,

                    });
                    // 错误提示
                    if (!result.Success)
                    {
                        switch (result.ErrorType)
                        {
                            case ErrorType.NoJAVA:
                                MessageBox.Show("Java错误！详细信息：" + result.ErrorMessage, "错误！");
                                break;
                            case ErrorType.AuthenticationFailed:
                                MessageBox.Show("登录时发生错误！详细信息：" + result.ErrorMessage, "错误！");
                                break;
                            case ErrorType.UncompressingFailed:
                                MessageBox.Show("文件错误！详细信息：" + result.ErrorMessage, "错误！");
                                break;
                            default:
                                MessageBox.Show(result.ErrorMessage, "错误！");
                                break;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("启动失败！", "错误！");
                }
            }
            else
            {
                MessageBox.Show("信息不完整！", "错误！");
            }
        }
        // b 离线 S
        // c 第三方 S
        // c-a 外置 S
        private void External_Start_1_Click(object sender, RoutedEventArgs e)
        {

        }
        // c-a 外置 E
        // c-b 统一通行证 S
        private async void External_Start_02_Click(object sender, RoutedEventArgs e)
        {
            var auth = new UnifiedPassAuthenticator(External_02_User_Name.Text, External_02_User_Password.Text, External_02_ServerID.Text);
            var account = auth.UnifiedPassAuthAsync();
            LaunchConfig args = new()
            {
                GameWindowConfig = new()
                {
                    Height = int.Parse(SettingsPage.GameH.Text),
                    Width = int.Parse(SettingsPage.GameW.Text),
                },
                Account = new()
                {

                },
            };
        }
        // c-a 统一通行证 E
        // c 第三方 E
        // 启动%登录 E

    }
}
