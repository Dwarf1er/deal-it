# Deal It

A game centered around dealing at school.

## Team

| name             | ID       |
|------------------|----------|
| Alexandre        | 40129457 |
| Antoine          | 40131102 |
| Arman            | X        |
| Julie            | 40077165 |

## Documentation

This section covers in depth the various designs of the game. This is meant for understand for those that want to contribute.

### Scenes

To creates scene, we use a combination of manual placement and the built-in Unity Tilemap tool. You can learn the basics [here](https://www.youtube.com/watch?v=ryISV_nH8qw).

### Design

For color consistency, we use the [CC-29](https://lospec.com/palette-list/cc-29) palette. A template character can be found on Google Docs (and eventually somewhere in this repo). [The Escapists](https://store.steampowered.com/app/298630/The_Escapists/) is a good source for inspiration/design.  

### AI

The AI uses a state-machine design. It was inspired by the video found [here](https://www.youtube.com/watch?v=G1bd75R10m4). Using this design allows to decouple AI behaviors, which makes it easy to reuse/sequence behaviors to create even more complex behaviors. For example, we can create a `GotoState`, which goes to a position, and if we chain many we are able to go to multiple spots. Following are pseudo-UMLs explaining the implementation.

#### AIState

The `AIState` is the abstract parent for all state classes. It provides the baseline functionaliy for the state-machine.

```csharp
/// The base class for AI states.
public abstract class AIState {
    /// The attached AI.
    private AI ai;

    /// The entry method for AI state. This behaves similar to the Start() method. It is meant to initialize the various parameters for the AIState just before calling Update. 
    public abstract void Enter();

    /// The state behavior applied to the attached AI. This is called every frame by the attached AI.
    public abstract void Update();

    /// The method called when exiting the class. This is useful for cleanup or resetting.
    public abstract void Exit();

    /// Method called by AI every tick to check if the current state needs to changet to a next state.
    public abstract AIState NextState();

    /// Checks if state is complete (always true if in a "idle" state).
    public abstract bool IsComplete();
}
```

#### SequenceState

We can implement a `SequenceState` by extending the `AIState`. This allows us to have a sequence of states, which is useful for complex behaviors. The "UML" is not provided, though it can be a very useful class to use/implement.

#### AI

Given we are using a state-machine, the AI class essentially serves just as data and running the state. It can be minimally implemented as follows (though adding utility methods can be useful).

```csharp
/// The base AI implementation.
public class AI {
    /// The current AI state.
    private AIState state;

    /// Base Unity Start. Important to start state at the same time.
    public void Start() {
        this.state = new IdleState(this);
        this.state.Enter();
    }

    /// Exits previous state and sets/starts next state. Should be private and be called by more descriptive methods.
    private void SetNextState(AIState nextState) {
        if(nextState == this.state) return;
        this.state.Exit();
        this.state = nextState;
        this.state.Enter();
    }

    /// Base Unity Update. Just calls the current state and checks if we need to change state.
    public override void Update() {
        this.state.Update();
        this.SetNextState(this.state.NextState());
    }
}
```

### Pathfinding

Unity does not provide pathfinding for 2D. There is a 3D variant, but is not great for 2D. Therefore, we can use abit of graph theory to implement pathfinding (given the game is on a flat grid). We can place `Node`s around the map, which has bidirectional edges to their neighboring `Node`, and use `Breadth-first search` to find paths between nodes. You can find a refresher on BFS [here](https://www.youtube.com/watch?v=QRq6p9s8NVg).

#### Node

`Node`s are both the position and edges. They form a network with their neighbors. They can be easily implemented (and visualed in Unity), with the following:

```csharp
public class Node : MonoBehaviour {
    /// The edges to the neighboring node. The edge class was skipped for now because shortest path is not that important. It could be an issue to keep in mind, but implementing it this way makes it alot cleaner to travel/make the path.
    public Node[] neighbors;

    /// This is a Unity utility that allows to draw debug information in the Unity editor. In this case, we draw spheres at the current position and lines to neighbors.
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.1f);

        foreach(Node neighbor in this.neighbors) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, neighbor.transform.position);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(neighbor.transform.position, 0.1f);
        }
    }
}
```

#### Graph

The graph class is just a utility over a `Node` array. It can be used to implement BFS (and other utilities) so that other classes don't need to think about it. It could be interesting to implement as follows:

```csharp
public class Graph : MonoBehaviour {
    private Node[] nodes;

    void Start() {
        this.nodes = this.GetComponentsInChildren<Node>();
    }

    /// Gets the nearest node on graph to Vector3 position. We can assume that the calling target will rarely be on a node, therefore this allows to find a node near the caller in O(n).
    public Node GetNearestNode(Vector3 target);

    /// Gets a path from near `from` to near `to`. It uses BFS to travel the graph and find a path. It is not necessarily the shortest because it doesn't considering the edge length. Using A* could be useful if there is ever a time where actual shortest path is important. 
    public Node[] GetPathTo(Vector3 from, Vector3 to);
}
```

### Player

The player can be implemented as usual. The only important thing is to abstract the controller and not use `Input` directly. Using either the [new Input system](https://www.youtube.com/watch?v=Pzd8NhcRzVo) or making a custom `Controller` class allows to us to implement multiplayer easily.

### Singleton

Singleton is generally an okay design pattern, but can restrict development. It would be preferable to never use this pattern. Not having assumptions of a single instance of a class will again make multiplayer easier ot implement.

### Event

It could be benificial to have an event system for notifying a handful/all AIs of changes (still testing as of writing this document). We can use something similar to MVC with `subscription`, `notification`, and AI method event handler. A specific case considered is a camera catching the player doing a bad act and have the guards change to a "pursuit" state. Could also be useful for routines (like classes starting and ending). [This video](https://www.youtube.com/watch?v=gx0Lt4tCDE0) seems to offer a nice system (though will get rid of Singleton).

### Acknowledgements

- Patrick de Arteaga, the soundtrack for the main menu of this game, Arex's Beat, was provided royalty free and can be found [here](https://patrickdearteaga.com/arcade-music/)
