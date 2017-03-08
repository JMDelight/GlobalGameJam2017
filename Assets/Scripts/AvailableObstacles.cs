using System.Collections.Generic;
using UnityEngine;

public class AvailableObstacles : MonoBehaviour {

  public GameObject stormPrefab;
  public GameObject whirlpoolPrefab;
  public GameObject seaMonsterPrefab;
  public GameObject sirenPrefab;
  public GameObject cursorParent;
  public GameObject SpawnVikingsObj;
  public GameObject UnicycleParent;
  public TextMesh actionText;
  public float actionTextTimer;

  public int numberOfObstacles = 3;

  public bool gameOver;

  public int availableStorms;
  public int availableWhirlpools;
  public int availableSeaMonsters;
  public int availableSirens;

  public List<GameObject> activeObstacles = new List<GameObject>();

  private int waveCount;


  // Use this for initialization
  void Start() {
    gameOver = false;
    //availableStormText = stormObj.transform.FindChild("Available Quantity").GetComponent<Text>();
    //availableWhirlpoolText = whirlpoolObj.transform.FindChild("Available Quantity").GetComponent<Text>();
    //availableSeaMonsterText = seaMonsterObj.transform.FindChild("Available Quantity").GetComponent<Text>();
    //availableSirenText = SirenObj.transform.FindChild("Available Quantity").GetComponent<Text>();
    waveCount = 0;
    availableStorms = 3;
    availableWhirlpools = 3;
    availableSeaMonsters = 3;
    availableSirens = 3;
    //UpdateAvailableObstacleText();
    ToggleActionText(true);
    actionTextTimer = 10.0f;

  }

  public void UpdateAvailableObstacleText() {
    //availableStormText.text = availableStorms.ToString();
    //availableWhirlpoolText.text = availableWhirlpools.ToString();
    //availableSeaMonsterText.text = availableSeaMonsters.ToString();
    //availableSirenText.text = availableSirens.ToString();
  }

  public void CreateStorm() {
    if (availableStorms > 0) {
      if (cursorParent.transform.childCount > 1) {
        DestroyObject(cursorParent.transform.GetChild(1).gameObject);
      }
      GameObject go = Instantiate(stormPrefab, cursorParent.transform) as GameObject;
      go.transform.position = cursorParent.transform.position;
      Debug.Log("Make Storm at Pos" + go.transform.position);
      go.transform.localPosition = new Vector3(0.0f, 0.1f, 0.1f);
      //go.transform.localPosition = new Vector3(0, 0.8f, 0);
      //ToggleObstacleButtons(false);
      actionText.text = "Storms destroy the \nSailboat but Surfers \nride their waves to safety.";
      ToggleActionText(true);
      actionTextTimer = 10.0f;

    }
  }
  public void CreateWhirlpool() {
    if (availableWhirlpools > 0) {
      if (cursorParent.transform.childCount > 1) {
        DestroyObject(cursorParent.transform.GetChild(1).gameObject);
      }
      GameObject go = Instantiate(whirlpoolPrefab, cursorParent.transform) as GameObject;
      go.transform.localPosition = Vector3.zero;
      go.transform.localRotation = Quaternion.identity;
      //ToggleObstacleButtons(false);
      actionText.text = "Whirlpools destroy \nthe Battleship but \nthe Sailboat maneuvers \naround them.";
      ToggleActionText(true);
      actionTextTimer = 10.0f;

    }
  }
  public void CreateSeaMonster() {
    if (availableSeaMonsters > 0) {
      if (cursorParent.transform.childCount > 1) {
        DestroyObject(cursorParent.transform.GetChild(1).gameObject);
      }
      GameObject go = Instantiate(seaMonsterPrefab, cursorParent.transform) as GameObject;
      go.transform.localPosition = Vector3.zero;
      go.transform.localRotation = Quaternion.identity;
      //ToggleObstacleButtons(false);
      actionText.text = "Sea Monsters destroy \nthe Surfer but is \nslain by the Battleship.";
      ToggleActionText(true);
      actionTextTimer = 10.0f;

    }
  }

  public void CreateSiren() {
    if (availableSirens > 0) {
      if (cursorParent.transform.childCount > 1) {
        DestroyObject(cursorParent.transform.GetChild(1).gameObject);
      }
      GameObject go = Instantiate(sirenPrefab, cursorParent.transform) as GameObject;
      go.transform.position = cursorParent.transform.position;
      //Debug.Log("Make Siren at pos " + go.transform.position);
      //go.transform.localPosition = new Vector3(0, 1, 0);
      go.transform.localRotation = Quaternion.identity;
      go.transform.GetChild(1).localPosition = Vector3.zero;
      go.transform.GetChild(1).localPosition = Vector3.zero;

      actionText.text = "Siren lures \nnearby vikings to \nits location";
      ToggleActionText(true);
      actionTextTimer = 10.0f;
      //mainCamera.GetComponent<CameraController>().StartLerpMove(new Vector3(0, 10, 0), new Vector3(90, 0, 0));
    }
  }

  public void ToggleActionText(bool active) {
    actionText.gameObject.SetActive(active);
  }

  public void DestroyObstacles() {
    Debug.Log(activeObstacles.Count);
    int temp = activeObstacles.Count;
    for (int i = 0; i < temp; i++) {
      Debug.Log(activeObstacles[i]);
      DestroyObject(activeObstacles[i]);

    }
    activeObstacles.Clear();
  }

