#include "Package.h"
#include "Package.g.cpp"

#include <filesystem>
#include <Windows.h>
#include <combaseapi.h>
#include <winrt/Windows.ApplicationModel.h>
#include <winrt/Windows.Storage.h>

namespace winrt::Misaka::Settings::implementation
{
    bool Package::RunWithPackageId()
    {
        static const bool cache = []()->bool
        {
            try
            {
                const auto _ = winrt::Windows::ApplicationModel::Package::Current();
            }
            catch (...)
            {
                return false;
            }
            return true;
        }();
        return cache;
    }

    winrt::hstring Package::ProgramPath()
    {
        static const winrt::hstring cache = []()->winrt::hstring
        {
            if (RunWithPackageId())
            {
                return winrt::Windows::ApplicationModel::Package::Current().InstalledLocation().Path();
            }
            else
            {
                std::wstring path(MAXINT16 + 1, L'\0');
                path.resize(::GetModuleFileNameW(nullptr, path.data(), static_cast<DWORD>(path.length())));
                return path.c_str();
            }
        }();
        return cache;
    }

    winrt::hstring Package::DataPath()
    {
        static const winrt::hstring cache = []()->winrt::hstring
        {
            if (RunWithPackageId())
            {
                return winrt::Windows::Storage::AppDataPaths::GetDefault().LocalAppData();
            }
            else
            {
                return std::format(L"{}\\Data", ProgramPath()).c_str();
            }
        }();
        return cache;
    }

    winrt::hstring Package::TemporaryPath()
    {
        static const winrt::hstring cache = []()->winrt::hstring
        {
            if (RunWithPackageId())
            {
                return winrt::Windows::Storage::ApplicationData::Current().TemporaryFolder().Path();
            }
            else
            {
                GUID guid{};
                winrt::check_hresult(::CoCreateGuid(&guid));
                return std::format(L"{0}\\Misaka_{1:X}-{2:X}-{3:X}-{4:X}{5:X}{6:X}{7:X}{8:X}{9:X}{10:X}{11:X}", std::filesystem::temp_directory_path().wstring(), guid.Data1, guid.Data2, guid.Data3, guid.Data4[0], guid.Data4[1], guid.Data4[2], guid.Data4[3], guid.Data4[4], guid.Data4[5], guid.Data4[6], guid.Data4[7]).c_str();
            }
        }();
        return cache;
    }
}
