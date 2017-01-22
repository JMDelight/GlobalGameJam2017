using UnityEngine;
using System.Collections;

public class StormMovement : MonoBehaviour {
    public float xMin = -9.0f;
    public float xMax = 9.0f;
    public float stormSpeed = 2;
    public float yPos;
    public float zMin = -6.0f;
    public float zMax = 6.0f;
    private Vector3 destination;
    public AvailableObsticles AO;
    public bool objectPlaced;
	// Use this for initialization
	void Start () {
        objectPlaced = false;
        AO = GameObject.Find("Available Obsticles").GetComponent<AvailableObsticles>();
        yPos = GetComponent<Transform>().position.y;
        GetNewDestination();
	}
	
	// Update is called once per frame
	void Update () {
        if (objectPlaced)
        {
            Move();
        }
       
    }

    private void Move()
    {
        Vector3 trip = destination - GetComponent<Transform>().position;
        if (trip.magnitude < 0.1)
        {
            GetNewDestination();
        }
        Vector3 heading = trip / trip.magnitude;
        GetComponent<Rigidbody>().MovePosition(GetComponent<Transform>().position + heading * Time.deltaTime * stormSpeed);
        
    }

    private void GetNewDestination()
    {
        float xTarget = 0;
        float zTarget = 0;
        while(Mathf.Abs(xTarget) < 1)
        {
            xTarget = Random.Range(xMin, xMax); 
        }
        while (Mathf.Abs(zTarget) < 1)
        {
            zTarget = Random.Range(zMin, zMax);
        }
        destination = new Vector3(xTarget, yPos, zTarget);
        Debug.Log(destination);
    }
}
