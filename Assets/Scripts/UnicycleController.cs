using UnityEngine;
using System.Collections;


public class UnicycleController : MonoBehaviour {

    private GameObject islandCenter;
    private Vector3 rotationOffset;
	// Use this for initialization
	void Start () {
        islandCenter = GameObject.Find("Island Center");
        //gameObject.transform.SetParent(islandCenter.transform);
        //rotationOffset = new Vector3(0, 90, 0);
	}
	// Update is called once per frame
	void Update () {
        gameObject.transform.RotateAround(Vector3.zero, Vector3.up, 100f * Time.deltaTime);
	}
}
