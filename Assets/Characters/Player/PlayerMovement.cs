using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float walkMoveStopRadius = .2f;
    [SerializeField] private float attackMoveStopRadius = 3f;

    private ThirdPersonCharacter characterScript;
    private CameraRaycast cameraRaycast;
    private AICharacterControl aiControl;
    private Vector3 clickedTarget, moveDestination;

    //To be used for the AI Character Controller, since a transform is needed
    private GameObject currentMoveObject;

    //TODO fix const numbers
    private const int Walkable = 8;
    private const int Enemy = 9;

    // Use this for initialization
    void Start () {
        characterScript = GetComponent<ThirdPersonCharacter>();
        cameraRaycast = Camera.main.GetComponent<CameraRaycast>();
        moveDestination = clickedTarget = transform.position;

        currentMoveObject = new GameObject("PlayerMoveToObject");
        currentMoveObject.transform.position = transform.position;

        aiControl = GetComponent<AICharacterControl>();

        //cameraRaycast.notifyLayerClickedObservers += setNewMoveDestination;
        cameraRaycast.notifyLayerClickedObservers += setTargetPosition;
        
    }
    
    private void setTargetPosition(RaycastHit rayHit, int layer) {
        switch (layer) {
            case Walkable:
                currentMoveObject.transform.position = rayHit.point;
                aiControl.SetTarget(currentMoveObject.transform);
                break;
            case Enemy:
                GameObject enemy = rayHit.collider.gameObject;
                aiControl.SetTarget(enemy.transform);
                break;
            default:
                print("ERROR: Layer not clickable: " + layer);
                break;
        }
    }
        
    private void setNewMoveDestination(RaycastHit rayHit, int layer) {
        
        clickedTarget = rayHit.point;
        switch (layer) {
            case Walkable:
                moveDestination = shortenVector(clickedTarget, walkMoveStopRadius);
                break;
            case Enemy:
                moveDestination = shortenVector(clickedTarget, attackMoveStopRadius);
                break;
            default:
                print("ERROR: Layer not clickable: " + layer);
                break;
        }
    }

    private Vector3 shortenVector(Vector3 vector, float shortenAmount) {
        Vector3 reductionVector = (vector - transform.position).normalized * shortenAmount;
        return vector - reductionVector;
    }

    private void OnDrawGizmos() {
        //Draw movement gizmos
        Gizmos.color = Color.black;
        if (currentMoveObject) {
            Gizmos.DrawLine(transform.position, currentMoveObject.transform.position);
            Gizmos.DrawSphere(currentMoveObject.transform.position, .1f);
        }

        //Draw attack gizmos
        Gizmos.color = new Color(0f, 255f, 0f, .75f);
        Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);

    }
    
}
