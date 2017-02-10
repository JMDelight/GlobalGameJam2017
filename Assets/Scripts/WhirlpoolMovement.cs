using UnityEngine;
using System.Collections;

public class WhirlpoolMovement : MonoBehaviour {
  public float circleSpeed = 0.2f;
  public float circleSize = 2f;
  public float circleGrowSpeed = 0.005f;
  //periodChange determines how long the spiral increases before decreasing again
  public float periodChange = 100;
  private Transform position;
  public float xPosStart;                     //Set in AvailableObjects script when object is placed
  public float zPosStart;                     //Set in AvailableObjects script when object is placed
  public bool objectPlaced;
  private bool circleIncreasing = true;
  public float startingTime;




  // Use this for initialization
  void Start() {
    position = GetComponent<Transform>();
    objectPlaced = false;
    xPosStart = 0;
    zPosStart = 0;
    //xPosStart = position.position.x;
    //zPosStart = position.position.z;


  }

  // Update is called once per frame
  void Update() {
    if (objectPlaced) {
      //Debug.Log(Time.time - startingTime);
      //Debug.Log(Mathf.Sin(Time.time * periodChange));
      var xPos = Mathf.Sin((Time.time - startingTime) * circleSpeed) * circleSize;
      var zPos = Mathf.Cos((Time.time - startingTime) * circleSpeed) * circleSize;

      position.position = new Vector3(xPos + xPosStart, position.position.y, zPos + zPosStart);
      if (Mathf.Sin((Time.time - startingTime) / periodChange) > 0) {
        //Debug.Log("increasing");
        circleIncreasing = true;
      } else {
        //Debug.Log("decreasing");
        circleIncreasing = false;
      }
      if (circleIncreasing) {
        circleSize += circleGrowSpeed;
      } else {
        circleSize -= circleGrowSpeed;
      }
    }
  }
}



