using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitManager : MonoBehaviour{
	public bool FinishedInit = false;
	public GameObject UnitPrefab;

	List<GameObject> friends;
	List<GameObject> enemies;

    // Start is called before the first frame update
    void Start(){
		// Read the units
		friends = new List<GameObject>();
		enemies = new List<GameObject>();
		string[] ls = TextParser.GetAllFriendlyLines();
		if(ls.Length > 1){
			for(int i = 1;i<ls.Length; i++){
				string unitName = ls[i].Split(',')[0];
				int x = int.Parse(ls[i].Split(',')[1]);
				int y = int.Parse(ls[i].Split(',')[2]);
				string dir = ls[i].Split(',')[3];

				GameObject obj = Instantiate(UnitPrefab);
				obj.name = "Friendly_" + i;
				obj.GetComponent<UnitController>().SetUnit(unitName,x,y,dir);
				obj.GetComponent<UnitController>().PrepTurn();
				obj.GetComponent<SpriteRenderer>().sprite = GetUnitSprite(unitName);
				friends.Add(obj);

				GlobalFuncs.DrawStuff(obj);
			}
		}

		ls = TextParser.GetAllEnemyLines();
		if(ls.Length > 0){
			for(int i = 0;i<ls.Length; i++){
				string unitName = ls[i].Split(',')[0];
				int x = int.Parse(ls[i].Split(',')[1]);
				int y = int.Parse(ls[i].Split(',')[2]);
				string dir = ls[i].Split(',')[3];

				GameObject obj = Instantiate(UnitPrefab);
				obj.name = "Enemy_" + i;
				obj.GetComponent<UnitController>().SetUnit(unitName,x,y,dir);
				obj.GetComponent<UnitController>().PrepTurn();
				obj.GetComponent<SpriteRenderer>().sprite = GetUnitSprite(unitName);
				enemies.Add(obj);

				GlobalFuncs.DrawStuff(obj);
			}
		}

        FinishedInit = true;
    }

	public static Sprite GetUnitSprite(string unitName){
		Sprite unitSprite = Resources.Load<Sprite>("UnitImages/" + unitName);				
		if(unitSprite == null)
			unitSprite = Resources.Load<Sprite>("UnitImages/" + unitName.Split(' ')[0]);
		if(unitSprite == null)
			Debug.LogError(string.Format("UnitManager.GetUnitSprie: Can't load image with name {0}",unitName));
		return unitSprite;
	}
}
