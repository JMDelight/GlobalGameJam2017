using UnityEngine;
using System.Collections;

public class RotateCenterObject : MonoBehaviour {
    public Vector3 rotateObjectRate;                //Set in inspector
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Rotate(rotateObjectRate);
	}
}
