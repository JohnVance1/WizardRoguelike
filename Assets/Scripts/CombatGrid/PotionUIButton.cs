using UnityEngine;

public class PotionUIButton : MonoBehaviour
{

    // public Potion heldPotion
    public bool[,] effectRadius;

    void Start()
    {
        effectRadius = new bool[3, 3]{
            {false, true, false},
            {true, true, true},
            {false, true, false} 
        };
    }

    public void SetPotion()
    {
        PlayerCombatGrid.Instance.currentAttackPotionArray = effectRadius;
        PlayerCombatGrid.Instance.state = PlayerState.UsePotion;

    }


}
