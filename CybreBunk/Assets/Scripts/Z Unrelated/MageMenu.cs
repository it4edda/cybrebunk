using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageMenu : MonoBehaviour
{ 
    static BodyState[] state;
    public void PeelBody(int partIndex) =>
        state[partIndex] = BodyState.Skinned;
    public void TranscribeBody(int partIndex) =>
        state[partIndex] = BodyState.Transcribed;
    public void HealBody(int partIndex) => 
        state[partIndex] = BodyState.Healthy;
}
[Serializable] public enum BodyState { Healthy, Skinned, Transcribed }
