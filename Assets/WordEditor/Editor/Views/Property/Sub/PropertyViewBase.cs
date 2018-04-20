using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WordNodeEditor {
    public class PropertyViewBase : ScriptableObject {
        virtual public void Draw(NodeDataBase data, ref Rect rect) {
        }
    }
}