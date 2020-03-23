using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
	[Header("Player Controller")]
	public Animator anim;
      

      [Header("Player Variables")]
	[Range(0, 200)] public float hp;
      [Range(0, 10)] public float walkSpeed;
      [Range(0, 10)] public float turnSpeed;

      private Transform _camTrans;
	private Vector3 movement;
      private Vector3 relativeMovement;
	private Quaternion newRotation;

	void Start()
	{
            _camTrans = Camera.main.transform;
	}

	
	// Update is called once per frame
	void Update () 
	{
		movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		relativeMovement = _camTrans.TransformVector(movement);
		
		if (movement.magnitude > 0)
		{
			anim.SetBool("walk", true);
			
			transform.Translate(transform.forward * walkSpeed * Time.deltaTime, Space.World);

			newRotation = Quaternion.LookRotation(relativeMovement);
			transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * turnSpeed);
			transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
		} else
		{
			anim.SetBool("walk", false);
		}
	}

	public void GetDamage(int _damage)
	{
		if(hp >= 0)
		{
                  hp -= _damage;
		
			if(hp <= 0)
			{
				anim.SetBool("dead", true);
			}
		}
	}
}