using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WordNodeEditor{
    [System.Serializable]
    public class NodeAreaData : ScriptableObject {
        [SerializeField]
        public List<NodeDataBase> nodeDataList;

        public NodeAreaData Init() {
            var startData = ScriptableObject.CreateInstance<StartData>().Init(1);
            startData.areaPosition = new Vector2(32768 - 600, 32768 - 300);
            
            nodeDataList = new List<NodeDataBase>();
            nodeDataList.Add(startData);
            return this;
        }

        public void Dispose() {
            return;
            foreach(var nodeData in nodeDataList) {
                nodeData.Dispose();
            }
            nodeDataList = null;
            GameObject.DestroyImmediate(this);
        }
    }
}