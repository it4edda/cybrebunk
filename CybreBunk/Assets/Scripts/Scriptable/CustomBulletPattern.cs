using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomBulletPattern", fileName = "NewBulletPattern")]
public class CustomBulletPattern : ScriptableObject
{
    public List<PatternRows> pattern;
    
    #region Structs
    [Serializable]
    public struct PatternRows
    {
        public List<BulletsInRows> bulletsToFire;
        public float secondsUntilNextRowFire;
    }

    [Serializable]
    public struct BulletsInRows
    {
        public GameObject bullet;
        [Range(-180, 180)]public float angle;
    }
    #endregion
}
