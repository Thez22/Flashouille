using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_Text statusText;
    public Button startButton;

    private void Start()
    {
        statusText.text = "En attente d’un autre joueur...";
        startButton.gameObject.SetActive(false);

        startButton.onClick.AddListener(() => {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("FlashouilleArena");
            }
        });
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            statusText.text = "Un joueur a rejoint ! Vous pouvez lancer la partie.";
            startButton.gameObject.SetActive(true);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            statusText.text = "Un joueur est parti. En attente...";
            startButton.gameObject.SetActive(false);
        }
    }
}
