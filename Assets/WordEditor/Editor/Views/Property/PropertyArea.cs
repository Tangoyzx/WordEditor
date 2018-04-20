using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WordNodeEditor {
    public class PropertyArea : ScriptableObject{
        private WordNodeEditorWindow _window;
        private CommonFunctionView _functionView;

        private Dictionary<NodeType, PropertyViewBase> _propertyView;
        private EmptyPropertyView _emptyPropertyView;
        

        public void Draw(ref Rect rect) {
            var functionRect = rect;
            functionRect.height = 60.0f;
            _functionView.Draw(ref functionRect);

            var propertyRect = rect;
            propertyRect.yMin += 80.0f;
            var selectingNode = _window.nodeArea.selectingNode;
            var propertyView = GetPropertyView(selectingNode);
            if (selectingNode == null) 
                propertyView.Draw(null, ref propertyRect);
            else {
                propertyView.Draw(selectingNode.data, ref propertyRect);
            }
        }

        public PropertyArea Init(WordNodeEditorWindow window) {
            _window = window;

            _functionView = ScriptableObject.CreateInstance<CommonFunctionView>().Init(_window);

            _propertyView = new Dictionary<NodeType, PropertyViewBase>();
            _emptyPropertyView = ScriptableObject.CreateInstance<EmptyPropertyView>();

            return this;
        }

        public void Dispose() {
            foreach(var propertyView in _propertyView.Values) {
                GameObject.DestroyImmediate(propertyView);
            }
            _propertyView = null;

            GameObject.DestroyImmediate(_functionView);

            GameObject.DestroyImmediate(this);
        }

        private PropertyViewBase GetPropertyView(NodeBase nodeBase) {
            if (nodeBase == null) return _emptyPropertyView;
            var type = nodeBase.data.type;
            PropertyViewBase propertyView;
            if (_propertyView.TryGetValue(type, out propertyView)) return propertyView;
            propertyView = NodeHelper.CreatePropertyView(type);
            _propertyView.Add(type, propertyView);
            return propertyView;
        }
        
    }
}