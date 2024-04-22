using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField]           float    radius;
    [SerializeField] protected bool     canInteract;
    [SerializeField]           Animator interactIcon;
    protected UserInterfacePopUp        popUp;
    Transform                           target;
    protected         bool              inRange;
    protected virtual void Start()
    {
        popUp  = FindObjectOfType<UserInterfacePopUp>();
        target = FindObjectOfType<PlayerMovement>().transform;
    }
    void Update()
    {
        InteractionPassive();

        inRange = Vector2.Distance(transform.position, target.position) < radius;
        interactIcon.SetBool("Showing", inRange);

        if (!inRange) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
        {
            if (canInteract) InteractionActive();
            else DeniedActive();
        }
    }
    protected virtual void InteractionPassive() { }

    /// <summary>
    ///     Using "Base InteractionActive" will remove the ability to interact with the object again.
    ///     AKA; "canInteract" will turn false.
    /// </summary>
    protected virtual void InteractionActive() { canInteract = false; }
    protected virtual void DeniedActive()
    {
        return;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
