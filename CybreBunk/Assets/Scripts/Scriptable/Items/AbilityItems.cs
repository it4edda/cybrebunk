using UnityEngine;

[CreateAssetMenu(menuName = "Items/AbilityItem")]
public class AbilityItems : ItemData
{
    public Ability.ChosenAbility itemAbility;
    
    public override void OnPickup(PlayerStats playerStats)
    {
        FindObjectOfType<Ability>().ability1 = itemAbility;
    }
}
