#pragma once
#include "App.g.h"

namespace winrt::Misaka::Islands::implementation
{
    template <typename D, typename... I>
    struct App_baseWithProvider : public App_base<D, ::winrt::Windows::UI::Xaml::Markup::IXamlMetadataProvider>
    {
    public:
        ::winrt::Windows::UI::Xaml::Markup::IXamlType GetXamlType(::winrt::Windows::UI::Xaml::Interop::TypeName const& type)
        {
            return AppProvider()->GetXamlType(type);
        }

        ::winrt::Windows::UI::Xaml::Markup::IXamlType GetXamlType(::winrt::hstring const& fullName)
        {
            return AppProvider()->GetXamlType(fullName);
        }

        ::winrt::com_array<::winrt::Windows::UI::Xaml::Markup::XmlnsDefinition> GetXmlnsDefinitions()
        {
            return AppProvider()->GetXmlnsDefinitions();
        }

    private:
        bool _contentLoaded{ false };
        ::winrt::com_ptr<XamlMetaDataProvider> _appProvider;
        ::winrt::com_ptr<XamlMetaDataProvider> AppProvider()
        {
            if (!_appProvider)
            {
                _appProvider = ::winrt::make_self<XamlMetaDataProvider>();
            }
            return _appProvider;
        }
    };

    class App : public App_baseWithProvider<App>
    {
    public:
        App();
    };
}

namespace winrt::Misaka::Islands::factory_implementation
{
    class App : public AppT<App, implementation::App> {};
}

int WINAPI wWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPWSTR lpCmdLine, int nShowCmd);