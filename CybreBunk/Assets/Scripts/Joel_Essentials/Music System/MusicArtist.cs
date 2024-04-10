using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicArtist : MonoBehaviour
{
    [SerializeField] GameObject musicPlayerPrefab;
    [SerializeField] int        songNumber;
    void Start()
    {
        MusicPlayer player;

        if (player = FindObjectOfType<MusicPlayer>()) player.ChangeMusic(player.allSong[songNumber]);
        else
        {
            player = Instantiate(musicPlayerPrefab).GetComponent<MusicPlayer>();
            player.ChangeMusic(player.allSong[songNumber]);
        }
        Destroy(gameObject);
    }
}
