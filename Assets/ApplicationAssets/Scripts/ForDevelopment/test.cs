using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnDrawGizmosSelected()
	{
		var r = GetComponent<Renderer>();
		if (r == null)
			return;
		var bounds = r.bounds;
		Gizmos.matrix = Matrix4x4.identity;
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(bounds.center, bounds.extents * 2);
	}
}
