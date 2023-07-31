namespace TuanjieDebugVSExtension
{
    internal class TuanjieProcess
    {
        public string ProjectName { get; set; }

        public string Machine { get; set;}
        public string Type { get; set; }
        public int Port { get; set; }
        public string Information { get; set; }

        private bool IsBackground { get; set; }
        private int processType { get; set; }

        private string Address { get; set; }

        public TuanjieProcess(string projName, string machine, int type, int port, string info, bool isBackground, string address)
        {
            ProjectName = projName;
            Machine = machine;
            processType = type;
            Port = port;
            Information = info;
            IsBackground = isBackground;
            Address = address;
            Type = ((processType == 1) ? @"Editor" : "Player") + (IsBackground ? @" (background)" : string.Empty);
        }

        public UnityTools.UnityProcess ToUnityProcess()
        {
            //return new UnityTools.UnityProcess(processType, Port, Address);
            return new UnityTools.UnityProcess(Port, Address, Machine, Port + 2, ProjectName, Information, processType, IsBackground);
        }
    }
}
