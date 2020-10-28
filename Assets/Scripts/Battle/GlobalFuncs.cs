using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalFuncs{
	static string UsedUnitIds = "";
	public static int GetNewID(){
		System.Random rnd = new System.Random();
		int i = rnd.Next(0,10000);
		while(UsedUnitIds.Contains("|"+i.ToString()+"|"))
			i = rnd.Next(0,10000);
		UsedUnitIds += "|"+i+"|";
		return i;
	}

	public static void DrawStuff(GameObject obj){
		int x = obj.GetComponent<UnitController>().x;
		int y = obj.GetComponent<UnitController>().y;
		int dir = obj.GetComponent<UnitController>().mechNewDir;

		// 	Set new position
		obj.transform.position = GameObject.Find("Map").GetComponent<MapGenerator>().GetRealXYFromMapXY(x,y);

		//	Set new rotation
		obj.transform.eulerAngles = new Vector3(0f,0f, DirToDeg(dir));
    }

	public static float DirToDeg(string dir){return DirToDeg(DirStrToInt(dir));}
	public static float DirToDeg(int dir){
		if(dir == 0){return 0f;}
		else if(dir == 1){return 60f;}
		else if(dir == 2){return 120f;}
		else if(dir == 3){return 180f;}
		else if(dir == 4){return 240f;}
		else{return 300f;}
	}

	public static void PostToConsole(string msg){
		GameObject.Find("Canvas").GetComponent<ConsoleManager>().PostMessage(msg);
	}

	public static string DirIntToStr(int dir){
		if(dir == 0){return "N";}
		else if(dir == 1){return "NW";}
		else if(dir == 2){return "SW";}
		else if(dir == 3){return "S";}
		else if(dir == 4){return "SW";}
		else{return "NE";}
	}
	public static int DirStrToInt(string dir){
		if(dir == "N"){return 0;}
		else if(dir == "NW"){return 1;}
		else if(dir == "SW"){return 2;}
		else if(dir == "S"){return 3;}
		else if(dir == "SE"){return 4;}
		else{return 5;}
	}
}