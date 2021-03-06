﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour{
	public int ID;
	public int team;
	public bool IsPlayer = false;
	public bool GoNext = false;

	public int x = 0, newX;
	public int y = 0, newY;

	public int mechDir = 0, mechNewDir;
	public int speed = 0, newSpeed;

	public float lastClock = 0f;
	public float clockInterval = 1f;
	public float clockOrder = 0f;
	public float GetNextClock(){return lastClock + clockInterval + clockOrder;}

	public Unit unit;

	void Update(){
		if(GoNext){
			GoNext = false;
			PrepTurn();

			if(IsPlayer){
				GameObject.Find("PlayerManager").GetComponent<PlayerUnitInputManager>().GetPlayerObject().GetComponent<UnitController>().PrepTurn();
				GameObject.Find("PlayerManager").GetComponent<PlayerUnitInputManager>().doGetInput = true;
				return;
			}

			if(GlobalFuncs.DoAIDebug)
				Debug.Log(string.Format("AI: {0} {1} {2}\t{3}",unit.unitName,team,ID,GetNextClock()));
			FinishTurn();
			GameObject.Find("GameManager").GetComponent<GameManager>().SetNext = true;
		}
	}

	public void SetUnit(string name, int x, int y, string dir){
		unit = new Unit(name);
		this.x = x;this.y = y;
		mechDir = GlobalFuncs.DirStrToInt(dir); 
	}

	public void PrepTurn(){
		mechNewDir = mechDir;
		newSpeed = speed;
		newX = x; newY = y;
	}

	public bool FinishTurn(){
		if(newSpeed > 0 && newX == x && newY == y && mechNewDir == mechDir)
			return false;

		mechDir = mechNewDir;
		speed = newSpeed;
		x = newX; y = newY;
		//	[TODO]	temporary
		lastClock = GetNextClock();

		return true;
	}

	public void ChangeSpeed(int dir){
		newSpeed += dir;
		if(newSpeed > (int)(1.5f*unit.walk + 0.5f)){
			newSpeed = (int)(1.5f*unit.walk + 0.5f);
			GlobalFuncs.PostToConsole(string.Format("Can't accelerate further, max speed forward is {0}",(int)(1.5f*unit.walk + 0.5f)));
		}else if(newSpeed < 0){
			newSpeed = 0;
			GlobalFuncs.PostToConsole(string.Format("Can't have a negative speed"));
		}else if(newSpeed > speed + 1){
			newSpeed = speed + 1;
			GlobalFuncs.PostToConsole(string.Format("Can't accelerate faster, max speed increase is 1"));
		}else if(newSpeed < speed - 1){
			newSpeed = speed - 1;
			GlobalFuncs.PostToConsole(string.Format("Can't decelerate faster, max speed decrease is 1"));
		}
	}

	void ResetMove(){
		mechNewDir = mechDir;
		newX = x; newY = y;
	}

	public void Move(int dir){
		// Can you move backwards?
		if(dir < 0 && newSpeed > unit.walk){
			GlobalFuncs.PostToConsole(string.Format("Can't move backwards with speed > {0}",unit.walk));
			return;
		}
		// Do you have speed to move?
		if(dir != 0 && newSpeed == 0){
			GlobalFuncs.PostToConsole(string.Format("Can't move forward or backward with Spd: 0"));
			return;
		}
		ResetMove();

		Vector2 pos = MapGenerator.GetNewCoords(x,y, (dir > 0 ? mechDir : GlobalFuncs.GetOppositeDir(mechDir) ) );
		Debug.Log(string.Format("Move, x: {0} y: {1}",(int)pos.x, (int)pos.y));
		newX = (int)pos.x;
		newY = (int)pos.y;
	}

	public void TurnLeft(){Turn(1);}
	public void TurnRight(){Turn(-1);}
	void Turn(int dir){
		//	Do you have speed to move?
		if(dir != 0 && newSpeed == 0){
			GlobalFuncs.PostToConsole(string.Format("Can't turn with Spd: 0"));
			return;
		}

		ResetMove();
		mechNewDir += dir;
		mechNewDir = (mechNewDir > 5 ? 0 : (mechNewDir < 0 ? 5 : mechNewDir));

		if(dir == -1){	//	Turn Right
			if(mechDir - mechNewDir > 1 || (mechDir == 0 && mechNewDir == 4) || (mechDir == 1 && mechNewDir == 5)){
				mechNewDir = (mechDir == 0 ? 5 : (mechDir == 1 ? 0 : mechDir - 1) );
				GlobalFuncs.PostToConsole("Can't turn farther to the right");
			}
		}else{			//	Turn Left
			if(mechNewDir - mechDir > 1 || (mechDir == 5 && mechNewDir == 1) || (mechDir == 4 && mechNewDir == 0) ){
				mechNewDir = (mechDir == 5 ? 0 : (mechDir == 4 ? 5 : mechDir + 1) );
				GlobalFuncs.PostToConsole("Can't turn farther to the left");
			}
		}
	}

	void Start(){ID = GlobalFuncs.GetNewID();}
}
