using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class NodeGeneratorComponent : MonoBehaviour
{
    public bool IsPlayerB;
    public RectTransform MusicNodeContainer;
    // 根据type来区分prefab，不需要每个配置
    // 注意：A和B的顺序必须和NodeType保持完全一样
    public List<GameObject> NodePrefab = new List<GameObject>();
    
    // todo Add object pool for all nodes 
    private float groupWaitSpan = 0f;
    
    private void Start()
    {
        // check for completion and increase level here

        SpawnLevel();
        StartCoroutine(ActivateNodes());
    }

    private void SpawnLevel()
    {
        foreach (var data in NodeManager.Instance.NodeData.NodeLists)
        {
            if (data.isPlayerB && IsPlayerB) SpawnSingleNode(data);
            else SpawnSingleNode(data);
            groupWaitSpan = data.GroupStartTime;
        }
    }

    private void SpawnSingleNode(MusicNodeList data)
    {
        foreach (var nodeData in data.NodeList)
        {
            HandleSpawnNode(nodeData);
        }
    }

    private GameObject FindSpawnPrefab(int type)
    {
        return NodePrefab[type];
    }
    
    private void HandleSpawnNode(MusicNode nodeData)
    {
        NodeReceiverComponent receiverComponent = NodeManager.Instance.FindStartPos(nodeData.Name);
        
        if (nodeData.Type == NodeType.NORMAL && receiverComponent == null) return;
        Transform trans = nodeData.Type == NodeType.NORMAL ? receiverComponent.transform : MusicNodeContainer;

        GameObject node = Instantiate(FindSpawnPrefab(nodeData.Type),trans); 
        Vector3 startPos = new Vector3(node.transform.position.x, transform.position.y, 0);
        node.transform.position = startPos;
        
        NodeBaseComponent nodeComponent = node.GetComponent<NodeBaseComponent>();
        nodeComponent.Setup(nodeData.Name,nodeData.NodeId,nodeData.Type,nodeData.StartTime);
        node.SetActive(false);
        
        NodeManager.Instance.nodeList.Add(node);
        
        if (nodeData.Type == NodeType.NORMAL)
        {
            nodeComponent.SetupReceiver(receiverComponent);
            node.transform.SetParent(receiverComponent.transform);
        }
        else node.transform.SetParent(MusicNodeContainer);
    }

    private IEnumerator ActivateNodes()
    {
        yield return new WaitForSeconds(groupWaitSpan);
        foreach (var node in NodeManager.Instance.nodeList)
        {
            node.SetActive(true);
            NodeBaseComponent nodeComponent = node.GetComponent<NodeBaseComponent>();
            HandleNodeMove(nodeComponent);
            yield return new WaitForSeconds(nodeComponent.StartTime);
        }
    }

    private async void HandleNodeMove(NodeBaseComponent nodeComponent)
    {
        // Change duration
        // Play sound here
        // Play with this animation

        Sequence timeline = DOTween.Sequence();
        timeline.Insert(0, nodeComponent.transform.DOMoveY(!IsPlayerB ? nodeComponent.transform.parent.position.y : MusicNodeContainer.transform.position.y, 1.5f).SetEase(Ease.Linear));
        timeline.Insert(1.5f, nodeComponent.transform.DOScale(0, 0.15f));
        await timeline.Play().AsyncWaitForCompletion();
        
        // todo list会有点问题
        
        // if (IsPlayerB && nodeComponent.IsCheck)
        // {
        //     NodeManager.Instance.nodeList.Remove(nodeComponent.gameObject);
        // }

        if (IsPlayerB && !nodeComponent.IsCheck && nodeComponent.gameObject.activeSelf)
        {
            // 处理当块过了并且玩家没有任何输入的情况
            nodeComponent.IsCheck = true;
            nodeComponent.gameObject.SetActive(false);
            NodeManager.Instance.ChangeState(StateType.MISS);
            return;
        }
        // use object pool and clean up 0.o
        // NodeManager.Instance.RemoveOnNodeList(nodeComponent.gameObject);
        Destroy(nodeComponent.gameObject);  
    }
}
