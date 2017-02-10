using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
  public float cameraSpeed = 0.4f;
  private bool isMoving = false;
  private bool isRotating = false;
  private Vector3 destinationPosition;
  private Vector3 destinationRotation;
  private Transform cameraTransform;
  private float startTime;
  private float journeyLength;
  private float rotationLength;

  void Start() {
    cameraTransform = GetComponent<Transform>();
    //StartLerpMove(new Vector3(0, 10, -5.5f), new Vector3(60, 0, 0));
    //StartLerpMove(new Vector3(0, 2, -2), new Vector3(45, 0, 0));
  }

  void Update() {
    if (isMoving) {
      LerpMove();
    }
    if (isRotating) {
      LerpRotate();
    }
  }

  private void LerpMove() {
    float distCovered = (Time.time - startTime) * cameraSpeed;
    float fracJourney = distCovered / journeyLength;
    transform.position = Vector3.Lerp(cameraTransform.position, destinationPosition, fracJourney);
    if (fracJourney >= 1) {
      Debug.Log("Move done at " + Time.time);
      isMoving = false;
    }
  }

  private void LerpRotate() {
    float distCovered = (Time.time - startTime) * cameraSpeed;
    float fracJourney = distCovered / journeyLength;
    transform.rotation = Quaternion.Lerp(cameraTransform.rotation, Quaternion.Euler(destinationRotation.x, destinationRotation.y, destinationRotation.z), fracJourney);
    if (fracJourney >= 1) {
      Debug.Log("Rotate done at " + Time.time);
      isRotating = false;
    }
  }

  public void StartLerpMove(Vector3 desiredDestination, Vector3 desiredRotation) {
    startTime = Time.time;
    destinationPosition = desiredDestination;
    destinationRotation = desiredRotation;
    journeyLength = Vector3.Distance(cameraTransform.position, destinationPosition);
    rotationLength = Vector3.Distance(cameraTransform.rotation.eulerAngles, destinationRotation);
    Debug.Log(journeyLength);
    isMoving = true;
    isRotating = true;
  }
}

