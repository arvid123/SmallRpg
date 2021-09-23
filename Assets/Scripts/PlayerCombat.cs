using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private CharacterMovement movement;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float attackRange = 1f;
    [SerializeField]
    private float attackDamage = 10f;
    [SerializeField]
    private LayerMask enemyLayers;

    private Vector2 mousePosition;
    private bool hitFramesActive;

    // Editor gizmos
    private void OnDrawGizmosSelected() {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    // Started swinging sword
    public void StartHitFrames() {
        hitFramesActive = true;
    }

    // Stopped swinging sword
    public void StopHitFrames() {
        hitFramesActive = false;
    }

    // Called upon pressing the attack button
    public void OnAttack(InputAction.CallbackContext context) {
        if (context.ReadValueAsButton()) {
            movement.StopForAnimation("Attack");
            animator.SetTrigger("Attack1");
            movement.AlignTo(Util.screenToGroundPoint(mousePosition, mainCamera));
        }
    }

    // Mouse was moved somewhere
    public void OnPoint(InputAction.CallbackContext context) {
        mousePosition = context.ReadValue<Vector2>();
    }

    void Update()
    {
        if (hitFramesActive) {
            // Detect which enemies were hit
            Collider[] enemiesHit = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
            // Damage enemies
            foreach (Collider enemy in enemiesHit) {
                if (enemy.CompareTag("Enemy")) {
                    enemy.GetComponent<CharacterStatus>().TakeDamage(attackDamage);
                }
            }
        }
    }
}
