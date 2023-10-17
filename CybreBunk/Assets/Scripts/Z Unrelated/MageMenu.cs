using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageMenu : MonoBehaviour
{
    BodyState[] state;
    void PeelBody(int partIndex) =>
        state[partIndex] = BodyState.Skinned;
    void TranscribeBody(int partIndex) =>
        state[partIndex] = BodyState.Transcribed;
    void HealBody(int partIndex) => 
        state[partIndex] = BodyState.Healthy;
    
    
}

[Serializable]
public enum BodyState
{
    Healthy, Skinned, Transcribed
}
