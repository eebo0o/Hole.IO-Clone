using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collectable : MonoBehaviour {
    private void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public int weight = 1;
    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.tag == "Disabler")
        {
            trigger.transform.parent.parent.GetComponent<PlayerManager>().AddScore(weight);
            Invoke("DisableObject", 1f);
        }
    }
    void DisableObject()
    {
        this.gameObject.SetActive(false);
    }
}
