using UnityEngine;

namespace WordNodeEditor {
    [System.Serializable]
    public class SelectionThreeData : NodeDataBase {
        [SerializeField]
        public string msg1 = "";
        [SerializeField]
        public string msg2 = "";
        [SerializeField]
        public string msg3 = "";

        override protected void ChildInit() {
            this.type = NodeType.SelectionThree;
            connectorDataList = new ConnectorData[] {
                ScriptableObject.CreateInstance<ConnectorData>().Init(1, id, ConnectorType.Input),
                ScriptableObject.CreateInstance<ConnectorData>().Init(2, id, ConnectorType.Output),
                ScriptableObject.CreateInstance<ConnectorData>().Init(3, id, ConnectorType.Output),
                ScriptableObject.CreateInstance<ConnectorData>().Init(4, id, ConnectorType.Output)
            };
        }
    }
}