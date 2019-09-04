using ArtAuto.Devices;
using ArtTester.Models;
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

namespace ArtTester.Controls
{
    /// <summary>
    /// Interaction logic for AdamDevicePanel.xaml
    /// </summary>
    public partial class AdamDevicePanel : UserControl
    {
        public AdamDevicePanel()
        {
            InitializeComponent();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            AdamViewModel avm = (AdamViewModel)this.DataContext;
            Button btn = (Button)sender;

            
            int index = doListBox.SelectedIndex;
            if (index != -1)
            {
                IDiscreteOutputDevice dev = avm.Device as IDiscreteOutputDevice;
                if (dev != null)
                {
                    bool state = dev.DiscreteOutputs[index].IsEnabled;
                    dev.SetDiscreteOutput(index, !state);
                }

            }

            
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            AdamViewModel avm = (AdamViewModel)this.DataContext;

        }
    }
}
