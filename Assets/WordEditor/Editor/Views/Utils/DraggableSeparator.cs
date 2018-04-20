using UnityEngine;
using UnityEditor;
using System.Collections;

namespace WordNodeEditor{
    [System.Serializable]
    public class DraggableSeparator : ScriptableObject, IView, IInit {

        [SerializeField]
        public bool dragging = false;

        [SerializeField]
        public Rect rect;

        [SerializeField]
        public bool initialized = false;

        private WordNodeEditorWindow _window;

        public void OnEnable() {
            base.hideFlags = HideFlags.HideAndDontSave;
        }

        [SerializeField]
        int minX;
        public int MinX {
            get {
                return minX;
            }
            set {
                minX = value;
                ClampX();
            }
        }

        [SerializeField]
        int maxX;
        public int MaxX {
            get {
                return maxX;
            }
            set {
                maxX = value;
                ClampX();
            }
        }

        public void Init(WordNodeEditorWindow window) {
            _window = window;
        }

        public void SetupY(int yPos, int height ) {
            rect.y = yPos;
            rect.height = height;
            rect.width = 7;
        }

        public void Draw() {
            GUI.Box( rect, "", EditorStyles.textField );
            
            if( rect.Contains( Event.current.mousePosition ) || dragging ) {
				GUIHelper.AssignCursor( rect, MouseCursor.ResizeHorizontal );
			}

            if(Event.current.isMouse){

                if( GUIHelper.ReleasedRawLMB() ) {
                    StopDrag();
                }
                if( dragging ) {
                    UpdateDrag();
                }
                if( GUIHelper.PressedLMB( rect ) ) {
                    StartDrag();
                }
            }
        }


        void ClampX(){
            rect.x = Mathf.Clamp( rect.x, minX, maxX );
        }
        int startDragOffset = 0;
        void StartDrag() {
            dragging = true;
            startDragOffset = (int)(Event.current.mousePosition.x - rect.x);
        }
        void UpdateDrag() {
            rect.x = Event.current.mousePosition.x - startDragOffset;
            ClampX();
        }
        void StopDrag() {
            dragging = false;
        }


    }
}
