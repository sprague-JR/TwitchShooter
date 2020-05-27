using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crossbow", menuName = "ScriptableObjects/Weapons/Crossbow", order = 1)]
public class CrossbowProfile : WeaponProfile
{



	public override void Fire()
	{
		Debug.Log("twang!");



	}
}
