using UnityEngine;
using System.Collections;

public class MoveToPosition : MonoBehaviour {

    public Vector2 moveToPosition;
    public float duration;

    public bool startMoving = true;
    private bool _isMoving = false;

    void Update()
    {
        if(startMoving && !_isMoving)
        {
            StartCoroutine(MoveFunction());
            startMoving = false;
        }
    }

    IEnumerator MoveFunction()
    {
        _isMoving = true;
        float timeSinceStarted = 0f;
        while (transform.position.x != moveToPosition.x || transform.position.y != moveToPosition.y)
        {
            timeSinceStarted += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, moveToPosition, timeSinceStarted / duration);
            yield return null;
        }
        _isMoving = false;
    }

}
