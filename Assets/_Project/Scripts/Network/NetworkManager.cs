using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance;

    [SerializeField] private string gameVersion = "1.0";

    private void Awake()
    {
        // Singleton persistant
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.AutomaticallySyncScene = true; //  Important pour synchroniser LoadLevel()
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRoom(string roomCode)
    {
        if (PhotonNetwork.IsConnected)
        {
            RoomOptions options = new RoomOptions { MaxPlayers = 2 };
            PhotonNetwork.CreateRoom(roomCode, options, TypedLobby.Default);
        }
        else
        {
            Debug.LogWarning(" Pas encore connecté à Photon.");
        }
    }

    public void JoinRoom(string roomCode)
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRoom(roomCode);
        }
        else
        {
            Debug.LogWarning(" Pas encore connecté à Photon.");
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(" Room joined: " + PhotonNetwork.CurrentRoom.Name);

        // ❌ Ne rien faire ici → Le MasterClient lancera la partie manuellement
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning($" Join failed ({returnCode}): {message}");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning($" Create failed ({returnCode}): {message}");
    }
}
