using UnityEngine;

public class ItemLogic : Interaction
{
    [Header("Item Exclusive")] 
    [SerializeField] ParticleSystem particle;

    protected override void InteractionPassive()
    {
        //THIS SHIT DONT WORK
        if (inRange && particle.isPlaying == false) particle.Play();
        else particle.Stop();
    }
    protected override void InteractionActive()
    {
        ItemManager.instance.SetItemSelect();
        base.InteractionActive();
        Destroy(gameObject);
    }
    //needs a popup on screen
    //ex: "YOU FOUND PILLS"
}
