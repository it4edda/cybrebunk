/*
using System;
using UnityEngine;

[Obsolete]
[CreateAssetMenu(fileName = "New Item", menuName = "Items/Movement")]
public class MovementItemData : ItemData, IMovementStats
{
    [Header("Movement Exclusive")]
    [SerializeField] float speedIncrease;
    public float SpeedIncrease
    {
        get => speedIncrease;
        set => speedIncrease = value;
    }
    protected override void SetEffect()
    {
        //add movement speed variable;
        base.SetEffect();
    }
}

public interface IMovementStats
{
    public float SpeedIncrease { get; set; }
}
*/
