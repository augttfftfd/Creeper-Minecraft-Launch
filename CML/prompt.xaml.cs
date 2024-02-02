using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CML
{
    /// <summary>
    /// prompt.xaml 的交互逻辑
    /// </summary>
    public partial class prompt : Window
    {
        public prompt()
        {
            InitializeComponent();

            SoundPlayer sound = new SoundPlayer("prompt.wav");
            sound.Play();





        }
        }
    }

