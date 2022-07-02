using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPropsSeter : MonoBehaviour {

    [ContextMenu("CreateColliders")]
    public void CreateColliderForASet()
    {
        Transform chiledProb;
        for (int i = 0; i < transform.childCount; i++)
        {
            chiledProb = transform.GetChild(i);
            chiledProb.gameObject.AddComponent<MeshCollider>().sharedMesh =chiledProb.gameObject.GetComponent<MeshFilter>().sharedMesh;
        }
    }
    [ContextMenu("AddMainComponents")]
    private void AddMainComponents()
    {
        MeshCollider[] colliders = GetComponentsInChildren<MeshCollider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].convex = true;
            colliders[i].gameObject.AddComponent<Rigidbody>();
            colliders[i].gameObject.AddComponent<Collectable>().weight=1;
        }
    }
}
