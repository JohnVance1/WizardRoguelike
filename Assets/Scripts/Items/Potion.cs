using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Flags]
public enum PotionEffect
{
    Light = 1,
    Shadow = 2,
    Fire = 4,
    Ice = 8,
    Earth = 16,
    Air = 32,

}



//[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Potion", order = 1)]
public class Potion
{
    
    public PotionInfo_SO info;
    
    public Potion(PotionInfo_SO potionInfo)
    {
        info = potionInfo;
        
    }


}
