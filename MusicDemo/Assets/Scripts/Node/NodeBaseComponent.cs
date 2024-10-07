using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class NodeBaseComponent : MonoBehaviour
{
    public TextMeshProUGUI NameLabel;
    protected string nodeName;
    protected int nodeId;
    protected int type;
    public float TimeSpan;
    protected NodeReceiverComponent receiver;


    public void Setup(string name, int id, int type, float timeSpan)
    {
        this.nodeName = name;
        this.nodeId = id;
        this.type = type;
        this.TimeSpan = timeSpan;
        NameLabel.text = name;
    }

    // setup sound here

    public void SetupReceiver(NodeReceiverComponent receiverComponent)
    {
        receiver = receiverComponent;
    }

}

// todo check this?
[System.Serializable]
public class NodeType
{
    public const int Normal = 1;
    public const int Hide = 2;
}
