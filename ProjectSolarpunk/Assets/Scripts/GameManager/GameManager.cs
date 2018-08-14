using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    bool bladeObtained;
    bool gunObtained;

    public bool BladeObtained {
        get
        {
            return bladeObtained;
        }
        set
        {
            bladeObtained = value;
            Player player = GameObject.Find("Player_Master").GetComponent<Player>();
            player.BladeObtained = true;
        }
    }

    public bool GunObtained
    {
        get
        {
            return gunObtained;
        }
        set
        {
            gunObtained = value;
            Player player = GameObject.Find("Player_Master").GetComponent<Player>();
            player.GunObtained = true;
        }
    }

}
