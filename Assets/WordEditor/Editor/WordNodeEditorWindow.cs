using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace WordNodeEditor {
	public class WordNodeEditorWindow : EditorWindow {
		private static WordNodeEditorWindow _instance;

		private DraggableSeparator _separatorLeft;
		public DraggableSeparator separatorLeft {
			get {return _separatorLeft;}
		}

		private NodeArea _nodeArea;
		public NodeArea nodeArea {
			get {return _nodeArea;}
		}

		private PropertyArea _propertyArea;
		public PropertyArea propertyArea{
			get {return _propertyArea;}
		}


		[MenuItem("WordNodeEditor/Window")]
		public static void Open() {
			Init();
		}

		public static void Init() {
			var instance = EditorWindow.GetWindow<WordNodeEditorWindow>();
			instance.InitializeInstance();
		}

		public void InitializeInstance() {
			InitSeparators();
			InitNodeArea();
			InitPropertyArea();

			NewData();
		}

		public void SaveData() {
			var savePath = EditorUtility.SaveFilePanel(
				"保存剧情数据",
				"Assets",
				"WordEditorData",
				"asset"
			);
			savePath = savePath.Substring(Application.dataPath.Length - 6);
			AssetDatabase.CreateAsset(nodeArea.data, savePath);
		}

		public void NewData() {
			var newAreaData = ScriptableObject.CreateInstance<NodeAreaData>().Init();
			nodeArea.SetupData(newAreaData);
		}

		public void OpenData() {
			var path = EditorUtility.OpenFilePanelWithFilters(
				"保存剧情数据",
				"Assets",
				new string[] {"剧情数据", "asset"}
			);
			path = path.Substring(Application.dataPath.Length - 6);
			var data = AssetDatabase.LoadAssetAtPath<NodeAreaData>(path);
			nodeArea.SetupData(data);
		}

		public const int TabOffset = 22;

		void OnEnabled(){
			_instance = this;
		}

		void OnDisabled() {
			_instance = null;
		}

		void Update(){
			if (focusedWindow == this)
				Repaint();
		}

		void OnGUI() {
			var fullRect = new Rect(0, 0, Screen.width,Screen.height);
			
			UpdateSeparators(ref fullRect);
			UpdateNodeArea(ref fullRect);
			UpdatePropertyArea(ref fullRect);
		}

		private void UpdateSeparators(ref Rect fullRect) {
			_separatorLeft.MinX = 320;
			_separatorLeft.MaxX = (int)( fullRect.width / 2f - _separatorLeft.rect.width );
			_separatorLeft.SetupY((int)fullRect.y, (int)fullRect.height);
			_separatorLeft.Draw();
		}

		private void InitSeparators(){
			_separatorLeft = ScriptableObject.CreateInstance<DraggableSeparator>();
			_separatorLeft.Init(this);
			_separatorLeft.rect = new Rect(320, 0, 0, 0);
		}

		private void UpdateNodeArea(ref Rect fullRect) {
			var areaRect = new Rect(fullRect);
			areaRect.x = _separatorLeft.rect.xMax;
			areaRect.width = fullRect.width - _separatorLeft.rect.xMax;
			areaRect.xMin += 5;
			areaRect.yMin += TabOffset + 5;
			areaRect.xMax -= 5;
			areaRect.yMax -= 5;
			_nodeArea.SetupArea(areaRect);

			_nodeArea.Draw();
		}

		private void InitNodeArea() {
			_nodeArea = ScriptableObject.CreateInstance<NodeArea>();
			_nodeArea.Init(this);
		}

		private void InitPropertyArea() {
			_propertyArea = ScriptableObject.CreateInstance<PropertyArea>().Init(this);
		}

		private void UpdatePropertyArea(ref Rect fullRect) {
			var propertyRect = fullRect;
			propertyRect.xMax = _separatorLeft.rect.x;
			_propertyArea.Draw(ref propertyRect);
		}

	}
}
