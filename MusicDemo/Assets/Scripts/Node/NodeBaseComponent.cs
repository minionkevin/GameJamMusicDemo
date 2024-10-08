using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class NodeBaseComponent : MonoBehaviour
{
    public TextMeshProUGUI NameLabel;
    public string NodeName;
    protected int nodeId;
    protected int type;
    public float TimeSpan;
    protected NodeReceiverComponent receiver;
    public bool IsCheck = false;


    public void Setup(string name, int id, int type, float timeSpan)
    {
        this.NodeName = name;
        this.nodeId = id;
        this.type = type;
        this.TimeSpan = timeSpan;
        if(NameLabel!=null) NameLabel.text = name;
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
