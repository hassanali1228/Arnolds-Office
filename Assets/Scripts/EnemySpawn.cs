using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	public GameManager m_gameManager;

	public GameObject deskPersonPrefab;
	public GameObject workerPrefab;
	public GameObject bossPrefab;

	public Transform m_transform;

	public bool hasSpawned = false;
	public bool lastHasSpawned = false;

	public float countDown = 0;
	public static int countDownLength = 15;

	void Start() {

	}
	
	// Update is called once per frame
	void Update () {
		if (countDown > 0)
			countDown -= 1 * Time.deltaTime;

		if (countDown < 1) {
			countDown = 0;
		}
			
	}

	public void Reset() {
		countDown = (float) countDownLength;
	}

	public void Decide() {
		if (m_transform.childCount > 0)
			return;
		
		
		int chance = Random.Range (1, 100);
		int difficulty = m_gameManager.level;

		// Boss
		if (chance <= (5 * difficulty)) {
			spawnBoss ();
		}

		else if (chance <= (difficulty * 10)) {
			spawnDeskPerson ();
		} 
			
		else if (chance <= (difficulty * 15)) { // probability of 25%
			spawnWorker ();
		} 

	}

	void spawnBoss() {
		hasSpawned = true;
		GameObject go = Instantiate (bossPrefab, m_transform) as GameObject;
		BossController worker = go.GetComponent<BossController> ();
		go.GetComponent<Transform> ().localPosition = new Vector3 (0, 0, 0);

		worker.m_spawn = this;
		worker.m_gameManager = m_gameManager;
	}

	void spawnWorker() {
		hasSpawned = true;
		GameObject go = Instantiate (workerPrefab, m_transform) as GameObject;
		WorkerController worker = go.GetComponent<WorkerController> ();
		go.GetComponent<Transform> ().localPosition = new Vector3 (0, 0, 0);

		worker.m_spawn = this;
		worker.m_gameManager = m_gameManager;
	}

	void spawnDeskPerson() {
		hasSpawned = true;
		GameObject go = Instantiate (deskPersonPrefab, m_transform) as GameObject;
		DeskWorker worker = go.GetComponent<DeskWorker> ();
		go.GetComponent<Transform> ().localPosition = new Vector3 (0, 0, 0);

		worker.m_spawn = this;
		worker.m_gameManager = m_gameManager;
	}

}
