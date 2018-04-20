using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
namespace WordNodeEditor {
    public static class NodeHelper {
        public static NodeDataBase GetNodeData(NodeType type) {
            if (type == NodeType.Start) {
                return ScriptableObject.CreateInstance<StartData>();
            } else if (type == NodeType.SimpleTalk) {
                return ScriptableObject.CreateInstance<SimpleTalkData>();
            } else if (type == NodeType.SelectionTwo) {
                return ScriptableObject.CreateInstance<SelectionTwoData>();
            } else if (type == NodeType.SelectionThree) {
                return ScriptableObject.CreateInstance<SelectionThreeData>();
            }
            return null;
        }

        public static NodeBase GetNode(NodeType type) {
            if (type == NodeType.Start) {
                return ScriptableObject.CreateInstance<StartNode>();
            } else if (type == NodeType.SimpleTalk) {
                return ScriptableObject.CreateInstance<SimpleTalkNode>();
            } else if (type == NodeType.SelectionTwo) {
                return ScriptableObject.CreateInstance<SelectionTwoNode>();
            } else if (type == NodeType.SelectionThree) {
                return ScriptableObject.CreateInstance<SelectionThreeNode>();
            }
            return null;
        }

        public static PropertyViewBase CreatePropertyView(NodeType type) {
            if (type == NodeType.Start) {
                return ScriptableObject.CreateInstance<StartPropertyView>();
            } else if (type == NodeType.SimpleTalk) {
                return ScriptableObject.CreateInstance<SimpleTalkPropertyView>();
            } else if (type == NodeType.SelectionTwo) {
                return ScriptableObject.CreateInstance<SelectionTwoProperty>();
            } else if (type == NodeType.SelectionThree) {
                return ScriptableObject.CreateInstance<SelectionThreeProperty>();
            }
            return null;
        }
    }
}