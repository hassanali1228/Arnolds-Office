using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	public GameManager m_gameManager;
	public LayerMask playerLayer;

	public Transform m_transform;

	private float size = 1f;
	private float range = 3f;

	public int floor;
	public int targetFloor;

	public bool goesUp;

	// Use this for initialization
	void Start () {
		if (floor == targetFloor)
			Debug.LogError ("Cannot take elevator to the same floor ...?");
	}
	
	// Update is called once per frame
	void Update () {
		bool isActivated = Physics2D.OverlapCircle(m_transform.position, size, playerLayer);


		if (isActivated) {
			Travel ();
		}

		bool isInRange = Physics2D.OverlapCircle(m_transform.position, range, playerLayer);

		if (!isInRange && (m_gameManager.floor == floor)) {
			
		}

	}

	void Travel() {
		m_gameManager.GoToFloor (targetFloor);
	}

}
