using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AvailableObsticles : MonoBehaviour {
    public GameObject stormObj;
    public GameObject whirlpoolObj;
    public GameObject seaMonsterObj;
    public GameObject stormPrefab;
    public GameObject whirlpoolPrefab;
    public GameObject seaMonsterPrefab;
    public GameObject cursorParent;
    public GameObject SpawnVikingsObj;
    public Text actionText;

    private float actionTextTimer;
    private Text availableStormText;
    private Text availableWhirlpoolText;
    private Text availableSeaMonsterText;

    private int availableStorms;
    private int availableWhirlpools;
    private int availableSeaMonsters;

    private int waveCount;


	// Use this for initialization
	void Start () {
        availableStormText = stormObj.transform.FindChild("Available Quantity").GetComponent<Text>();
        availableWhirlpoolText = whirlpoolObj.transform.FindChild("Available Quantity").GetComponent<Text>();
        availableSeaMonsterText = seaMonsterObj.transform.FindChild("Available Quantity").GetComponent<Text>();
        waveCount = 0;
        availableStorms = 3;
        availableWhirlpools = 3;
        availableSeaMonsters = 3;
        UpdateAvailableObsticleText();
        actionText.text = "Place your obsticles...";
        ToggleActionText(true);
        actionTextTimer = 10f;


    }

    public void UpdateAvailableObsticleText() {
        availableStormText.text = availableStorms.ToString();
        availableWhirlpoolText.text = availableWhirlpools.ToString();
        availableSeaMonsterText.text = availableSeaMonsters.ToString();
    }

    public void CreateStorm() {
        if (availableStorms > 0)
        {
            if (cursorParent.transform.childCount > 0)
            {
                DestroyObject(cursorParent.transform.GetChild(0).gameObject);
            }
            GameObject go = Instantiate(stormPrefab, cursorParent.transform) as GameObject;
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
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

    // Update is called once per frame

    void Update () {
        if (cursorParent.transform.childCount > 0)
        {
            if (Input.GetButtonUp("Place Obsticle"))
            {
                GameObject temp = cursorParent.transform.GetChild(0).gameObject;
                temp.transform.parent = null;
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
                        WPMTemp.zPosStart = temp.transform.position.z;
                        WPMTemp.objectPlaced = true;
                        availableWhirlpools--;
                        break;
                    case "Sea Monster(Clone)":
                        SeamonsterMovement SMMTemp = temp.GetComponent<SeamonsterMovement>();
                        SMMTemp.objectPlaced = true;
                        SMMTemp.AttachToParent();
                        availableSeaMonsters--;
                        break;

                }
                if (availableStorms == 0 && availableWhirlpools == 0 && availableSeaMonsters == 0)
                {
                    switch (waveCount)
                    {
                        case 0:
                            SpawnVikingsObj.GetComponent<SpawnVikings>().SpawnWave(1, 1, 1);
                            waveCount++;
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
                }
                else
                {
                    ToggleObsticleButtons(true);
                }
                UpdateAvailableObsticleText();
                Debug.Log("Mouse Button Clicked");
            }
            if (Input.GetButtonUp("Cancel"))
            {
                DestroyObject(cursorParent.transform.GetChild(0).gameObject);
                ToggleObsticleButtons(true);
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
