using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public GameManager m_gameManager;
	public ItemSpawn m_spawn;

	public int pointsGained;

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log (other.gameObject.name);
		if (other.gameObject.tag == "player") {
			Debug.Log ("Got Item");
			m_gameManager.AddScore (pointsGained);
			m_spawn.Reset ();
			Object.Destroy (gameObject);
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
