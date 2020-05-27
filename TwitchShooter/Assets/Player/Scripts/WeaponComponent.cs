using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
	// Start is called before the first frame update

	public WeaponProfile primary;
	public bool active;


    // Update is called once per frame
    public void OnActiveUpdate()
    {
		primary.OnUpdate(10,Input.GetAxisRaw("Fire1"));
        
    }





	public void QuickDeactivate()
	{
		active = false;
		foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
		{
			mr.enabled = false;
		}

		foreach (SkinnedMeshRenderer smr in GetComponentsInChildren<SkinnedMeshRenderer>())
		{
			smr.enabled = false;
		}
	}

	public IEnumerator Deactivate()
	{
		yield return null;
		//animate weapon being lowered;

		QuickDeactivate();
	}


	public void QuickActivate()
	{
		active = true;
		foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
		{
			mr.enabled = true;
		}

		foreach (SkinnedMeshRenderer smr in GetComponentsInChildren<SkinnedMeshRenderer>())
		{
			smr.enabled = false;
		}
	}

	public IEnumerator Activate()
	{
		QuickActivate();

		yield return null;
		//animate weapon being raised
	}

}
