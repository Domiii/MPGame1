using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.MonoBehaviour {
	
	void Start () {
		if (photonView.isMine) {
			StartLocalPlayer ();
		}
	}

	// only run this method on start if the object belongs to local player
	void StartLocalPlayer() {
		InitMainCamera ();
	}

	void InitMainCamera() {
		// make sure, the main camera always rolls with the local player
		Camera.main.transform.parent = transform;
		var p = Camera.main.transform.localPosition;
		p.x = p.y = 0;
		Camera.main.transform.localPosition = p;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
