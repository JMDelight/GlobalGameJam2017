using UnityEngine;
using System.Collections;

public class VikingAI : MonoBehaviour {

  public Vector3 islandCenter;
  public float movementSpeed;                 //Set in inspector
  private AvailableObstacles AO;
  private SpawnVikings SV;

  private Vector3 rotateOffset;
  void Start() {
    AO = GameObject.Find("Available Obstacles").GetComponent<AvailableObstacles>();
    SV = GameObject.Find("Spawn Vikings").GetComponent<SpawnVikings>();
    rotateOffset = new Vector3(0, -90, 0);
  }

  // Update is called once per frame
  void Update() {
    transform.LookAt(islandCenter);
    transform.Rotate(rotateOffset);
    transform.position = Vector3.MoveTowards(transform.position, islandCenter, movementSpeed * Time.deltaTime);
    if (Vector3.Distance(transform.position, Vector3.zero) < 2) {
      AO.gameOver = true;
      Debug.Log("YOU LOSE!");
      AO.actionText.text = "The Vikings have taken the island!!! YOU LOSE.";
      AO.actionTextTimer = 5f;
      AO.ToggleActionText(true);
      AO.DestroyObstacles();
      AO.RestartGameButton.SetActive(true);
      AO.SirenObj.SetActive(false);
      SV.DestroyVikings();
      //AO.mainCamera.GetComponent<CameraController>().StartLerpMove(new Vector3(0, 2, -2), new Vector3(45, 0, 0));
      AO.UnicycleParent.SetActive(true);
    }
  }
}
