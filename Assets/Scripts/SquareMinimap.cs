using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This makes sure that an attached Camera is always square in size
/// See: http://blog.theknightsofunity.com/implementing-minimap-unity/
/// </summary>
public class SquareMinimap : MonoBehaviour {
	public float cameraHeight = 0.125f;

	Resolution res;
	Camera camera;

	void Start () {
		// fix camera size at the beginning
		Reset ();

	}

	void Update () {
		if (res.height != Screen.currentResolution.height || res.width != Screen.currentResolution.width) {
			// fix camera size if resolution changed
			Reset ();
		}
	}

	void Reset() {
		camera = GetComponent<Camera> ();
		res = Screen.currentResolution;


		var size = cameraHeight * Screen.currentResolution.height;
		camera.pixelRect = new Rect (0, 0, size, size);
	}
}
