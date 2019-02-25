using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{

    // Expose the speed parameter to Unity.
    public float speed = 0.1f;
    Vector2 dest = Vector2.zero;

    // Start is called before the first frame update
    void Start() {
        dest = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate() {
        Debug.Log((Vector2)transform.position + ", " + dest + ", ");
        // Move closer to the current destination.
        Vector2 updated_pos = Vector2.MoveTowards(transform.position, dest, speed);

        // Move the RigidBody2D rather than setting tranform.position directly.
        GetComponent<Rigidbody2D>().MovePosition(updated_pos);

        // Check for keyboard input if not moving (at destination).
        if (((Vector2)transform.position - dest).SqrMagnitude() < 0.05)
        {
            GetComponent<Rigidbody2D>().MovePosition(dest);

            float randDirection = Random.Range(0, 2);
            randDirection = 2 * (randDirection - 0.5f);
            if (Random.Range(0, 2) == 0 && CollisionFree(-new Vector2(randDirection, 0)))
            {
                dest = (Vector2)transform.position - new Vector2(randDirection, 0);
            } else if (CollisionFree(-new Vector2(0, randDirection)))
            {
                dest = (Vector2)transform.position - new Vector2(0, randDirection);
            }
        }
    }

    bool CollisionFree(Vector2 direction) {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + direction, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }
}
