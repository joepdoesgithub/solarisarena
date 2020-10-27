using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour{
	float displacement = 3.3f;

	GameObject player = null;
	float z;
	Vector3 pos;

	void Start(){
		z = Camera.main.transform.position.z;
	}

    // Update is called once per frame
    void Update(){
		if(player == null)
			player = GameObject.Find("PlayerUnit");

		pos.x = player.transform.position.x + displacement;
		pos.y = player.transform.position.y;
		pos.z = z;

		Camera.main.transform.position = pos;
    }
}
