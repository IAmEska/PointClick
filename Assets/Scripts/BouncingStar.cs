using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class BouncingStar : MonoBehaviour {

    public float dropToPositionY = 1.5f;

    public Transform renderLineTo;

    private bool isFromStart = true;
    private LineRenderer _lineRenderer;
    

	// Use this for initialization
	void Start ()
    {
        isFromStart = true;
        
       // _lineRenderer = 
    }
	
    void FixedUpdate()
    {
        if(isFromStart)
        {
            //var newPosition = Vector3.Lerp()
        }
    }


	// Update is called once per frame
	void Update ()
    {
	    
	}
}
 