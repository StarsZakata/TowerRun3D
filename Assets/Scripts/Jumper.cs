using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
	[SerializeField] private float jumpForce;
	private Rigidbody rirgidbody;
	private bool isgrounded;

	private void Start()
	{
		rirgidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && isgrounded == true) {
			isgrounded = false;
				rirgidbody.AddForce(Vector3.up * jumpForce);		
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.TryGetComponent(out Road road)){
			isgrounded = true;
		}
	}
}
