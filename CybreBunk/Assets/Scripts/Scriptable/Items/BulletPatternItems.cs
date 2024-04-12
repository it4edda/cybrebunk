using UnityEngine;

public class BulletPatternItems : ItemData
{
    public CustomBulletPattern bulletPattern;
    
    public override void OnPickup(PlayerStats playerStats)
    {
        //TODO change bullet pattern
        Debug.Log("Change bullet pattern...");
    }
}
