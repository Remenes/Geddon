using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour {

    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float attackRadius = 3f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float timeBetweenAttacks = .5f;
    private float sqredDetectionRadius;
    private float sqredAttackRadius;
    private bool inAttackRadius { get { return sqredPlayerDistance() < sqredAttackRadius; } }
    private bool inDetectionRadius { get { return sqredPlayerDistance() < sqredDetectionRadius; } }


    private AICharacterControl aiControl;
    
    private GameObject player;
    [SerializeField] private GameObject projectileToShoot;
    [SerializeField] private GameObject projectileSocket;
    [SerializeField] private Vector3 aimOffset = new Vector3(0, 1f, 0);
    private bool isShooting = false;
    private IEnumerator attackRoutine;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        sqredDetectionRadius = Mathf.Pow(detectionRadius, 2); //Sqr of the dectection range
        sqredAttackRadius = Mathf.Pow(attackRadius, 2); //Sqr of the attack range

        aiControl = GetComponent<AICharacterControl>();
        attackRoutine = attackPlayer(timeBetweenAttacks);
    }

    void Update() {
        //In move radius, but out of attack radius
        if (inDetectionRadius && !inAttackRadius) {
            aiControl.SetTarget(player.transform);
        }
        else {
            //TODO ? maybe take this off so the enemy will change the player no matter what
            aiControl.SetTarget(transform);
        }

        if (inAttackRadius) {
//            transform.LookAt(player.transform);
            if (!isShooting) {
                isShooting = true;
                StartCoroutine(attackRoutine);
            }
        }
        if (!inAttackRadius) {
            isShooting = false;
            StopCoroutine(attackRoutine);
        }
    }

    private IEnumerator attackPlayer(float timeBetweenAttacks) {
        while (true) {
            transform.LookAt(player.transform);
            shootPlayer();
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
    }

    private void shootPlayer() {
        GameObject newProjectile = Instantiate(projectileToShoot, projectileSocket.transform.position, Quaternion.identity);
        Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
        projectileComponent.setDamage(damage);

        Vector3 direction = (player.transform.position - projectileSocket.transform.position + aimOffset).normalized;
        newProjectile.GetComponent<Rigidbody>().velocity = direction * projectileComponent.speed;

        //TODO change this so that the projectile destroys itself better
        Destroy(newProjectile, 3f);
    }
    
    ///<summary>
    ///Finds the squared distance from the player to this enemy's game object
    ///Compare this with the squared detection Range since performance will be slightly better
    ///</summary>
    private float sqredPlayerDistance() {
        return (player.transform.position - transform.position).sqrMagnitude; 
    }

    private void OnDrawGizmos() {
        //Detection Radius
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        
        //Attack Radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

}
