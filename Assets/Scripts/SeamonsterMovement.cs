using UnityEngine;
using System.Collections;
using UnityEditor;

public class SeamonsterMovement : MonoBehaviour {
    public float seekingRadius = 2;
    public float movementSpeed = 0.1f;
    private bool isSeeking = false;
    private Transform self;
    private Transform destination;
    public Transform worldCenter;
    public bool objectPlaced;                       //Set in AvailableObsticles script when the object is placed.

	// Use this for initialization
	void Start () {
        worldCenter = GameObject.Find("Island Center").transform;
        self = GetComponent<Transform>();
        //AttachToParent();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (objectPlaced)
        {
            if (!isSeeking)
            {
                CheckForTargets();
            }
            else
            {
                SeekTarget();
            }
        }   
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Battleship(Clone)" | other.gameObject.name == "Sailboat(Clone)" | other.gameObject.name == "Surfer(Clone)")
        {
            AttachToParent();
        }
       
    }

    public void AttachToParent()                //Set in AvailableObsticles script when the object is placed.
    {
        Vector3 perpindicularVector = Vector3.Cross(worldCenter.position - self.position, new Vector3(worldCenter.position.x, 10.123f, worldCenter.position.y) - self.position);
        self.rotation = Quaternion.LookRotation(perpindicularVector);
        self.SetParent(worldCenter);
    }
    public void SeekTarget()
    {
        if (destination != null)
        {
            Vector3 heading = destination.position - self.position;
            heading = heading / heading.magnitude;
            self.GetComponent<Rigidbody>().MovePosition(self.position + heading * Time.deltaTime);
        }
    }

    private void CheckForTargets()
    {
        Collider[] possibleTargets = Physics.OverlapSphere(self.position, seekingRadius);
        if(possibleTargets.Length > 1)
        {
            Debug.Log(possibleTargets);
            Debug.Log(possibleTargets.Length);
            foreach (Collider element in possibleTargets)
            {
                string prefabName = element.gameObject.name;
                //string prefabName = PrefabUtility.GetPrefabParent(element.GetComponent<Transform>()).name;
                if (prefabName == "Surfer(Clone)" | prefabName == "Sailboat(Clone)" | prefabName == "Battleship(Clone)")
                {
                    destination = element.GetComponent<Transform>();
                    isSeeking = true;
                    self.SetParent(null);
                    self.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    Debug.Log(destination);
                }
            }
        }        
    }


}
