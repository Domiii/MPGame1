using UnityEngine;
using System.Collections;

public class ClickToShoot2D : MonoBehaviour {
	bool isShooting = false;
	Plane testPlane;

	void Start () {
		// create a plane at (0,0,0) whose normal points upward (i.e. an XZ-aligned plane through the origin)
		testPlane = new Plane ();
	}

	void Update () {
		if (!PlayerInputManager.Instance.CanGameReceiveClick) {
			_StopShooting ();
		}
		else if (Input.GetMouseButton (0)) {
			_StartShooting ();
		} else if (isShooting) {
			_StopShooting ();
		}
	}

	void _StartShooting () {
		// see: http://answers.unity3d.com/questions/269760/ray-finding-out-x-and-z-coordinates-where-it-inter.html
		// cast ray onto plane that goes through our current position and normal points upward
		isShooting = true;
		var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		SendMessage ("StartShootingAt", (Vector2)target);
	}

	void _StopShooting () {
		SendMessage ("StopShooting", SendMessageOptions.DontRequireReceiver);
		isShooting = false;
	}
}