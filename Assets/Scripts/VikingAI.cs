using UnityEngine;
using System.Collections;

public class VikingAI : MonoBehaviour {

    public Vector3 islandCenter;

    private Vector3 rotateOffset;
   void Start () {
        rotateOffset = new Vector3(0, -90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(islandCenter);
        transform.Rotate(rotateOffset);
        transform.position = Vector3.MoveTowards(transform.position, islandCenter, 0.5f * Time.deltaTime);
    }
}
