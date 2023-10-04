using Opc.UaFx;
using Opc.UaFx.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetOPCServer
{
    public class NodeManager: OpcNodeManager
    {

        public NodeManager()
        : base("http://mynamespace/machine")
        {
        }

        protected override IEnumerable<IOpcNode> CreateNodes(OpcNodeReferenceCollection references)
        {
            // Define custom root node.
            var machineOne = new OpcFolderNode(new OpcName("Machine", this.DefaultNamespaceIndex));

            // Add custom root node to the Objects-Folder (the root of all server nodes):
            references.Add(machineOne, OpcObjectTypes.ObjectsFolder);

            // Add custom sub node beneath of the custom root node:
            var alarm = new OpcDataVariableNode<bool>(machineOne, "Alarm");
            var reset = new OpcDataVariableNode<bool>(machineOne, "Reset");
            var temperature = new OpcDataVariableNode<double>(machineOne, "Temperature");
            
          

            // Return each custom root node using yield return.
            yield return machineOne;
        }
    }
}
