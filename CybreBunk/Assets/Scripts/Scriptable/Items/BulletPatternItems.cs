using UnityEngine;

[CreateAssetMenu(menuName = "Items/PatternItem", fileName = "NewPatternItem")]
public class BulletPatternItems : ItemData
{
    public CustomBulletPattern bulletPattern;
    
    public override void OnPickup(PlayerStats playerStats)
    {
        //TODO change bullet pattern
        FindObjectOfType<PlayerAttack>().ChangePattern(bulletPattern);
    }
}
