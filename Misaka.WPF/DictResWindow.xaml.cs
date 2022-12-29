using DictionaryHelperLibrary;
using HandyControl.Controls;
using MecabHelperLibrary;
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
using System.Windows.Shapes;
using TTSHelperLibrary;

namespace Misaka.WPF
{
    /// <summary>
    /// DictResWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DictResWindow : System.Windows.Window
    {
        private string sourceWord;
        private TextSpeechHelper _textSpeechHelper;
        private IDict _dict;

        public DictResWindow(string word,string kana = "----", TextSpeechHelper tsh = null)
        {
            sourceWord = word;
            InitializeComponent();
            if (tsh == null)
            {
                _textSpeechHelper = new TextSpeechHelper();
            }
            else {
                _textSpeechHelper = tsh;
            }


            if (Misaka.Settings.Legacy.Instance.appSettings.ttsVoice == "")
            {
                Growl.InfoGlobal(Application.Current.Resources["TranslateWin_NoTTS_Hint"].ToString());
            }
            else
            {
                _textSpeechHelper.SetTTSVoice(Misaka.Settings.Legacy.Instance.appSettings.ttsVoice);
                _textSpeechHelper.SetVolume(Misaka.Settings.Legacy.Instance.appSettings.ttsVolume);
                _textSpeechHelper.SetRate(Misaka.Settings.Legacy.Instance.appSettings.ttsRate);
            }

            if (Misaka.Settings.Legacy.Instance.appSettings.xxgPath != string.Empty)
            {
                _dict = new XxgJpzhDict();
                _dict.DictInit(Misaka.Settings.Legacy.Instance.appSettings.xxgPath, string.Empty);
            }

            string ret = _dict.SearchInDict(sourceWord);

            SourceWord.Text = sourceWord;

            Kana.Text = kana;

            this.Topmost = true;
            DicResText.Text = XxgJpzhDict.RemoveHTML(ret);
        }

        ~DictResWindow() {
            _textSpeechHelper = null;
            _dict = null;
        }

        private void TTS_Btn_Click(object sender, RoutedEventArgs e)
        {
            _textSpeechHelper.SpeakAsync(sourceWord);
        }

        private void Search_Btn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.baidu.com/s?wd=" + sourceWord);
        }
    }
}
