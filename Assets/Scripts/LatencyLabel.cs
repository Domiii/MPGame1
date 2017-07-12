using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LatencyLabel : MonoBehaviour {
	public Text text;

	// Use this for initialization
	void Start () {
		if (text == null) {
			text = GetComponent<Text> ();
		}

		StartCoroutine (UpdateText());
	}

	IEnumerator UpdateText() {
		while (text != null) {
			text.text = PhotonNetwork.GetPing () + " ms";
			yield return new WaitForSeconds (0.5f);
		}
	}
}
