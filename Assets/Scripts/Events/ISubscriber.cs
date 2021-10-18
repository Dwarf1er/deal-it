using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubscriber {
    bool HasDistance();
    Transform GetTransform();
}
