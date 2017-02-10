using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ViveController : MonoBehaviour {
  private SteamVR_TrackedObject trackedObject;
  private SteamVR_Controller.Device device;
  private float _triggerValue;

  public float triggerValue {
    get {
      return _triggerValue;
    }
    set {
      _triggerValue = value;
    }
  }

  public delegate void ViveControllerHandler(ViveController viveController);
  public event ViveControllerHandler OnTriggerTouchDown;
  public event ViveControllerHandler OnTriggerTouchUp;
  public event ViveControllerHandler OnTriggerAxisChange;
  public event ViveControllerHandler OnGripPress;
  public event ViveControllerHandler OnGripRelease;

  void Awake() {
    trackedObject = GetComponent<SteamVR_TrackedObject>();
  }

  void FixedUpdate() {
    SetCurrentDevice();
    HandleDeviceInput();
  }

  private void SetCurrentDevice() {
    int deviceIndex = (int)trackedObject.index;
    device = SteamVR_Controller.Input(deviceIndex);
  }

  private void HandleDeviceInput() {
    if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {
      if (OnTriggerTouchDown != null) {
        //Debug.Log("Event Trigger Down");
        OnTriggerTouchDown(this);
      }
    }
    if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) {
      Debug.Log("Event Trigger Up");

      if (OnTriggerTouchUp != null) {
        Debug.Log("Event Trigger Up");

        OnTriggerTouchUp(this);
      }
    }

    /// grip press for mover 'jetpack' mode
    if (device.GetPress(Valve.VR.EVRButtonId.k_EButton_Grip)) {
      if (OnGripPress != null) {
        OnGripPress(this);
      }
    } else {
      if (OnGripRelease != null) {
        OnGripRelease(this);
      }
    }

    if (device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis1).x != triggerValue) {
      triggerValue = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis1).x;
      if (OnTriggerAxisChange != null) {
        OnTriggerAxisChange(this);
      }
    }
  }

  //Strength in TriggerHapticPulse is actually duration in microseconds
  public void CreateHapticFeedback(ushort strength) {
    if (strength > 3999) strength = 3999;
    device.TriggerHapticPulse(strength);
  }
}
