using UnityEngine;
using System.Collections;

public class SpawnVikings : MonoBehaviour {

    public Camera mainCamera;
    public GameObject surfer;
    public GameObject sailBoat;
    public GameObject battleShip;
    public float offScreenPosY;
    public float offScreenPosX;

   
	// Use this for initialization
	void Start () {

	}
	
    public void SpawnWave(int surfers, int sailboats, int battleships)
    {
        for (int i = 0; i < surfers; i++)
        {
            SpawnViking(surfer);
        }
        for (int i = 0; i < sailboats; i++)
        {
            SpawnViking(sailBoat);
        }
        for (int i = 0; i < battleships; i++)
        {
            SpawnViking(battleShip);
        }
    }

    public void SpawnViking(GameObject viking)
    {
        float tempY = 0;
        float tempX = 0;
        int tempSide = Random.Range(0, 4);
        switch (tempSide)
        {
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
    }



	// Update is called once per frame
	void Update () {
	
	}
}
