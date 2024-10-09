using TMPro;
using UnityEngine;

public class NodeBaseComponent : MonoBehaviour
{
    public TextMeshProUGUI NameLabel;
    public string NodeName;
    protected int nodeId;
    public int type;
    public float StartTime;
    protected NodeReceiverComponent receiver;
    public bool IsCheck = false;

    public void Setup(string name, int id, int type, float startTime)
    {
        this.NodeName = name;
        this.nodeId = id;
        this.type = type;
        this.StartTime = startTime;
        if(NameLabel!=null) NameLabel.text = name;
    }

    // setup sound here

    public void SetupReceiver(NodeReceiverComponent receiverComponent)
    {
        receiver = receiverComponent;
    }

}

