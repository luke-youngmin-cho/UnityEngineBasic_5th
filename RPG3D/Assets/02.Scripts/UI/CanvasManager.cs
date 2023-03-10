using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ULB.RPG.UISystems
{
    public class CanvasManager : SingletonMonoBase<CanvasManager>
    {
        private ObservableLinkedList<IUI> _canvasList = new ObservableLinkedList<IUI>();
        private EventSystem _eventSystem;
        private PointerEventData _pointerEventData;
        private List<RaycastResult> _rayCastResults;
        private CanvasRenderer _canvasRenderer;
        private IUI _ui;

        public void Register(IUI ui)
        {
            ui.OnShow += () =>
            {
                _canvasList.AddLast(ui);
                ReorderAll();
            };

            ui.OnHide += () =>
            {
                _canvasList.Remove(ui);
                ReorderAll();
            };
        }

        protected override void Init()
        {
            base.Init();
            _eventSystem = GameObject.FindObjectOfType<EventSystem>();
            _canvasList.CollectionChanged += ReorderAll;
        }

        private void OnGUI()
        {
            if (Event.current.isMouse &&
                Event.current.type == EventType.MouseDown)
            {
                _pointerEventData = new PointerEventData(_eventSystem);
                _pointerEventData.position = new Vector2(Event.current.mousePosition.x,
                                                         Event.current.mousePosition.y);
                _rayCastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(_pointerEventData, _rayCastResults);

                foreach (var result in _rayCastResults)
                {
                    if (result.gameObject.TryGetComponent(out _canvasRenderer) &&
                        result.gameObject.transform.root.TryGetComponent(out _ui))
                    {
                        if (_canvasList.Last == null || 
                            _canvasList.Last.Value != _ui)
                        {
                            _canvasList.Remove(_ui);
                            _canvasList.AddLast(_ui);
                            break;
                        }
                    }
                }
            }
        }

        private void ReorderAll()
        {
            int order = 0;
            foreach (IUI ui in _canvasList)
            {
                ui.canvas.sortingOrder = order++;
            }
        }
    }
}
