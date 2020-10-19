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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Lsj.Util.Win32;

namespace Walterlv.Issues.NonClientParent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Window? _childWindow;

        private void EmbedButton_Click(object sender, RoutedEventArgs e)
        {
            if (_childWindow == null)
            {
                _childWindow = new ChildWindow();
                _childWindow.Top = 200;
                _childWindow.Left = 0;
                _childWindow.Closed += ChildWindow_Closed;
            }

            var childHandle = new WindowInteropHelper(_childWindow).EnsureHandle();
            var parentHandle = new WindowInteropHelper(this).Handle;
            User32.SetParent(childHandle, parentHandle);

            _childWindow.Show();
        }

        private void ReleaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (_childWindow is null)
            {
                return;
            }

            var childHandle = new WindowInteropHelper(_childWindow).Handle;
            User32.SetParent(childHandle, IntPtr.Zero);
        }

        private void ChildWindow_Closed(object sender, EventArgs e)
        {
            _childWindow.Close();
            _childWindow = null;
        }
    }
}
