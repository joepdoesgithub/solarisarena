using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitInputManager : MonoBehaviour{
	public bool FinishedInit = false;

	GameObject obj = null;
	public GameObject objPrefab;

	public bool doGetInput = false;

    // Update is called once per frame
    void Update(){
		if(!doGetInput)
			return;

		bool doDraw = false;

		// Confirm your move
		if(Input.GetKeyDown(KeyCode.Return)){
			doDraw=true;
		}
		
		//	Speed up
		else if(Input.GetKeyDown(KeyCode.W)){
			obj.GetComponent<UnitController>().ChangeSpeed(1);
			doDraw=true;
		}
		//	Speed down
		else if(Input.GetKeyDown(KeyCode.S)){
			obj.GetComponent<UnitController>().ChangeSpeed(-1);
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
	}

    // Start is called before the first frame update
    void Start(){
        obj = Instantiate(objPrefab);
		obj.name = "PlayerUnit";

		string s,dir;int x,y;
		TextParser.GetInitPlayerUnit(out s, out x, out y,out dir);
		obj.GetComponent<UnitController>().SetUnit(s,x,y,dir);
		obj.GetComponent<UnitController>().PrepTurn();

		GlobalFuncs.DrawStuff(obj);

		FinishedInit = true;
    }
}
