﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvailableObstacles : MonoBehaviour {
  public GameObject stormObj;
  public GameObject whirlpoolObj;
  public GameObject seaMonsterObj;
  public GameObject stormPrefab;
  public GameObject whirlpoolPrefab;
  public GameObject seaMonsterPrefab;
  public GameObject sirenPrefab;
  public GameObject cursorParent;
  public GameObject SpawnVikingsObj;
  public GameObject RestartGameButton;
  public GameObject SirenObj;
  public GameObject UnicycleParent;
  public Text actionText;

  public int numberOfObstacles = 3;

  public bool gameOver;
  public float actionTextTimer;
  //private Text availableStormText;
  //private Text availableWhirlpoolText;
  //private Text availableSeaMonsterText;
  //private Text availableSirenText;

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
    UpdateAvailableObstacleText();
    actionText.text = "Place your obstacles...";
    ToggleActionText(true);
    actionTextTimer = 10f;
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
      go.transform.localPosition = new Vector3(0, 0.8f, 0);
      go.transform.localRotation = Quaternion.Euler(-110f, 0, 0);
      //ToggleObstacleButtons(false);
      actionText.text = "Storms destroy the Sailboat but Surfers ride their waves to safety.";
      ToggleActionText(true);
      actionTextTimer = 5f;
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
      actionText.text = "Whirlpools destroy the Battleship but the Sailboat maneuvers around them.";
      ToggleActionText(true);
      actionTextTimer = 5f;
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
      actionText.text = "Sea Monsters destroys the Surfer but is slain by the Battleship.";
      ToggleActionText(true);
      actionTextTimer = 5f;
    }
  }

  public void CreateSiren() {
    if (availableSirens > 0) {
      if (cursorParent.transform.childCount > 1) {
        DestroyObject(cursorParent.transform.GetChild(1).gameObject);
      }
      GameObject go = Instantiate(sirenPrefab, cursorParent.transform) as GameObject;
      go.transform.localPosition = new Vector3(0, 1, 0);
      go.transform.localRotation = Quaternion.identity;
      go.transform.GetChild(1).localPosition = Vector3.zero;
      go.transform.GetChild(1).localPosition = Vector3.zero;

      actionText.text = "Siren lures nearby vikings to its location";
      ToggleActionText(true);
      actionTextTimer = 5f;
      //mainCamera.GetComponent<CameraController>().StartLerpMove(new Vector3(0, 10, 0), new Vector3(90, 0, 0));
    }
  }

  public void ToggleObstacleButtons(bool buttonOn) {
    stormObj.SetActive(buttonOn);
    whirlpoolObj.SetActive(buttonOn);
    seaMonsterObj.SetActive(buttonOn);
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
    UpdateAvailableObstacleText();
    ToggleObstacleButtons(true);
    //mainCamera.GetComponent<CameraController>().StartLerpMove(new Vector3(0, 10, 0), new Vector3(90, 0, 0));
  }


  public void RestartGame() {
    //mainCamera.GetComponent<CameraController>().StartLerpMove(new Vector3(0, 10, 0), new Vector3(90, 0, 0));
    waveCount = 0;
    RestartGameButton.SetActive(false);
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
      SirenObj.SetActive(true);
    } else {
      ToggleObstacleButtons(true);
    }
  }

  public void dropObstacle() {
    if (cursorParent.transform.childCount > 1) {
      GameObject temp = cursorParent.transform.GetChild(1).gameObject;
      Transform obstacleTransform = temp.GetComponent<Transform>();
      obstacleTransform.rotation = Quaternion.identity;
      Debug.Log("Drop " + temp.name);
      temp.transform.parent = null;
      activeObstacles.Add(temp);
      Debug.Log(temp.name);
      switch (temp.name) {
        case "Storm(Clone)":
          availableStorms--;
          obstacleTransform.position = new Vector3(obstacleTransform.position.x, 0.8f, obstacleTransform.position.y);
          StormMovement SMTemp = temp.GetComponent<StormMovement>();
          SMTemp.yPos = temp.transform.position.y;
          SMTemp.objectPlaced = true;
          Debug.Log(availableStorms);
          break;
        case "Whirlpool(Clone)":
          obstacleTransform.position = new Vector3(obstacleTransform.position.x, 0.0f, obstacleTransform.position.y);

          WhirlpoolMovement WPMTemp = temp.GetComponent<WhirlpoolMovement>();
          WPMTemp.xPosStart = temp.transform.position.x;
          WPMTemp.zPosStart = temp.transform.position.z - WPMTemp.circleSize;
          WPMTemp.startingTime = Time.time;
          WPMTemp.objectPlaced = true;
          availableWhirlpools--;
          break;
        case "Sea Monster(Clone)":
          obstacleTransform.position = new Vector3(obstacleTransform.position.x, 0.0f, obstacleTransform.position.y);
          SeamonsterMovement SMMTemp = temp.GetComponent<SeamonsterMovement>();
          SMMTemp.objectPlaced = true;
          SMMTemp.AttachToParent();
          availableSeaMonsters--;
          break;
        case "Siren(Clone)":
          obstacleTransform.position = new Vector3(obstacleTransform.position.x, 0.0f, obstacleTransform.position.y);
          //mainCamera.GetComponent<CameraController>().StartLerpMove(new Vector3(0, 10, -5.5f), new Vector3(60, 0, 0));
          SirenControl SCTemp = temp.GetComponent<SirenControl>();
          SCTemp.objectPlaced = true;
          SCTemp.SetupSiren();
          availableSirens--;
          break;
      }
      if (availableStorms == 0 && availableWhirlpools == 0 && availableSeaMonsters == 0 && temp.name != "Siren(Clone)") {
        //mainCamera.GetComponent<CameraController>().StartLerpMove(new Vector3(0, 10, -5.5f), new Vector3(60, 0, 0));
        //SirenObj.SetActive(true);
        //if (availableSirens == 0) {
        //  SirenObj.SetActive(false);
        //}
        actionText.text = "Wave: " + waveCount.ToString();
        actionTextTimer = 10f;
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
    }
  }
}