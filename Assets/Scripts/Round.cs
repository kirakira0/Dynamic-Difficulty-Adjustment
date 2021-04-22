using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round
{
    public List<Policy> policies;

    public Round(List<Policy> policies) {
        this.policies = policies;
    }

    public Policy GetPolicy(int policyIndex) {
        return this.policies[policyIndex];
    }

    public int GetLength() {
        return this.policies.Count;
    }

}
