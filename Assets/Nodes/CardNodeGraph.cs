using UnityEngine;
using XNode;

[CreateAssetMenu(fileName = "New Card Deck", menuName = "Reigns/Card Deck")]
public class CardNodeGraph : NodeGraph
{
    public BaseNode startNode;
    public BaseNode currentNode;

    public void Start()
    {
        currentNode = startNode;
        currentNode?.Execute();
    }

    public void Execute()
    {
        currentNode?.Execute();
    }
}
