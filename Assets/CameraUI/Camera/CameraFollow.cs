using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private GameObject player;

    [SerializeField] private KeyCode rotateLeft = KeyCode.Q;
    [SerializeField] private KeyCode rotateRight = KeyCode.E;

    //Rotation of the camera in degrees
    private float currentRotationY = 0;
    private int newRotationY = 0;
    [SerializeField] private int rotationYChangeAmount = 90;
    [SerializeField] private float rotationChangeTime = 2;
    private float rotationYAmountDelta;

    //Default this to twice the rotationChangeAmount
    private int maxRotationDifferenceLimit;

    //When the game starts
    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        rotationYAmountDelta = rotationYChangeAmount * Time.deltaTime / rotationChangeTime;
        maxRotationDifferenceLimit = 2 * rotationYChangeAmount; // twice the rotation change AMount
    }

    //Update after the player moves
    void LateUpdate() {
        transform.position = player.transform.position;

        getChangeRotation();

        if (currentRotationY != newRotationY) {
            currentRotationY += rotationYAmountDelta * (currentRotationY - newRotationY < 0 ? 1 : -1);

            //If the difference is small enough, then make the current Rotation equal to the new Rotation
            if (absRotationDifference() < rotationYAmountDelta)
                currentRotationY = newRotationY;
        }
        else {
            currentRotationY %= 360;
            newRotationY %= 360;
        }
        
        transform.rotation = Quaternion.Euler(0, currentRotationY, 0);
	}

    private void getChangeRotation() {
        //If there is already a lot of rotation going on, don't let them rotate
        if (absRotationDifference() > maxRotationDifferenceLimit)
            return;

        if (Input.GetKeyDown(rotateLeft)) {
            newRotationY += rotationYChangeAmount;
        } else if (Input.GetKeyDown(rotateRight)) {
            newRotationY -= rotationYChangeAmount;
        }
            
    }

    private float absRotationDifference() {
        return Mathf.Abs(currentRotationY - newRotationY);
    }
}
