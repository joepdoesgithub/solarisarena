using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour{
	public Text infoText;

	GameObject playerUnit = null;
	GameObject gameManager = null;
	int run;


    // Update is called once per frame
    void Update(){
		if(playerUnit == null){playerUnit = GameObject.Find("PlayerUnit");return;}
		if(gameManager == null){gameManager = GameObject.Find("GameManager");return;}
        
		UnitController uc = playerUnit.GetComponent<UnitController>();		
		run = (int)(1.5f * uc.unit.walk + 0.5f);
		infoText.text = 
				string.Format("Spd: {0} > {1}  Walk: {2}  Run:{3}\n",uc.speed,uc.newSpeed,uc.unit.walk,run) +
				string.Format("Dir: {0} > {1}\n",GlobalFuncs.DirIntToStr(uc.mechDir),GlobalFuncs.DirIntToStr(uc.mechNewDir));
    }
}
