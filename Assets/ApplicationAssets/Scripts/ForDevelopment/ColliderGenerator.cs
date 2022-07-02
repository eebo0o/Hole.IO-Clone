using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ColliderGenerator : MonoBehaviour
{

	[ContextMenu("CreateColliders")]
	public void CreateColliderForASet()
	{
		Transform chiledBulding;
		for (int i = 0; i < transform.childCount; i++)
		{
			chiledBulding = transform.GetChild(i);
			if (chiledBulding.childCount > 1)
			{
				CreateColliderForOneObject(chiledBulding.gameObject);
			}
			else
			{
				chiledBulding.gameObject.AddComponent<BoxCollider>();
			}

		}
	}

	private void CreateColliderForOneObject(GameObject GO)
	{
		if(GO.name.Contains("(Manual)"))
			return;
		BoxCollider collider = GO.AddComponent<BoxCollider>();
		Bounds bounds = new Bounds(GO.transform.position, Vector3.one);
		Renderer[] renderers = GO.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < renderers.Length; i++)
		{
			bounds.Encapsulate(renderers[i].bounds);
		}
		collider.size = bounds.size;
		Vector3 newCenter = collider.center;
		newCenter.y = collider.bounds.extents.y; 
		collider.center = newCenter;
	}

	[ContextMenu("DeleteColliders")]
	private void DeleteAllColliders()
	{
		Collider[] colliders = GetComponentsInChildren<Collider>();
		for (int i = 0; i < colliders.Length; i++)
		{
			if(colliders[i].name.Contains("(Manual)"))
				continue;
			DestroyImmediate(colliders[i]);
		}
	}
	
	[ContextMenu("AddMainComponents")]
	private void AddMainComponents()
	{
		Collider[] colliders = GetComponentsInChildren<Collider>();
		for (int i = 0; i < colliders.Length; i++)
		{
			colliders[i].gameObject.AddComponent<Rigidbody>();
			colliders[i].gameObject.AddComponent<Collectable>().weight=4;
		}
	}
}
