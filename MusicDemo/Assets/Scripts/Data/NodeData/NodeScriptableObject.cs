using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MusicNodeData", menuName = "ScriptableObjects/MusicNodeData", order = 1)]
public class NodeScriptableObject : ScriptableObject
{
    public List<MusicNode> NodeList = new List<MusicNode>();
    public List<float> GroupStartTime;
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
    // 在哪一段，必须要匹配上面的GroupStartTime
    // 假设group=0，也就是groupstarttime[0] = 这个片段的开始时间
    // groupstarttime[0] = 10s,startTime=0.5,也就说这个块会在10.5秒的时候开始掉落
    public int Group;
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