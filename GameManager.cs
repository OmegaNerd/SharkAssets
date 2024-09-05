using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float difficultyMultiply = 1.5f;
    public static float sharkMoveSpeed = 4f;
    public static GameState state;
    public static float obsMoveSpeed = 0.2f;
    public static float fishMoveSpeed = 0.1f;
    public static float birdMoveSpeed = 0.1f;
    public static SpringStack springStack1;
    public static SpringStack springStack2;
    public static List<SpringOne> springOnes = new List<SpringOne>();
    public static bool canDamage = true;
    public static bool damageFlag = false;
    public static float mana = 1f;
    public static float manaMinus = 0.04f;
    public static bool musicOn = true;
    public static bool damageSoundFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void GetDamage(float damageValue) {
        if (canDamage == true) {
            canDamage = false;
            damageFlag = true;
            mana -= damageValue;
            damageSoundFlag = true;
        }
    }

    public static void GetMana(float manaValue)
    {

        mana += manaValue;
        if (mana > 1) {
            mana = 1;
        }
    }

    public static void UpdateState(GameState newState) {
        state = newState;
    }
}


public enum GameState
{
    Game,
    Lose,
    Pause
}