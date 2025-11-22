using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;
using ExitGames.Client.Photon;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreTextPlayer1;
    public TextMeshProUGUI scoreTextPlayer2;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
{
    Player[] players = PhotonNetwork.PlayerList;
    Debug.Log($"ScoreManager → Nombre de joueurs : {players.Length}");

    if (players.Length > 0 && players[0].CustomProperties.TryGetValue("Score", out object score1))
    {
        string name1 = players[0].NickName;
        scoreTextPlayer1.text = $"{name1}: {score1}";
    }
    else
    {
        Debug.LogWarning("Joueur 1 score non trouvé");
    }

    if (players.Length > 1 && players[1].CustomProperties.TryGetValue("Score", out object score2))
    {
        string name2 = players[1].NickName;
        scoreTextPlayer2.text = $"{name2}: {score2}";
    }
    else
    {
        Debug.LogWarning("Joueur 2 score non trouvé");
    }
}}

