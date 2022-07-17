using System.Collections.Generic;
using UnityEngine;

namespace Orazum.UI 
{
    public class UIEventsUpdater : MonoBehaviour
    {
        private const float SQR_MAGNITUDE_POINTER_TOUCH_TOLERANCE = 10;

        [SerializeField]
        private float _offset = 1;

        private Dictionary<int, IPointerTouchHandler> _touchHandlers;
        private Dictionary<int, IPointerEnterExitHandler> _enterExitHandlers;
        private Dictionary<int, IPointerLocalPointHandler> _localPointHandlers;

        private int _movingUICount = 0;
        private int _finishedMovingUICount = 0;
        private bool _isBeganTouchValid;

        private Vector2 _pressMousePos;
        private Vector2 _currentMousePos;

        private void Awake()
        {
            _touchHandlers      = new Dictionary<int, IPointerTouchHandler>();
            _enterExitHandlers  = new Dictionary<int, IPointerEnterExitHandler>();
            _localPointHandlers = new Dictionary<int, IPointerLocalPointHandler>();

            UIDelegatesContainer.FuncEventsUpdater += GetUpdater;
        }

        private void OnDestroy()
        { 
            _touchHandlers.Clear();
            _enterExitHandlers.Clear();
            _localPointHandlers.Clear();
            
            UIDelegatesContainer.FuncEventsUpdater -= GetUpdater;
        }

        public void AddPointerTouchHandler(IPointerTouchHandler handler)
        {
            _touchHandlers.Add(handler.InstanceID, handler);
        }

        public void AddPointerEnterExitHandler(IPointerEnterExitHandler handler)
        {
            handler.EnterState = false;
            _enterExitHandlers.Add(handler.InstanceID, handler);
        }

        public void AddPointerLocalPointHandler(IPointerLocalPointHandler handler)
        {
            _localPointHandlers.Add(handler.InstanceID, handler);
        }

        public void RemovePointerTouchHandler(IPointerTouchHandler handler)
        {
            _touchHandlers.Remove(handler.InstanceID);
        }

        public void RemovePointerEnterExitHandler(IPointerEnterExitHandler handler)
        {
            handler.EnterState = false;
            _enterExitHandlers.Remove(handler.InstanceID);
        }

        public void RemovePointerLocalPointHandler(IPointerLocalPointHandler handler)
        {
            _localPointHandlers.Remove(handler.InstanceID);
        }

        private void RegisterMovingUI()
        {
            _movingUICount++;
        }

        private void UnregisterMovingUI()
        {
            _movingUICount--;
        }

        private void OnMovingUIFinishedMove()
        {
            _finishedMovingUICount++;
            if (_finishedMovingUICount == _movingUICount)
            {
                UpdatePointerHandlers();
                _finishedMovingUICount = 0;
            }
        }

        private void Update()
        {
            if (_movingUICount == 0)
            {
                UpdatePointerHandlers();
                _finishedMovingUICount = 0;
            }
        }

        private void UpdatePointerHandlers()
        {
            _currentMousePos = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                _isBeganTouchValid = true;
                _pressMousePos = Input.mousePosition;
                return;
            }
            if (_isBeganTouchValid)
            {
                if ((_currentMousePos - _pressMousePos).sqrMagnitude > SQR_MAGNITUDE_POINTER_TOUCH_TOLERANCE)
                {
                    _isBeganTouchValid = false;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_isBeganTouchValid)
                {
                    NotifyOnePointerTouchIfNeeded();
                    _isBeganTouchValid = false;
                }
                else
                { 
                    NotifyManyPointerExitIfNeeded();
                }
                return;
            }

            NotifyManyPointerExitIfNeeded();
            NotifyManyPointerEnterIfNeeded();
            NotifyLocalPointUpdateIfNeeded();
        }

        private void NotifyOnePointerTouchIfNeeded()
        { 
            foreach (var pair in _touchHandlers)
            {
                var handler = pair.Value;
                if (RectTransformUtility.RectangleContainsScreenPoint(handler.Rect, _currentMousePos))
                { 
                    handler.OnPointerTouch();
                    return;
                }
            }
        }

        private void NotifyManyPointerExitIfNeeded()
        {
            foreach (var pair in _enterExitHandlers)
            {
                var handler = pair.Value;
                if (!RectTransformUtility.RectangleContainsScreenPoint(handler.InteractionRect, 
                    _currentMousePos, null, Vector4.one * _offset))
                {
                    if (handler.EnterState)
                    {
                        handler.EnterState = false;
                        handler.OnPointerExit();
                    }
                }
            }
        }

        private void NotyfyManyPointerExitWithNoTouchPos()
        { 
            foreach (var pair in _enterExitHandlers)
            {
                var handler = pair.Value;
                if (handler.EnterState)
                {
                    handler.EnterState = false;
                    handler.OnPointerExit();
                }
            }            
        }

        private void NotifyManyPointerEnterIfNeeded()
        { 
            foreach (var pair in _enterExitHandlers)
            {
                var handler = pair.Value;
                if (RectTransformUtility.RectangleContainsScreenPoint(handler.InteractionRect, 
                    _currentMousePos, null, Vector4.one * _offset))
                {
                    if (!handler.EnterState)
                    {
                        handler.EnterState = true;
                        handler.OnPointerEnter();
                    }
                }
            }
        }

        private void NotifyLocalPointUpdateIfNeeded()
        { 
            foreach (var pair in _localPointHandlers)
            {
                var handler = pair.Value;
                if (handler.ShouldUpdateLocalPoint)
                {
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(handler.Rect,
                        _currentMousePos, null, out Vector2 localPoint
                    );
                    handler.UpdateLocalPoint(new Vector2Int((int)localPoint.x, (int)localPoint.y));
                }
            }
        }

        private UIEventsUpdater GetUpdater()
        {
            return this;
        }
    }
}