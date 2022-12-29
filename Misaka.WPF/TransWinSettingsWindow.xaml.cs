﻿using System;
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

namespace Misaka.WPF
{
    /// <summary>
    /// TransWinSettingsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TransWinSettingsWindow : Window
    {
        TranslateWindow translateWin;

        List<string> FontList;

        public TransWinSettingsWindow(TranslateWindow Win)
        {
            translateWin = Win;

            InitializeComponent();

            FontList = new List<string>();

            System.Drawing.Text.InstalledFontCollection fonts = new System.Drawing.Text.InstalledFontCollection();
            foreach (System.Drawing.FontFamily family in fonts.Families)
            {
                FontList.Add(family.Name);
            }

            sourceFont.ItemsSource = FontList;
            firstFont.ItemsSource = FontList;
            secondFont.ItemsSource = FontList;

            EventInit();

            UI_Init();

            this.Topmost = true;
        }

        /// <summary>
        /// 事件初始化
        /// </summary>
        private void EventInit()
        {
            sourceFont.SelectionChanged += delegate
            {
                translateWin.SourceTextFont = FontList[sourceFont.SelectedIndex];
                Misaka.Settings.Legacy.Instance.appSettings.TF_srcTextFont = FontList[sourceFont.SelectedIndex];
            };

            firstFont.SelectionChanged += delegate
            {
                translateWin.FirstTransText.FontFamily = new FontFamily(FontList[firstFont.SelectedIndex]);
                Misaka.Settings.Legacy.Instance.appSettings.TF_firstTransTextFont = FontList[firstFont.SelectedIndex];
            };

            secondFont.SelectionChanged += delegate
            {
                translateWin.SecondTransText.FontFamily = new FontFamily(FontList[secondFont.SelectedIndex]);
                Misaka.Settings.Legacy.Instance.appSettings.TF_secondTransTextFont = FontList[secondFont.SelectedIndex];
            };

            sourceFontSize.ValueChanged += delegate
            {
                translateWin.SourceTextFontSize = (int) sourceFontSize.Value;
                Misaka.Settings.Legacy.Instance.appSettings.TF_srcTextSize = sourceFontSize.Value;
            };

            firstFontSize.ValueChanged += delegate
            {
                translateWin.FirstTransText.FontSize = firstFontSize.Value;
                Misaka.Settings.Legacy.Instance.appSettings.TF_firstTransTextSize = firstFontSize.Value;
            };

            secondFontSize.ValueChanged += delegate
            {
                translateWin.SecondTransText.FontSize = secondFontSize.Value;
                Misaka.Settings.Legacy.Instance.appSettings.TF_secondTransTextSize = secondFontSize.Value;
            };

            OpacityBar.ValueChanged += delegate
            {
                translateWin.BackWinChrome.Opacity = OpacityBar.Value / 100;
                Misaka.Settings.Legacy.Instance.appSettings.TF_Opacity = OpacityBar.Value;
            };

            DropShadowCheckBox.Click += delegate
            {
                Misaka.Settings.Legacy.Instance.appSettings.TF_DropShadow = (bool)DropShadowCheckBox.IsChecked;
            };

            KanaCheckBox.Click += delegate
            {
                Misaka.Settings.Legacy.Instance.appSettings.TF_isKanaShow = (bool)KanaCheckBox.IsChecked;
            };

            HirakanaCheckBox.Click += delegate
            {
                Misaka.Settings.Legacy.Instance.appSettings.TF_Hirakana = (bool)HirakanaCheckBox.IsChecked;
            };

            KanaBoldCheckBox.Click += delegate
            {
                Misaka.Settings.Legacy.Instance.appSettings.TF_SuperBold = (bool)KanaBoldCheckBox.IsChecked;
            };

            ColorfulCheckBox.Click += delegate
            {
                Misaka.Settings.Legacy.Instance.appSettings.TF_Colorful = (bool)ColorfulCheckBox.IsChecked;
            };
          

        }

        /// <summary>
        /// UI初始化
        /// </summary>
        private void UI_Init()
        {
            BrushConverter brushConverter = new BrushConverter();
            BgColorBlock.Background = (Brush) brushConverter.ConvertFromString(Misaka.Settings.Legacy.Instance.appSettings.TF_BackColor);
            firstColorBlock.Background = (Brush) brushConverter.ConvertFromString(Misaka.Settings.Legacy.Instance.appSettings.TF_firstTransTextColor);
            secondColorBlock.Background = (Brush) brushConverter.ConvertFromString(Misaka.Settings.Legacy.Instance.appSettings.TF_secondTransTextColor);

            for (int i = 0; i < FontList.Count; i++)
            {
                if (Misaka.Settings.Legacy.Instance.appSettings.TF_srcTextFont == FontList[i])
                {
                    sourceFont.SelectedIndex = i;
                }

                if (Misaka.Settings.Legacy.Instance.appSettings.TF_firstTransTextFont == FontList[i])
                {
                    firstFont.SelectedIndex = i;
                }

                if (Misaka.Settings.Legacy.Instance.appSettings.TF_secondTransTextFont == FontList[i])
                {
                    secondFont.SelectedIndex = i;
                }
            }

            sourceFontSize.Value = Misaka.Settings.Legacy.Instance.appSettings.TF_srcTextSize;
            firstFontSize.Value = Misaka.Settings.Legacy.Instance.appSettings.TF_firstTransTextSize;
            secondFontSize.Value = Misaka.Settings.Legacy.Instance.appSettings.TF_secondTransTextSize;

            OpacityBar.Value = Misaka.Settings.Legacy.Instance.appSettings.TF_Opacity;
            DropShadowCheckBox.IsChecked = Misaka.Settings.Legacy.Instance.appSettings.TF_DropShadow;
            KanaCheckBox.IsChecked = Misaka.Settings.Legacy.Instance.appSettings.TF_isKanaShow;
            HirakanaCheckBox.IsChecked = Misaka.Settings.Legacy.Instance.appSettings.TF_Hirakana;
            KanaBoldCheckBox.IsChecked = Misaka.Settings.Legacy.Instance.appSettings.TF_SuperBold;
            ColorfulCheckBox.IsChecked = Misaka.Settings.Legacy.Instance.appSettings.TF_Colorful;
        }

        private void ChooseColorBtn_Click(object sender, RoutedEventArgs e)
        {
            var picker = HandyControl.Tools.SingleOpenHelper.CreateControl<HandyControl.Controls.ColorPicker>();
            var window = new HandyControl.Controls.PopupWindow
            {
                PopupElement = picker,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                MinWidth = 0,
                MinHeight = 0,
                Title = "选择颜色"
            };
            picker.Confirmed += delegate
            {
                if (sender == BgColorBtn)
                {
                    BgColorBlock.Background = picker.SelectedBrush;
                    translateWin.BackWinChrome.Background = picker.SelectedBrush;
                    Misaka.Settings.Legacy.Instance.appSettings.TF_BackColor = picker.SelectedBrush.ToString();
                }
                else if (sender == firstColorBtn)
                {
                    firstColorBlock.Background = picker.SelectedBrush;
                    translateWin.FirstTransText.Fill = picker.SelectedBrush;
                    Misaka.Settings.Legacy.Instance.appSettings.TF_firstTransTextColor = picker.SelectedBrush.ToString();
                }
                else if (sender == secondColorBtn)
                {
                    secondColorBlock.Background = picker.SelectedBrush;
                    translateWin.SecondTransText.Fill = picker.SelectedBrush;
                    Misaka.Settings.Legacy.Instance.appSettings.TF_secondTransTextColor = picker.SelectedBrush.ToString();
                }
                window.Close();
            };
            picker.Canceled += delegate
            {
                window.Close();
            };
            window.Show();
        }

        private void TransWinSettingsWin_Closed(object sender, EventArgs e)
        {
            translateWin.dtimer.Start();
        }
    }
}
