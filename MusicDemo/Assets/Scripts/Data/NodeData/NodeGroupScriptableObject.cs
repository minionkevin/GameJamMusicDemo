using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicGroup", menuName = "ScriptableObjects/MusicGroupData", order = 1)]
public class MusicGroupScriptableObject : ScriptableObject
{
    public List<int> groupAOrder = new List<int>();
    public List<int> groupBOrder = new List<int>();
    public List<float> GroupStartTime = new List<float>();
}
