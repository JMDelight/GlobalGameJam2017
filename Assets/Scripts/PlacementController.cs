using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController : MonoBehaviour {

  public ViveController viveController;
  public AvailableObstacles AO;


  void Awake() {
    viveController = GetComponent<ViveController>();
  }

  void OnEnable() {
    viveController.OnTriggerTouchDown += OnTriggerTouchDown;
    viveController.OnTriggerTouchUp += OnTriggerTouchUp;
    viveController.OnGripPress += OnGripPress;
    //viveController.OnGripRelease += OnGripRelease;

  }

  void OnDisable() {
    viveController.OnTriggerTouchDown -= OnTriggerTouchDown;
    viveController.OnTriggerTouchUp -= OnTriggerTouchUp;
    viveController.OnGripPress -= OnGripPress;
    //viveController.OnGripRelease -= OnGripRelease;

  }

  private void OnTriggerTouchDown(ViveController viveController) {
    Debug.Log("TriggerDown");
    if (AO.availableSeaMonsters > 0) {
      AO.CreateSeaMonster();
    } else if (AO.availableWhirlpools > 0) {
      AO.CreateWhirlpool();
    } else if (AO.availableStorms > 0) {
      AO.CreateStorm();
    } else if (AO.availableSirens > 0) {
      AO.CreateSiren();
    }
  }

  private void OnTriggerTouchUp(ViveController viveController) {
    Debug.Log("TriggerUp");

    AO.dropObstacle();
  }

  private void OnRumble(ushort rumble) {
    if (rumble > 0) {
      viveController.CreateHapticFeedback((ushort)(rumble));
    }
  }

  private void OnGripPress(ViveController viveController) {
    if (AO.gameOver) {
      AO.RestartGame();
    }
  }

  private void OnGripRelease(ViveController viveController) {
   

  }

  void Update() {
    //Debug.Log(GetComponent<Transform>().position);
    //int timeChecker = (int)(Time.time * 100);
    //if (timeChecker % 30 == 2) {
    Debug.Log("Controller" + GetComponent<Transform>().position);

    //}
  }

}
