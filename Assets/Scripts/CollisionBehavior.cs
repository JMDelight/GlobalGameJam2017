﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollisionBehavior : MonoBehaviour {

    public Collider Storm;
    public Collider SeaMonster;
    public Collider Whirlpool;
    private int attackerValue;
    private AvailableObsticles AO;
    private SpawnVikings SV;

    private void Awake()
    {
    }

    // Use this for initialization
    void Start () {
        //Debug.Log(this.gameObject.name);
        //Debug.Log(PrefabUtility.GetPrefabParent(this.gameObject).name);
        //string name = PrefabUtility.GetPrefabParent(this.gameObject).name;
        AO = GameObject.Find("Available Obsticles").GetComponent<AvailableObsticles>();
        SV = GameObject.Find("Spawn Vikings").GetComponent<SpawnVikings>();
        if (gameObject.name == "Surfer(Clone)")
        {
            attackerValue = 1;
        }
        else if (gameObject.name == "Sailboat(Clone)")
        {
            attackerValue = 2;
        }
        else if (gameObject.name == "Battleship(Clone)")
        {
            attackerValue = 3;
        }
        else
        {
            Debug.LogWarning("Problem with collision bhavior script, improper attacker prefab.  " + gameObject.name);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("In OnTriggerEnter.  AttackerValue=" + attackerValue);
        int defenderValue;
        if (other.gameObject.name == "Whirlpool(Clone)")
        {
            defenderValue = 1;
        }
        else if (other.gameObject.name == "Sea Monster(Clone)")
        {
            defenderValue = 2;
        } else if (other.gameObject.name == "Storm(Clone)")
        {
            defenderValue = 3;
        }
        else return;
        Debug.Log("DefenderValue=" + defenderValue);
        int resultFight = (3 + defenderValue - attackerValue) % 3;
        Debug.Log("ResultNumber=" + resultFight);
        if(resultFight == 1)
        {
            Debug.Log("Destroyed in a loss");
            SV.livingVikings.Remove(gameObject);
            Destroy(gameObject);
        }
        else if(resultFight == 0)
        {
            int random = Random.Range(0, 2);
            Debug.Log(random);
            if (random == 0)
            {
                if (other.name == "Sea Monster(Clone)")
                {
                    int secondChance = Random.Range(0, 2);
                    if (secondChance == 1)
                    {
                        Debug.Log("Destroyed in a tie");
                        SV.livingVikings.Remove(gameObject);
                        Destroy(gameObject);
                        
                    }else
                    {
                        Debug.Log("Passed - Tie");
                        PassObstacle(other.gameObject);
                    }
                }else
                {
                    Debug.Log("Destroyed in a tie");
                    SV.livingVikings.Remove(gameObject);
                    Destroy(gameObject);
                }
            }
            else
            {
                Debug.Log("Passed - Tie");
                PassObstacle(other.gameObject);
            }
        }
        else
        {
            Debug.Log("Passed - Immune");
            PassObstacle(other.gameObject);
        }
    }

    private void PassObstacle(GameObject passedObsticle)
    {
        if(passedObsticle.name == "Sea Monster(Clone)")
        {
            AO.activeObsticles.Remove(passedObsticle);
            DestroyObject(passedObsticle);
        }
    }

}
