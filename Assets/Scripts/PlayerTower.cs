using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerTower : MonoBehaviour{
	[SerializeField] private Human StartHuman;
	[SerializeField] private Transform distanceChecker;
	[SerializeField] private float fixationMaxDistance;
	[SerializeField] private BoxCollider checkCollider;
	private List<Human> humans;
	public event UnityAction<int> HumanAdded;


	private void Start(){
		humans = new List<Human>();
		Vector3 spawnPoint = transform.position;
		humans.Add(Instantiate(StartHuman, spawnPoint, Quaternion.identity, transform));
		humans[0].Run();
		HumanAdded?.Invoke(humans.Count);
	}

	private void OnTriggerEnter(Collider other)	{
		if (other.gameObject.TryGetComponent(out Human human)){
			Tower collisionTower = human.GetComponentInParent<Tower>();
			if (other != null)
			{
				List<Human> collectedhumans = collisionTower.CollectHuman(distanceChecker, fixationMaxDistance);
				if (collectedhumans != null)
				{
					humans[0].StopRun();
					for (int i = collectedhumans.Count - 1; i >= 0; i--)
					{
						Human insertHuman = collectedhumans[i];
						InsetrHuman(insertHuman);
						DisplaysChecker(insertHuman);
					}
					HumanAdded?.Invoke(humans.Count);

					humans[0].Run();
				}
				collisionTower.Break();
			}
		}
	}

	private void InsetrHuman(Human collectedhumans) {
		humans.Insert(0, collectedhumans);
		SetHumanPoisition(collectedhumans);
	}

	private void SetHumanPoisition(Human human) {
		human.transform.parent = transform;
		human.transform.localPosition = new Vector3(0, human.transform.localPosition.y, 0);
		human.transform.localRotation = Quaternion.identity;
	}

	private void DisplaysChecker(Human human) {
		float displaceScale = 1.5f;
		Vector3 distanceChecerNewPosition = distanceChecker.position;
		distanceChecerNewPosition.y -= human.transform.localScale.y * displaceScale;
		distanceChecker.position = distanceChecerNewPosition;

		checkCollider.center = distanceChecker.localPosition;
	}
}
