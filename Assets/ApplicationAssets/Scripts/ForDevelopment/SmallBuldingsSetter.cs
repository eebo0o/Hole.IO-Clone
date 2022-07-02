using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBuldingsSetter : MonoBehaviour {

	
	
	
	[ContextMenu("CreateColliders")]
	public void CreateColliderForASet()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			CreateColliderForOneObject(transform.GetChild(i).gameObject);
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
		//Vector3 newCenter = collider.center;
		//newCenter.y = bounds.extents.y; 
		//collider.center = newCenter;
	}
	
	
	
	
	
	[ContextMenu("SetBuldingsNames")]
	private void SetBuldingsNames()
	{
		for (int i = 0; i < transform.childCount ; i++)
		{
			transform.GetChild(i).gameObject.name="SB"+(i+1);
		}
	}
	
	[ContextMenu("CreateNewCenter")]
	private void CallibrateBuldingsCenters()
	{
		for (int i = 0; i < transform.childCount ; i++)
		{
			CallibrateBuldingCenters(transform.GetChild(i).gameObject);
		}
	}


	private void CallibrateBuldingCenters(GameObject buildingGO)
	{
		Vector3 TotalPositions = Vector3.zero;
		
		List<Transform> chileds = new List<Transform>();
		for (int i = 0; i < buildingGO.transform.childCount ; i++)
		{
			chileds.Add(buildingGO.transform.GetChild(i));
			chileds[i].parent = null;
			TotalPositions += chileds[i].position;
		}

		TotalPositions /= chileds.Count;
		GameObject newObject = new GameObject(buildingGO.name);
		newObject.transform.parent = buildingGO.transform.parent;
		newObject.transform.rotation = buildingGO.transform.rotation;
		newObject.transform.position = TotalPositions;
		for (int i = 0; i < chileds.Count; i++)
		{
			chileds[i].parent = newObject.transform;
		}
		DestroyImmediate(buildingGO);
	}
}
