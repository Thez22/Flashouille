using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    private static bool alreadySpawned = false;

    private void Start()
    {
        if (alreadySpawned) return;
        alreadySpawned = true;

        int actor = PhotonNetwork.LocalPlayer.ActorNumber;
        Transform spawn = actor == 1 ? spawnPoint1 : spawnPoint2;

        PhotonNetwork.Instantiate("Player", spawn.position, Quaternion.identity);
    }
}
