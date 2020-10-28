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
		SetAllUnitsClockOrder();
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

	List<GameObject> GetAllUnits(){
		List<GameObject> l = new List<GameObject>();
		l.Add( GameObject.Find("PlayerManager").GetComponent<PlayerUnitInputManager>().GetPlayerObject() );
		l.AddRange( GameObject.Find("Units").GetComponent<UnitManager>().GetFriendlies() );
		l.AddRange( GameObject.Find("Units").GetComponent<UnitManager>().GetEnemies() );
		return l;
	}

	void SetAllUnitsClockOrder(){
		List<GameObject> l = GetAllUnits();
		System.Random rnd = new System.Random();

		float diff = 0.0000f;
		while(l.Count > 0){
			int ii = rnd.Next(0,l.Count);
			l[ii].GetComponent<UnitController>().clockOrder = diff;
			diff += 0.0001f;
			l.RemoveAt(ii);
		}
	}
}
