using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRaycast : MonoBehaviour {

    //INSPECTOR PROPERTIES RENDERED BY CUSTOM EDITOR
    [SerializeField] int[] layerPriorities;

    float maxRaycastDistance = 100f;
    int previousLayerPriority = -1;

    //Setup delegates for broadcasting layer changes to other classes
    public delegate void OnLayerChange(int newLayer);
    public event OnLayerChange notifyLayerChangeObservers;
    
    public delegate void OnLayerClick(RaycastHit raycastHit, int layerHit);
    public event OnLayerClick notifyLayerClickedObservers;

    private Camera cam;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		//Check if cursor is over an interactable UI element
        if (EventSystem.current.IsPointerOverGameObject()) {
            NotifyObserversIfLayerChanged(5);
            return;
        }

        //Raycast to max distance, every frame
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] raycastHits = Physics.RaycastAll(ray, maxRaycastDistance);

        RaycastHit? priorityHit = FindTopPriorityHit(raycastHits);

        //Check if cursor isn't over any layer
        if (!priorityHit.HasValue) {
            NotifyObserversIfLayerChanged(0); //default layer
            return;
        }

        //Notify delegates of layer change
        int layerHit = priorityHit.Value.collider.gameObject.layer;
        NotifyObserversIfLayerChanged(layerHit);

        //Notify delegates of highest priority game object under mouse when clicked
        if (Input.GetMouseButton(0)) {
            notifyLayerClickedObservers(priorityHit.Value, layerHit);
        }
        
    }

    private void NotifyObserversIfLayerChanged(int newLayer) {
        if (newLayer != previousLayerPriority) {
            previousLayerPriority = newLayer;
            notifyLayerChangeObservers(newLayer);
        }
    }
    
    private RaycastHit? FindTopPriorityHit(RaycastHit[] raycastHits) {
        foreach (int layer in layerPriorities) {
            foreach (RaycastHit rayHit in raycastHits) {
                if (rayHit.collider.gameObject.layer == layer) {
                    return rayHit;
                }
            }
        }

        return null;
    }

}
