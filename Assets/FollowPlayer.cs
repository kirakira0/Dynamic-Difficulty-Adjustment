using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	private float yThreshold;
	public Transform player;
  	public Vector3 offset;

	void Awake() {
		yThreshold = GameObject.Find("LowerCameraThreshold").transform.position.y;
	}

	void FixedUpdate () {
		// Camera follows the player with specified offset position
		if (player.position.y >= yThreshold) {
			transform.position = new Vector3 (player.position.x + offset.x, player.position.y + offset.y, offset.z); 
		} else {
			transform.position = new Vector3 (player.position.x + offset.x, transform.position.y + offset.y, offset.z); 
		}
	}
}