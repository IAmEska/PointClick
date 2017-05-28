using UnityEngine;
using System.Collections;

public interface IDragable
{
    void OnDrag(Vector2 position);

    void OnDragEnded();
}
