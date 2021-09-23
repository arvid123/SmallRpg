using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float health = 100;
    [SerializeField]
    private float hitInvulTime = 0.5f;

    private bool dead;
    private float invulUntil = 0f;

    public void TakeDamage(float damage) {
        if (Time.time >= invulUntil) {
            invulUntil = Time.time + hitInvulTime;
            health -= damage;
            if (health <= 0) {
                Die();
            }
            else {
                animator.SetTrigger("GetHit");
            }
        }
    }

    private void Die() {
        dead = true;
        animator.SetTrigger("Die");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
