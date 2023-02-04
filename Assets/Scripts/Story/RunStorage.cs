using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunStorage : MonoBehaviour
{

    [SerializeField]
    private StoryPlayer storyPlayer;

    private List<(StoryArea, GamePoint)> UnlockedList;


    public static RunStorage Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception("Not allowed to have more than one run storages (Singleton funnies)");
        }

    }

    public void Unlock(StoryArea area, GamePoint gamepoint)
    {
        UnlockedList.Add((area, gamepoint));
        storyPlayer.UnlockPoint(area, gamepoint);
    }

    public void ReunlockAll()
    {
        UnlockedList.ForEach((unlock) =>
        {
            var (area, point) = unlock;
            storyPlayer.UnlockPoint(area, point);
        });
    }


}
