using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour{
    [SerializeField] private PlayerTower playerTower;
    [SerializeField] private Vector3 offsetPoisiton;
    [SerializeField] private Vector3 offsetRotation;
	[SerializeField] private float moveSpeed;
	private Vector3 targetOffsetPosition;
	private void Start()	{
		targetOffsetPosition = playerTower.transform.position + offsetPoisiton;
	}
	private void OnEnable()	{
		playerTower.HumanAdded += OnHumanAdded;
	}
	private void OnDisable()	{
		playerTower.HumanAdded -= OnHumanAdded;
	}
	private void Update()	{
		UpdatePoisition();
		targetOffsetPosition = Vector3.MoveTowards(offsetPoisiton, targetOffsetPosition, moveSpeed * Time.deltaTime);
	}
	private void UpdatePoisition() {
		transform.position = playerTower.transform.position;
		transform.localPosition += offsetPoisiton;
		Vector3 lookAtPoint = playerTower.transform.position + offsetRotation;
        transform.LookAt(lookAtPoint);
    }
	private void OnHumanAdded(int count) {
		targetOffsetPosition += offsetPoisiton + (Vector3.up + Vector3.back) * count/2;
		UpdatePoisition();
	}
}
