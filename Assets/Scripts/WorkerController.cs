using UnityEngine;
using System.Collections;

public class WorkerController : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
		sensed = false;
		hasSensed = false;
	}

	void FixedUpdate() {
		m_transform.rotation =  Quaternion.Euler (0, 0, 0);
	}

	// Update is called once per frame
	void Update () {

		currentDirection = direction;
		speed = walkSpeed;

		if (flip) {
			direction = -1f;
			Flip ();
			return;
		}

		if(hasSensed)
			sensed = Physics2D.OverlapCircle(m_transform.position, 13f, playerLayer);
		else
			sensed = Physics2D.OverlapCircle(m_transform.position, 8f, playerLayer);

		if (sensed) {
			m_animator.SetFloat ("Speed", 0.7f);

			float x = m_gameManager.m_playerTransform.position.x;

			if (x > m_transform.position.x) {
				direction = -1f;

			}
			else if (x < m_transform.position.x) {
				direction = 1f;

			}

			speed = runSpeed;

			hasSensed = true;



		} else if (!sensed) {
			m_animator.SetFloat ("Speed", 0.3f);


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

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "weapon") {
			Die ();
		} else if (other.gameObject.tag == "wall") {
			
		}
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
