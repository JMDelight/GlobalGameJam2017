using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController : MonoBehaviour {

  public ViveController viveController;
  public AvailableObstacles obstacleScript;


  void Awake() {
    viveController = GetComponent<ViveController>();
  }

  void OnEnable() {
    viveController.OnTriggerTouchDown += OnTriggerTouchDown;
    viveController.OnTriggerTouchUp += OnTriggerTouchUp;
    viveController.OnGripPress += OnGripPress;
    viveController.OnGripRelease += OnGripRelease;

  }

  void OnDisable() {
    viveController.OnTriggerTouchDown -= OnTriggerTouchDown;
    viveController.OnTriggerTouchUp -= OnTriggerTouchUp;
    viveController.OnGripPress -= OnGripPress;
    viveController.OnGripRelease -= OnGripRelease;

  }

  private void OnTriggerTouchDown(ViveController viveController) {
    Debug.Log("TriggerDown");
    if(obstacleScript.availableSeaMonsters > 0) {
      obstacleScript.CreateSeaMonster();
    } else if (obstacleScript.availableWhirlpools > 0) {
      obstacleScript.CreateWhirlpool();
    } else if (obstacleScript.availableStorms > 0) {
      obstacleScript.CreateStorm();
    } else if (obstacleScript.availableSirens > 0) {
      obstacleScript.CreateSiren();
    }
  }

  private void OnTriggerTouchUp(ViveController viveController) {
    Debug.Log("TriggerUp");

    obstacleScript.dropObstacle();
  }

  private void OnRumble(ushort rumble) {
    if (rumble > 0) {
      viveController.CreateHapticFeedback((ushort)(rumble));
    }
  }

  private void OnGripPress(ViveController viveController) {
    
  }

  private void OnGripRelease(ViveController viveController) {
   

  }

}
