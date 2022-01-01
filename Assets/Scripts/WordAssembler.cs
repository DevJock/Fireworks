using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordAssembler : MonoBehaviour
{
    public static GameObject wordObjectForWord(string word)
    {
        word = word.ToUpper();
        float moveValue = 0.025f;
        float charPosition = 0;
        int mid = word.Length / 2;
        charPosition = -mid * moveValue;
        GameObject wordGameObject = new GameObject();
        wordGameObject.name = word;
        for (int i = 0; i < word.Length; i++)
        {
            char ch = word[i];
            if ((ch < 'A' || ch > 'Z'))
            {
                continue;
            }
            GameObject character = Instantiate(Resources.Load("Prefabs/Letters/letter" + ch) as GameObject);
            character.name = "" + ch;
            character.transform.parent = wordGameObject.transform;
            character.transform.localPosition = new Vector3(charPosition, 0, 0);
            character.transform.localRotation = new Quaternion(0, 180, 0, 0);
            character.GetComponent<Renderer>().material.color = Color.gray;
            charPosition += moveValue;
        }
        BoxCollider collider = wordGameObject.AddComponent<BoxCollider>();
        collider.isTrigger = false;
        collider.center = new Vector3(0, 0.0125f, 0);
        collider.size = new Vector3(wordGameObject.name.Length * moveValue, 0.03f, 0.01f);
        return wordGameObject;
    }

}
