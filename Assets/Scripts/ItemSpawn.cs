using UnityEngine;
using System.Collections;

public class ItemSpawn : MonoBehaviour {

	public Transform m_transform;

	public GameObject staplerPrefab;
	public GameObject paperPrefab;
	public GameObject penPrefab;
	public GameManager m_gameManager;

	public float countDown = 0;
	public static int countDownLength = 15;
	public bool hasSpawned = false;

	// Use this for initialization
	void Start () {
	
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

		if (countDown != 0)
			return;


		int chance = Random.Range (1, 100);
		int difficulty = m_gameManager.level;


		if ((chance/difficulty) <= 15) {
			Spawn (staplerPrefab);
		}

		else if ((chance / difficulty) <=50) {
			Spawn (paperPrefab);
		} 
		//			
		else if ((chance / difficulty) <=80) { // probability of 25%
			Spawn (penPrefab);
		} 

	}

	void Spawn (GameObject prefab) {
		hasSpawned = true;
		GameObject go = Instantiate (prefab, m_transform) as GameObject;
		PowerUp pu = go.GetComponent<PowerUp> ();
		go.GetComponent<Transform> ().localPosition = new Vector3 (0, 0, 0);

		pu.m_spawn = this;
		pu.m_gameManager = m_gameManager;
	}
}
