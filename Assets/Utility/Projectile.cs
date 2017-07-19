using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    private int damageCaused = 10;

    [SerializeField] private string tagToDamage;
   
    public void setDamage(int newDamage) { damageCaused = newDamage; }
    public int getDamage() { return damageCaused; }

    private void OnCollisionEnter(Collision collision) {
        Component damageableComponent = collision.gameObject.GetComponent(typeof(IDamageable));
        if (damageableComponent && collision.collider.CompareTag(tagToDamage)) {
            (damageableComponent as IDamageable).TakeDamage(damageCaused);
            Destroy(gameObject, Time.deltaTime);
        }
    }
}
