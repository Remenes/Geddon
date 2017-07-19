using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health {

    public override void TakeDamage(int amount) {
        base.TakeDamage(amount); //TODO Add other features for enemy-exclusive taking damage
        if (base.healthAsPercentage == 0) Destroy(gameObject);
    }
    
}
