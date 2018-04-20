using UnityEngine;

namespace WordNodeEditor {
    [System.Serializable]
    public class SimpleTalkData : NodeDataBase {
        [SerializeField]
        public string msg = "";

        override protected void ChildInit() {
            this.type = NodeType.SimpleTalk;
            connectorDataList = new ConnectorData[] {
                ScriptableObject.CreateInstance<ConnectorData>().Init(1, id, ConnectorType.Input),
                ScriptableObject.CreateInstance<ConnectorData>().Init(2, id, ConnectorType.Output)
            };
        }
    }
}