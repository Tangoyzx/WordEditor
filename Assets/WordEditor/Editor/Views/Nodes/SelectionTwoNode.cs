using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WordNodeEditor {
    public class SelectionTwoNode : SimpleNode {

        protected override void DrawBox() {
            GUI.Box(_rect, "双选项");

            var mData = (SelectionTwoData)data;

            var drawRect = _rect;
            drawRect.height = 30;

            drawRect.y = drawRect.y + 26;
            GUI.Label(drawRect, mData.msg1);

            drawRect.y = drawRect.y + 32;
            GUI.Label(drawRect, mData.msg2);
        }

        protected override void ChildInit() {
            _rect.height = 96.0f;
        }
    }
}