  public void ResetAvailableObstacles() {
    availableSeaMonsters = 3;
    availableWhirlpools = 3;
    availableStorms = 3;
    availableSirens = 3;
    //UpdateAvailableObstacleText();
    //ToggleObstacleButtons(true);
    //mainCamera.GetComponent<CameraController>().StartLerpMove(new Vector3(0, 10, 0), new Vector3(90, 0, 0));
  }


  public void RestartGame() {
    //mainCamera.GetComponent<CameraController>().StartLerpMove(new Vector3(0, 10, 0), new Vector3(90, 0, 0));
    waveCount = 0;
    ResetAvailableObstacles();
    gameOver = false;
    UnicycleParent.SetActive(false);
  }

  public void ClearCursorChildren() {
    //mainCamera.GetComponent<CameraController>().StartLerpMove(new Vector3(0, 10, -5.5f), new Vector3(60, 0, 0));
    if (cursorParent.transform.childCount > 1) {
      DestroyObject(cursorParent.transform.GetChild(1).gameObject);
    }
    if (cursorParent.transform.GetChild(1).gameObject.name == "Siren(Clone)" && GameObject.Find("Spawn Viking").GetComponent<SpawnVikings>().waveStarted) {
      //SirenObj.SetActive(true);
    } else {
      //ToggleObstacleButtons(true);
    }
  }

  public void dropObstacle() {
    if (cursorParent.transform.childCount > 1) {

      GameObject temp = cursorParent.transform.GetChild(1).gameObject;
      Vector3 tempV3 = temp.GetComponent<Transform>().position;
      Transform obstacleTransform = temp.GetComponent<Transform>();
      obstacleTransform.rotation = Quaternion.identity;
      Debug.Log("Drop " + temp.name + " Pos " + obstacleTransform.position);
      temp.transform.parent = null;
      activeObstacles.Add(temp);
      switch (temp.name) {
        case "Storm(Clone)":
          availableStorms--;
          obstacleTransform.position = new Vector3(obstacleTransform.position.x, 0.8f, obstacleTransform.position.z);
          StormMovement SMTemp = temp.GetComponent<StormMovement>();
          SMTemp.yPos = temp.transform.position.y;
          SMTemp.objectPlaced = true;
          obstacleTransform.localRotation = Quaternion.Euler(-90f, 0, 0);

          Debug.Log(availableStorms);
          break;
        case "Whirlpool(Clone)":
          obstacleTransform.position = new Vector3(obstacleTransform.position.x, 0.0f, obstacleTransform.position.z);

          WhirlpoolMovement WPMTemp = temp.GetComponent<WhirlpoolMovement>();
          Debug.Log(temp.transform.position);
          WPMTemp.xPosStart = temp.transform.position.x;
          //WPMTemp.zPosStart = temp.transform.position.z - WPMTemp.circleSize;
          WPMTemp.zPosStart = temp.transform.position.z - 1.0f;

          WPMTemp.startingTime = Time.time;
          WPMTemp.objectPlaced = true;
          availableWhirlpools--;
          temp.GetComponent<Transform>().position = new Vector3(tempV3.x, 0.0f, tempV3.z);

          break;
        case "Sea Monster(Clone)":
          obstacleTransform.position = new Vector3(obstacleTransform.position.x, 0.0f, obstacleTransform.position.z);
          SeamonsterMovement SMMTemp = temp.GetComponent<SeamonsterMovement>();
          SMMTemp.objectPlaced = true;
          SMMTemp.AttachToParent();
          availableSeaMonsters--;
          temp.GetComponent<Transform>().position = new Vector3(tempV3.x, 0.0f, tempV3.z);

          break;
        case "Siren(Clone)":
          obstacleTransform.position = new Vector3(obstacleTransform.position.x, 0.0f, obstacleTransform.position.z);
          //mainCamera.GetComponent<CameraController>().StartLerpMove(new Vector3(0, 10, -5.5f), new Vector3(60, 0, 0));
          SirenControl SCTemp = temp.GetComponent<SirenControl>();
          SCTemp.objectPlaced = true;
          SCTemp.SetupSiren();
          availableSirens--;
          temp.GetComponent<Transform>().position = new Vector3(tempV3.x, 0.0f, tempV3.z);

          break;
      }
      if (availableStorms == 0 && availableWhirlpools == 0 && availableSeaMonsters == 0 && temp.name != "Siren(Clone)") {
        //mainCamera.GetComponent<CameraController>().StartLerpMove(new Vector3(0, 10, -5.5f), new Vector3(60, 0, 0));
        //SirenObj.SetActive(true);
        //if (availableSirens == 0) {
        //  SirenObj.SetActive(false);
        //}
        actionText.text = "Wave: " + waveCount.ToString();
        switch (waveCount) {
          case 0:
            SpawnVikingsObj.GetComponent<SpawnVikings>().SpawnWave(1, 1, 1);
            break;
          case 1:
            SpawnVikingsObj.GetComponent<SpawnVikings>().SpawnWave(3, 2, 1);
            break;
          case 2:
            SpawnVikingsObj.GetComponent<SpawnVikings>().SpawnWave(6, 4, 2);
            break;
          case 3:
            Debug.Log("Winner!");
            break;
        }
        waveCount++;
      }
      Debug.Log("End Drop " + temp.name + " Pos " + obstacleTransform.position);

    }

  }

  void Update() {
    if (actionTextTimer > 0) {
      actionTextTimer -= 1f * Time.deltaTime;
      //Debug.Log(actionTextTimer);
      if (actionTextTimer < 0) {
        actionTextTimer = 0;
      }
    } else if (actionText.gameObject.activeInHierarchy && actionTextTimer == 0) {
      ToggleActionText(false);
    }
  }
}