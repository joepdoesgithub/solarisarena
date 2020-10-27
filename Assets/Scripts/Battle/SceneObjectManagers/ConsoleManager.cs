using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleManager : MonoBehaviour{
	public Text consoleText;

	List<string> msgs;
	string[] screenlines;

	int lineLength = 105;
	int maxLines = 8;

    // Start is called before the first frame update
    void Start(){
        consoleText.text = "";

		msgs = new List<string>();
		screenlines = new string[maxLines];
    }
	
	public void PostMessage(string msg){
		msgs.Add("> " + msg);

		for(int i = 0;i<maxLines;i++)
			screenlines[i] = "";
		int index = screenlines.Length - 1;
		int msgIndex = msgs.Count - 1;

		while(index >= 0 && msgIndex >= 0){
			string s = msgs[msgIndex];
			msgIndex--;
			string[] cuts = CutMessage(s);

			for(int i = cuts.Length - 1;i>=0 && index >= 0;i--){
				screenlines[index] = cuts[i];
				index--;
			}
		}

		string t = "";
		for(int i = 0;i<screenlines.Length;i++)
			t += screenlines[i] + "\n";
		consoleText.text = t;
	}

	string[] CutMessage(string msg){
		if(msg.Length <= lineLength)
			return new string[]{msg};
		
		List<string> l = new List<string>();
		while(true){
			l.Add(msg.Substring(0,lineLength));
			msg = msg.Substring(lineLength,msg.Length - lineLength);
			if(msg.Length <= lineLength){
				l.Add(msg);
				break;
			}
		}
		return l.ToArray();
	}
}
