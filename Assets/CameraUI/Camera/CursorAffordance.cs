using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour {

    [SerializeField]
    private Texture2D walkCursor = null;
    [SerializeField]
    private Texture2D overEnemyCursor = null;
    [SerializeField]
    private Texture2D unknownCursor = null;

    [SerializeField]
    private Vector2 hotspotOffset = new Vector2(96, 96);
    
    CameraRaycast cameraRayCast;

    const int Walkable = 8;
    const int Enemy = 9;

    // Use this for initialization
    void Start () {
        cameraRayCast = GetComponent<CameraRaycast>();
        cameraRayCast.notifyLayerChangeObservers += setCursorOnLayer;
	}

    private void setCursorOnLayer(int newLayer) { 
        Cursor.SetCursor(getCursorIcon(newLayer), hotspotOffset, CursorMode.Auto);
	}

    private Texture2D getCursorIcon(int layerToGet) {
        switch (layerToGet) { //TODO change this so they're not magic nums
            case Walkable:
                return walkCursor;
            case Enemy:
                return overEnemyCursor;
            default:
                return unknownCursor;
        }
    }
}
