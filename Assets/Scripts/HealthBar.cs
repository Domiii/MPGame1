using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//[RequireComponent(typeof(Image))]
public class HealthBar : MonoBehaviour {
	public Unit unit;
	public Color goodColor;
	public Color badColor;

	Image image;

	HealthBar() {
	}

	void Reset() {
		goodColor = Color.green;
		badColor = Color.red;
	}

	void Start() {
		image = GetComponent<Image> ();

		// try finding the Unit
		if (unit == null) {
			// children
			unit = GetComponentInChildren<Unit> ();
		}
		if (unit == null && transform.parent != null) {
			// parent
			unit = GetComponentInParent<Unit> ();
			if (unit == null) {
				// siblings
				unit = transform.parent.GetComponentInChildren<Unit> ();
			}
		}
	}

	void Update() {
		var ratio = unit.health / unit.maxHealth;

		// set color
		var color = Color.Lerp(badColor, goodColor, ratio);
		image.color = color;

		// set size
		image.fillAmount = ratio;
	}
}