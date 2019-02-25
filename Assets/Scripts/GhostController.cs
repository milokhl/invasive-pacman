using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    // Expose the speed parameter to Unity.
    public float speed = 0.1f;
    
    // If the ghost doesn't eat for this many tiles, it will die.
    public int maxTilesWithoutFood = 10;
    int tilesWithoutFood = 0;

    // Private members.
    Vector2 dest = Vector2.zero;
    Vector2 waypoint = Vector2.zero;
    Vector2 direction = new Vector2(1, 0);

    float deathTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        dest = (Vector2)transform.position;

        waypoint = (Vector2)transform.position;

        direction = RandomDirection();

        // Initalize to false to make sure ghosts are alive at start.
        GetComponent<Animator>().SetBool("isDead", false);
    }

    // Called whenever the ghost hits a collider.
    void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: this seems unsafe
        if (other.gameObject.name == "Food(Clone)") {
            bool is_edible = other.gameObject.GetComponent<FoodController>().IsEdible();
            if (is_edible) {
                tilesWithoutFood = 0;
            } else {
                tilesWithoutFood += 1;
            }

            // Ghost starves.
            if (tilesWithoutFood >= maxTilesWithoutFood) {
                GetComponent<Animator>().SetBool("isDead", true);
                speed = 0.0f; // Stop the ghost.
            }
        }
    }

    // Returns a random collision-free direction to move in.
    Vector2 RandomDirection()
    {
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

    void FixedUpdate()
    {
        if (speed <= 0) {
            deathTimer += Time.deltaTime;

            if (deathTimer >= 3.0f) {
                Destroy(this.gameObject);
            }
            
            return;
        }

        // Move closer to the current destination.
        Vector2 updated_pos = Vector2.MoveTowards(transform.position, dest, speed);

        // Move the RigidBody2D rather than setting tranform.position directly.
        GetComponent<Rigidbody2D>().MovePosition(updated_pos);

        if ((Vector2)transform.position == dest) {

            Vector2 playerDirection = GetPlayerDirection();
            if (playerDirection != new Vector2(0, 0))
            {
                direction = playerDirection;
            }

            // If the direction of motion is free, keeping going that way.
            if (CollisionFree(direction)) {
                dest += direction;
            } else {
                // Otherwise, try to find a free direction to move in.
                direction = RandomDirection();   
            }
        }
    }

    bool CollisionFree(Vector2 dir)
    {
        Vector2 pos = transform.position;

        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(pos, dir, 1.0f);

        for (int i = 0; i < hits.Length; i++) {
            RaycastHit2D hit = hits[i];
            if (hit.collider.name == "Wall(Clone)") {
                return false;
            }
        }

        return true;
    }

    Vector2 GetPlayerDirection()
    {
        Vector2 pos = transform.position;
        
        List<Vector2> directions = new List<Vector2>();
        directions.Add(new Vector2(0, 1));
        directions.Add(new Vector2(0, -1));
        directions.Add(new Vector2(1, 0));
        directions.Add(new Vector2(-1, 0));
        
        for (int i = 0; i < directions.Count; i++)
        {
            // TODO: for now assume a constant maze size of 11x11
            Debug.Log(2 * directions[i]);
            RaycastHit2D hit = Physics2D.Raycast(pos + 2 * directions[i], pos);
            Debug.Log(hit.collider);
            if (hit.collider != null && hit.collider.tag == "Player")
            {
                return directions[i];
            }
        }
        return new Vector2(0, 0);
    }
}
