using System.Collections.Generic;
using UnityEngine;

public class NodeManager : BaseSingleton<NodeManager>
{
     public NodeScriptableObject NodeData;
     private List<NodeReceiverComponent> receiverList = new List<NodeReceiverComponent>();
     
     
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
    
}
