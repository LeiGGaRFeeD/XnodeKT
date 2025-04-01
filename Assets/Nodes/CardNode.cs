using UnityEngine;
using XNode;

public class CardNode : BaseNode
{
    [Input] public BaseNode entry;

    public string characterName;
    public string dialogue;
    public string leftAction;
    public string rightAction;

    [Output] public BaseNode leftExit;
    [Output] public BaseNode rightExit;

    public override void Execute()
    {
        // Отображаем карточку с соответствующей информацией
        GameManager.Instance.ShowCard(characterName, dialogue, leftAction, rightAction, this);
    }

    public void ChooseLeft()
    {
        // Сохраняем ход в историю для возможности отката
        GameManager.Instance.RecordMove(this, false);

        BaseNode next = null;
        NodePort port = GetOutputPort("leftExit");

        if (port != null && port.Connection != null)
        {
            next = port.Connection.node as BaseNode;
            if (next != null)
            {
                // Исправление ошибки CS0165 - правильное получение графа
                CardNodeGraph graph = this.graph as CardNodeGraph;
                if (graph != null)
                {
                    graph.currentNode = next;
                    next.Execute();
                }
            }
        }
    }

    public void ChooseRight()
    {
        // Сохраняем ход в историю для возможности отката
        GameManager.Instance.RecordMove(this, true);

        BaseNode next = null;
        NodePort port = GetOutputPort("rightExit");

        if (port != null && port.Connection != null)
        {
            next = port.Connection.node as BaseNode;
            if (next != null)
            {
                // Исправление ошибки CS0165 - правильное получение графа
                CardNodeGraph graph = this.graph as CardNodeGraph;
                if (graph != null)
                {
                    graph.currentNode = next;
                    next.Execute();
                }
            }
        }
    }
}
