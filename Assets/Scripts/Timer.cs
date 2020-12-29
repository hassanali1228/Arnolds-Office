using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public Rigidbody2D m_rigidBody2D;

	// Use this for initialization
	void Start () {
		Object.Destroy (gameObject, 5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other) {
		m_rigidBody2D.mass = 1000;
		m_rigidBody2D.velocity = new Vector2 (0, -9f);
		GetComponent<BoxCollider2D> ().enabled = false;
		LateCall ();
	}

	IEnumerator LateCall() {
		yield return new WaitForSeconds (3f);

		gameObject.SetActive (false);
	}
}
