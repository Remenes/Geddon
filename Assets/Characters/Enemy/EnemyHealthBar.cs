using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour {

    private Health enemyHealth;
    private RawImage healthBarImage;

    Rect defaultRect;

    // Use this for initialization
    void Start() {
        enemyHealth = GetComponentInParent<Health>();
        healthBarImage = GetComponent<RawImage>();

        defaultRect = healthBarImage.uvRect;
    }

    void Update() {
        float xValue = -(enemyHealth.healthAsPercentage / 2) + .5f;
        
        healthBarImage.uvRect = new Rect(xValue, defaultRect.y, defaultRect.width, defaultRect.height);
    }
}
