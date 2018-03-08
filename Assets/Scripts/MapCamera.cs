using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapCamera : MonoBehaviour {

	public Shader unlitShader;

	void Reset() {
		DoUpdate ();
	}

	void Start() {
		DoUpdate ();
	}

	public void DoUpdate() {
		unlitShader = unlitShader ?? Shader.Find("Unlit/Color");
		GetComponent<Camera>().SetReplacementShader(unlitShader,"");
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(MapCamera))]
public class MapCameraEditor : Editor {
	Shader lastShader;

	public override void OnInspectorGUI ()
	{
		var t = (MapCamera)target;
		if (lastShader != t.unlitShader) {
			lastShader = t.unlitShader;
			t.DoUpdate ();
		}

		base.OnInspectorGUI ();
	}
}
#endif