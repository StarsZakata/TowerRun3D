using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[RequireComponent(typeof(Rigidbody))]
public class PathFollower : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private PathCreator pathCreator;

    private Rigidbody rigidbodyMan;
    private float distanceTravelled;

	private void Start()
	{
		rigidbodyMan = GetComponent<Rigidbody>();
		rigidbodyMan.MovePosition(pathCreator.path.GetPointAtDistance(distanceTravelled));
	}

	private void Update()
	{
		distanceTravelled += Time.deltaTime * speed;
		Vector3 nextPoint = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
		nextPoint.y = transform.position.y;
		
		transform.LookAt(nextPoint);
		rigidbodyMan.MovePosition(nextPoint);
	}
}
