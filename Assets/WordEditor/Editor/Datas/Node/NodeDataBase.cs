using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WordNodeEditor {
    [System.Serializable]
    public enum NodeType { 
        Start,
        SimpleTalk,
        SelectionTwo,
        SelectionThree
    }

    [System.Serializable]
    public class NodeDataBase : ScriptableObject{
        [SerializeField]
        public int id;
        [SerializeField]
        public NodeType type;
        [SerializeField]
        public ConnectorData[] connectorDataList;
        [SerializeField]
        public Vector2 areaPosition;

        public NodeDataBase Init(int id) {
            this.id = id;
            ChildInit();
            return this;
        }

        public void Dispose() {
            ChildDispose();

            foreach(var connectorData in connectorDataList) {
                connectorData.Dispose();
            }
            connectorDataList = null;

            GameObject.DestroyImmediate(this);
        }

        virtual protected void ChildInit() {   
        }

        virtual protected void ChildDispose() {

        }
    }
}