using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnVikings : MonoBehaviour {

  public Camera mainCamera;
  public GameObject surfer;
  public GameObject sailBoat;
  public GameObject battleShip;
  public float offScreenPosY;
  public float offScreenPosX;
  public List<GameObject> livingVikings = new List<GameObject>();
  public bool waveStarted;
  private AvailableObstacles AO;

  // Use this for initialization
  void Start() {
    AO = GameObject.Find("Available Obstacles").GetComponent<AvailableObstacles>();
  }

  public void SpawnWave(int surfers, int sailboats, int battleships) {
    waveStarted = true;

    for (int i = 0; i < surfers; i++) {
      SpawnViking(surfer);
    }
    for (int i = 0; i < sailboats; i++) {
      SpawnViking(sailBoat);
    }
    for (int i = 0; i < battleships; i++) {
      SpawnViking(battleShip);
    }
  }

  public void SpawnViking(GameObject viking) {
    float tempY = 0;
    float tempX = 0;
    int tempSide = Random.Range(0, 4);
    switch (tempSide) {
      case 0:
        Debug.Log("Spawn North");
        tempY = offScreenPosY;
        tempX = Random.Range(-offScreenPosX, offScreenPosX);
        break;
      case 1:
        Debug.Log("Spawn South");
        tempY = -offScreenPosY;
        tempX = Random.Range(-offScreenPosX, offScreenPosX);
        break;
      case 2:
        Debug.Log("Spawn East");
        tempY = Random.Range(-offScreenPosY, offScreenPosY);
        tempX = offScreenPosX;
        break;
      case 3:
        Debug.Log("Spawn West");
        tempY = Random.Range(-offScreenPosY, offScreenPosY);
        tempX = -offScreenPosX;
        break;
    }
    //tempX = Random.Range(0.0f, 1.0f);
    //tempY = Random.Range(0.0f, 1.0f);
    Vector3 tempPos = new Vector3(tempX, 0.5f, tempY);
    Debug.Log("tempPos: " + tempPos);
    GameObject go = Instantiate(viking) as GameObject;
    go.transform.position = tempPos;
    livingVikings.Add(go);
  }

  public void DestroyVikings() {
    for (int i = 0; i < livingVikings.Count; i++) {
      DestroyObject(livingVikings[i]);
    }
    livingVikings.Clear();
  }



  // Update is called once per frame
  void Update() {
    Debug.Log(livingVikings.Count);
    if (livingVikings.Count == 0 && waveStarted && !AO.gameOver) {
      waveStarted = false;
      Debug.Log("Wave Cleared");
      AO.actionText.text = "Wave Cleared!";
      AO.ToggleActionText(true);
      AO.actionTextTimer = 5f;
      AO.DestroyObstacles();
      AO.ResetAvailableObstacles();
      AO.SirenObj.SetActive(false);
      AO.ClearCursorChildren();
    }
  }
}
