using Config.Net;
using Misaka.WPF;

namespace Misaka.Settings.Legacy
{
    public static class Instance
    {
        public static IAppSettings appSettings; //应用设置
        public static IRepeatRepairSettings repairSettings; //去重方法参数

        static Instance()
        {
            appSettings = new ConfigurationBuilder<IAppSettings>().UseIniFile($@"{Settings.Package.DataPath}\settings\settings.ini").Build();
            repairSettings = new ConfigurationBuilder<IRepeatRepairSettings>().UseIniFile($@"{Settings.Package.DataPath}\settings\repairSettings.ini").Build();
        }
    }
}
