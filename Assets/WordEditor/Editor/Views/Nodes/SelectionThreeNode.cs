using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WordNodeEditor {
    public class SelectionThreeNode : SimpleNode {

        protected override void DrawBox() {
            GUI.Box(_rect, "三选项");

            var mData = (SelectionThreeData)data;

            var drawRect = _rect;
            drawRect.height = 30;

            drawRect.y = drawRect.y + 26;
            GUI.Label(drawRect, mData.msg1);

            drawRect.y = drawRect.y + 32;
            GUI.Label(drawRect, mData.msg2);

            drawRect.y = drawRect.y + 32;
            GUI.Label(drawRect, mData.msg3);
        }

        protected override void ChildInit() {
            _rect.height = 128.0f;
        }
    }
}