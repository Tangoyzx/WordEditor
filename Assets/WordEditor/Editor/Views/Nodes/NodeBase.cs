using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WordNodeEditor {
    public class NodeBase : ScriptableObject, IView {
        private static Color HIGHLIGHT_COLOR = new Color(0, 1, 1, 1);

        private static int __id = 0;
        private int _id = 0;
        protected WordNodeEditorWindow _window;
        protected Rect _rect;
        
        protected int _rectWidth;
        protected int _rectHeight;
        
        protected int _inputCount;
        protected int _outputCount;
        protected Dictionary<int, Connector> _connectorMap;
        protected NodeDataBase _data;
        public NodeDataBase data {
            get {return _data;}
        }

        private bool _movingNode;
        public void Init(WordNodeEditorWindow window, NodeDataBase data) {
            _window = window;
            _data = data;

            _movingNode = false;
            _rect = new Rect(0, 0, 96, 96);

            InitConnectors();
            ChildInit();
        }

        public void SetPos(Vector2 pos) {
            SetPos(pos.x, pos.y);
        }

        public void SetPos(float posX, float posY) {
            _rect.x = posX;
            _rect.y = posY;
            _data.areaPosition.x = posX;
            _data.areaPosition.y = posY;
        }

        public void Draw() {
            DrawHighlight();
            ChildDraw();
            DrawConnectors();
            UpdateNodeMove();
        }

        public Connector GetConnector(int id) {
            return _connectorMap[id];
        }

        public void Dispose() {
            ChildDispose();

            foreach(var connector in _connectorMap.Values) {
                connector.Dispose();
            }

            GameObject.DestroyImmediate(this);
       }

        virtual protected void ChildDraw() {

        }

        virtual protected void ChildInit() {
        }

        virtual protected void ChildDispose() {
        }
        
        protected void UpdateNodeMove() {
            if( GUIHelper.ReleasedLMB() && _movingNode) {
				_movingNode = false;
                Event.current.Use();
			}

            var isHover = isHovering();

            bool isDragging = _movingNode && (Event.current.type == EventType.MouseDrag);

            if (isDragging){
                SetPos(_rect.x + Event.current.delta.x, _rect.y + Event.current.delta.y);
                
                Event.current.Use();
            }

            if( GUIHelper.PressedLMB() && isHover) {
				_movingNode = true;
                _window.nodeArea.selectingNode = this;
                Event.current.Use();
			}
        }

        private bool isHovering() {
            return _rect.Contains(Event.current.mousePosition);
        }

        private void DrawHighlight() {
            if (_window.nodeArea.selectingNode != this) return;
            var orgColor = GUI.color;

            GUI.color = HIGHLIGHT_COLOR;
            var highlightRect = _rect;
            highlightRect.xMin -= 5;
            highlightRect.xMax += 5;
            highlightRect.yMin -= 5;
            highlightRect.yMax += 5;
            GUI.Box(highlightRect, "");

            GUI.color = orgColor;
        }

        private void DrawConnectors() {
            var inputIndex = 0;
            var outputIndex = 0;
            var posX = 0.0f;
            var posY = 0.0f;
            var inputOffset = _rect.height / (float)(_inputCount + 1);
            var outputOffset = _rect.height / (float)(_outputCount + 1);
            var inputHeight = inputOffset;
            var outputHeight = outputOffset;
            foreach(var connector in _connectorMap.Values) {
                var totalCount = 0;
                var curIndex = 0;
                if (connector.type == ConnectorType.Input) {                    
                    totalCount = _inputCount;
                    curIndex = inputIndex++;
                    connector.SetPos(new Vector2(_rect.x, inputHeight + _rect.y));
                    inputHeight += inputOffset;
                } else if (connector.type == ConnectorType.Output) {
                    totalCount = _outputCount;
                    curIndex = outputIndex++;
                    connector.SetPos(new Vector2(_rect.xMax, outputHeight + _rect.y));
                    outputHeight += outputOffset;
                }
                connector.Draw();
            }
        }

        private void InitConnectors() {
            _connectorMap = new Dictionary<int, Connector>();
            _inputCount = 0;
            _outputCount = 0;

            foreach(var connectorData in _data.connectorDataList) {
                AddConnector(connectorData);
            }
        }

        protected void AddConnector(ConnectorData connectorData) {
            if (connectorData.type == ConnectorType.Input) {
                _inputCount += 1;
            }
            if (connectorData.type == ConnectorType.Output) {
                _outputCount += 1;
            }
            var connector = ScriptableObject.CreateInstance<Connector>().Init(_window, this, connectorData);
            _connectorMap.Add(connectorData.id, connector);

            _rect.height = Mathf.Max(_inputCount, _outputCount) * 30.0f;
        }
    } 
}