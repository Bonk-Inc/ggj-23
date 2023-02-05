using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyMe : MonoBehaviour
{

    [SerializeField]
    private string singleName = string.Empty;

    private static HashSet<string> created = new HashSet<string>();

    private void Awake()
    {
        if (singleName != string.Empty)
        {
            if (created.Contains(singleName))
            {
                Destroy(gameObject);
            }
            else
            {
                created.Add(singleName);
            }
        }

        DontDestroyOnLoad(gameObject);
    }
}
