using Aurora_Star_Launcher.ViewModels.Pages;
using Wpf.Ui.Controls;
using StarLight_Core.Authentication;
using System.Diagnostics;
using MessageBox = System.Windows.MessageBox;
using StarLight_Core.Models.Launch;

namespace Aurora_Star_Launcher.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        private string versionURL;

        public DashboardViewModel ViewModel { get; }

        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            // 自动寻找版本
            var versions = StarLight_Core().ToArray();
            version.ItemsSource = versions;//绑定数据源
            version.DisplayMemberPath = "Id";//设置comboBox显示的为版本Id

            // 初始选择
            version.SelectedIndex = 1;
            SettingsPage.JavaList.SelectedIndex = 1;

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
        private async void Offine_Start_Click (object sender, RoutedEventArgs e)
        {
            if (Offine_User_Name.Text != string.Empty && SettingsPage.JavaList.Text != string.Empty && version.Text != string.Empty && SettingsPage.MemoryBox.Text != string.Empty)
            {
                try
                {
                    string verText = version.SelectedItem.ToString();
                    var auth = new OfflineAuthentication(Offine_User_Name.Text);
                    var account = await auth.Name.();
                    LaunchConfig args = new()
                    {
                        GameWindowConfig = new()
                        {
                            Height = int.Parse(SettingsPage.GameH.Text),
                            Width = int.Parse(SettingsPage.GameW.Text),
                            IsFullScreen = false,
                        },
                        Account = new()
                        {
                            BaseAccount = account
                        },
                        GameCoreConfig = new()
                        {
                            Root = ".minecraft",
                            Version = verText,
                        },
                        JavaConfig = new()
                        {
                            JavaPath = SettingsPage.JavaList.SelectedValue + "\\javaw.exe"
                        }
                    };
                    var launch = new MinecraftLaunch(args);
                    var la = await launch.Lanchasymc(ReportProgess);
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
            if (External_02_User_Name.Text != string.Empty && External_02_User_Password.Text != string.Empty && External_02_ServerID.Text != string.Empty && SettingsPage.JavaList.Text != string.Empty && version.Text != string.Empty && SettingsPage.MemoryBox.Text != string.Empty)
            {
                try
                {
                    string verText = version.SelectedItem.ToString();
                    var auth = new UnifiedPassAuthenticator(External_02_User_Name.Text, External_02_User_Password.Text, External_02_ServerID.Text);
                    var account = await auth.UnifiedPassAuthAsync();
                    LaunchConfig args = new()
                    {
                        GameWindowConfig = new()
                        {
                            Height = int.Parse(SettingsPage.GameH.Text),
                            Width = int.Parse(SettingsPage.GameW.Text),
                            IsFullScreen = false,
                        },
                        Account = new()
                        {
                            BaseAccount = account
                        },
                        GameCoreConfig = new()
                        {
                            Root = ".minecraft",
                            Version = verText,
                        },
                        JavaConfig = new()
                        {
                            JavaPath = SettingsPage.JavaList.SelectedValue + "\\javaw.exe"
                        }
                    };
                    var launch = new MinecraftLaunch(args);
                    var la = await launch.Lanchasymc(ReportProgess);
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
        // c-a 统一通行证 E
        // c 第三方 E
        // 启动%登录 E

    }
}
