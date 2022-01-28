using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Human : MonoBehaviour{
	[SerializeField] private Transform fixationpoint;
	public Transform FixationPoint => fixationpoint; //(можно только читать)
	private Animator animator;
	private void Awake()	{
		animator = GetComponent<Animator>();
	}
	public void Run() {
		animator.SetBool("isRun", true);
	}
	public void StopRun() {
		animator.SetBool("isRun", false);
	}
}
