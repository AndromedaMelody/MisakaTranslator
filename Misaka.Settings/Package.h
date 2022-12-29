#pragma once

#include "Package.g.h"

namespace winrt::Misaka::Settings
{
    namespace implementation
    {
        class Package : public PackageT<Package>
        {
        public:
            Package() = delete;
            static bool RunWithPackageId();
            static winrt::hstring ProgramPath();
            static winrt::hstring DataPath();
            static winrt::hstring TemporaryPath();
        };
    }

    namespace factory_implementation
    {
        class Package : public PackageT<Package, implementation::Package> {};
    }
}
