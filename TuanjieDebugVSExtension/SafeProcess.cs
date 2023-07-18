using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace TuanjieDebugVSExtension
{
    internal static class SafeProcess
    {
        public static IEnumerable<Process> GetProcesses()
        {
            try
            {
                return (IEnumerable<Process>)Process.GetProcesses();
            }
            catch (Win32Exception)
            {
                return (IEnumerable<Process>)Array.Empty<Process>();
            }
        }

        public static T ProcessProperty<T>(this Process process, Func<Process, T> accessor)
        {
            try
            {
                return accessor(process);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
