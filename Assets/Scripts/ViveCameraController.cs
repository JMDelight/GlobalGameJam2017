using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveCameraController : MonoBehaviour {
  public float heightMin = 0;
  public float heightMax = 1.0f;
  private Transform cameraTransform = null;
  private Transform parentTransform;
  private bool foundEye = false;

	// Use this for initialization
	void Start () {
    parentTransform = GetComponent<Transform>();
    cameraTransform = parentTransform.Find("Camera (eye)").GetComponent<Transform>();
    Debug.Log(cameraTransform);

  }

  // Update is called once per frame
  void LateUpdate () {

    if (cameraTransform.position.y < heightMin) {
      Debug.Log("Too Low");
      float heightDif = heightMin - cameraTransform.position.y;
      Debug.Log("Diff: " + heightDif);
      parentTransform.Translate(new Vector3(0, heightDif, 0));
    } else if (cameraTransform.position.y > heightMax) {
      Debug.Log("Too High");
      float heightDif = heightMax - cameraTransform.position.y;
      Debug.Log("Diff: " + heightDif);
      parentTransform.Translate(new Vector3(0, heightDif, 0));

    }

    Debug.Log(cameraTransform.position);
    

  }
}
