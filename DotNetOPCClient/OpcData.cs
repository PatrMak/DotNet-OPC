using Opc.UaFx.Client;


namespace DotNetOPCClient
{
    public class OpcData
    {
        OpcClient client;
        string folderNode;
        public OpcData(string opcAdress, string folderNode)
        {
            client = new OpcClient(opcAdress);
            client.Connect();
            this.folderNode = folderNode;
        }

        public void Disconnet()
        {
            if (client != null)
                client.Disconnect();
        }



        public void Reset(bool reset, string variableName)
        {
            string nodeName = folderNode +variableName;
            client.WriteNode(nodeName, reset);
        }

        public bool Alarm(string variableName)
        {
            string nodeName = folderNode+ variableName;
            var status = client.ReadNode(nodeName);
            return (bool)status.Value;
        }

        public double Temperature(string variableName)
        {
            string nodeName = folderNode + variableName;
            var temperature = client.ReadNode(nodeName);
            return (double)temperature.Value;
        }
    }
}
