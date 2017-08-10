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
	private bool currentlyHolding = false;
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

		if (!currentlyHolding && interactableItems.Count != 0 && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			interObj = interactableItems [0];

			InteractableObj intObjScript = interObj.GetComponentInParent<InteractableObj> ();

			if (intObjScript.returnOnDrop){
				//set orig position
				oldPosition = interObj.transform.position;
				oldRotation = interObj.transform.rotation;
			}


			//snaps pos to controller attachPoint
			if (intObjScript.snapPosToAttach) {
				interObj.transform.position = attachPoint.transform.position;
			}

			//snaps rot to controller attachPoint
			if (intObjScript.snapRotToAttach) {
				interObj.transform.rotation = attachPoint.transform.rotation;
			}

			if (intObjScript.grabType == GrabTypeDropList.Parent) {
				//parent attach mode
				interObj.transform.parent = attachPoint.transform;
				currentlyHolding = true;
			}

			if (intObjScript.grabType == GrabTypeDropList.FixedJoint) {
				//fixedJoint attach mode
				joint = interObj.AddComponent<FixedJoint> ();
				joint.connectedBody = attachPoint;
				currentlyHolding = true;
			}
		}
		else if (currentlyHolding && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
		{
			InteractableObj intObjScript = interObj.GetComponentInParent<InteractableObj> ();
			var rgbd = interObj.GetComponent<Rigidbody>();
			
			if (intObjScript.grabType == GrabTypeDropList.Parent){
				//parent detach mode
				interObj.transform.parent = null;
				currentlyHolding = false;
			}

			if (intObjScript.grabType == GrabTypeDropList.FixedJoint){
				//fixedJoint detach mode
				Object.DestroyImmediate(joint);
				joint = null;
				currentlyHolding = false;
			}	
			
			if (intObjScript.isThrowable) {
				//stuff for throwing and momentum
				var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
				if (origin != null) {
					rgbd.velocity = origin.TransformVector (device.velocity);
					rgbd.angularVelocity = origin.TransformVector (device.angularVelocity);
				} else {
					rgbd.velocity = device.velocity;
					rgbd.angularVelocity = device.angularVelocity;
				}
				rgbd.maxAngularVelocity = rgbd.angularVelocity.magnitude;
			}

			if (intObjScript.returnOnDrop && !intObjScript.isThrowable){
				//snap back to orig position
				//Debug.Log ("returning "+interObj.name+" to original pos");
				interObj.transform.position = oldPosition;
				interObj.transform.rotation = oldRotation;
			}
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
		if (collider.GetComponentInParent<InteractableObj> () != null) {
			InteractableObj intObj = collider.GetComponentInParent<InteractableObj> ();
			if (interactableItems.Contains (intObj.gameObject)) {
				//Debug.Log ("I'm no longer interacting");
				interactableItems.Remove (intObj.gameObject);
			}
		}

	}
}
