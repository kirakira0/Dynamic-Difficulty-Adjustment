using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * @author Kira Toal
 * @updated October 24, 2021
 */
public class Game
{
    public List<Round> rounds;

    public Game(List<Round> rounds) {
        this.rounds = rounds;
    }

    public Policy GetPolicy(int round, int policy) {
        return this.rounds[round].GetPolicy(policy);
    }

    public int PoliciesInRound(int round) {
        return this.rounds[round].GetLength(); 
    }

    public int RoundsInGame() {
        return this.rounds.Count; 
    }
}
