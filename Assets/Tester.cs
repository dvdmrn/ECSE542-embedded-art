using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("HEY bub");
        string phrase = "The quick brown fox\njumps over the lazy dog.";
        print(phrase);
        string[] words = phrase.Split('\n');

        foreach (var word in words)
        {
            print(word);
        }

    }

    
}
