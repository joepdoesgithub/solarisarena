﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitInputManager : MonoBehaviour{
	public bool FinishedInit = false;

	public GameObject GetPlayerObject(){return obj;}
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
			bool legalMove = obj.GetComponent<UnitController>().FinishTurn();
			doDraw=true;

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
	}

    // Start is called before the first frame update
    void Start(){
        obj = Instantiate(objPrefab);
		obj.name = "PlayerUnit";

		string s,dir;int x,y;
		TextParser.GetInitPlayerUnit(out s, out x, out y,out dir);
		obj.GetComponent<UnitController>().SetUnit(s,x,y,dir);
		obj.GetComponent<UnitController>().team = 1;
		obj.GetComponent<UnitController>().IsPlayer = true;
		obj.GetComponent<UnitController>().PrepTurn();
		obj.GetComponent<SpriteRenderer>().sprite = UnitManager.GetUnitSprite(s);

		GlobalFuncs.DrawStuff(obj);

		FinishedInit = true;
    }
}
