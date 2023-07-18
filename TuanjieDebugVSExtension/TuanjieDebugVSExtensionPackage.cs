global using System;
global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using Task = System.Threading.Tasks.Task;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;

namespace TuanjieDebugVSExtension
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.TuanjieDebugVSExtensionString)]
    [ProvideOptionPage(typeof(OptionsProvider.GeneralOptions), "TuanjieDebugExtension", "General", 0, 0, true, SupportsProfiles = true)]
    public sealed class TuanjieDebugVSExtensionPackage : ToolkitPackage
    {
        private static OutputWindowPane s_outputPane;

        private async Task CreateOutputWindowPaneAsync()
        {
            s_outputPane = await VS.Windows.CreateOutputWindowPaneAsync($"{nameof(TuanjieDebugVSExtension)} Output");
        }
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.RegisterCommandsAsync();
            await this.CreateOutputWindowPaneAsync();
        }

        public static void LogDebugOutput(string message)
        {
            ThreadHelper.JoinableTaskFactory.Run(async () => await LogDebugOutputAsync(message));
        }

        public static async Task LogDebugOutputAsync(string message)
        {
            await s_outputPane?.WriteLineAsync(message);
        }
    }
}