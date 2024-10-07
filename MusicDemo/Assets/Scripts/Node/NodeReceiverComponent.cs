using TMPro;
using UnityEngine;

public class NodeReceiverComponent : MonoBehaviour
{
    public TextMeshProUGUI NameLabel;
    public string Name;
    // public NodeManager

    // generate them in the future
    // add data in
    public void Setup()
    {
        
    }

    public void Start()
    {
        NameLabel.text = Name;
        NodeManager.Instance.AddReceiver(this);
    }
}
