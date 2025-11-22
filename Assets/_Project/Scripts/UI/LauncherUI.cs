using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using ExitGames.Client.Photon;

public class LauncherUI : MonoBehaviour
{
    public TMP_InputField roomInput;
    public TMP_InputField nameInput;
    public Button createButton;
    public Button joinButton;

    private void Start()
    {
        createButton.onClick.AddListener(() => {
            SetPlayerName();
            if (!string.IsNullOrEmpty(roomInput.text))
                NetworkManager.Instance.CreateRoom(roomInput.text);
        });

        joinButton.onClick.AddListener(() => {
            SetPlayerName();
            if (!string.IsNullOrEmpty(roomInput.text))
                NetworkManager.Instance.JoinRoom(roomInput.text);
        });
    }

    private void SetPlayerName()
    {
        string name = nameInput.text.Trim();
        if (string.IsNullOrEmpty(name))
            name = "Joueur";

        if (name.Length > 12)
            name = name.Substring(0, 12);

        PhotonNetwork.NickName = name;

        Hashtable props = new Hashtable
{
    { "PlayerId", System.Guid.NewGuid().ToString() },
    { "PlayerName", name },
    { "Score", 0 } 
};
PhotonNetwork.LocalPlayer.SetCustomProperties(props);

    }
}
