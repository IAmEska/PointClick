using UnityEngine;

public class TouchManager : MonoBehaviour {
    
    private IDragable _dragObject;
    private int _touchId = -1;

    /// <summary>
    /// Handle touch on object
    /// </summary>
    /// <param name="position"></param>
    /// <param name="fingerId"></param>
    private void BeginTouch(Vector3 position, int fingerId)
    {
        var worldPosition = Camera.main.ScreenToWorldPoint(position);
        var hitInfo = Physics2D.Raycast(worldPosition, Vector2.zero);
        if (hitInfo)
        {
            var touchable = hitInfo.transform.GetComponent<ITouchable>();
            if (touchable != null)
                touchable.OnTouch();

            if (_dragObject == null)
            {
                var dragable = hitInfo.transform.GetComponent<IDragable>();
                if (dragable != null)
                {
                    _dragObject = dragable;
                    _touchId = fingerId;
                    _dragObject.OnDrag(worldPosition);
                }
            }
        }
    }

    /// <summary>
    /// Handle drag on object
    /// </summary>
    /// <param name="position"></param>
    private void Drag(Vector3 position)
    {
        if (_dragObject == null)
            return;

        var worldPosition = Camera.main.ScreenToWorldPoint(position);
        _dragObject.OnDrag(worldPosition);
        
    }

    /// <summary>
    /// Clear drag object
    /// </summary>
    private void ClearDrag()
    {
        if (_dragObject != null)
            _dragObject.OnDragEnded();

        _dragObject = null;
        _touchId = -1;
    }

	void Update ()
    {
        // if touch is supported
        if (Input.touchSupported)
        {
            foreach (var touch in Input.touches)
            { 
                if (touch.phase == TouchPhase.Began)
                { 
                    BeginTouch(touch.position, touch.fingerId);
                }
                else if(_touchId == touch.fingerId && _dragObject != null)
                {
                    if (touch.phase == TouchPhase.Moved)
                    { 
                        Drag(touch.position);
                    }
                    else if (touch.phase == TouchPhase.Stationary)
                    {
                        Drag(touch.position);
                    }
                    else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
                    {
                        ClearDrag();
                    }
                }
            }
        }

        // if mouse is supported
        if (Input.mousePresent)
        {

            if(Input.GetMouseButtonDown(0))
            {
                BeginTouch(Input.mousePosition, -1);
            }

            else if (Input.GetMouseButton(0) && _dragObject != null)
            {
                Drag(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0) && _dragObject != null)
            {
                ClearDrag();
            }
        }
        
	}
}
