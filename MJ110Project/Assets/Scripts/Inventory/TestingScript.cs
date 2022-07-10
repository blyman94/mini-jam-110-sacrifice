using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{

    public collectable item;
    // Start is called before the first frame update
    void Start()
    {
        item.Collect();
    }

}
