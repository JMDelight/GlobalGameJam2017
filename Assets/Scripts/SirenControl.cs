using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SirenControl : MonoBehaviour {
  public float LureRadius;
  private Collider sirenCollider;
  public List<Collider> LuredEnemies;
  private AvailableObstacles AO;
  private bool readyToDestroy = false;

  public bool objectPlaced;


  // Use this for initialization
  void Start() {
    AO = GameObject.Find("Available Obstacles").GetComponent<AvailableObstacles>();
    objectPlaced = false;

    sirenCollider = GetComponent<SphereCollider>();
    (sirenCollider as SphereCollider).radius = LureRadius;
  }

  public void SetupSiren() {
    LuredEnemies = new List<Collider>(Physics.OverlapSphere(GetComponent<Transform>().position, LureRadius));
    for (int i = LuredEnemies.Count - 1; i >= 0; i--) {
      if (LuredEnemies[i].name == "Surfer(Clone)" || LuredEnemies[i].name == "BattleShip(Clone)" || LuredEnemies[i].name == "Sailboat(Clone)") {
        LuredEnemies[i].GetComponent<VikingAI>().islandCenter = GetComponent<Transform>().position + new Vector3(0, 1.2f, 0);
      } else {
        Debug.Log("ENEMY NAME: " + LuredEnemies[i].gameObject.name);
        Debug.LogWarning(LuredEnemies);
        LuredEnemies.Remove(LuredEnemies[i]);
        Debug.LogWarning(LuredEnemies);
      }
    }
    foreach (Collider enemy in LuredEnemies) {

    }
    readyToDestroy = true;
  }

  private void OnTriggerEnter(Collider other) {
    if (objectPlaced) {
      if (other.gameObject.name == "Sailboat(Clone)" | other.gameObject.name == "BattleShip(Clone)" | other.gameObject.name == "Surfer(Clone)") {
        LuredEnemies.Add(other);
        other.GetComponent<VikingAI>().islandCenter = GetComponent<Transform>().position + new Vector3(0, 1.2f, 0);
      }
    }
  }

  // Update is called once per frame
  void Update() {
    Debug.Log("Siren pos " + GetComponent<Transform>().position);

    if (objectPlaced) {
      foreach (Collider enemy in LuredEnemies) {
        if (enemy != null) {
          //Debug.Log("Enemy Name: " + enemy.gameObject.name + "Distance: " + Vector3.Distance(GetComponent<Transform>().position, enemy.GetComponent<Transform>().position));
          if (Vector3.Distance(GetComponent<Transform>().position, enemy.GetComponent<Transform>().position) < 1.7f) {
            DestroySiren();
          }
        }
      }
    }
  }

  private void DestroySiren() {
    if (readyToDestroy) {
      Debug.LogWarning("readyToDestroy is true!!");
      foreach (Collider enemy in LuredEnemies) {
        if (enemy != null) {
          enemy.GetComponent<VikingAI>().islandCenter = Vector3.zero;
        }
      }
      AO.activeObstacles.Remove(gameObject);
      DestroyObject(gameObject);
    }

  }
}
