using System.Collections.Generic;
using UnityEngine;

public interface INode {
    Vector3 GetPosition();

    void AddNeighbor(INode node);

    HashSet<INode> GetNeighbors();

    bool Equals(INode node);
}
