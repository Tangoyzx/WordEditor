using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WordNodeEditor {
    public class SimpleTalkPropertyView : PropertyViewBase {
        override public void Draw(NodeDataBase data, ref Rect rect) {
            var drawRect = rect;
            drawRect.height = 30;
            GUI.Label(drawRect, "单句节点");

            drawRect.y += 40;
            var simpleTalkData = (SimpleTalkData)data;
            var msg = GUI.TextArea(drawRect, simpleTalkData.msg);

            if (!msg.Equals((simpleTalkData.msg))) {
                simpleTalkData.msg = msg;
            }
        }
    }
}