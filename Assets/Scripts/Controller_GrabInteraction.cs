using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class Controller_GrabInteraction : MonoBehaviour {

	public Rigidbody attachPoint;

	SteamVR_TrackedObject trackedObj;
	FixedJoint joint;

	List<GameObject> interactableItems = new List<GameObject>();

	private bool isInteracting = false;
	private GameObject interObj;
	private GameObject cldObj;

	private Vector3 oldPosition;
	private Quaternion oldRotation;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void FixedUpdate()
	{
		var device = SteamVR_Controller.Input((int)trackedObj.index);

		if (joint == null && interactableItems.Count != 0 && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			interObj = interactableItems [0];

			//set orig position
			oldPosition = interObj.transform.position;
			oldRotation = interObj.transform.rotation;

			//snaps to controller attachPoint
			interObj.transform.position = attachPoint.transform.position;

			joint = interObj.AddComponent<FixedJoint>();
			joint.connectedBody = attachPoint;
		}
		else if (joint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
		{
			var rgbd = interObj.GetComponent<Rigidbody>();
			Object.DestroyImmediate(joint);
			joint = null;

			//snap back to orig position
			interObj.transform.position = oldPosition;
			interObj.transform.rotation = oldRotation;


			//stuff for throwing and momentum
			/*var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
			if (origin != null)
			{
				rgbd.velocity = origin.TransformVector(device.velocity);
				rgbd.angularVelocity = origin.TransformVector(device.angularVelocity);
			}
			else
			{
				rgbd.velocity = device.velocity;
				rgbd.angularVelocity = device.angularVelocity;
			}*/

			//rgbd.maxAngularVelocity = rgbd.angularVelocity.magnitude;
		}

	}


	void OnTriggerEnter(Collider collider){
		//Debug.Log ("I hit something");
		InteractableObj intObj = collider.GetComponentInParent<InteractableObj>();
		if (intObj != null && intObj.isGrabbable){
			//Debug.Log ("I'm interacting");
			interactableItems.Add (intObj.gameObject);
		}

	}

	void OnTriggerExit(Collider collider){
		//Debug.Log ("I hit something");
		InteractableObj intObj = collider.GetComponentInParent<InteractableObj>();
		if (interactableItems.Contains (intObj.gameObject)){
			//Debug.Log ("I'm no longer interacting");
			interactableItems.Remove (intObj.gameObject);
		}

	}
}
