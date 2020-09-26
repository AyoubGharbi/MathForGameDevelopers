using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DetectionBehavior : MonoBehaviour
{
	// expose detection radius on the inspector
	// so we can modify it in realtime
	[SerializeField]
	private float _detectionRadius = 1f;
	
	// the player transform so we can
	// get the position later on
	[SerializeField]
	private Transform _player;
	
	[SerializeField]
	private GUIStyle _labelStyle;
	
	// this is a built-in Unity function which
	// is used for debugging, we are implementing
	// everything in this function for the sake of simplicity.
	void OnDrawGizmos()
	{
		// cache positions
		var playerPos = _player.position;
		var zombiePos = transform.position;
		
		// distance between the player and the zombie
		var rangeDist = Vector3.Distance(playerPos, zombiePos);
		
		// the player is in range only when the distance
		// is less or equal to the detection radius
		var isInRange = rangeDist <= _detectionRadius;
		
		// draw a line between two vectors
		Gizmos.DrawLine(zombiePos, playerPos);
		
		DrawDistanceInformation(playerPos, zombiePos, rangeDist);
		
		// draw a wire sphere to visualize
		// the detection range and change color
		// according to the player's status
		Gizmos.color = isInRange ? Color.green : Color.red;
		Gizmos.DrawWireSphere(zombiePos, _detectionRadius);
	}
	
	#if UNITY_EDITOR
	// display a label containing the distance between
	// the player and the zombie
	void DrawDistanceInformation(Vector3 playerPos, Vector3 rootPos, float rangeDist)
	{
		var labelPos = rootPos + (playerPos - rootPos) * .5f;
		Handles.Label(labelPos, $"{rangeDist}", _labelStyle);
	}
	#endif
}
