namespace TuanjieDebugVSExtension
{
    internal class TuanjieProcess
    {
        public string ProjectName{get;set;}
        public string Type{get;set;}
        public int Port{get;set;}
        public string Information{get;set;}

        public TuanjieProcess(string projName, string type, int port, string info)
        {
            ProjectName = projName;
            Type = type;
            Port = port;
            Information = info;
        }

        public UnityTools.UnityProcess ToUnityProcess()
        {
            int type = 0 == string.Compare(Type, "Editor", true) ? 1 : 16;
            return new UnityTools.UnityProcess(type, Port);
        }
    }
}
