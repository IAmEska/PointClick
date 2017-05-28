using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TrikoAnimace : MonoBehaviour, IDragable {

    public Sprite[] atlas;
    
    public Transform minBound; // odkud 
    public Transform maxBound; // kam, lze tahnout prstem pro zvednuti trika
    public Transform actualBound;

    public float touchDistance = 0.5f; // plocha dotyku

    private float _timePerFrame;
    private SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start ()
    {
        _timePerFrame = (float)1 / atlas.Length;
        _spriteRenderer = GetComponent<SpriteRenderer>(); 
    }
	
    /// <summary>
    /// Udalost pri drag and drop
    /// </summary>
    /// <param name="pos"></param>
	public void OnDrag(Vector2 pos)
    {
        // je mimo pozici dotyku
        if (pos.y > actualBound.position.y + touchDistance || pos.y < actualBound.position.y - touchDistance)
            return;

        // normalizovany cas animace
        float animTime = 0;
        if (pos.y <= minBound.position.y)
            animTime = 1;
        else if (pos.y >= maxBound.position.y)
            animTime = 0;
        else
        {
            float maxDistance = maxBound.position.y - minBound.position.y;
            float actualPosition = maxBound.position.y - pos.y;
            animTime = actualPosition / maxDistance;
        }

        // index snimku
        int frameIndex = (int) (animTime / _timePerFrame);
        _spriteRenderer.sprite = atlas[frameIndex];

        // nastaveni aktualni pozice trika
        Vector3 boundPos = actualBound.position;
        if (animTime == 1)
            boundPos.y = minBound.position.y;
        else if (animTime == 0)
            boundPos.y = maxBound.position.y;
        else
            boundPos.y = pos.y;

        actualBound.position = boundPos;
    }

    /// <summary>
    /// CallBack po ukonceni tazeni prstem
    /// </summary>
    public void OnDragEnded()
    {
        // TODO pokud se triko zvedne a jeste nema, tak si ho typek vrati zpatky
    }
}
