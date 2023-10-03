using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurdleAnimation : MonoBehaviour
{
    public Animator hurdleAnimator;
    PolygonCollider2D PolygonCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        PolygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            HurdleFallAnimation();
        }
    }
public void HurdleFallAnimation()
    {
        print("player hit hurdle");


        hurdleAnimator.SetTrigger("Hit");
        PolygonCollider2D.enabled = false;
    }
    
}
