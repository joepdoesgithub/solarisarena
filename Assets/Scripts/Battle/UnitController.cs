using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour{
	public int x = 0;
	public int y = 0;

	public int mechDir = 0, mechNewDir;
	public int speed = 0, newSpeed;

	public Unit unit;

	public void SetUnit(string name, int x, int y, string dir){
		unit = new Unit(name);
		this.x = x;this.y = y;
		mechDir = GlobalFuncs.DirStrToInt(dir); 
	}

	public void PrepTurn(){
		mechNewDir = mechDir;
		newSpeed = speed;
	}

	public void FinishTurn(){
		mechDir = mechNewDir;
		speed = newSpeed;
	}

	public void ChangeSpeed(int dir){
		newSpeed += dir;
		if(newSpeed > (int)(1.5f*unit.walk + 0.5f)){
			newSpeed = (int)(1.5f*unit.walk + 0.5f);
			GlobalFuncs.PostToConsole(string.Format("Can't accelerate further, max speed forward is {0}",(int)(1.5f*unit.walk + 0.5f)));
		}else if(newSpeed < -1 * unit.walk){
			newSpeed = -1 * unit.walk;
			GlobalFuncs.PostToConsole(string.Format("Can't move backwards faster, max speed backwards is {0}",-1 * unit.walk));
		}else if(newSpeed > speed + 1){
			newSpeed = speed + 1;
			GlobalFuncs.PostToConsole(string.Format("Can't accelerate faster, max speed increase is 1"));
		}else if(newSpeed < speed - 1){
			newSpeed = speed - 1;
			GlobalFuncs.PostToConsole(string.Format("Can't decelerate faster, max speed decrease is 1"));
		}
	}

	public void TurnLeft(){Turn(1);}
	public void TurnRight(){Turn(-1);}
	void Turn(int dir){
		string s = string.Format("Old: {0}, dir {1}",mechNewDir,dir);
		mechNewDir += dir;
		s += " |" + mechNewDir;
		mechNewDir = (mechNewDir > 5 ? 0 : (mechNewDir < 0 ? 5 : mechNewDir));
		s += "| -> " + mechNewDir;

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
		Debug.Log(s + ". F: " + mechNewDir);
	}
}
