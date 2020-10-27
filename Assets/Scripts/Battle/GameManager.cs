using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
	public struct STurnOrderObject{
		public GameObject pObj;
		public float timeToMove;
	}

	public STurnOrderObject[] turnOrderObjecs;
	int numOrderObjects = 10;

    // Start is called before the first frame update
    void DoStart(){
		firstStart = false;
        turnOrderObjecs = new STurnOrderObject[numOrderObjects];
    }

    // Update is called once per frame
    void Update(){
		if(!fullInit){CheckInit();return;}
        if(firstStart){DoStart();}
    }

	bool fullInit = false, firstStart = true;
	void CheckInit(){
		if(!GameObject.Find("Units").GetComponent<UnitManager>().FinishedInit){fullInit = false;return;}
		if(!GameObject.Find("PlayerManager").GetComponent<PlayerUnitInputManager>().FinishedInit){fullInit = false;return;}
		
		Debug.Log("GameManager: Initiation of all objects is finished");
		fullInit = true;
	}
}
