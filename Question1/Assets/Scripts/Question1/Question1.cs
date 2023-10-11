using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question1 : MonoBehaviour
{
    private List<char> characterList = new List<char>();
    int[] map = new int[26];
    void Start()
    {
        InitilizeList();
        RemoveRandom();
        Debug.Log("This is the random " + FindRandom());
    }

    private void InitilizeList()
    {
        for (int i = 0; i < 26; i++)
        {
            char x = (char)('A' + i);
            characterList.Add(x);
        }

        int n = characterList.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            char temp = characterList[i];
            characterList[i] = characterList[j];
            characterList[j] = temp;
        }
    }

    private void RemoveRandom()
    {
        int randomIndex = Random.Range(0, characterList.Count);
        char removedCharacter = characterList[randomIndex];
        characterList.RemoveAt(randomIndex);
    }

    private char FindRandom()
    {
        int n = characterList.Count;
        for (int i = 0; i < n; i++)
        {
            map[characterList[i] - 'A'] = 1;
        }

        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == 0)
            {
                return (char)('A' + i);
            }
        }
        return 'A';
    }
    void Update()
    {

    }
}
