using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour {
    public int foodLevel = 10;

    // Called whenever something enters the BoxCollider2D of the Food prefab.
    void OnTriggerEnter2D(Collider2D co) {
        if (co.name == "PacManPlayer") {
            foodLevel -= 1;
            Debug.Log("collide");
        }
    }
}
