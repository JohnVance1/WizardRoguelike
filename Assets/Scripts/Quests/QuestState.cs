using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum for the different states a quest can be in
/// </summary>
public enum QuestState
{
    REQUIREMENTS_NOT_MET,
    CAN_START,
    IN_PROGRESS,
    CAN_FINISH, 
    FINISHED
}
