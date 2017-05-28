using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class DrawLine : MonoBehaviour {

    public float drawUnitsPerSecond = 0.5f;
    public bool draw = true;
    public Vector2[] linePoints;

    private bool _isDrawing = false;
    private LineRenderer _lineRenderer;

	// Use this for initialization
	void Start ()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetPositions(new Vector3[] { }); // clear all set positions
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(draw && !_isDrawing)
        {
            StartCoroutine(DrawFunction());
            draw = false;
        }
	}

    IEnumerator DrawFunction()
    {
        _isDrawing = true;
        int i = 1;
        _lineRenderer.SetPosition(0, Vector3.zero);
        Vector3 actualDrawPosition = Vector3.zero;
        while (i < linePoints.Length)
        {
            float timeSinceStarted = 0f;
            Vector3 destination = linePoints[i];
            float duration = Vector3.Distance(linePoints[i - 1], destination) / drawUnitsPerSecond;
            while (actualDrawPosition.x != destination.x || actualDrawPosition.y != destination.y)
            {
                timeSinceStarted += Time.deltaTime;
                actualDrawPosition = Vector3.Lerp(linePoints[i-1], destination, timeSinceStarted / duration);
                _lineRenderer.SetPosition(i, actualDrawPosition);
                yield return null;
            }
            i++;
            yield return null;
        }
        _isDrawing = false;
    }
}
