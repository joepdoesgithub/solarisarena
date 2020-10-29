using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour{
	public int width = 5;
	public int height = 10;

	public GameObject HexPrefab;

	static float OuterRadius = 2f / Mathf.Sqrt(3);
	static float ShortEdge = Mathf.Sqrt(3) / 3f;

    // Start is called before the first frame update
	void Start(){
        for(int i = -width+1;i<width;i++){
			for(int j = -height+1;j<height;j++){
				float x = i * ((OuterRadius + ShortEdge)/2f);
				float y = ( i%2==0 ? 0 : 0.5f) + j;

				Vector3 pos = new Vector3(x, y, .2f);

				GameObject o = Instantiate(HexPrefab);
				o.transform.SetParent(transform, false);
				o.transform.localPosition = pos;
			}
		}
    }

	public Vector3 GetRealXYFromMapXY(int x, int y){
		return new Vector3(
			x * ((OuterRadius + ShortEdge)/2f),
			( x%2==0 ? 0f : 0.5f) + y,
			0f	);
	}

	public struct SAdjHex{
		public int x;public int y;public string dir;
		public SAdjHex(int x_,int y_,string dir_){x=x_;y=y_;dir=dir_;}}

	public static SAdjHex[] GetAdjHexes(int x, int y){
		SAdjHex[] arr = new SAdjHex[6];
		arr[0] = new SAdjHex(x,y+1,"N");
		arr[1] = new SAdjHex(x+1, (x%2==0 ? y : y+1),"NE");
		arr[2] = new SAdjHex(x+1, (x%2==0 ? y-1 : y),"SE");
		arr[3] = new SAdjHex(x,y-1,"S");
		arr[4] = new SAdjHex(x-1, (x%2==0 ? y : y+1),"SW");
		arr[5] = new SAdjHex(x-1, (x%2==0 ? y-1 : y),"NW");
		return arr;
	}
	public static Vector2 GetNewCoords(int x, int y, int dir){
		string sDir = GlobalFuncs.DirIntToStr(dir);
		SAdjHex[] arr = GetAdjHexes(x,y);
		Vector2 v = new Vector2();
		foreach(SAdjHex s in arr){
			if(s.dir == sDir){
				v.x = s.x;
				v.y = s.y;
				return v;
			}
		}
		return v;
	}
}
