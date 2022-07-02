using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour {

    [SerializeField]
    float speed;

    private float horizontalInput;
    private float verticalInput;
	
	// Update is called once per frame
	void Update () {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
	}


    void FixedUpdate()
    {

        transform.Translate(transform.forward * verticalInput * speed * Time.fixedDeltaTime);
        transform.Translate(transform.right * horizontalInput * speed * Time.fixedDeltaTime);
    }

}
