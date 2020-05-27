using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject[] weapons;
	public int active = 1;

	private KeyCode[] numerics = 
	{
		KeyCode.Alpha1,
		KeyCode.Alpha2,
		KeyCode.Alpha3,
		KeyCode.Alpha4,
		KeyCode.Alpha5,
		KeyCode.Alpha6,
		KeyCode.Alpha7,
		KeyCode.Alpha8,
		KeyCode.Alpha9
	};
	private bool switching;

    void Start()
    {
		for(int i = 0; i < weapons.Length; i++)
		{
			weapons[i].GetComponent<WeaponComponent>().QuickDeactivate();
		}

		
		weapons[active].GetComponent<WeaponComponent>().QuickActivate();
        
    }

    // Update is called once per frame
    void Update()
    {
		if (!switching)
		{
			for(int i = 0; i < Mathf.Min(numerics.Length,weapons.Length);i++)
			{
				if(Input.GetKeyDown(numerics[i]) && i != active)
				{
					Debug.Log(i + 1);
					StartCoroutine(switchWeapon(active, i));
					active = i;
					return;
				}
			}

			weapons[active].GetComponent<WeaponComponent>().OnActiveUpdate();
		}
    }



	private IEnumerator switchWeapon(int a, int b)
	{
		switching = true;
		StartCoroutine(weapons[a].GetComponent<WeaponComponent>().Deactivate());

		while (!weapons[a].GetComponent<WeaponComponent>().active)
			yield return null;



		StartCoroutine(weapons[b].GetComponent<WeaponComponent>().Activate());
		switching = false;
	}

}
