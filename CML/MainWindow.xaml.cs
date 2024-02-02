using MinecraftLaunch.Classes.Interfaces;
using MinecraftLaunch.Classes.Models.Game;
using MinecraftLaunch.Classes.Models.Launch;
using MinecraftLaunch.Components.Authenticator;
using MinecraftLaunch.Components.Fetcher;
using MinecraftLaunch.Components.Launcher;
using MinecraftLaunch.Components.Resolver;
using NAudio.Wave;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SoundPlayer sound = new SoundPlayer("open.wav");
            sound.Play();


            IGameResolver gameResolver = new GameResolver(".minecraft");
            var games = gameResolver.GetGameEntitys();
            try
            {
                    gameversion.ItemsSource = games;
            }
            catch {
                Directory.CreateDirectory(".minecraft");

            }


            





            JavaFetcher javaFetcher = new JavaFetcher();
            var JavaList = javaFetcher.Fetch();



            //java控件
            foreach (JavaEntry javalist in JavaList)
            {
                java.Items.Add(javalist.JavaVersion);

            }
            //java.ItemsSource = JavaList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text != "" && java.Text != "" && gameversion.Text != "")


            {
                JavaFetcher javaFetcher = new JavaFetcher();
                Dictionary<string, string> javas = new Dictionary<string, string>();
                foreach (JavaEntry javalist in javaFetcher.Fetch())
                {
                    //Java表
                    javas[javalist.JavaVersion] = javalist.JavaPath;

                }
                IGameResolver gameResolver = new GameResolver(".minecraft");

                var account = new OfflineAuthenticator(username.Text).Authenticate();


                LaunchConfig config = new LaunchConfig
                {
                    Account = account, //账户信息的获取请使用验证器，使用方法请跳转至验证器文档查看
                    GameWindowConfig = new GameWindowConfig
                    {
                        // Width = < 窗口宽度 >,
                        // Height = < 窗口高度 >,
                        IsFullscreen = false
                    },
                    JvmConfig = new JvmConfig(javas[java.Text])
                    {
                        MaxMemory = 4090,
                        //MinMemory = < 最小内存 >
                    },

                    //ServerConfig = new(< 端口号 >, < ip地址 >),
                    IsEnableIndependencyCore = true//是否启用版本隔离
                };
                Launcher launcher = new(gameResolver, config);



                // 使用Dispatcher来在UI线程上执行代码
                Application.Current.Dispatcher.Invoke(async () =>
                {



                    var gameProcessWatcher = await launcher.LaunchAsync(gameversion.Text);
                    

                    gameProcessWatcher.OutputLogReceived += (sender, args) =>
                    {

                        Debug.WriteLine(args.Text);
                    };


                });
                
            }
            else
            {   
                prompt prompt = new prompt();
                prompt.ShowDialog();
               

            }
        }
    }
}