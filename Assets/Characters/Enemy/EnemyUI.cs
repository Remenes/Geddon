using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour {

    [Tooltip("A GameObject for the EnemyUI")]
    [SerializeField] private GameObject enemyUI_Canvas;

    private Camera cameraToLookAt;

	// Use this for initialization
	void Start () {
        cameraToLookAt = Camera.main;
        Instantiate(enemyUI_Canvas, transform.position, Quaternion.identity, transform);
	}
	
	void LateUpdate () { 
        transform.LookAt(cameraToLookAt.transform);
	}
}
