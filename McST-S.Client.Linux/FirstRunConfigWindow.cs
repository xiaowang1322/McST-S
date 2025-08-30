using Avalonia.Controls;
using McST_S.Client.Linux.Views;

namespace McST_S.Client.Linux
{
    public class FirstRunConfigWindow : Window
    {
        public FirstRunConfigWindow()
        {
            this.Content = new FirstRunConfigView();
            this.Width = 500;
            this.Height = 350;
            this.Title = "McST-S 首次配置";
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.CanResize = false;
        }
    }
}