using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WordNodeEditor {
    [System.Serializable]
    public enum ConnectorType {
        Input,
        Output
    }

    [System.Serializable]
    public struct ConnectorPair {
        [SerializeField]
        public int nodeId;
        [SerializeField]
        public int connectorId;
    }

    [System.Serializable]
    public class ConnectorData : ScriptableObject {
        [SerializeField]
        public int id;
        [SerializeField]
        public int nodeId;
        [SerializeField]
        public ConnectorType type;
        [SerializeField]
        public List<ConnectorPair> connectorPairList;

        public ConnectorData Init(int id, int nodeId, ConnectorType type) {
            this.id = id;
            this.type = type;
            this.nodeId = nodeId;
            this.connectorPairList = new List<ConnectorPair>();
            return this;
        }

        public void Dispose() {
            connectorPairList = null;

            GameObject.DestroyImmediate(this);
        }
    }
}