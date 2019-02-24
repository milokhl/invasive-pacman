using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour {
    // The food level is reset to this every 'regrowTime' seconds.
    public int maxFoodLevel = 2;
    
    // Maintains the amount of food in this square.
    public int foodLevel = 2;
    
    // Incremented at every FixedUpdate, keeps track of time elapsed since last reset.
    float timer = 0;

    // Whenever the timer hits this value, food grows back.
    public float regrowTime = 10;

    Sprite spriteAlive = null;
    Sprite spriteDying = null;
    Sprite spriteDead = null;

    void Start() {
        spriteAlive = Resources.Load<Sprite>("Food");
        spriteDying = Resources.Load<Sprite>("DyingFood");
        spriteDead = Resources.Load<Sprite>("FoodDead");
    }

    void FixedUpdate() {
        // Increment the elapsed time.
        timer += Time.deltaTime;

        if (timer > regrowTime) {
            timer = 0;

            // Regrow the food.
            foodLevel += 1;
            foodLevel = System.Math.Min(foodLevel, maxFoodLevel);
        }
    }

    // Do any updates to the appearance before rendering.
    // TODO: running this at 60FPS is wasteful
    void Update() {
        UpdateSprite();
    }

    // Called whenever something enters the BoxCollider2D of the Food prefab.
    void OnTriggerEnter2D(Collider2D co) {
        // If PacManPlayer enters the food, decrement its level.
        if (co.name == "PacManPlayer") {
            foodLevel -= 1;
            foodLevel = System.Math.Max(foodLevel, 0);
        }
    }

    // Update the appearance of the food sprite based on its level.
    void UpdateSprite() {
        if (foodLevel == maxFoodLevel) {
            GetComponent<SpriteRenderer>().sprite = spriteAlive;
        } else if (foodLevel == 0) {
            GetComponent<SpriteRenderer>().sprite = spriteDead;
        } else {
            GetComponent<SpriteRenderer>().sprite = spriteDying;
        }
    }
}
