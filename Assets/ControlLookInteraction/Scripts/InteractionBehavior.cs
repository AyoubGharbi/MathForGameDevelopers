using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBehavior : MonoBehaviour
{
	[SerializeField]
	private float _lookThreshold;
	
    [SerializeField]
    private Transform _pDetectionTrans;
    
    private Rigidbody _rgbdy;
    
    // cache rigidbody of this object
    private void Awake()
    {
		TryGetComponent(out _rgbdy);
	}
    
    void OnDrawGizmos()
    {
		// only execute this Gizmos in Play mode
		if(!Application.isPlaying)
			return;
			
		// cache bunch of reuseable variables	
		var playerPos = _pDetectionTrans.position;
		var objPos = transform.position;
		var direction = (objPos - playerPos).normalized;
	
		// calculate the dot product value
		var dotProdVal = Vector3.Dot(direction, _pDetectionTrans.forward);
		
		// compare the value calculated with a given threshold
		// to determine whether the player is looking towards the object
		var isLooking = dotProdVal >= _lookThreshold;
		
		// draw line between the player and the object
		// with a colored feedback
		Gizmos.color = isLooking ? Color.green : Color.red;
		Gizmos.DrawLine(playerPos, objPos);
		
		// allow the object to fall
		// if the player is looking at it
		_rgbdy.isKinematic = !isLooking;
	}
}
