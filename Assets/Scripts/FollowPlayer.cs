using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public Transform player;
  	public float xOffset;

	void FixedUpdate () {
		// Camera follows the player with specified offset position
		transform.position = new Vector3 (player.position.x + xOffset, transform.position.y, transform.position.z); 
	}
}