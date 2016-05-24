using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        //MoveWindow関数の宣言
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int MoveWindow(IntPtr hwnd, int x, int y,
            int nWidth, int nHeight, int bRepaint);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process[] plist = System.Diagnostics.Process.GetProcessesByName(ProcessName.Text);
            foreach (var p in plist)
            {
                p.WaitForInputIdle();
                MoveWindow(p.MainWindowHandle, 
                    GetValue(PositionX, 0),
                    GetValue(PositionY, 10),
                    GetValue(SizeX, 100),
                    GetValue(SizeY, 200),
                    1);

            }
        }

        private int GetValue(TextBox inputVal, int initVal)
        {
            int result;
            if(int.TryParse(inputVal.Text, out result))
            {
                return result;
            }
            return initVal;
        }

    }
}
