using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class NodeGeneratorComponent : MonoBehaviour
{
    public bool IsPlayerB;
    public RectTransform MusicNodeContainer;
    
    // 开始到现在
    // 一个片段里面的节奏
    
    // todo Add object pool for all nodes 
    private List<int> randomNodeList = new List<int>();
    
    private void Start()
    {
        // todo read in data here
        // randomNodeList.Add(0);
        // randomNodeList.Add(2);
        // randomNodeList.Add(3);
        // randomNodeList.Add(1);
        // randomNodeList.Add(0);
        // randomNodeList.Add(2);
        
        randomNodeList.Add(0);
        randomNodeList.Add(0);
        randomNodeList.Add(0);
        randomNodeList.Add(0);
        randomNodeList.Add(0);
        randomNodeList.Add(0);
        randomNodeList.Add(0);
        randomNodeList.Add(0);
        randomNodeList.Add(0);
        randomNodeList.Add(0);

        foreach (var num in randomNodeList)
        {
            HandleSpawnNode(NodeManager.Instance.NodeData.nodeList[num]);   
        }
        
        StartCoroutine(ActivateNodes());
    }
    
    
    private void HandleSpawnNode(MusicNode nodeData)
    {
        NodeReceiverComponent receiverComponent = NodeManager.Instance.FindStartPos(nodeData.Name);
        if (receiverComponent == null) return;
        
        GameObject node = IsPlayerB ? Instantiate(nodeData.NodeBPrefab, MusicNodeContainer) : Instantiate(nodeData.NodePrefab, receiverComponent.transform);
        Vector3 startPos = new Vector3(node.transform.position.x, transform.position.y, 0);
        node.transform.position = startPos;
        
        NodeBaseComponent nodeComponent = node.GetComponent<NodeBaseComponent>();
        nodeComponent.Setup(nodeData.Name,nodeData.NodeId,nodeData.Type,nodeData.TimeSpan);
        node.SetActive(false);
        NodeManager.Instance.AddOnNodeList(node);
        
        // maybe we dont need setup receiver, .parent can do same thing
        nodeComponent.SetupReceiver(receiverComponent);
    }

    private IEnumerator ActivateNodes()
    {
        foreach (var node in NodeManager.Instance.nodeList)
        {
            node.SetActive(true);
            NodeBaseComponent nodeComponent = node.GetComponent<NodeBaseComponent>();
            HandleNodeMove(nodeComponent);
            yield return new WaitForSeconds(nodeComponent.TimeSpan);
        }
    }

    private async void HandleNodeMove(NodeBaseComponent nodeComponent)
    {
        // Change duration
        // Play sound here
        // Play with this animation

        Sequence timeline = DOTween.Sequence();
        timeline.Insert(0, nodeComponent.transform.DOMoveY(nodeComponent.transform.parent.position.y, 1.5f).SetEase(Ease.Linear));
        timeline.Insert(1.5f, nodeComponent.transform.DOScale(0, 0.15f));
        await timeline.Play().AsyncWaitForCompletion();

        if (IsPlayerB)
        {
            // 处理当块过了并且玩家没有任何输入的情况
            nodeComponent.IsCheck = true;
            nodeComponent.gameObject.SetActive(false);
            NodeManager.Instance.ChangeState(StateType.MISS);
            return;
        }
        NodeManager.Instance.RemoveOnNodeList(nodeComponent.gameObject);
        Destroy(nodeComponent.gameObject);  
    }
}
