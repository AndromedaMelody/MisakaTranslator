#pragma once

#include "MainPage.g.h"

namespace winrt::Misaka::Islands::implementation
{
    struct MainPage : MainPageT<MainPage>
    {
    public:
        MainPage();
    };
}

namespace winrt::Misaka::Islands::factory_implementation
{
    struct MainPage : MainPageT<MainPage, implementation::MainPage> {};
}
