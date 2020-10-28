using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
	STurnOrderObject[] turnOrderObjects;
	int numOrderObjects = 12;

    // Start is called before the first frame update
    void DoStart(){
		firstStart = false;
		SetAllUnitsClockOrder();

		CreateTurnOrderBlock();
    }

    // Update is called once per frame
    void Update(){
		if(!fullInit){CheckInit();return;}
        if(firstStart){DoStart();}
    }

	struct STurnOrderObject{public int id; public float timeToMove; }
	struct SOrder{ public int id;public float nextF; public float dT; }
	void CreateTurnOrderBlock(){
        // Get all units and fill in list
		List<GameObject> lObjs = GameObject.Find("GameManager").GetComponent<GameManager>().GetAllUnits();
		SOrder[] l = new SOrder[lObjs.Count];
		for(int i = 0;i<lObjs.Count;i++){
			l[i] = new SOrder();
			l[i].id = lObjs[i].GetComponent<UnitController>().ID;
			l[i].nextF = lObjs[i].GetComponent<UnitController>().GetNextClock();
			l[i].dT = lObjs[i].GetComponent<UnitController>().clockInterval;
		}		
		
		turnOrderObjects = new STurnOrderObject[numOrderObjects];
		int index = 0,ii;
		while(index < turnOrderObjects.Length){
			float min = float.MaxValue;ii = 0;
			for(int i = 0;i<l.Length;i++){ if(l[i].nextF < min){min = l[i].nextF;ii = i;} }

			turnOrderObjects[index] = new STurnOrderObject();
			turnOrderObjects[index].id = l[ii].id;
			turnOrderObjects[index].timeToMove = l[ii].nextF;
			index++;
			l[ii].nextF += l[ii].dT;
		}
	}
	public string GetTurnOrderString(){
		if(turnOrderObjects.Length <= 0){return "";}

		string s = "";
		for(int i = 0;i < turnOrderObjects.Length; i++){
			UnitController u = GlobalFuncs.GetUnit(turnOrderObjects[i].id).GetComponent<UnitController>();
			s += string.Format("{0}:{1} {2}\n",i,u.team,u.unit.unitName);
		}
		return s;
	}

	bool fullInit = false, firstStart = true;
	void CheckInit(){
		if(!GameObject.Find("Units").GetComponent<UnitManager>().FinishedInit){fullInit = false;return;}
		if(!GameObject.Find("PlayerManager").GetComponent<PlayerUnitInputManager>().FinishedInit){fullInit = false;return;}
		
		Debug.Log("GameManager: Initiation of all objects is finished");
		fullInit = true;
	}

	public List<GameObject> GetAllUnits(){
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
