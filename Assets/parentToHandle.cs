using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parentToHandle : MonoBehaviour
{

    public GameObject parentHandle;
    public float maxHeight;
    public float rotationSpeed;
    public float transformSpeed;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float yVal = transform.position.y;
        yVal = Mathf.Clamp(parentHandle.transform.position.y, 0, maxHeight);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(parentHandle.transform.position.x, yVal, parentHandle.transform.position.z), transformSpeed);
        Vector3 currentRotEuler = transform.rotation.eulerAngles;
        Vector3 targetRotEuler = parentHandle.transform.rotation.eulerAngles;
        // transform.rotation = Quaternion.Euler(new Vector3(Mathf.MoveTowards(currentRotEuler.x, targetRotEuler.x, rotationSpeed.x), Mathf.MoveTowards(currentRotEuler.y, targetRotEuler.y, rotationSpeed.y), Mathf.MoveTowards(currentRotEuler.z, targetRotEuler.z, rotationSpeed.z)));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, parentHandle.transform.rotation, rotationSpeed);
    }
}