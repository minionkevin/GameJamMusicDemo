using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MusicNodeData", menuName = "ScriptableObjects/MusicNodeData", order = 1)]
public class NodeScriptableObject : ScriptableObject
{
    public List<MusicNodeList> NodeLists = new List<MusicNodeList>();
}

[System.Serializable]
public class MusicNodeList
{
    public List<MusicNode> NodeList = new List<MusicNode>();
    public int GroupStartTime;
    public bool isPlayerB;
}

[System.Serializable]
public class MusicNode
{
    // 需要和receiverName保持完全一致
    public string Name;
    // 暂时没用
    public int NodeId;
    // 用来表示node类型
    public int Type;
    // 在某一段的什么时候发出
    public float StartTime;
}

[System.Serializable]
public class NodeType
{
    public const int NORMAL = 0;
    public const int HIDE = 1;
    public const int LINE = 2;
    public const int LISTEN = 3;
    public const int START = 4;
    public const int STARTLINE = 5;
}