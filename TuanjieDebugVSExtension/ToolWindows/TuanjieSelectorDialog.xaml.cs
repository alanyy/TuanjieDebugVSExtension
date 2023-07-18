using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.PlatformUI;

namespace TuanjieDebugVSExtension
{
    public partial class TuanjieSelectorDialog : DialogWindow
    {
        List<TuanjieProcess> tuanjieProcesses;
        public TuanjieSelectorDialog()
        {
            InitializeComponent();
            GetData();
        }

        private void Refresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GetData();
        }

        private void OK_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (DataGrid.SelectedIndex != -1)
                {
                    var tuanjieProcess = tuanjieProcesses[DataGrid.SelectedIndex];
                    var unityProcess = tuanjieProcess.ToUnityProcess();
                    UnityTools.DebuggerEngineFactory.LaunchDebugger(UnityTools.UnityPackage.GetCurrent(), unityProcess);
                }
            }
            catch (Exception ex)
            {
                TuanjieDebugVSExtensionPackage.LogDebugOutput($"[TuanjieDebugExtension] {ex}");
            }
            finally
            {
                this.Close();
            }
        }

        private void CancelBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void GetData()
        {
            try
            {
                tuanjieProcesses = UnityProbe.GetUnityProcesses(General.Instance.InformationFormat).ToList();
                DataGrid.ItemsSource = tuanjieProcesses;
            }
            catch (Exception ex)
            {
                TuanjieDebugVSExtensionPackage.LogDebugOutput($"[TuanjieDebugExtension] {ex}");
            }
        }
    }
}
