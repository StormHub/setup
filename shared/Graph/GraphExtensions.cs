namespace Graph;

public readonly struct Node<T>(T value, IReadOnlyCollection<Node<T>> children) : IEquatable<Node<T>>
{
    public T Value { get; } = value;

    public IReadOnlyCollection<Node<T>> Children { get; } = children;

    public bool Equals(Node<T> other) => 
        EqualityComparer<T>.Default.Equals(Value, other.Value)
        && Children.Equals(other.Children);

    public override bool Equals(object? obj) => obj is Node<T> other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(Value, Children);
}

public static class GraphExtensions
{
    extension<T>(IEnumerable<Node<T>> nodes)
    {
        public IReadOnlyCollection<Node<T>> TopologicalSort()
        {
            var result = nodes.Order().ToList();
            result.Reverse();
            return result.ToArray();
        }

        public IReadOnlyCollection<Node<T>> Order()
        {
            var result = new List<Node<T>>();
        
            var visited = new HashSet<Node<T>>();
            var visiting = new HashSet<Node<T>>();

            foreach (var node in nodes)
            {
                if (!visited.Contains(node))
                {
                    if (!DepthFirstSearch(node, visited, visiting, result))
                        throw new InvalidOperationException($"Cycle detected in graph {node.Value}");
                }
            }

            return result.ToArray();
        }
    }

    private static bool DepthFirstSearch<T>(Node<T> node, HashSet<Node<T>> visited, HashSet<Node<T>> visiting, List<Node<T>> result)
    {
        if (visiting.Contains(node)) return false; // cycle detected
        if (visited.Contains(node)) return true;

        visiting.Add(node);
        foreach (var child in node.Children)
        {
            if (!DepthFirstSearch(child, visited, visiting, result))
            {
                return false;
            }
        }
        visiting.Remove(node);
        
        visited.Add(node);
        result.Add(node);

        return true;
    }    
}