using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WordNodeEditor {
    public class EmptyPropertyView : PropertyViewBase {
        override public void Draw(NodeDataBase data, ref Rect rect) {
            var labelRect = rect;
            labelRect.height = 30;
            GUI.Label(labelRect, "未选中节点");
        }
    }
}