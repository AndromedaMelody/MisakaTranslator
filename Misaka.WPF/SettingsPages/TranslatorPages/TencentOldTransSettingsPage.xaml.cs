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
using TranslatorLibrary;

namespace Misaka.WPF.SettingsPages.TranslatorPages
{
    /// <summary>
    /// TencentOldTransSettingsPage.xaml 的交互逻辑
    /// </summary>
    public partial class TencentOldTransSettingsPage : Page
    {
        public TencentOldTransSettingsPage()
        {
            InitializeComponent();
            TransAppIDBox.Text = Misaka.Settings.Legacy.Instance.appSettings.TXOSecretId;
            TransSecretKeyBox.Text = Misaka.Settings.Legacy.Instance.appSettings.TXOSecretKey;
        }

        private async void AuthTestBtn_Click(object sender, RoutedEventArgs e)
        {
            Misaka.Settings.Legacy.Instance.appSettings.TXOSecretId = TransAppIDBox.Text;
            Misaka.Settings.Legacy.Instance.appSettings.TXOSecretKey = TransSecretKeyBox.Text;
            ITranslator Trans = new TencentOldTranslator();
            Trans.TranslatorInit(TransAppIDBox.Text, TransSecretKeyBox.Text);
            if (await Trans.TranslateAsync("apple", "zh", "en") != null)
            {
                HandyControl.Controls.Growl.Success($"腾讯云{Application.Current.Resources["APITest_Success_Hint"]}");
            }
            else
            {
                HandyControl.Controls.Growl.Error($"腾讯云{Application.Current.Resources["APITest_Error_Hint"]}\n{Trans.GetLastError()}");
            }
        }

        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(TencentOldTranslator.GetUrl_allpyAPI());
        }

        private void DocBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(TencentOldTranslator.GetUrl_Doc());
        }

        private void BillBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(TencentOldTranslator.GetUrl_bill());
        }

        private async void TransTestBtn_Click(object sender, RoutedEventArgs e)
        {
            ITranslator Trans = new TencentOldTranslator();
            Trans.TranslatorInit(Misaka.Settings.Legacy.Instance.appSettings.TXOSecretId, Misaka.Settings.Legacy.Instance.appSettings.TXOSecretKey);
            string res = await Trans.TranslateAsync(TestSrcText.Text, TestDstLang.Text, TestSrcLang.Text);
            if (res != null)
            {
                HandyControl.Controls.MessageBox.Show(res, Application.Current.Resources["MessageBox_Result"].ToString());
            }
            else
            {
                HandyControl.Controls.Growl.Error($"腾讯云{Application.Current.Resources["APITest_Error_Hint"]}\n{Trans.GetLastError()}");
            }
        }
    }
}
