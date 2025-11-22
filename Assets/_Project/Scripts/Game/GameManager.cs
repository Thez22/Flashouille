using Photon.Pun;
using UnityEngine;
using ExitGames.Client.Photon;


public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;

    public Transform spawnPoint1;
    public Transform spawnPoint2;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void HandleRoundEnd(string winnerId, string winnerName, string loserId, string loserName)
{
    Debug.Log($"Vainqueur : {winnerName} ({winnerId}) | Perdant : {loserName} ({loserId})");

    //  AJOUT : Incrémentation du score du gagnant
    foreach (Photon.Realtime.Player p in Photon.Pun.PhotonNetwork.PlayerList)
    {
        if (p.UserId == winnerId)
        {
            int oldScore = 0;

            if (p.CustomProperties.TryGetValue("Score", out object scoreObj))
                oldScore = (int)scoreObj;

            int newScore = oldScore + 1;

            ExitGames.Client.Photon.Hashtable ht = new ExitGames.Client.Photon.Hashtable
            {
                { "Score", newScore }
            };

            p.SetCustomProperties(ht);
            break;
        }
    }

    // Mise à jour affichage score
    ScoreManager.Instance.UpdateScoreUI();

    //  Partie d’origine NON modifiée
    foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
    {
        PhotonView view = player.GetComponent<PhotonView>();
        if (view != null && view.Owner != null)
        {
            string id = view.Owner.CustomProperties["PlayerId"] as string;
            if (id == loserId && view.IsMine)
            {
                PhotonNetwork.Destroy(player);
                RespawnLocalPlayer();
                break;
            }
        }
    }
}

private void RespawnLocalPlayer()
{
    int actor = PhotonNetwork.LocalPlayer.ActorNumber;
    Transform spawnPoint = (actor == 1) ? spawnPoint1 : spawnPoint2;

    PhotonNetwork.Instantiate("Player", spawnPoint.position, Quaternion.identity);
}}

