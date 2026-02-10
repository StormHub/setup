namespace Graph.Tests;

public sealed class GraphTests
{
    [Fact]
    public void Order_WithLinearDependencies()
    {
        // A -> B -> C
        var nodeC = new Node<char>('C', []);
        var nodeB = new Node<char>('B', [ nodeC ]);
        var nodeA = new Node<char>('A', [ nodeB ]);
        
        var nodes = new[] { nodeA, nodeB, nodeC };
        var result = nodes.Order();
        
        Assert.Collection(result,   
            first => Assert.Equal('A', first.Value),
            second => Assert.Equal('B', second.Value),
            third => Assert.Equal('C', third.Value));
    }

    [Fact]
    public void Order_WithMultipleDependencies_ReturnsValidTopologicalOrder()
    {
        // D -> [B, C]
        // B -> A
        var nodeA = new Node<char>('A', []);
        var nodeB = new Node<char>('B', [ nodeA ]);
        var nodeC = new Node<char>('C', []);
        var nodeD = new Node<char>('D', [ nodeB, nodeC ]);
        
        var nodes = new[] { nodeA, nodeB, nodeC, nodeD };
        var result = nodes.Order();
        
        Assert.Collection(result,   
            first => Assert.Equal('D', first.Value),
            second => Assert.Equal('C', second.Value),
            third => Assert.Equal('B', third.Value),
            fourth => Assert.Equal('A', fourth.Value));
    }
}