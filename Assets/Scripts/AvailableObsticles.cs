using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AvailableObsticles : MonoBehaviour {
    public GameObject mainCamera;
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

    public bool gameOver;
    public float actionTextTimer;
    private Text availableStormText;
    private Text availableWhirlpoolText;
    private Text availableSeaMonsterText;
    private Text availableSirenText;

    private int availableStorms;
    private int availableWhirlpools;
    private int availableSeaMonsters;
    private int availableSirens;

    public List<GameObject> activeObsticles = new List<GameObject>();

    private int waveCount;


	// Use this for initialization
	void Start () {
        gameOver = false;
        availableStormText = stormObj.transform.FindChild("Available Quantity").GetComponent<Text>();
        availableWhirlpoolText = whirlpoolObj.transform.FindChild("Available Quantity").GetComponent<Text>();
        availableSeaMonsterText = seaMonsterObj.transform.FindChild("Available Quantity").GetComponent<Text>();
        availableSirenText = SirenObj.transform.FindChild("Available Quantity").GetComponent<Text>();
        waveCount = 0;
        availableStorms = 3;
        availableWhirlpools = 3;
        availableSeaMonsters = 3;
        availableSirens = 3;
        UpdateAvailableObsticleText();
        actionText.text = "Place your obsticles...";
        ToggleActionText(true);
        actionTextTimer = 10f;


    }

    public void UpdateAvailableObsticleText() {
        availableStormText.text = availableStorms.ToString();
        availableWhirlpoolText.text = availableWhirlpools.ToString();
        availableSeaMonsterText.text = availableSeaMonsters.ToString();
        availableSirenText.text = availableSirens.ToString();
    }

    public void CreateStorm() {
        if (availableStorms > 0)
        {
            if (cursorParent.transform.childCount > 0)
            {
                DestroyObject(cursorParent.transform.GetChild(0).gameObject);
            }
            GameObject go = Instantiate(stormPrefab, cursorParent.transform) as GameObject;
            go.transform.localPosition = new Vector3(0, 0.8f, 0);
            //go.transform.localRotation = Quaternion.EulerAngles(-110f, 0 , 0);
            ToggleObsticleButtons(false);
            actionText.text = "Storms destroy the Sailboat but Surfers ride their waves to safety.";
            ToggleActionText(true);
            actionTextTimer = 5f;
        }
    }
    public void CreateWhirlpool()
    {
        if (availableWhirlpools > 0)
        {
            if (cursorParent.transform.childCount > 0)
            {
                DestroyObject(cursorParent.transform.GetChild(0).gameObject);
            }
            GameObject go = Instantiate(whirlpoolPrefab, cursorParent.transform) as GameObject;
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            ToggleObsticleButtons(false);
            actionText.text = "Whirlpools destroy the Battleship but the Sailboat maneuvers around them.";
            ToggleActionText(true);
            actionTextTimer = 5f;
        }
    }
    public void CreateSeaMonster()
    {
        if (availableSeaMonsters > 0)
        {
            if (cursorParent.transform.childCount > 0)
            {
                DestroyObject(cursorParent.transform.GetChild(0).gameObject);
            }
            GameObject go = Instantiate(seaMonsterPrefab, cursorParent.transform) as GameObject;
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            ToggleObsticleButtons(false);
            actionText.text = "Sea Monsters destroys the Surfer but is slain by the Battleship.";
            ToggleActionText(true);
            actionTextTimer = 5f;
        }
    }

    public void CreateSiren()
    {
        if (availableSirens > 0)
        {
            if (cursorParent.transform.childCount > 0)
            {
                DestroyObject(cursorParent.transform.GetChild(0).gameObject);
            }
            GameObject go = Instantiate(sirenPrefab, cursorParent.transform) as GameObject;
            go.transform.localPosition = new Vector3(0, 1, 0);
            go.transform.localRotation = Quaternion.identity;
            go.transform.GetChild(0).localPosition = Vector3.zero;
            go.transform.GetChild(1).localPosition = Vector3.zero;

            actionText.text = "Siren lures nearby vikings to its location";
            ToggleActionText(true);
            actionTextTimer = 5f;
            mainCamera.GetComponent<CameraController>().StartLerpMove(new Vector3(0, 10, 0), new Vector3(90, 0, 0));
        }
    }

    public void ToggleObsticleButtons(bool buttonOn)
    {
        stormObj.SetActive(buttonOn);
        whirlpoolObj.SetActive(buttonOn);
        seaMonsterObj.SetActive(buttonOn);
    }

    public void ToggleActionText(bool active)
    {
        actionText.gameObject.SetActive(active);
    }

    public void DestroyObsticles()
    {
        Debug.Log(activeObsticles.Count);
        int temp = activeObsticles.Count;
        for (int i = 0; i < temp; i++)
        {
            Debug.Log(activeObsticles[i]);
            DestroyObject(activeObsticles[i]);
            
        }
        activeObsticles.Clear();
    }

    public void ResetAvailableObsticles()
    {
        availableSeaMonsters = 3;
        availableWhirlpools = 3;
        availableStorms = 3;
        availableSirens = 3;
        UpdateAvailableObsticleText();
        ToggleObsticleButtons(true);
        mainCamera.GetComponent<CameraController>().StartLerpMove(new Vector3(0, 10, 0), new Vector3(90, 0, 0));
    }


    public void RestartGame()
    {
        mainCamera.GetComponent<CameraController>().StartLerpMove(new Vector3(0, 10, 0), new Vector3(90, 0 , 0));
        waveCount = 0;
        RestartGameButton.SetActive(false);
        ResetAvailableObsticles();
        gameOver = false;
        UnicycleParent.SetActive(false);
    }

    public void ClearCursorChildren()
    {
        mainCamera.GetComponent<CameraController>().StartLerpMove(new Vector3(0, 10, -5.5f), new Vector3(60, 0, 0));
        DestroyObject(cursorParent.transform.GetChild(0).gameObject);
        if (cursorParent.transform.GetChild(0).gameObject.name == "Siren(Clone)" && GameObject.Find("Spawn Viking").GetComponent<SpawnVikings>().waveStarted)
        {
            SirenObj.SetActive(true);
        }else
        {
            ToggleObsticleButtons(true);
        }
    }

    // Update is called once per frame

    void Update () {
        if (cursorParent.transform.childCount > 0)
        {
            if (Input.GetButtonDown("Place Obsticle"))
            {
                GameObject temp = cursorParent.transform.GetChild(0).gameObject;
                temp.transform.parent = null;
                activeObsticles.Add(temp);
                Debug.Log(temp.name);
                switch (temp.name)
                {
                    case "Storm(Clone)":
                        availableStorms--;
                        StormMovement SMTemp = temp.GetComponent<StormMovement>();
                        SMTemp.yPos = temp.transform.position.y;
                        SMTemp.objectPlaced = true;
                        Debug.Log(availableStorms);
                        break;
                    case "Whirlpool(Clone)":
                        WhirlpoolMovement WPMTemp = temp.GetComponent<WhirlpoolMovement>();
                        WPMTemp.xPosStart = temp.transform.position.x;
                        WPMTemp.zPosStart = temp.transform.position.z - WPMTemp.circleSize;
                        WPMTemp.startingTime = Time.time;
                        WPMTemp.objectPlaced = true;
                        availableWhirlpools--;
                        break;
                    case "Sea Monster(Clone)":
                        SeamonsterMovement SMMTemp = temp.GetComponent<SeamonsterMovement>();
                        SMMTemp.objectPlaced = true;
                        SMMTemp.AttachToParent();
                        availableSeaMonsters--;
                        break;
                    case "Siren(Clone)":
                        mainCamera.GetComponent<CameraController>().StartLerpMove(new Vector3(0, 10, -5.5f), new Vector3(60, 0, 0));
                        SirenControl SCTemp = temp.GetComponent<SirenControl>();
                        SCTemp.objectPlaced = true;
                        SCTemp.SetupSiren();
                        availableSirens--;
                        break;

                }
                Debug.Log("TEMP.NAME: " + temp.name);
                if (availableStorms == 0 && availableWhirlpools == 0 && availableSeaMonsters == 0 && temp.name != "Siren(Clone)")
                {
                    mainCamera.GetComponent<CameraController>().StartLerpMove(new Vector3(0, 10, -5.5f), new Vector3(60, 0, 0));
                    SirenObj.SetActive(true);
                    if (availableSirens == 0)
                    {
                        SirenObj.SetActive(false);
                    }
                    actionText.text = "Wave: " + waveCount.ToString();
                    actionTextTimer = 10f;
                    switch (waveCount)
                    {
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
                else
                {
                    if (temp.name != "Siren(Clone)" )
                    {
                        ToggleObsticleButtons(true);
                        
                    }
                }
                UpdateAvailableObsticleText();
                Debug.Log("Mouse Button Clicked");
            }
            if (Input.GetButtonUp("Cancel"))
            {
                ClearCursorChildren();
            }
        }
        if (actionTextTimer > 0)
        {
            actionTextTimer -= 1f * Time.deltaTime;
            //Debug.Log(actionTextTimer);
            if (actionTextTimer < 0)
            {
                actionTextTimer = 0;
            }
        }else if(actionText.gameObject.activeInHierarchy && actionTextTimer == 0)
        {
            ToggleActionText(false);
        }
	}
}
