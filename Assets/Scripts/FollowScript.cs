using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {

  public Transform followedObject;
  public Vector3 offsetPosition;
  public Vector3 offsetRotation;
  private Transform thisTransform;
	// Use this for initialization
	void Start () {
    thisTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
    thisTransform.position = followedObject.position;
    thisTransform.rotation = followedObject.rotation;
    thisTransform.Translate(offsetPosition);
    thisTransform.Rotate(offsetRotation);
    


  }
}
