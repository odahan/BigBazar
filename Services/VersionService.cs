using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BigBazar.Services
{
    public class VersionService : IVersionService
    {
        readonly Assembly? assembly = Assembly.GetExecutingAssembly();

        private readonly AssemblyTitleAttribute? titleAttribute;
        private readonly AssemblyDescriptionAttribute? descriptionAttribute;
        private readonly AssemblyCompanyAttribute? companyAttribute;
        private readonly AssemblyProductAttribute? productAttribute;
        private readonly AssemblyCopyrightAttribute? copyrightAttribute;
        private readonly Version? assemblyVersion;


        public VersionService()
        {
            titleAttribute = assembly.GetCustomAttribute<AssemblyTitleAttribute>();
            descriptionAttribute = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>();
            companyAttribute = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();
            productAttribute = assembly.GetCustomAttribute<AssemblyProductAttribute>();
            copyrightAttribute = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();
            assemblyVersion = assembly.GetName().Version;
        }

        public string Title => titleAttribute?.Title ?? string.Empty;
        public string Description => descriptionAttribute?.Description ?? string.Empty;
        public string Company => companyAttribute?.Company ?? string.Empty;
        public string Product => productAttribute?.Product ?? string.Empty;
        public string Copyright => copyrightAttribute?.Copyright ?? string.Empty;
        public string Version => assemblyVersion?.ToString() ?? string.Empty;

    }
}
