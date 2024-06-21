using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private float lifeTime = 2f;
    private float lifeTimeRemaining;
    
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lifeTimeRemaining = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimeRemaining -= Time.deltaTime;
        
        Color color = spriteRenderer.color;
        color.a = Mathf.Clamp01(lifeTimeRemaining / lifeTime);
        spriteRenderer.color = color;
        
        if (lifeTimeRemaining <= 0)
        {
            Destroy(gameObject);
        }
    }
}
