using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnHandler : MonoBehaviour
{

    public abstract IEnumerator RunTurn();

}
