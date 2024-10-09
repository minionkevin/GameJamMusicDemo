using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeReceiverManagerComponent : MonoBehaviour
{
    public List<Button> InputBtns = new List<Button>();
    public List<NodeReceiverComponent> ReceiverList = new List<NodeReceiverComponent>();

    public void UpdateInputName(List<string> nameData)
    {
        for (int i = 0; i < nameData.Count; i++)
        {
            ReceiverList[i].RefreshName(nameData[i]);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            InputBtns[0].onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            InputBtns[1].onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            InputBtns[2].onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            InputBtns[3].onClick.Invoke();
        }
        
    }
}
