using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WordNodeEditor {
    public class SimpleTalkNode : SimpleNode {

        protected override void DrawBox() {
            GUI.Box(_rect, "单句剧情");

            var drawRect = _rect;
            drawRect.yMin = _rect.yMin + 30;
            GUI.Label(drawRect, ((SimpleTalkData)data).msg);
        }

        protected override void ChildInit() {
            _rect.height = 96.0f;
        }
    }
}