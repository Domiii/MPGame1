using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/// <summary>
/// Shooter is used by other components to shoot stuff.
/// Make sure to setup it's weapon first (especially the bulletPrefab)!
/// </summary>
public class Shooter2D : MonoBehaviour {
	public WeaponConfig weapon = new WeaponConfig ();
	public float turnSpeed = 1000.0f;
	public Transform shootTransform;

	Vector2 currentTarget;
	float lastShotTime = -10000;
	bool isAttacking;

	public bool IsAttacking {
		get {
			return isAttacking;
		}
	}

	public void StartShootingAt (Vector2 target) {
		currentTarget = target;
		isAttacking = true;
	}

	public void StopShooting () {
		isAttacking = false;
	}


	public void ShootAt (Transform t) {
		ShootAt (t.position);
	}

	public void ShootAt (Vector2 target) {
		if (weapon == null || weapon.bulletPrefab == null) {
			return;
		}

		var angle = GetAngleToward(target);
		if (weapon.bulletCount > 1) {
			// shoot N bullets in a cone from -coneAngle/2 to +coneAngle/2
			angle -= weapon.coneAngle/2;
			var deltaAngle = weapon.coneAngle / (weapon.bulletCount - 1);

			for (var i = 0; i < weapon.bulletCount; ++i) {
				//dir.Normalize ();
				ShootBullet (angle);
				angle += deltaAngle;

			}
		} else {
			// just one bullet straight forward
			ShootBullet (angle);
		}

		// reset shoot time
		lastShotTime = Time.time;
	}

	void Awake () {
		if (weapon == null) {
			print ("Shooter is missing Weapon");
			return;
		}
		if (weapon.bulletPrefab == null) {
			print ("Shooter's Weapon is missing Bullet Prefab");
			return;
		}

		if (shootTransform == null) {
			shootTransform = transform;
		}
		isAttacking = false;
	}

	void Update () {
		if (isAttacking) {
			// some debug stuff
			var dir = currentTarget - (Vector2)transform.position;
			Debug.DrawRay (transform.position, dir);

			// rotate toward target
			//RotateTowardTarget ();

			// keep shooting
			var delay = Time.time - lastShotTime;
			if (delay < weapon.attackDelay) {
				// still on cooldown
				return;
			}
			ShootAt (currentTarget);
		}
	}

	float GetAngleToward (Vector2 target) {
		var dir = target - (Vector2)shootTransform.position;
		return GetAngleFromDirection (dir);
	}

	float GetAngleFromDirection (Vector2 dir) {
		return Vector2.SignedAngle(Vector2.right, dir);
	}

	Quaternion GetRotationFromAngle(float targetAngle) {
		return Quaternion.AngleAxis (targetAngle, Vector3.back);
	}

	void RotateTowardTarget () {
		var rigidbody = GetComponent<Rigidbody2D> ();
		if (rigidbody != null && (rigidbody.constraints & RigidbodyConstraints2D.FreezePositionY) != 0) {
			// don't rotate if rotation has been constrained
			return;
		}

		var agent = GetComponent<NavMeshAgent> ();
		if (agent != null) {
			turnSpeed = agent.angularSpeed;
		}
		var targetAngle = GetAngleToward (currentTarget);
		var targetQ = GetRotationFromAngle(targetAngle);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, targetQ, Time.deltaTime * turnSpeed);
	}

	void ShootBullet(float angle) {
		ShootBullet (new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)));
	}

	void ShootBullet (Vector2 dir) {
		// create a new bullet
		var rotation = GetRotationFromAngle(GetAngleFromDirection (dir));
		var go = PhotonNetwork.Instantiate (weapon.bulletPrefab.name, shootTransform.position, rotation, 0);
		var bullet = go.GetComponent<Bullet2D> ();

		// set bullet faction
		FactionManager.SetFaction (bullet.gameObject, gameObject);

		// set velocity
		var rigidbody = bullet.GetComponent<Rigidbody2D> ();
		rigidbody.velocity = dir * bullet.speed;
		bullet.damageMin = weapon.damageMin;
		bullet.damageMax = weapon.damageMax;

		if (weapon.ttl > 0) {
			StartCoroutine (bullet.DestroyThisLater(weapon.ttl));
		}
	}

	void OnDeath (DamageInfo damageInfo) {
		enabled = false;
	}
}