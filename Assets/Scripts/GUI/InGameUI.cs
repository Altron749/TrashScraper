using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour {
    public PowerUpTimer vacuum;
    public PowerUpTimer highJump;
    public PowerUpTimer jetPack;
    public PowerUpTimer freeze;
    public PowerUpTimer doubleScore;




    public void AddTime(float time, int type)
    {
        switch (type)
        {
            case 1:
                vacuum.AddTime(time);
                break;
            case 2:
                highJump.AddTime(time);
                break;
            case 3:
                jetPack.AddTime(time);
                break;
            case 4:
                freeze.AddTime(time);
                break;
            case 5:
                doubleScore.AddTime(time);
                break;
            default:
                break;
        }
    }
}
