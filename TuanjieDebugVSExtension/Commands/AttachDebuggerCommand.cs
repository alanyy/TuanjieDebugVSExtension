namespace TuanjieDebugVSExtension
{
    [Command(PackageIds.CmdIdAttachAndDebug)]
    internal sealed class AttachDebuggerCommand : BaseCommand<AttachDebuggerCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await VS.MessageBox.ShowWarningAsync("AttachDebuggerCommand", "Button clicked");
        }
    }
}
