using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour {

    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float damageAOEDistance = 2f;
    [SerializeField] private float minTimeBetweenHits = 1f;
    private float hitDistance; 
    private Vector3 aoeOrigin;

    private GameObject currentTarget;
    private CameraRaycast cameraRaycast;

    private float lastHitTime = 0;

    //TODO change so the layers are not so much mandatory in every script that uses them
    private int EnemyLayer = 9;

    void Start() {
        cameraRaycast = Camera.main.GetComponent<CameraRaycast>();
        cameraRaycast.notifyLayerClickedObservers += selectTarget;

        hitDistance = damageAOEDistance * 2;
        aoeOrigin = transform.position;
    }

    private void selectTarget(RaycastHit rayHit, int layer) {
        if (layer == EnemyLayer && Time.time - lastHitTime > minTimeBetweenHits) {
            GameObject targetedEnemy = rayHit.collider.gameObject;

            if ((targetedEnemy.transform.position - transform.position).magnitude > hitDistance) {
                return;
            }

            currentTarget = targetedEnemy;
            Vector3 aoeOriginOffset = (currentTarget.transform.position - transform.position).normalized * damageAOEDistance;
            //Vector3 aoeOrigin = aoeOriginOffset + transform.position;
            aoeOrigin = aoeOriginOffset + transform.position;
            GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in allEnemies) {
                if (posInAOE(enemy.transform.position, aoeOrigin)) {
                    IDamageable enemyScript = enemy.GetComponent<IDamageable>();
                    enemyScript.TakeDamage(damageAmount);
                }
            }

            lastHitTime = Time.time;    
        }
    }

    private bool posInAOE(Vector3 pos, Vector3 aoeOrigin) {
        return Vector3.Distance(pos, aoeOrigin) < damageAOEDistance; 
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.black;
        showAOEGizmo();
    }

    private void showAOEGizmo() {
        Gizmos.DrawWireSphere(aoeOrigin, damageAOEDistance);
    }

}
