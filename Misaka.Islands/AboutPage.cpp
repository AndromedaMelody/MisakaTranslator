#include "pch.h"
#include "AboutPage.h"
#if __has_include("AboutPage.g.cpp")
#include "AboutPage.g.cpp"
#endif

namespace winrt::Misaka::Islands::implementation
{
    AboutPage::AboutPage()
    {
        InitializeComponent();
    }

    void AboutPage::OpenLink_Click(Windows::Foundation::IInspectable const& sender, Windows::UI::Xaml::RoutedEventArgs const& e)
    {
        UNREFERENCED_PARAMETER(e);

        hstring tag = unbox_value<hstring>(unbox_value<Windows::UI::Xaml::FrameworkElement>(sender).Tag());
        Windows::Foundation::Uri uri(std::format(L"https://github.com/hanmin0822/MisakaTranslator/{0}", tag));
        Windows::System::Launcher::LaunchUriAsync(uri);
    }
}
