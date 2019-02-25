using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    // Expose the speed parameter to Unity.
    public float speed = 0.1f;
    Vector2 dest = Vector2.zero;
    Vector2 direction = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start() {
        dest = (Vector2)transform.position;
        direction = RandomDirection();
    }

    // Returns a random collision-free direction to move in.
    Vector2 RandomDirection() {
        List<Vector2> valid_directions = new List<Vector2>();
        if (CollisionFree(new Vector2(0, 1))) {
            valid_directions.Add(new Vector2(0, 1));
        }
        if (CollisionFree(new Vector2(0, -1))) {
            valid_directions.Add(new Vector2(0, -1));
        }
        if (CollisionFree(new Vector2(1, 0))) {
            valid_directions.Add(new Vector2(1, 0));
        }
        if (CollisionFree(new Vector2(-1, 0))) {
            valid_directions.Add(new Vector2(-1, 0));
        }

        if (valid_directions.Count > 0) {
            int choice = Random.Range(0, valid_directions.Count);
            return valid_directions[choice];
        }

        return new Vector2(1, 0); // No valid directions found.
    }

    void FixedUpdate() {
        // Move closer to the current destination.
        Vector2 updated_pos = Vector2.MoveTowards(transform.position, dest, speed);

        // Move the RigidBody2D rather than setting tranform.position directly.
        GetComponent<Rigidbody2D>().MovePosition(updated_pos);

        // Check for keyboard input if not moving (at destination).
        if (((Vector2)transform.position - dest).SqrMagnitude() < 0.05) {
        // if ((Vector2)transform.position == dest) {
            GetComponent<Rigidbody2D>().MovePosition(dest);

            // If the direction of motion is free, keeping going that way.
            if (CollisionFree(direction)) {
                dest += direction;
            } else {
                // Otherwise, try to find a free direction to move in.
                direction = RandomDirection();   
            }
        }
    }

    bool CollisionFree(Vector2 dir) {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }
}
