using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // Expose the speed parameter to Unity.
    public float speed = 0.2f;
    private Vector2 dest = Vector2.zero;

    private float deathTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        dest = transform.position;
        this.tag = "Player";
        GetComponent<Animator>().SetBool("isDead", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Ghost(Clone)") {
            bool ghost_is_dead = other.gameObject.GetComponent<Animator>().GetBool("isDead");

            // Dead ghosts can't kill player.
            if (!ghost_is_dead) {
                GetComponent<Animator>().SetBool("isDead", true);
                speed = 0.0f;
            }
        }
    }

    void FixedUpdate()
    {
        if (speed <= 0.0f) {
            deathTimer += Time.deltaTime;

            if (deathTimer >= 0.75f) {
                Destroy(this.gameObject);
            }
            
            return;
        }

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

        // Animation Parameters
        Vector2 dir = (dest - (Vector2)transform.position);
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    bool CollisionFree(Vector2 dir)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }
}
