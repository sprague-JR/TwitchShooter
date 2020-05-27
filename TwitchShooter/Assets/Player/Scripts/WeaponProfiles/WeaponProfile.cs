using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponProfile : ScriptableObject
{
	public AmmoType ammo;
	public int ammoCap;
	public bool Semi;
	public float fireDelay;

	public float fireTimer = Mathf.Infinity;
	private bool hasFired = false;



	protected int OnFire(int ammoCount)
	{

		if(fireTimer >= fireDelay && (!Semi || !hasFired))
		{
			//ready to fire
			Fire();
			ammoCount --;
			fireTimer = 0f;
			hasFired = true;


		}

		return ammoCount;
	}


	public int OnUpdate(int ammoCount, float fireKey)
	{
		fireTimer += Time.deltaTime;

		if(fireKey > 0 && ammoCount > 0)
		{
			ammoCount = OnFire(ammoCount);
		}

		if (fireKey == 0)
			hasFired = false;





		return ammoCount;
	}



	public abstract void Fire();
	




}

public enum AmmoType
{
	bolt, shell
}
