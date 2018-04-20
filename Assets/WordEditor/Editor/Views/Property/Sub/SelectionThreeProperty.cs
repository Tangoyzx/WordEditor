using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WordNodeEditor {
    public class SelectionThreeProperty : PropertyViewBase {
        override public void Draw(NodeDataBase data, ref Rect rect) {
            var drawRect = rect;
            drawRect.height = 30;
            GUI.Label(drawRect, "三选项节点");

            
            var threeData = (SelectionThreeData)data;

            drawRect.y += 40;
            GUI.Label(drawRect, "第一选项");

            drawRect.y += 40;
            var msg = GUI.TextArea(drawRect, threeData.msg1);
            if (!msg.Equals((threeData.msg1))) {
                threeData.msg1 = msg;
            }

            drawRect.y += 40;
            GUI.Label(drawRect, "第二选项");

            drawRect.y += 40;
            msg = GUI.TextArea(drawRect, threeData.msg2);
            if (!msg.Equals((threeData.msg2))) {
                threeData.msg2 = msg;
            }

            drawRect.y += 40;
            GUI.Label(drawRect, "第三选项");

            drawRect.y += 40;
            msg = GUI.TextArea(drawRect, threeData.msg3);
            if (!msg.Equals((threeData.msg3))) {
                threeData.msg3 = msg;
            }
        }
    }
}