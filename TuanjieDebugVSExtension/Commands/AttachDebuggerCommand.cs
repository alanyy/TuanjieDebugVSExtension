using System.Windows;

namespace TuanjieDebugVSExtension
{
    [Command(PackageIds.CmdIdAttachAndDebug)]
    internal sealed class AttachDebuggerCommand : BaseCommand<AttachDebuggerCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            TuanjieSelectorDialog dialog = new TuanjieSelectorDialog();
            await dialog.ShowDialogAsync();
        }
    }
}
