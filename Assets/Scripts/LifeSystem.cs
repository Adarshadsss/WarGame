using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class LifeSystem: MonoBehaviour
{
    private float initialLife = 1f; 
    private float lifeDecreaseRate = .1f; 
    public Slider slider;
    private float currentLife;
    private string mytag;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private CapsuleCollider collider;

   

  
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        mytag = this.gameObject.tag;
        currentLife = initialLife;
    }

    void Update()
    {
        if (currentLife <= 0)
        {
            animator.SetFloat("Fall", 1);
            DestroyImmediate(collider);

            navMeshAgent.velocity = Vector3.zero;
            StartCoroutine(DeadAgent());
        }
    }

    IEnumerator DeadAgent()
    {
        yield return new WaitForSeconds(2);
        
        this.gameObject.tag = "Untagged";

        DestroyImmediate(gameObject); // Destroy the object when its life reaches 0.


    }


    void OnCollisionStay(Collision collision)
    {
        if (mytag != collision.gameObject.tag)
        {
            LifeSystem otherLifeSystem = collision.gameObject.GetComponent<LifeSystem>();

            if (otherLifeSystem != null)
            {
                // Decrease the life of both objects during the collision.
                float decreaseAmount = lifeDecreaseRate * Time.deltaTime;
                currentLife -= decreaseAmount;
                slider.value = currentLife;
                otherLifeSystem.currentLife -= decreaseAmount;
            }

          
        }
       
    }
   
}
