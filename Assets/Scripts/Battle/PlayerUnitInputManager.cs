using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitInputManager : MonoBehaviour{
	public bool FinishedInit = false;

	public GameObject GetPlayerObject(){return obj;}
	GameObject obj = null;
	GameObject objGhost = null;
	bool showGhost = false;
	public GameObject objPrefab;

	public bool doGetInput = false;

    // Update is called once per frame
    void Update(){
		if(!doGetInput)
			return;

		bool doDraw = false;

		// Confirm your move
		if(Input.GetKeyDown(KeyCode.Return)){
			bool legalMove = obj.GetComponent<UnitController>().FinishTurn();
			doDraw=true;
			if(objGhost != null){Destroy(objGhost);}

			if(legalMove){			
				doGetInput = false;
				GameObject.Find("GameManager").GetComponent<GameManager>().SetNext = true;
			}else
				GlobalFuncs.PostToConsole("Can't finish move, illegal move, likely standing still with >0 speed.");
		}
		
		//	Speed up
		else if(Input.GetKeyDown(KeyCode.UpArrow)){
			obj.GetComponent<UnitController>().ChangeSpeed(1);
			doDraw=true;
		}
		//	Speed down
		else if(Input.GetKeyDown(KeyCode.DownArrow)){
			obj.GetComponent<UnitController>().ChangeSpeed(-1);
			doDraw=true;
		}
		//	Move forward
		else if(Input.GetKeyDown(KeyCode.W)){
			obj.GetComponent<UnitController>().Move(1);
			doDraw=true;
		}
		//	Move Backward
		else if(Input.GetKeyDown(KeyCode.S)){
			obj.GetComponent<UnitController>().Move(-1);
			doDraw=true;
		}
		//	Turn left
		else if(Input.GetKeyDown(KeyCode.A)){
			obj.GetComponent<UnitController>().TurnLeft();
			doDraw=true;
		}
		//	Turn right
		else if(Input.GetKeyDown(KeyCode.D)){			
			obj.GetComponent<UnitController>().TurnRight();
			doDraw=true;
		}

		if(doDraw)
			GlobalFuncs.DrawStuff(obj);

		int x,y,dir;string unitName;
		obj.GetComponent<UnitController>().GetGhostCoords(out x, out y, out dir, out unitName);
		if(objGhost == null)
			objGhost = SpawnUnit(unitName,"PlayerGhost",x,y,GlobalFuncs.DirIntToStr(dir),true);
		else
			objGhost.GetComponent<UnitController>().SetUnit(unitName,x,y,GlobalFuncs.DirIntToStr(dir));
		
		GlobalFuncs.DrawStuff(objGhost);
	}

    // Start is called before the first frame update
    void Start(){
		string unitName,dir;int x,y;
		TextParser.GetInitPlayerUnit(out unitName, out x, out y,out dir);

		obj = SpawnUnit(unitName, "PlayerUnit",x,y,dir);

		GlobalFuncs.DrawStuff(obj);

		FinishedInit = true;
    }

	GameObject SpawnUnit(string unitName, string objName, int x, int y, string dir, bool ghost = false){
        GameObject lObj = Instantiate(objPrefab);
		lObj.name = objName;
		lObj.GetComponent<UnitController>().SetUnit(unitName,x,y,dir);
		lObj.GetComponent<UnitController>().team = 1;
		lObj.GetComponent<UnitController>().IsPlayer = true;
		lObj.GetComponent<UnitController>().PrepTurn();
		lObj.GetComponent<SpriteRenderer>().sprite = UnitManager.GetUnitSprite(unitName);
		return lObj;
	}
}
