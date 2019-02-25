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

    void FixedUpdate() {
        // Move closer to the current destination.
        Vector2 updated_pos = Vector2.MoveTowards(transform.position, dest, speed);

        // Move the RigidBody2D rather than setting tranform.position directly.
        GetComponent<Rigidbody2D>().MovePosition(updated_pos);

        // Check for keyboard input if not moving (at destination).
        if ((Vector2)transform.position == dest) {
            if (Input.GetKey(KeyCode.UpArrow) && CollisionFree(Vector2.up)) {
                dest = (Vector2)transform.position + Vector2.up;
            }
            if (Input.GetKey(KeyCode.RightArrow) && CollisionFree(Vector2.right)) {
                dest = (Vector2)transform.position + Vector2.right;
            }
            if (Input.GetKey(KeyCode.DownArrow) && CollisionFree(-Vector2.up)) {
                dest = (Vector2)transform.position - Vector2.up;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && CollisionFree(-Vector2.right)) {
                dest = (Vector2)transform.position - Vector2.right;
            }
        }
    }

    bool CollisionFree(Vector2 direction) {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + direction, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }
}
