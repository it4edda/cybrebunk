using UnityEngine;

public class ItemLogic : Interaction
{
    [Header("Item Exclusive"), SerializeField]
     ItemData itemData;
    [SerializeField] ParticleSystem particle;
    PlayerStats                     stats;

    protected override void InteractionPassive()
    {
        //THIS SHIT DONT WORK
        if (inRange && particle.isPlaying == false) particle.Play();
        else particle.Stop();
    }
    protected override void InteractionActive()
    {
        SetEffect(gameObject);
        base.InteractionActive();
        Destroy(gameObject);
    }
    void SetEffect(GameObject a) // move this to ItemLogic instead, nils
    {
        //stats.movement += movementSpeedIncrease;
        stats        =  FindObjectOfType<PlayerStats>();
        PlayerInventory.instance.AddItem(itemData);
        //attack speed

        //subscribe to unity event
        Debug.Log("Did Card Effect");
    }
    void OnDisable() { DamageDealer.OnHitEvent -= SetEffect; }
    //needs a popup on screen
    //ex: "YOU FOUND PILLS"
}
