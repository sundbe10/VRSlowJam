using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildIndicatorManager : MonoBehaviour
{

    public float maxWidth = 2;
    public float height = 0.2f;
    public float depth = 1;

    private Camera camera;
    private HealthManager sheildHealth;
    private BlockBehavior blockBehavior;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        blockBehavior = transform.parent.GetComponent<BlockBehavior>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        camera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (blockBehavior.state == BlockBehavior.State.Blocking)
        {
            sheildHealth = blockBehavior.shieldHealth;
            spriteRenderer.enabled = true;
            //makes sprite always face camera
            transform.LookAt(camera.transform.position, -Vector3.up);
            //connects health bar width to current shieldStrangth left
            setWidth();
        }
        else{
            spriteRenderer.enabled = false;
        }
    }

    public void setWidth()
    {
        
        float healthLeftWidth = ((float)sheildHealth.currentHealth /
            (float)sheildHealth.maxHealth) * maxWidth;
        transform.localScale = new Vector3(healthLeftWidth, height, depth);
        return;
    }
}