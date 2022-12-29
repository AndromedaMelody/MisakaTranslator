#include "pch.h"
#include "App.h"
#include "App.g.cpp"

//#include "MainPage.h"
#include "SettingsPage.h"

#include <winrt/Misaka.Settings.h>

namespace winrt::Misaka::Islands::implementation
{
    App::App()
    {
        Initialize();
    }
}

int WINAPI wWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPWSTR lpCmdLine, int nShowCmd)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    winrt::init_apartment(winrt::apartment_type::single_threaded);

    winrt::Misaka::Islands::App app = winrt::make<winrt::Misaka::Islands::implementation::App>();
    //winrt::Misaka::WinUI::MainPage mainPage = winrt::make<winrt::Misaka::WinUI::implementation::MainPage>();
    winrt::Misaka::Islands::SettingsPage mainPage = winrt::make<winrt::Misaka::Islands::implementation::SettingsPage>();

    auto appRes = winrt::Windows::ApplicationModel::Resources::ResourceLoader::GetForCurrentView(L"App");
    winrt::hstring windowName = appRes.GetString(L"MainWindow/Title");
    winrt::hstring executionMode = winrt::Misaka::Settings::Package::RunWithPackageId() ? appRes.GetString(L"MainWindow/ExecutionMode/Centennial") : appRes.GetString(L"MainWindow/ExecutionMode/Classic");
    std::wstring title = std::format(L"{0} ({1})", windowName, executionMode);

    HWND mainWindow = CreateWindowExW(WS_EX_LEFT, L"Mile.Xaml.ContentWindow", title.c_str(), WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, nullptr, nullptr, hInstance, winrt::get_abi(mainPage));
    if (!mainWindow)
    {
        return -1;
    }
    ShowWindow(mainWindow, nShowCmd);
    UpdateWindow(mainWindow);

    MSG msg;
    while (GetMessageW(&msg, nullptr, 0, 0))
    {
        if (msg.message == WM_SYSKEYDOWN && msg.wParam == VK_F4)
        {
            SendMessageW(GetAncestor(msg.hwnd, GA_ROOT), msg.message, msg.wParam, msg.lParam);
            continue;
        }
        TranslateMessage(&msg);
        DispatchMessageW(&msg);
    }

    app.Close();
    return static_cast<int>(msg.wParam);
}