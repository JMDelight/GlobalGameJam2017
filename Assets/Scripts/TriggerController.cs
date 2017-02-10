using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class TriggerController : MonoBehaviour {
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

  public delegate void TriggerControllerHandler(TriggerController triggerController);
  public event TriggerControllerHandler OnTriggerTouchDown;
  public event TriggerControllerHandler OnTriggerTouchUp;
  public event TriggerControllerHandler OnTriggerAxisChange;
  public event TriggerControllerHandler OnGripPress;
  public event TriggerControllerHandler OnGripNoPress;

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
        OnTriggerTouchDown(this);
      }
    } else if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) {
      if (OnTriggerTouchUp != null) {
        OnTriggerTouchUp(this);
      }
    }

    /// grip press for mover 'jetpack' mode
    if (device.GetPress(Valve.VR.EVRButtonId.k_EButton_Grip)) {
      if (OnGripPress != null) {
        OnGripPress(this);
      }
    } else {
      if (OnGripNoPress != null) {
        OnGripNoPress(this);
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
