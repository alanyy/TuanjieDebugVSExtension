using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace TuanjieDebugVSExtension
{
    internal class UnityProbe
    {
        private static readonly Regex _legacyProjectRegex = new Regex("Unity( [^-]*)?( - \\[.*\\])? - (?<scene>.*(\\.unity)|(Untitled)) - (?<project>.*) - (?<platform>[^-]*)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
        private static readonly Regex _projectRegex = new Regex("(?<project>.*) - (?<scene>.*) - (?<platform>[^-]*) - Tuanjie( [^-]*)?( - \\[.*\\])?", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

        public static IEnumerable<TuanjieProcess> GetUnityProcesses(
          string informationFormat = null)
        {
            return SafeProcess.GetProcesses().Where<Process>((Func<Process, bool>)(p => p.ProcessProperty<string>((Func<Process, string>)(_ => _.ProcessName)) == "Tuanjie")).Select<Process, TuanjieProcess>((Func<Process, TuanjieProcess>)(p => UnityProcessFor(p, informationFormat)));
        }

        public static string GetInformation(Process process, string informationFormat) => process == null || string.IsNullOrEmpty(informationFormat) ? string.Empty : informationFormat.Replace(string.Format("{{{0}}}", (object)InformationField.ProcessId), process.Id.ToString()).Replace(string.Format("{{{0}}}", (object)InformationField.ProcessWindowTitle), process.ProcessProperty<string>((Func<Process, string>)(p => p.MainWindowTitle)));

        public static TuanjieProcess UnityProcessFor(
          Process process,
          string informationFormat = null)
        {
            int num = process != null ? UnityProbe.GetDebuggerPort(process.Id) : throw new ArgumentNullException(nameof (process));
            string address = "127.0.0.1";
            string machine = Dns.GetHostName();
            int processId = process.Id;
            int debugPort = num;
            int messagerPort = num + 2;
            string projName = ExtractProject(process.ProcessProperty<string>((Func<Process, string>) (p => p.MainWindowTitle)));
            string info = GetInformation(process, informationFormat);
            bool isBackground = process.ProcessProperty<bool>((Func<Process, bool>)(p => p.MainWindowHandle == IntPtr.Zero));
            return new TuanjieProcess(projName, machine, 1, debugPort, info, isBackground, address);
        }

        public static int GetDebuggerPort(int processId) => 56000 + processId % 1000;

        public static string ExtractProject(string windowTitle)
        {
            string groupname = "project";
            if (string.IsNullOrEmpty(windowTitle))
                return string.Empty;
            Match match = _legacyProjectRegex.Match(windowTitle);
            if (!match.Success)
            {
                match = _projectRegex.Match(windowTitle);
            }
            return !match.Success ? string.Empty : ExtractProjectName(match.Groups[groupname].Value.Trim());
        }

        private static string ExtractProjectName(string unityProject)
        {
            if (unityProject.EndsWith("]", StringComparison.OrdinalIgnoreCase))
            {
                int length = unityProject.LastIndexOf('[');
                if (length > 0)
                    return unityProject.Substring(0, length).Trim();
            }
            return unityProject;
        }
    }
}
