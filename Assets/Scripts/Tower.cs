using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

	[SerializeField] private Vector2Int humanInTowerRange;
	[SerializeField] private Human[] humansTemplates;

	private List<Human> humanInTower;

	private void Start()
	{
		humanInTower = new List<Human>();
		int humaninTowerCount = Random.Range(humanInTowerRange.x, humanInTowerRange.y);
		SpawnHumans(humaninTowerCount);
	}

	private void SpawnHumans(int humanCount) {
		Vector3 spawnPoint = transform.position;
		for (int i = 0; i < humanCount; i++) {
			Human spawnedHuman = humansTemplates[Random.Range(0, humansTemplates.Length)];
			humanInTower.Add(Instantiate(spawnedHuman, spawnPoint, Quaternion.identity, transform));
			humanInTower[i].transform.localPosition = new Vector3(0, humanInTower[i].transform.localPosition.y, 0);
			spawnPoint = humanInTower[i].FixationPoint.position;
		}
	}


	public List<Human> CollectHuman(Transform distanceChecker, float fixationMaxDistance) {
		for (int i = 0; i < humanInTower.Count; i++){
			float distanceBetweenPoints = CheckDistanceY(distanceChecker, humanInTower[i].FixationPoint.transform);
			if (distanceBetweenPoints < fixationMaxDistance) {
				List<Human> collectedHumans = humanInTower.GetRange(0, i + 1);
				humanInTower.RemoveRange(0, i + 1);
				return collectedHumans;
			}
		}
		return null;
	}

	private float CheckDistanceY(Transform distanceChecker,Transform humanFixationPoint) {
		Vector3 distanceCheckerY = new Vector3(0, distanceChecker.position.y, 0);
		Vector3 humanFixationPointY = new Vector3(0, humanFixationPoint.position.y, 0);
		return Vector3.Distance(distanceCheckerY, humanFixationPointY);
	}

	public void Break()
	{
		//rigidbody=добавляем iskinematic=false
		Destroy(gameObject);
	}
}
