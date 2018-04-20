using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WordNodeEditor {
    public class CommonFunctionView : ScriptableObject {
        private WordNodeEditorWindow _window;

        public void Draw(ref Rect rect) {
            var buttonRect = rect;
            var buttonWidth = (buttonRect.width - 20) / 3.0f;

            buttonRect.width = buttonWidth;
            if (GUI.Button(buttonRect, "新建")) {
                _window.NewData();
            }

            buttonRect.x += buttonWidth + 10;
            if (GUI.Button(buttonRect, "打开")) {
                _window.OpenData();
            }

            buttonRect.x += buttonWidth + 10;
            if (GUI.Button(buttonRect, "保存")) {
                _window.SaveData();
            }
        }

        public CommonFunctionView Init(WordNodeEditorWindow window) {
            _window = window;
            return this;
        }
    }
}