using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    // Scoring System
    int currentScore;
    int currentPlayerHealth;

    // Reset Values

    // World Values
    public float groundSurface = -1;
    public int maxEnemySpawn = 2;

    // Player Values
    public float maxSpeed = 10f;
    public float starSpeed = 50f;

    // Enemy Values
    public int flattenSteps = 5;

    // Debris
    public int debrisSpawnCount = 6;

    // Rotator
    public int rotatorSpeed = 15;
}
