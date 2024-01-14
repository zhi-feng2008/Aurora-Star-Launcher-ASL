using Aurora_Star_Launcher.ViewModels.Pages;
using Wpf.Ui.Controls;
using StarLight_Core.Authentication;
using System.Diagnostics;
using StarLight_Core.Models.Launch;
using StarLight_Core.Utilities;
using StarLight_Core.Launch;
using System.IO;
using System.Reflection;
using MessageBox = System.Windows.MessageBox;
using MessageBoxButton = System.Windows.MessageBoxButton;

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

            // 获取应用程序的根目录路径  
            string appRootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // 创建文件夹路径  
            string folderPath = Path.Combine(appRootDir, "ASL");
            string fileName = "config.txt"; // 文件名  
            string filePath = Path.Combine(folderPath, fileName); // 拼接文件夹路径和文件名  

            try
            {
                // 检查文件夹是否存在  
                if (Directory.Exists(folderPath))
                {
                    // 检查文件是否存在  
                    if (!File.Exists(filePath))
                    {
                        // 如果文件不存在，则创建它  
                        File.Create(filePath).Close();
                        MessageBox.Show("文件已成功创建!");
                    }
                    else
                    {

                    }
                }
                else
                {
                    // 如果文件夹不存在，则创建文件夹和文件  
                    Directory.CreateDirectory(folderPath);
                    File.Create(filePath).Close();
                }
            }
            catch
            {
                MessageBox.Show("配置文件或文件夹未正常创建！", "⚠️警告！", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            try
            {
                // 自动寻找版本
                var versions = GameCoreUtil.GetGameCores();
                version.ItemsSource = versions;//绑定数据源
                version.DisplayMemberPath = "Id";//设置comboBox显示的为版本Id

                // 初始选择
                version.SelectedIndex = 1;
                SettingsPage.JavaList.SelectedIndex = 1;
            }
            catch
            {
                MessageBox.Show("无法获取Minecarft版本信息，若是第一次使用，请无视此消息！", "提示");
            };

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
                    var account = auth.OfflineAuth();
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
                    var la = await launch.LaunchAsync(ReportProgress);
                        
                    static void ReportProgress(ProgressReport report)
                    {
                        Console.WriteLine($"{report.Description} ({report.Percentage}%)"); // 显示当前操作与进度
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
                    var launch = new StarLight_Core.Launch.MinecraftLaunch(args);
                    var la = await launch.LaunchAsync(ReportProgress);
                    
                    static void ReportProgress(ProgressReport report)
                    {
                        Console.WriteLine($"{report.Description} ({report.Percentage}%)"); // 显示当前操作与进度
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
        // c-a 统一通行证 E
        // c 第三方 E
        // 启动%登录 E

    }
}
