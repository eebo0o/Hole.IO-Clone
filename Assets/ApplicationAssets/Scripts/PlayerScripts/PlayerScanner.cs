using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScanner : MonoBehaviour {

    [SerializeField]
    float radius;
    [SerializeField]
    Transform sphereCasterCenterTransform;

    List<GameObject> victims;
	void Start () {
        victims = new List<GameObject>();
        radius = CalculateRaduis();
    }

    void FixedUpdate()
    {
        radius = CalculateRaduis();
        var nearbyObjects = Physics.OverlapSphere(sphereCasterCenterTransform.position, radius);
        for (int i = 0; i < victims.Count; i++)
        {
            GameObject nearbyObjectRb;
            bool existed = false;
            foreach (var nearbyObject in nearbyObjects)
            {
                nearbyObjectRb = nearbyObject.gameObject;
                if (victims[i] == nearbyObjectRb)
                {
                    existed = true;
                }
            }
            if (!existed)
            {
                victims[i].gameObject.layer = 9;
                victims.Remove(victims[i]);
                i--;
            }
        }


        foreach (var nearbyObject in nearbyObjects)
        {
            MeshRenderer renderer = nearbyObject.GetComponent<MeshRenderer>();
                float targetRadiusX = 0;
                float targetRadiusZ = 0;

            if (renderer != null)
            {
                float minX = renderer.bounds.min.x;
                float maxX = renderer.bounds.max.x;
                float minZ = renderer.bounds.min.z;
                float maxZ = renderer.bounds.max.z;
                targetRadiusX = (maxX - minX) / 2;
                targetRadiusZ = (maxZ - minZ) / 2;
            }
            if (nearbyObject == gameObject || nearbyObject.gameObject.layer != 9 || (targetRadiusX >= radius && targetRadiusZ >= radius))
            {
                continue;
            }


            GameObject nearbyObjectRb = nearbyObject.gameObject;
            if (!nearbyObjectRb || victims.Contains(nearbyObjectRb))
            {
                continue;
            }
            else
            {
                victims.Add(nearbyObjectRb);
                nearbyObject.gameObject.layer = 10;
                nearbyObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
    private float CalculateRaduis()
    {
        //return (transform.parent.localScale.x + (transform.parent.localScale.x / 2)) / 2;
        return (transform.parent.localScale.x + (transform.parent.localScale.x / 4)) / 2;
    }
    private void OnDrawGizmos()
    {
        Debug.DrawLine(sphereCasterCenterTransform.position - new Vector3(radius, 0, 0), sphereCasterCenterTransform.position + new Vector3(radius, 0, 0), Color.blue);
        Debug.DrawLine(sphereCasterCenterTransform.position - new Vector3(0, 0, radius), sphereCasterCenterTransform.position + new Vector3(0, 0, radius), Color.blue);
    }
}
