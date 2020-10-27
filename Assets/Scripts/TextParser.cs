using System;
using UnityEngine;

public static class TextParser{
	public static void GetInitPlayerUnit(out string unitName, out int x, out int y, out string dir){
		TextAsset txt = Resources.Load("GameInit") as TextAsset;
		string[] lines = txt.text.Split(new[] { Environment.NewLine },StringSplitOptions.None);
		string line = "";
		for(int i = 0;i<lines.Length - 1;i++){
			if(lines[i] == "[FRIEND]"){
				line = lines[i+1];
				break;
			}
		}

		unitName = line.Split(',')[0];
		x = int.Parse(line.Split(',')[1]);
		y = int.Parse(line.Split(',')[2]);
		dir = line.Split(',')[3];
	}
}