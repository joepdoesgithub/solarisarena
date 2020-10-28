using System;
using System.Collections.Generic;
using UnityEngine;

public static class TextParser{
	public static void GetInitPlayerUnit(out string unitName, out int x, out int y, out string dir){
		string line = GetAllFriendlyLines()[0];
		unitName = line.Split(',')[0];
		x = int.Parse(line.Split(',')[1]);
		y = int.Parse(line.Split(',')[2]);
		dir = line.Split(',')[3];
	}

	public static string[] GetAllFriendlyLines(){
		TextAsset txt = Resources.Load("GameInit") as TextAsset;
		string[] lines = txt.text.Split(new[] { Environment.NewLine },StringSplitOptions.None);
		List<string> l = new List<string>();

		bool read = false;
		for(int i = 0;i<lines.Length - 1;i++){
			if(lines[i] == "[FRIEND]")
				read = true;
			else if(read && lines[i] == "[/FRIEND]")
				return l.ToArray();
			else if(read)
				l.Add(lines[i]);
		}
		return new string[]{};
	}

	public static string[] GetAllEnemyLines(){
		TextAsset txt = Resources.Load("GameInit") as TextAsset;
		string[] lines = txt.text.Split(new[] { Environment.NewLine },StringSplitOptions.None);
		List<string> l = new List<string>();

		bool read = false;
		for(int i = 0;i<lines.Length - 1;i++){
			if(lines[i] == "[ENEMY]")
				read = true;
			else if(read && lines[i] == "[/ENEMY]")
				return l.ToArray();
			else if(read)
				l.Add(lines[i]);
		}
		return new string[]{};
	}
}