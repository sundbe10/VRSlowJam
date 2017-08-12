using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineDrawer : MonoBehaviour {

	public GameObject startObject;
	public GameObject endObject;
	public Color32 lineColor;
	public float lineWidth = 0.1f;

	LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.startColor = lineColor;
		lineRenderer.endColor = lineColor;
		lineRenderer.startWidth = lineWidth;
		lineRenderer.endWidth = lineWidth;
	}
	
	// Update is called once per frame
	void Update () {
		lineRenderer.SetPosition(0, startObject.transform.position);
		lineRenderer.SetPosition(1, endObject.transform.position);
	}
}
