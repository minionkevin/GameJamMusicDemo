using DG.Tweening;
using TMPro;
using UnityEngine;

public class NodeReceiverComponent : MonoBehaviour
{
    public TextMeshProUGUI NameLabel;
    public string Name;
    
    private const int NodeGeneratorPosY = 800;

    private void Awake()
    {
        NameLabel.text = Name;
    }

    public void RefreshName(string newName)
    {
        // todo 考虑中途+receiver的情况
        // NodeManager.Instance.CleanReceiver();
        Name = newName;
        NameLabel.text = Name;
    }

    public void HandlePlayerInput()
    {
        foreach (var node in NodeManager.Instance.nodeList)
        {
            if (node == null) continue;
            var nodeComponent = node.GetComponent<NodeBaseComponent>();
            
            if(nodeComponent.IsCheck || !node.activeSelf)continue;
            nodeComponent.IsCheck = true;
            CheckForScore(nodeComponent);
            break;
        }

        // todo cleanup and reuse
        // 用objectpool就不用担心删除问题
        // 失活之后直接cleanup就可以继续排队了
    }

    private void HandleOverAnimation(NodeBaseComponent node)
    {
        // Add animation here
        node.transform.DOScale(0, 0.2f).WaitForCompletion();
        node.gameObject.SetActive(false);
    }
    
    private void CheckForScore(NodeBaseComponent node)
    {
        if (!node.NodeName.Equals("NA") && !node.NodeName.Equals(Name))
        {
            NodeManager.Instance.ChangeState(StateType.MISS);
        }
        else if (DistanceCheck(node.transform.position, transform.position, 0.08f * NodeGeneratorPosY / 1.5f))
        {
            NodeManager.Instance.ChangeState(StateType.PERFECT);
        }
        else if (DistanceCheck(node.transform.position, transform.position, 0.12f * NodeGeneratorPosY / 1.5f))
        {
            NodeManager.Instance.ChangeState(StateType.GREAT);
        }
        else if (DistanceCheck(node.transform.position, transform.position, 0.16f * NodeGeneratorPosY / 1.5f))
        {
            NodeManager.Instance.ChangeState(StateType.GOOD);
        }
        else NodeManager.Instance.ChangeState(StateType.MISS);
        
        HandleOverAnimation(node);
    }

    private bool DistanceCheck(Vector3 posA, Vector3 posB, float maxYDis)
    {
        return Mathf.Abs(posA.y - posB.y) <= maxYDis;
    }
}
