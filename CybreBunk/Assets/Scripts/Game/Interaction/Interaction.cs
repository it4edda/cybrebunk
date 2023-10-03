using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField]           float    radius;
    [SerializeField] protected bool     canInteract;
    [SerializeField]           Animator interactIcon;
    Transform                           target;
    protected         bool              inRange;
    protected virtual void              Start() { target = FindObjectOfType<PlayerMovement>().transform; }
    void Update()
    {
        InteractionPassive();

        inRange = Vector2.Distance(transform.position, target.position) < radius;
        interactIcon.SetBool("Showing", inRange);

        if (!canInteract || !inRange) return;

        if (Input.GetKeyDown(KeyCode.Space)) InteractionActive();
    }

    //MAKE ADDITIONAL INTERACTION COROUTINES?
    //item logic would profit from this
    protected virtual void InteractionPassive() { }

    /// <summary>
    ///     Using "Base InteractionActive" will remove the ability to interact with the object again.
    ///     AKA; "canInteract" will turn false.
    /// </summary>
    protected virtual void InteractionActive() { canInteract = false; }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
