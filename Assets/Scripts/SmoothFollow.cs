using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {

	public float dampTime;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	public Camera m_camera;


	// Update is called once per frame
	void Update () {
			
		if (target) {

			Vector3 point = m_camera.WorldToViewportPoint (target.position);
			Vector3 delta = target.position - m_camera.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, point.z));

			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, new Vector3(destination.x, destination.y + 2f, destination.z), ref velocity, dampTime);
		}
	}
}
