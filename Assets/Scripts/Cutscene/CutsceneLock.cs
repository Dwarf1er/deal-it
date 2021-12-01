using UnityEngine;
using System.Collections.Generic;

public class CutsceneLock : CutsceneAbstract {
    private static Dictionary<string, bool> locks = new Dictionary<string, bool>();
    private string name;
    private bool to_lock;

    public CutsceneLock(string name, bool to_lock) {
        this.name = name;
        this.to_lock = to_lock;
    }

    public override void Enter() {
        if(!to_lock) {
            locks[name] = false;
        } else if(!locks.ContainsKey(name)) {
            locks[name] = true;
            to_lock = false;
        }
    }

    public override bool Loop() {
        if(!to_lock) return false;

        if(!locks[name]) {
            locks[name] = true;
            return false;
        }

        return true;
    }

    public override void Exit() {}
}
