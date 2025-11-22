using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PlayerTag : MonoBehaviourPun
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!photonView.IsMine) return;

        PhotonView otherView = collision.gameObject.GetComponentInParent<PhotonView>();
        if (otherView == null)
        {
            Debug.LogWarning("PhotonView non trouvé dans l'objet ou ses parents.");
            return;
        }

        string winnerId = PhotonNetwork.LocalPlayer.CustomProperties["PlayerId"] as string;
        string loserId = otherView.Owner.CustomProperties["PlayerId"] as string;

        string winnerName = PhotonNetwork.LocalPlayer.CustomProperties["PlayerName"] as string;
        string loserName = otherView.Owner.CustomProperties["PlayerName"] as string;

        photonView.RPC("DeclareWinner", RpcTarget.AllBuffered, winnerId, winnerName, loserId, loserName);
    }

    [PunRPC]
    private void DeclareWinner(string winnerId, string winnerName, string loserId, string loserName)
    {
        GameManager.Instance.HandleRoundEnd(winnerId, winnerName, loserId, loserName);
    }
}
