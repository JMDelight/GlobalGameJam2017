using UnityEngine;
using System.Collections;

public class BattleShipAnimation : MonoBehaviour {

    private Animator[] anims;

    // Use this for initialization
    void Start () {
        anims = transform.GetComponentsInChildren<Animator>();
        for (int i = 0; i < anims.Length; i++)
        {
            switch (i) {
                case 0:
                anims[i].Play("Move Oars");
                    break;
                case 1:
                    anims[i].Play("Row");
                    break;
            }
            //anims[i].Play("Row");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
