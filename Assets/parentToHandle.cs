using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parentToHandle : MonoBehaviour {

    public GameObject parentHandle;
    public float maxHeight;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        float yVal = transform.position.y;
        yVal = Mathf.Clamp(parentHandle.transform.position.y, 0, maxHeight);
        transform.position = new Vector3(parentHandle.transform.position.x,yVal,parentHandle.transform.position.z);
        transform.rotation = parentHandle.transform.rotation;
    }
}
