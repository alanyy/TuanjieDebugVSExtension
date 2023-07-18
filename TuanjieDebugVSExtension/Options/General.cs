using System.ComponentModel;
using System.Runtime.InteropServices;

namespace TuanjieDebugVSExtension
{
    internal partial class OptionsProvider
    {
        // Register the options with this attribute on your package class:
        // [ProvideOptionPage(typeof(OptionsProvider.GeneralOptions), "TuanjieDebugVSExtension", "General", 0, 0, true, SupportsProfiles = true)]
        [ComVisible(true)]
        public class GeneralOptions : BaseOptionPage<General> { }
    }

    public class General : BaseOptionModel<General>
    {
        [Category("Tuanjie Process")]
        [DisplayName("Information Format")]
        [Description("Process information format.")]
        [DefaultValue(true)]
        public string InformationFormat { get; set; } = string.Format("PID:{{{0}}}", (object) InformationField.ProcessId);
    }
}
