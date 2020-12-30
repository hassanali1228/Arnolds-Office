using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour {

	public int pointsGained;

	public Transform m_transform;
	public LayerMask playerLayer;
	public Animator  m_animator;
	public AudioSource m_audioSource;
	public BoxCollider2D m_collider;
	public Rigidbody2D m_rigidBody2D;
	public GameManager m_gameManager;
	public EnemySpawn m_spawn;

	public AudioClip hurtSound;

	public bool sensed;
	bool hasSensed;

	float direction = 1;
	float countDownLength = 3.0f;
	float countDown = 0.0f;

	public float walkSpeed = 5.0f;
	public float runSpeed = 15.0f;

	float currentDirection;
	bool flip = false;

	float speed = 0;

	float health;
	float maxHealth;
	public Transform healthBar;

	// Use this for initialization
	void Start () {
		sensed = false;
		hasSensed = false;
		speed = 3;
		maxHealth = 1 * m_gameManager.level;	
		health = maxHealth;

		if(health < 1) {
			health = 1;
			maxHealth = 1;
		}

		if (health >= 5) {
			health = 5;
			maxHealth = 5;
		}
	}

	void FixedUpdate() {
		m_transform.rotation =  Quaternion.Euler (0, 0, 0);
	}

	// Update is called once per frame
	void Update () {

		speed = m_gameManager.level * 3;

		if (Physics2D.OverlapCircle (m_transform.position, 0.5f, playerLayer)) {
			m_gameManager.m_playerController.Hurt ();
		}

		currentDirection = direction;
		speed = walkSpeed;

		if (flip) {
			direction = -1f;
			Flip ();
			return;
		}
			if(true) {

			if (countDown <= 0.0f) {
				direction = direction * -1f;
				countDown = countDownLength;
			} else {
				countDown -= 1 * Time.deltaTime;
			}

			hasSensed = false;
		}

		Flip ();

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "wall") {

		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "weapon") {
  			Hurt ();
		} else if (other.gameObject.tag == "wall") {

		}
	}
		

	void Hurt() {
		health -= 1;
		if (health <= 0)
			Die ();
		healthBar.localScale = new Vector3 (health / maxHealth, healthBar.localScale.y, healthBar.localScale.z);
	}
	void Fired() {
		m_gameManager.Fired ();

	}


	void Die() {
		m_audioSource.clip = hurtSound;
		m_audioSource.Play ();
		m_spawn.Reset ();
		m_collider.enabled = false;

		Object.Destroy (gameObject, 3);
		m_gameManager.AddScore (pointsGained);
	}

	void Flip() {
		m_rigidBody2D.velocity = new Vector2(speed * direction, - 9f);
		if (direction != currentDirection)
			m_transform.localScale = new Vector3 (m_transform.localScale.x * -1f, m_transform.localScale.y, m_transform.localScale.z);
	}
}
