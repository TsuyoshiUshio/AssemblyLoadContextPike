
using FrameworkLib;
using PluginBase;

namespace ReferenceNetFrameworkPlugin
{
    public class ReferenceNetFrameworkPlugin : ICommand
    {
        public string Name  { get => "refnet";  }

    public string Description  { get => "Reference .NET Framework Lib";  }

    public int Execute()
        {
            new NetFrameworkSample().Execute();
            return 0;
        }
    }
}