using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Shotgun", menuName = "ScriptableObjects/Weapons/Shotgun", order = 1)]
public class ShotgunProfile : WeaponProfile
{



	public override void Fire()
	{
		Debug.Log("Bang");



	}
}
