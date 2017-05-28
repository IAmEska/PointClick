using UnityEngine;
using System.Collections;

public class CharacterMoveBehaviour : ICharacterBehaviour {

    private Rigidbody2D rigidbody;
    private float speed;
    private int direction = 1;

    public CharacterMoveBehaviour(Rigidbody2D rigidbody, float speed)
    {
        this.rigidbody = rigidbody;
        this.speed = speed;
    }

    /// <summary>
    /// Nastavní směr pohybu postavy
    /// </summary>
    public bool Direction
    {
        set
        {
            if (value)
                direction = 1;
            else
                direction = -1;    
        }
    }

	public void Behave()
    {
        // pohybujeme se doprava / doleva ve smeru objektu
        Vector2 velocity = rigidbody.velocity;
        velocity.x = direction * speed;
        rigidbody.velocity = velocity;
    }
}
