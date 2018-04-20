using UnityEngine;
namespace WordNodeEditor {
    [System.Serializable]
    public class StartData : NodeDataBase {
        override protected void ChildInit() {
            this.type = NodeType.Start;
            connectorDataList = new ConnectorData[] {
                ScriptableObject.CreateInstance<ConnectorData>().Init(1, id, ConnectorType.Output),
            };
        }
    }
}