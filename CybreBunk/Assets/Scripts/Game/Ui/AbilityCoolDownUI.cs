using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCoolDownUI : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Image image;

    Ability ability;

    void Start()
    {
        ability = FindObjectOfType<Ability>();
    }

    void Update()
    {
        SetCooldown();
    }

    public void GetNewAbility(ItemData itemData)
    {
        switch (ability.ability1)
        {
            case Ability.ChosenAbility.None:
                break;
            case Ability.ChosenAbility.DarkArts:
                slider.maxValue = ability.darkArtsVariables.baseVariables.abilityCooldown;
                image.sprite = itemData.itemIcon;
                break;
            case Ability.ChosenAbility.AoeAttack:
                slider.maxValue = ability.aoeAttackVariables.baseVariables.abilityCooldown;
                image.sprite = itemData.itemIcon;
                break;
            case Ability.ChosenAbility.Blast:
                slider.maxValue = ability.blastVariables.baseVariables.abilityCooldown;
                image.sprite    = itemData.itemIcon;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    

    void SetCooldown()
    {
        slider.value = ability.Cooldown1;
    }
}
