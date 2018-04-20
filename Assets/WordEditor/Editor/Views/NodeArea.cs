using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WordNodeEditor {
    public class NodeArea : ScriptableObject, IView{
        public NodeBase selectingNode;
        private List<NodeBase> _nodes;
        private Rect _screenRect;
        private Vector2 _offset;
        private float _zoom;
        private float _zoomTarget;

        private bool _movingCam;

        private NodeAreaData _data;
        public NodeAreaData data {
            get {return _data;}
        }

        private WordNodeEditorWindow _window;
        private Dictionary<int, NodeBase> _idMap;

        private GenericMenu _menu;

        public void Init(WordNodeEditorWindow window) {
            _window = window;

            _nodes = new List<NodeBase>();
            _idMap = new Dictionary<int, NodeBase>();
            
            _zoom = 1;
            _zoomTarget = 1;
            _offset = new Vector2(32768 - 400, 32768 - 300);

            _movingCam = false;

            InitAddMenu();
        }

        public void SetupData(NodeAreaData data) {
            ClearNodes();
            if(_data != null) _data.Dispose();
            _data = data;
            foreach(var nodeData in data.nodeDataList) {
                AddNode(nodeData);
            }
            CenterOffset();
        }

        public void SetupArea(Rect rect) {
            _screenRect = rect;
        }

        public void AddNode(NodeDataBase data) {
            var node = NodeHelper.GetNode(data.type);
            node.Init(_window, data);
            node.SetPos(data.areaPosition);
            _nodes.Add(node);
            _idMap.Add(data.id, node);
        }

        public NodeBase GetNode(int id) {
            return _idMap[id];
        }

        public void SetZoom(float setZoom){
            Vector2 oldWidth = new Vector2(_screenRect.width,_screenRect.height)/_zoom;
			_zoom = ClampZoom(setZoom);
			Vector2 newWidth = new Vector2(_screenRect.width,_screenRect.height)/_zoom;
			Vector2 delta = newWidth - oldWidth;

            Vector2 normalizedMouseCoords = (Event.current.mousePosition - new Vector2(_screenRect.x, _screenRect.y));

			normalizedMouseCoords.x /= _screenRect.width;
			normalizedMouseCoords.y /= _screenRect.height;


			_offset -= Vector2.Scale(delta, normalizedMouseCoords);

			if(_zoom == 1f)
				SnapOffset();
        }
        
        public void Draw() {
            ZoomHelper.Begin(_zoom, _screenRect, _offset);
            {
                foreach(var node in _nodes) {
                    node.Draw();
                }

                UpdateCameraMove();
            }

            if (GUIHelper.ReleasedLMB()) {
                Connector.connectingSource = null;
            }

            if (Connector.connectingSource != null) {
                GUIHelper.DrawLine(Connector.connectingSource.rect.center, Event.current.mousePosition, Color.blue);
            }

            ZoomHelper.End(_zoom);

            UpdateCameraZoom();
            UpdateCreate();
        }

        public void CenterOffset() {
            _offset = new Vector2(32768 - 400, 32768 - 300);
        }

        public Vector2 ZoomSpaceToScreenSpace(Vector2 in_vec) {
            return (in_vec - _offset + _screenRect.TopLeft()) * _zoom + _screenRect.TopLeft() + (Vector2.up * (WordNodeEditorWindow.TabOffset)) *(_zoom - 1);
        }

        public Vector2 ScreenSpaceToZoomSpace(Vector2 in_vec) {
            return ( in_vec - (Vector2.up * (WordNodeEditorWindow.TabOffset))*(_zoom-1) - _screenRect.TopLeft() ) / _zoom - _screenRect.TopLeft() + _offset;
        }

        public bool MouseInNodeArea(bool transformToScreenSpace = false) {
            var pos = Event.current.mousePosition;
            if (transformToScreenSpace)
                pos = ZoomSpaceToScreenSpace(pos);
            return _screenRect.Contains(pos);
        }

        private void UpdateCameraMove() {
            if( GUIHelper.ReleasedCameraMove() ) {
				_movingCam = false;
			}


            bool isDragging = _movingCam && (Event.current.type == EventType.MouseDrag) && MouseInNodeArea(true);

            if (isDragging){
                _offset -= Event.current.delta;
                SnapOffset();
                GUI.FocusControl("null");

                Event.current.Use();
            }

            if( GUIHelper.PressedCameraMove() ) {
				_movingCam = true;
                selectingNode = null;
			}
        }

        private void UpdateCameraZoom() {
            if (Event.current.type == EventType.ScrollWheel){
				_zoomTarget = ClampZoom(_zoomTarget * (1f-Event.current.delta.y*0.02f));
			}

            SetZoom(Mathf.Lerp(_zoom, _zoomTarget, 0.2f));
        }

        void SnapOffset(){
			_offset.x = Mathf.Round(_offset.x);
			_offset.y = Mathf.Round(_offset.y);
		} 

        private float ClampZoom(float zoom) {
            return Mathf.Clamp(zoom, 0.125f, 1f);
        }

        private void ClearNodes() {
            selectingNode = null;
            Connector.connectingSource = null;

            foreach(var node in _nodes) {
                node.Dispose();
            }
            _nodes.Clear();
            _idMap.Clear();
        }

        private void UpdateCreate() {
            if (GUIHelper.ReleasedRMB()) {
                _menu.ShowAsContext();
            }
        }

        private int GetUniqueId() {
            var i = 99;
            while(i-- > 0) {
                var newId = Random.Range(100000, 999999);
                if (!_idMap.ContainsKey(newId)) return newId;
            }
            return -1;
        }

        private void InitAddMenu() {
            _menu = new GenericMenu();
            _menu.AddItem(new GUIContent("剧情/单句剧情"), false, CreateNode, NodeType.SimpleTalk);
            _menu.AddItem(new GUIContent("选项/双选项"), false, CreateNode, NodeType.SelectionTwo);
            _menu.AddItem(new GUIContent("选项/三选项"), false, CreateNode, NodeType.SelectionThree);
        }

        private void CreateNode(object msg) {
            var nodeType = (NodeType)msg;
            var data = NodeHelper.GetNodeData(nodeType);
            data.Init(GetUniqueId());
            
            var areaPos = _offset;
            data.areaPosition = areaPos;
            _data.nodeDataList.Add(data);
            AddNode(data);
        }
    } 
}