using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class NodeGeneratorComponent : MonoBehaviour
{
    // todo Add object pool for all nodes 
    private List<GameObject> nodeList = new List<GameObject>();
    
    private void Start()
    {
        // todo read in data here
        for (int i = 0; i < 20; i++)
        {
            HandleSpawnNode(NodeManager.Instance.NodeData.nodeList[Random.Range(0, NodeManager.Instance.NodeData.nodeList.Count - 1)]);
        }

        StartCoroutine(ActivateNodes());
    }
    
    
    private void HandleSpawnNode(MusicNode nodeData)
    {
        NodeReceiverComponent receiverComponent = NodeManager.Instance.FindStartPos(nodeData.Name);
        if (receiverComponent == null) return;
        
        GameObject node = Instantiate(nodeData.NodePrefab,receiverComponent.transform);
        Vector3 startPos = new Vector3(node.transform.position.x, transform.position.y, 0);
        node.transform.position = startPos;
        
        NodeBaseComponent nodeComponent = node.GetComponent<NodeBaseComponent>();
        nodeComponent.Setup(nodeData.Name,nodeData.NodeId,nodeData.Type,nodeData.TimeSpan);
        node.SetActive(false);
        nodeList.Add(node);
        
        // maybe we dont need setup receiver, .parent can do same thing
        nodeComponent.SetupReceiver(receiverComponent);
    }

    private IEnumerator ActivateNodes()
    {
        foreach (var node in nodeList)
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
        Sequence timeline = DOTween.Sequence();
        timeline.Insert(0, nodeComponent.transform.DOMoveY(nodeComponent.transform.parent.position.y, 1.0f).SetEase(Ease.Linear));
        timeline.Insert(1.15f, nodeComponent.transform.DOScale(0, 0.15f));
        await timeline.Play().AsyncWaitForCompletion();
        
        Destroy(nodeComponent.gameObject);
    }
}
