using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMe : MonoBehaviour {

	[SerializeField] private float xRotationsPerMinute = 1f;
	[SerializeField] private float yRotationsPerMinute = 1f;
	[SerializeField] private float zRotationsPerMinute = 1f;
	
	void Update () {
        float xDegreesPerFrame = degreesPerFrame(xRotationsPerMinute); 
        //transform.RotateAround (transform.position, transform.right, xDegreesPerFrame);

		float yDegreesPerFrame = degreesPerFrame(yRotationsPerMinute); 
        //transform.RotateAround (transform.position, transform.up, yDegreesPerFrame);

        float zDegreesPerFrame = degreesPerFrame(zRotationsPerMinute);
        //transform.RotateAround (transform.position, transform.forward, zDegreesPerFrame);
        transform.Rotate(xDegreesPerFrame, yDegreesPerFrame, zDegreesPerFrame);
	}

    ///<summary>
    ///Converts rotations per minute into degrees per frame
    ///</summary>
    private float degreesPerFrame(float rotationsPerMinute) {
        // rotations * minute^-1 
        // make it 'degrees * frame^-1'
        // 1 rotation = 360 degrees
        int degreesPerRotation = 360;
        // 1 minute = 60 seconds
        int secondsPerMinute = 60;
        // Time.deltaTime = 1 second -> frame
        float degreesPerMinute = rotationsPerMinute * degreesPerRotation;
        float degreesPerSecond = degreesPerMinute / secondsPerMinute;

        float degreesPerFrame = degreesPerSecond * Time.deltaTime;
        return degreesPerFrame;
    }
}
