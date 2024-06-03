using UnityEngine;

[CreateAssetMenu(menuName = "Items/PatternItem", fileName = "NewPatternItem")]
public class OnHitItems : ItemData
{
    //public CustomBulletPattern bulletPattern;
    
    public override void OnPickup(PlayerStats playerStats)
    {
        Debug.Log("SOMETHING AIN'T RIGHT HERE");
        //FindObjectOfType<PlayerAttack>().ChangePattern(bulletPattern);
    }
}
