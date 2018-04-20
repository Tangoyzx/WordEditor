using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WordNodeEditor {
    public class SelectionTwoProperty : PropertyViewBase {
        override public void Draw(NodeDataBase data, ref Rect rect) {
            var drawRect = rect;
            drawRect.height = 30;
            GUI.Label(drawRect, "双选项节点");

            
            var twoData = (SelectionTwoData)data;

            drawRect.y += 40;
            GUI.Label(drawRect, "第一选项");

            drawRect.y += 40;
            var msg = GUI.TextArea(drawRect, twoData.msg1);
            if (!msg.Equals((twoData.msg1))) {
                twoData.msg1 = msg;
            }

            drawRect.y += 40;
            GUI.Label(drawRect, "第二选项");

            drawRect.y += 40;
            msg = GUI.TextArea(drawRect, twoData.msg2);
            if (!msg.Equals((twoData.msg2))) {
                twoData.msg2 = msg;
            }
        }
    }
}