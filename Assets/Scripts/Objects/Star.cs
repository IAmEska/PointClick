using UnityEngine;
using System.Collections;
using System;

public class Star : MonoBehaviour, IDragable
{
    public Vector2 basePosition; // puvodni pozice hvezdy
    public float maxDistance = 4; // maximalni vzdalenost, kam jde hvezdu tahnout

    /// <summary>
    /// Tazeni prstem
    /// </summary>
    /// <param name="position"></param>
    public void OnDrag(Vector2 position)
    {
        if(Vector2.Distance(position, basePosition) < maxDistance)
            transform.position = position;
        else
            transform.position = Vector2.MoveTowards(basePosition, position, maxDistance);
    }
    
    /// <summary>
    /// Callback po ukonceni tazenim prstem
    /// </summary>
    public void OnDragEnded()
    {
       
    }
}
