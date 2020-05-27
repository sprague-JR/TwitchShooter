using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
	// Start is called before the first frame update
	CharacterController cc;
	public LayerMask grounded;
	Vector3 direction;
	public float maxSpeed, acceleration, JumpVelocity, prevJump;
	float curSpeed;
	public float gravity;

	public float mouseX, mouseY;
	float timeGrounded;




	public float bobAmplitude, bobPhase;
	float bobStart;


	public float weapon_tiltX;
	float weapon_lerpx;

	public float weapon_tilty;
	float weapon_lerpy;

    void Start()
    {
		cc = GetComponent<CharacterController>();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		direction = Vector3.zero;
		bobStart = transform.GetChild(0).transform.localPosition.y;
    }

	// Update is called once per frame
	void Update()
	{

		if (isGrounded())
		{
			timeGrounded += Time.deltaTime;
			direction = (transform.forward * Input.GetAxisRaw("Vertical")) +
				(transform.right * Input.GetAxisRaw("Horizontal"));
			direction = direction.normalized;


			if (direction.sqrMagnitude > 0)
				cc.Move(direction * curSpeed * Time.deltaTime);
			else
				cc.Move(Vector3.Lerp(cc.velocity * Time.deltaTime, Vector3.zero, acceleration * 2f * Time.deltaTime));
			
			if (Input.GetAxisRaw("Jump") - prevJump > 0)
			{
				cc.Move(Vector3.up * JumpVelocity * Time.deltaTime);
			}
			
			prevJump = Input.GetAxisRaw("Jump");

		}
		else
		{
			timeGrounded = 0f;
			cc.Move(((direction * curSpeed) + Vector3.up * (cc.velocity.y - gravity)) * Time.deltaTime);

		}

		
		curSpeed = Mathf.Lerp(curSpeed, direction.magnitude * maxSpeed, acceleration * Time.deltaTime);
	}

	private void LateUpdate()
	{
		

		
		//----------------------------------Horizontal rotation---------------------------------------
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseX * Time.deltaTime);
		



		//---------------------------------Vertical rotation------------------------------------------
		Vector3 pitch = transform.GetChild(0).localRotation.eulerAngles;
		pitch.x -= (Input.GetAxis("Mouse Y") * mouseY * Time.deltaTime) % 360f;
		if (pitch.x < 275f && pitch.x > 180f) pitch.x = 275f;
		if (pitch.x > 85f && pitch.x < 180f) pitch.x = 85f;

		transform.GetChild(0).transform.localRotation = Quaternion.Euler(pitch);

		//--------------------------------Headbob animation-------------------------------------------
		if(isGrounded())
			transform.GetChild(0).localPosition = new Vector3(0, bobStart + (bobAmplitude * Mathf.Sin(bobPhase * Time.time) * (curSpeed / maxSpeed)), 0);


		//-------------------------------Weapon animation---------------------------------------------

		WeaponManager wm = GetComponent<WeaponManager>();
		if (wm)
		{
			GameObject current = wm.weapons[wm.active];
			weapon_lerpx = Mathf.Lerp(weapon_lerpx, Input.GetAxisRaw("Mouse X") * weapon_tiltX, 6f * Time.deltaTime);
			weapon_lerpy = Mathf.Lerp(weapon_lerpy, Input.GetAxisRaw("Mouse Y") * weapon_tilty, 6f * Time.deltaTime);
			current.transform.localRotation = Quaternion.Euler(weapon_lerpy, 180 + weapon_lerpx, 0);

		}
		
		
	}

	private bool isGrounded()
	{
		return Physics.OverlapSphere(transform.position + (Vector3.down * 0.6f), 0.5f, grounded).Length > 0;
	}
}
