using System;
using System.Windows;
using System.Windows.Controls;

namespace Misaka.WPF.SettingsPages
{
    /// <summary>
    /// HookSettingsPage.xaml 的交互逻辑
    /// </summary>
    public partial class HookSettingsPage : Page
    {
        public HookSettingsPage()
        {
            InitializeComponent();

            AutoHookCheckBox.IsChecked = Convert.ToBoolean(Misaka.Settings.Legacy.Instance.appSettings.AutoHook);
            AutoDetachCheckBox.IsChecked = Convert.ToBoolean(Misaka.Settings.Legacy.Instance.appSettings.AutoDetach);
        }

        private void AutoHookCheckBox_Click(object sender, RoutedEventArgs e)
        {
            Misaka.Settings.Legacy.Instance.appSettings.AutoHook = Convert.ToString(AutoHookCheckBox.IsChecked);
        }

        private void AutoDetachCheckBox_Click(object sender, RoutedEventArgs e)
        {
            Misaka.Settings.Legacy.Instance.appSettings.AutoDetach = Convert.ToString(AutoDetachCheckBox.IsChecked);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Common.ExportTextractorHistory())
            {
                HandyControl.Controls.Growl.Success(Application.Current.Resources["HookSettingsPage_SuccessHint"].ToString());
            }
            else
            {
                HandyControl.Controls.Growl.Error(Application.Current.Resources["HookSettingsPage_ErrorHint"].ToString());
            }
        }

    }
}
