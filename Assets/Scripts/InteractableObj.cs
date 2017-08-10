using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrabTypeDropList{
	Parent,FixedJoint
}

public class InteractableObj : MonoBehaviour {

	public GrabTypeDropList grabType = GrabTypeDropList.Parent;
	public bool isGrabbable = true;
	public bool snapPosToAttach = true;
	public bool snapRotToAttach = true;
	public bool isThrowable = true;
	public bool returnOnDrop = true;


}
