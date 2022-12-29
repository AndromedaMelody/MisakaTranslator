#include "pch.h"
#include "SettingsPage.h"
#include "SettingsPage.g.cpp"
#include <winrt/Misaka.Settings.h>

namespace winrt::Misaka::Islands::implementation
{
    SettingsPage::SettingsPage()
    {
        InitializeComponent();
    }

    void SettingsPage::NavView_SelectionChanged(Windows::Foundation::IInspectable const& sender, Windows::UI::Xaml::Controls::NavigationViewSelectionChangedEventArgs const& args)
    {
        UNREFERENCED_PARAMETER(sender);

        if (args.SelectedItemContainer())
        {
            Windows::UI::Xaml::Interop::TypeName pageTypeName;
            hstring tag = unbox_value<hstring>(args.SelectedItemContainer().Tag());

            auto appRes = winrt::Windows::ApplicationModel::Resources::ResourceLoader::GetForCurrentView(L"SettingsPage");
            hstring pageName = appRes.GetString(std::format(L"{0}Item/Content", tag));
            hstring defaultHeader = appRes.GetString(L"NavigationView/Header");

            pageTypeName.Name = std::format(L"Misaka.Islands.{0}Page", tag);
            pageTypeName.Kind = Windows::UI::Xaml::Interop::TypeKind::Metadata;
            Windows::UI::Xaml::Media::Animation::SlideNavigationTransitionInfo pageTransitions;
            pageTransitions.Effect(Windows::UI::Xaml::Media::Animation::SlideNavigationTransitionEffect::FromBottom);

            ContentFrame().Navigate(pageTypeName, nullptr, pageTransitions);
            SettingsNavigation().Header(box_value(std::format(L"{0} - {1}", defaultHeader, pageName)));
        }
    }
}
