using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component is only added to the local player!
/// </summary>
[RequireComponent(typeof(ClickToShoot2D))]
public class LocalPlayerControls : MonoBehaviour {
	public float speed = 5;

	public void StartLocalPlayer() {
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
	void FixedUpdate () {
		var move = Vector3.zero;
		move.x = Input.GetAxisRaw ("Horizontal");
		move.y = Input.GetAxisRaw ("Vertical");
		move.Normalize ();

		transform.Translate (move);
	}
}
