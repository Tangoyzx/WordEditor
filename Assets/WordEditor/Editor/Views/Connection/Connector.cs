using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WordNodeEditor {
    public class Connector : ScriptableObject, IView {
        public static Connector connectingSource = null;

        
        private WordNodeEditorWindow _window;
        private NodeBase _node;
        public NodeBase node {
            get {return _node;}
        }
        private ConnectorData _data;
        public ConnectorData data {
            get {return _data;}
        }
        public ConnectorType type {
            get{ return _data.type;}
        }
        private Rect _rect;
        public Rect rect {
            get {return _rect;}
        }

        public Connector Init(WordNodeEditorWindow window, NodeBase node, ConnectorData data) {
            _window = window;
            _node = node;
            _data = data;

            _rect = new Rect(0, 0, 20, 20);

            return this;
        }

        public void SetPos(Vector2 pos) {
            _rect.y = pos.y - _rect.width * 0.5f;

            if (type == ConnectorType.Input) {
                _rect.x = pos.x - _rect.width;
            } else {
                _rect.x = pos.x;
            }
        }


        public void Draw() {
            var orgColor = GUI.color;

            if (connectingSource == this) {
                GUI.color = Color.blue;
            } else if (isHovering() && CanConnectToSource()){
                GUI.color = Color.blue;
            } else {
                GUI.color = Color.white;
            }

            GUI.DrawTexture(_rect, EditorGUIUtility.whiteTexture);

            if (type == ConnectorType.Output) {
                var nodeArea = _window.nodeArea;
                foreach(var connectorPair in _data.connectorPairList) {
                    var linkToConnector = nodeArea.GetNode(connectorPair.nodeId).GetConnector(connectorPair.connectorId);
                    GUIHelper.DrawLine(this._rect.center, linkToConnector.rect.center, Color.white);
                }
            }
            
            GUI.color = orgColor;

            if (isPressed()) {
                Event.current.Use();

                if (connectingSource == null) {
                    connectingSource = this;
                }
            }

            if (GUIHelper.ReleasedLMB()) {
                if (CanConnectToSource()) {
                    AddConnector(connectingSource);
                    connectingSource.AddConnector(this);
                    connectingSource = null;

                    Event.current.Use();
                }
            }
        }

        public void Dispose() {
            GameObject.DestroyImmediate(this);
        }

        public void AddConnector(Connector connector) {
            if (type == ConnectorType.Output) {
                _data.connectorPairList.Clear();
            }
            var pair = new ConnectorPair();
            pair.nodeId = connector.node.data.id;
            pair.connectorId = connector.data.id;
            
            _data.connectorPairList.Add(pair);
        }

        private bool isPressed() {
            return isHovering() && GUIHelper.PressedLMB();
        }

        private bool isReleased() {
            return isHovering() && GUIHelper.ReleasedLMB();
        }

        private bool isHovering() {
            if (!_window.nodeArea.MouseInNodeArea(true)) {
                return false;
            }
            return _rect.Contains(Event.current.mousePosition);
        }

        private bool CanConnectToSource() {
            if (connectingSource == null) return false;
            if (connectingSource.node == this._node) return false;
            if (connectingSource.type == this.type) return false;
            if (!isHovering()) return false;
            return true;
        }
    }
}