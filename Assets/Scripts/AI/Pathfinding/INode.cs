using System.Collections.Generic;
using UnityEngine;

public interface INode : IWithPosition {
    void AddNeighbor(INode node);

    HashSet<INode> GetNeighbors();

    bool Equals(INode node);
}
