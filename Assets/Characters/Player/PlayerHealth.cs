using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health {


    public override void TakeDamage(int damageAmount) {
        base.TakeDamage(damageAmount);
        //TODO Add other features for player-exclusive taking damage
    }
    
}
