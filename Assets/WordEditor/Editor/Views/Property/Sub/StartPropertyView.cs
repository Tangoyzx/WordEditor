using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WordNodeEditor {
    public class StartPropertyView : PropertyViewBase {
        override public void Draw(NodeDataBase data, ref Rect rect) {
            var labelRect = rect;
            labelRect.height = 30;
            GUI.Label(labelRect, "开始节点");
        }
    }
}