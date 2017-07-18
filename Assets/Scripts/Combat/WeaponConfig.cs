using UnityEngine;
using System.Collections;

[System.Serializable]
public class WeaponConfig {
	public Bullet bulletPrefab;
	public float attackDelay = 1.2f;
	public float damageMin = 10;
	public float damageMax = 15;

	[Header("Time to live")]
	public float ttl = 1.0f;

	public int bulletCount = 3;
	public float coneAngle = 30;
}