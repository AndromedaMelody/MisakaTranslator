#pragma once

#include "SettingsPage.g.h"

namespace winrt::Misaka::Islands::implementation
{
    struct SettingsPage : SettingsPageT<SettingsPage>
    {
    public:
        SettingsPage();
        void NavView_SelectionChanged(Windows::Foundation::IInspectable const& sender, Windows::UI::Xaml::Controls::NavigationViewSelectionChangedEventArgs const& args);
    };
}

namespace winrt::Misaka::Islands::factory_implementation
{
    struct SettingsPage : SettingsPageT<SettingsPage, implementation::SettingsPage> {};
}
