using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MusicNodeData", menuName = "ScriptableObjects/MusicNodeData", order = 1)]
public class NodeScriptableObject : ScriptableObject
{
    public List<MusicNode> nodeList = new List<MusicNode>();
}

[System.Serializable]
public class MusicNode
{
    public string Name;
    public int NodeId;
    public int Type;
    public float TimeSpan;
    public GameObject NodePrefab;
    public GameObject NodeBPrefab;
}