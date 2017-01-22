using UnityEngine;
using System.Collections;

public class MoveWithMouse : MonoBehaviour {
    public Camera mainCamera;
    public GameObject availableObsticlesPanel;
    private Vector3 mousePos;
    private Vector3 objectPos;
	// Use this for initialization
	void Start () {
        mousePos = Input.mousePosition;
        mousePos.z = 2;
	}
	
	// Update is called once per frame
	void Update () {
        mousePos = Input.mousePosition;
        mousePos.z = mainCamera.transform.position.y;
        objectPos = mainCamera.ScreenToWorldPoint(mousePos);
        objectPos.y = 0;
        gameObject.transform.position = objectPos;
	}
}
