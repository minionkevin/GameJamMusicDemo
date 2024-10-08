using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NodeManager : BaseSingleton<NodeManager>
{
    public NodeScriptableObject NodeData;
    public TextMeshProUGUI StateLabel;
    private List<NodeReceiverComponent> receiverList = new List<NodeReceiverComponent>();
    
    // change to private later
    public List<GameObject> nodeList = new List<GameObject>();


    public void AddReceiver(NodeReceiverComponent receiver)
    {
        receiverList.Add(receiver);
    }

    public NodeReceiverComponent FindStartPos(string nodeName)
    {
        foreach (var item in receiverList)
        {
            if (item.Name.Equals(nodeName)) return item;
        }
        return null;
    }

    public void AddOnNodeList(GameObject item)
    {
        nodeList.Add(item);
    }

    public void RemoveOnNodeList(GameObject item)
    {
        nodeList.Remove(item);
    }

    public void ChangeState(int type)
    {
        // Add color/animation here
        string displayText = "";
        switch (type)
        {
            case StateType.MISS:
                displayText = "MISS";
                break;
            case StateType.GOOD:
                displayText = "GOOD";
                break;
            case StateType.GREAT:
                displayText = "GREAT";
                break;
            case StateType.PERFECT:
                displayText = "PERFECT";
                break;
        }
        StateLabel.text = displayText;
    }

}

public class StateType
{
    public const int MISS = 0;
    public const int GOOD = 1;
    public const int GREAT = 2;
    public const int PERFECT = 3;
}

