using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour {
    private bool foodAlive = true;
    
    // Incremented at every FixedUpdate, keeps track of time elapsed since last reset.
    private float timer = 0;

    // Whenever the timer hits this value, food grows back.
    public float regrowTime = 10;
    
    private Sprite spriteAlive = null;
    private Sprite spriteDead = null;

    // Load all the sprites once, to speed things up during gameplay.
    // Anything placed in a folder called "Resources" is accessible through
    // the Resources.Load API call.
    void Start()
    {
        spriteAlive = Resources.Load<Sprite>("Food");
        spriteDead = Resources.Load<Sprite>("FoodDead");
    }

    void FixedUpdate()
    {
        // Increment the elapsed time.
        timer += Time.deltaTime;

        // If this food hasn't been touched in 'regrowTime' seconds, increment.
        if (timer > regrowTime) {
            RegrowFood();
        }
    }

    // Do any updates to the appearance before rendering.
    // TODO: running this at 60FPS is wasteful
    void Update()
    {
        UpdateSprite();
    }

    // Returns true if a ghost can eat this food patch.
    public bool IsEdible()
    {
        return (foodAlive);
    }

    // Called whenever something enters the BoxCollider2D of the Food prefab.
    void OnTriggerEnter2D(Collider2D co)
    {
        // If PacManPlayer enters the food, decrement its level.
        if (co.tag == "Player") {
            KillFood();
        }
    }

    void RegrowFood()
    {
        foodAlive = true;
        timer = 0;
    }

    void KillFood()
    {
        foodAlive = false;
        timer = 0;
    }

    // Update the appearance of the food sprite based on its level.
    void UpdateSprite()
    {
        if (foodAlive) {
            GetComponent<SpriteRenderer>().sprite = spriteAlive;
        } else {
            GetComponent<SpriteRenderer>().sprite = spriteDead;
        }
    }
}
