using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour {

    private ICharacterBehaviour behaviour;
    public float speed;

	// Use this for initialization
	void Start ()
    {

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        behaviour = new CharacterMoveBehaviour(rigidbody, speed);
	}
	
	// Update is called once per frame
	void Update ()
    {
        behaviour.Behave();
	}
}
