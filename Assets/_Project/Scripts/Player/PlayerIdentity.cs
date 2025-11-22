using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerIdentity : MonoBehaviourPun
{
    private void Start()
    {
        if (photonView.IsMine)
        {
            object nameObj;
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("PlayerName", out nameObj))
            {
                photonView.RPC("SetPlayerName", RpcTarget.AllBuffered, nameObj.ToString());
            }
        }
    }

    [PunRPC]
    private void SetPlayerName(string name)
    {
        TextMeshPro nameText = GetComponentInChildren<TextMeshPro>();
        if (nameText != null)
        {
            nameText.text = name;
        }
    }
}
