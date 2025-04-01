using XNode;

public abstract class BaseNode : Node
{
    [Output] public BaseNode exit;

    public virtual void Execute()
    {
        // Переопределяется дочерними классами
    }

    public BaseNode NextNode(string exitPortName = "exit")
    {
        BaseNode nextNode = null;
        NodePort port = GetOutputPort(exitPortName);

        if (port != null && port.Connection != null)
        {
            nextNode = port.Connection.node as BaseNode;

            if (nextNode != null)
            {
                // Исправление ошибки CS0165 - правильное получение графа
                CardNodeGraph graph = this.graph as CardNodeGraph;
                if (graph != null)
                {
                    graph.currentNode = nextNode;
                    nextNode.Execute();
                }
            }
        }

        return nextNode;
    }
}
