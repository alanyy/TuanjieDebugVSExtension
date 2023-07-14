using System.Linq;

namespace TuanjieDebugVSExtension
{
    [Command(PackageIds.CmdIdAttachAndDebug)]
    internal sealed class AttachDebuggerCommand : BaseCommand<AttachDebuggerCommand>
    {
        private string _informationFormat = string.Format("PID:{{{0}}}", (object) InformationField.ProcessId);
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            try
            {
                var tuanjieProcesses = UnityProbe.GetUnityProcesses(_informationFormat);
                var unityProcess = tuanjieProcesses.SingleOrDefault();

                UnityTools.DebuggerEngineFactory.LaunchDebugger(UnityTools.UnityPackage.GetCurrent(), unityProcess);
            }
            catch (Exception ex)
            {
                await TuanjieDebugVSExtensionPackage.LogDebugOutputAsync($"[TuanjieDebugExtension] {ex}");
            }
        }
    }
}
