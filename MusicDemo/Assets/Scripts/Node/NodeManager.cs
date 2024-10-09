using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NodeManager : BaseSingleton<NodeManager>
{
    public NodeScriptableObject NodeData;
    public MusicGroupScriptableObject GroupData;
    public TextMeshProUGUI StateLabel;
    public List<NodeReceiverComponent> receiverList = new List<NodeReceiverComponent>();
    public NodeGeneratorComponent NodeGenerator;
    public NodeReceiverManagerComponent receiverManager;
    
    // change to private later
    public List<GameObject> nodeList = new List<GameObject>();

    private int GroupAIndex = 0;
    private int GroupBIndex = 0;

    public void Start()
    {
        // Start process data and spawn
        NodeGenerator.StartSpawn();
        
        foreach (var index in GroupData.groupAOrder)
        {
            StartCoroutine(Test(NodeData.NodeLists[index].GroupStartTime));
        }   
    }

    public void SpawnLevel()
    {
        if (GroupBIndex > GroupData.groupBOrder.Count - 1 || GroupAIndex > GroupData.groupAOrder.Count - 1) return;
        if (GroupData.groupAOrder[GroupAIndex] > NodeData.NodeLists.Count - 1 || GroupData.groupBOrder[GroupBIndex] > NodeData.NodeLists.Count - 1) return;
        
        // Update receiver
        receiverManager.UpdateInputName(NodeGenerator.IsPlayerB ? NodeData.NodeLists[GroupData.groupBOrder[GroupBIndex]].NodeName : NodeData.NodeLists[GroupData.groupAOrder[GroupAIndex]].NodeName);
        // Spawn level
        NodeGenerator.SpawnLevel(NodeGenerator.IsPlayerB ? NodeData.NodeLists[GroupData.groupBOrder[GroupBIndex]] : NodeData.NodeLists[GroupData.groupAOrder[GroupAIndex]]);

        StartCoroutine(NodeGenerator.ActivateNodes());
        
        GroupBIndex++;
        GroupAIndex++;
    }
    
    IEnumerator Test(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SpawnLevel();
    }

    public NodeReceiverComponent FindStartPos(string nodeName)
    {
        foreach (var item in receiverList)
        {
            if (item.Name.Equals(nodeName)) return item;
        }
        return null;
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

