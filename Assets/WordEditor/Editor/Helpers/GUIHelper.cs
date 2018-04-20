using UnityEditor;
using UnityEngine;

namespace WordNodeEditor {
    public static class GUIHelper {
        public static bool PressedLMB( Rect r ) {
            return ( PressedLMB() && r.Contains(Event.current.mousePosition));
        }

        public static bool PressedLMB() {
            return ( Event.current.type == EventType.MouseDown ) && ( Event.current.button == 0 );
        }

        public static bool ReleasedLMB() {
            return ( Event.current.type == EventType.MouseUp ) && ( Event.current.button == 0 );
        }

        public static bool PressedMMB() {
            return ( Event.current.type == EventType.MouseDown ) && ( Event.current.button == 2 );
        }

        public static bool ReleasedRawMMB() {
            return ( Event.current.rawType == EventType.MouseUp ) && ( Event.current.button == 2 );
        }

        public static bool ReleasedRawLMB() {
            return ( Event.current.rawType == EventType.MouseUp ) && ( Event.current.button == 0 );
        }

        public static bool ReleasedRawRMB() {
            return ( Event.current.rawType == EventType.MouseUp ) && ( Event.current.button == 1 );
        }

        public static bool PressedRMB() {
            return ( Event.current.type == EventType.MouseDown ) && ( Event.current.button == 1 );
        }

        public static bool ReleasedRMB() {
            return ( Event.current.type == EventType.MouseUp ) && ( Event.current.button == 1 );
        }

        public static void AssignCursor( Rect r, MouseCursor cursor ) {
            EditorGUIUtility.AddCursorRect( r, cursor );
        }

        public static bool PressedCameraMove(){
			return ( PressedLMB() || PressedMMB() );
		}

		public static bool ReleasedCameraMove(){
			return ( ReleasedRawLMB() || ReleasedRawMMB() );
		}

        public static void DrawLine(Vector3 fromPos, Vector3 toPos, Color color) {
            var orgColor = GUI.color;

            GUI.color = color;

            Handles.BeginGUI();
            var orgHandleColor = Handles.color;
            Handles.color = color;
            Handles.DrawPolyLine( new Vector3[] { fromPos, toPos } );
            Handles.color = orgHandleColor;
            Handles.EndGUI();

            GUI.color = orgColor;
        }
    }
}
