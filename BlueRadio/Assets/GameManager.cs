using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{

    public static GameManager Instance;
    public int BlueInfluence;
    public int RedInfluence;

    public List<HouseObject> housesList;

    void Start()
    {
        Instance = this;
        BlueInfluence = 0;
        RedInfluence = 0;
    }

    void Update()
    {
        BlueInfluence = 0;
        RedInfluence = 0;
        foreach (HouseObject house in housesList)
        {
            if (house.influence == 1)
            {
                BlueInfluence++;
            }
            else if (house.influence == 2)
            {
                RedInfluence++;
            }
        }
    }

    public enum TeamType
    {
        ATANDTURF,
        VERIZONE,
        COUNT,
    };

    [SyncVar]
    private TeamType nextTeam;

    public TeamType team;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        team = ++nextTeam;
    }
}
 
