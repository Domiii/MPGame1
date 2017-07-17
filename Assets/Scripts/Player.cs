using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.MonoBehaviour {
	void Awake () {
		if (photonView.isMine) {
			AddLocalComponent<LocalPlayerControls> ("StartLocalPlayer");
			var material = GameObject.Find ("Ground").GetComponent<MeshRenderer> ().material;
		}
	}

	void AddLocalComponent<C>(string startMethodName = null) 
		where C : Component
	{
		var comp = GetComponent<C> ();
		if (comp == null) {
			comp = gameObject.AddComponent <C> ();
		}
		if (startMethodName != null) {
			comp.SendMessage (startMethodName);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
