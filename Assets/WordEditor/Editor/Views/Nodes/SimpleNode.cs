using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WordNodeEditor {
    public class SimpleNode : NodeBase {
        protected override void ChildDraw() {
            DrawBox();
        }

        protected virtual void DrawBox() {
            GUI.Box(_rect, "");
        }
    }
}