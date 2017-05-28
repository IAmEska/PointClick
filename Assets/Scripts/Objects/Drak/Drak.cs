using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class Drak : MonoBehaviour {

    public LineRenderer _lineRenderer;

    private List<Vector3> _linePositions;

	// Use this for initialization
	void Start ()
    {
        _linePositions = new List<Vector3>();
        _lineRenderer = GetComponent<LineRenderer>();
	}
	
    /// <summary>
    /// Pridej pozici do line rendereru
    /// </summary>
    /// <param name="position"></param>
    public void AddLinePositions(Vector2 position)
    {
        _linePositions.Add(position);

        _lineRenderer.SetVertexCount(_linePositions.Count);

        if(_linePositions.Count > 0)
            _lineRenderer.SetPosition(_linePositions.Count - 1, position);
    }

    /// <summary>
    /// Odstran pozice z line rendereru od urciteho indexu
    /// </summary>
    /// <param name="fromIndex"></param>
    public void RemoveLinePositions(int fromIndex)
    {
        _linePositions.RemoveRange(fromIndex, _linePositions.Count - fromIndex);
        _lineRenderer.SetVertexCount(_linePositions.Count);
        _lineRenderer.SetPositions(_linePositions.ToArray());
    }

    public int LineCurrectIndex
    {
        get { return _linePositions.Count; }
    }
    
}
