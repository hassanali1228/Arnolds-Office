using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[SerializeField]
	private EnemySpawn[] enemySpawns;
	[SerializeField]
	private Elevator[] elevators;
	private ItemSpawn[] itemSpawns;
	public int level = 0;
	public int floor = 1;
	public int score = 0;

	public float initialTime;

	public Transform m_playerTransform;
	public PlayerController m_playerController;

	public Text scoreText;
	public Text levelText;

	public bool hasUsedElevator;

	public AudioClip winGameSound;
	public AudioClip loseGameSound;

	float elevatorCountDown = 0;
	float countDownLength = 3f;
	bool counting = false;
		
	public AudioSource m_audioSource;
	public Camera m_camera;

	public GameObject endGameObject;

	public float timer = 0f;
	public float timerLength = 15f;

	public GameObject youreFired;

	// Use this for initialization
	void Start () {


		enemySpawns = Object.FindObjectsOfType (typeof(EnemySpawn)) as EnemySpawn[];
		elevators = Object.FindObjectsOfType (typeof(Elevator)) as Elevator[];
		itemSpawns = Object.FindObjectsOfType (typeof(ItemSpawn)) as ItemSpawn[];


		// Ignore collisions between ball and player
		Physics2D.IgnoreLayerCollision(9, 10, true);
		// Playr and enemy
		Physics2D.IgnoreLayerCollision(10, 11, true);
		Physics2D.IgnoreLayerCollision(10, 12, true);
		Physics2D.IgnoreLayerCollision(10, 13, true);
		Physics2D.IgnoreLayerCollision(12, 13, true);
		Physics2D.IgnoreLayerCollision(12, 11, true);
		Physics2D.IgnoreLayerCollision(13, 11, true);
		// enemies among themselves

		// Power up and enemies
		Physics2D.IgnoreLayerCollision(11, 14, true);
		Physics2D.IgnoreLayerCollision(12, 14, true);
		Physics2D.IgnoreLayerCollision(13, 14, true);

		initialTime = Time.timeSinceLevelLoad;

		hasUsedElevator = false;

		level = 0;
	}
	
	// Update is called once per frame
	void Update () {

		levelText.text = "Level: " + level.ToString();

		if (timer <= 0f) {
			level++;
			timer = timerLength;
		} else {
			timer -= 1f * Time.deltaTime;
		}
				
		foreach (EnemySpawn spawn in enemySpawns) {
			if (spawn.m_transform.childCount == 0)
				spawn.Decide ();
		}

		foreach (ItemSpawn spawn in itemSpawns) {
			if (spawn.m_transform.childCount == 0)
				spawn.Decide ();
		}

		if (elevatorCountDown <= 0) {

			if (hasUsedElevator && counting) {
				hasUsedElevator = false;
				counting = false;
			} else if(hasUsedElevator && !counting) {
				elevatorCountDown = countDownLength;
				counting = true;
			}
		}

		if (counting) {
			elevatorCountDown -= 1 * Time.deltaTime;
		}
	}

	public void AddScore(int val) {
		score += val;
		scoreText.text = "Score: " + score.ToString ();
		GetComponent<AudioSource> ().Play ();
	}

	public void GoToFloor(int target) {

		if (hasUsedElevator)
			return;

		hasUsedElevator = true;

		bool targetGoesUp = false;

		if (target > floor)
			targetGoesUp = false;
			// Find elevator
		else if (target < floor)
			targetGoesUp = true;
		else
			Debug.LogError ("Cant travel to the same floor");
		floor = target;

		foreach (Elevator elevator in elevators) {
			if (elevator.goesUp == targetGoesUp && (elevator.floor == target)) {
//				 Found correct elevator
				m_playerTransform.position = elevator.m_transform.position;
			}
		}

		Debug.Log ("Traveled to floor: " + target.ToString ());
	}

	public void NextLevel() {
		Debug.Log ("Next level");
	}

	public void ElevatorStatus(bool status) {
		hasUsedElevator = status;
	}

	public void Fired() {
		m_camera.gameObject.GetComponent<AudioSource> ().Pause ();
		endGameObject.SetActive (true);
		endGameObject.GetComponent<Text> ().text = "You're fired!";
		endGameObject.GetComponent<Text> ().color = Color.red;
//		Time.timeScale = 0;
		youreFired.SetActive(true);
		m_playerController.controllable = false;
		Debug.Log ("end game");
	}



}
