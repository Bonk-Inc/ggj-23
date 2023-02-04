using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Story Point", menuName = "story/story points/story point")]
public class StoryPoint : GamePoint
{

    [field: SerializeField]
    public string Title { get; private set; }

    [field: SerializeField]
    public string Story { get; private set; }

    [field: SerializeField]
    public Sprite Background { get; private set; }

    [field: SerializeField]
    public AudioClip Music { get; private set; }

    [field: SerializeField]
    public List<Decision> Decisions { get; private set; }

}
