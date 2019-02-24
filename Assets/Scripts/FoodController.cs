using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour {
    public int maxFoodLevel = 10;
    public int foodLevel = 10;

    float timer = 0;
    public float regrowTime = 10; // Increment the foodlevel at this many sec.

    void FixedUpdate() {
        // Increment the elapsed time.
        timer += Time.deltaTime;

        if (timer > regrowTime) {
            foodLevel += 1;
            foodLevel = System.Math.Min(foodLevel, maxFoodLevel);
            timer = 0;
        }
    }

    // Called whenever something enters the BoxCollider2D of the Food prefab.
    void OnTriggerEnter2D(Collider2D co) {
        // If PacManPlayer enters the food, decrement its level.
        if (co.name == "PacManPlayer") {
            foodLevel -= 1;
            foodLevel = System.Math.Max(foodLevel, 0);
        }
    }
}
