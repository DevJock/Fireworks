using UnityEngine;

public class WordShow : MonoBehaviour
{
    public string message;
    public string heroWord;
    public string[] words;
    public GameObject[] wordObjs;
    public GameObject heroWordObj;

    // Use this for initialization
    private void Start()
    {
        words = message.Split(' ');
        heroWordObj = WordAssembler.wordObjectForWord(heroWord);
        wordObjs = new GameObject[words.Length];
        for (var i = 0; i < words.Length; i++) wordObjs[i] = WordAssembler.wordObjectForWord(words[i]);
        Animate();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void Animate()
    {
        var heroXOffset = 0.0f;
        var heroYOffset = 0.0f;
        var xOffset = 0.15f;
        float currentXOffset = 0;
        var yOffset = 0.05f;
        var currentYOffset = yOffset * wordObjs.Length;
        heroWordObj.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        heroWordObj.transform.position = new Vector3(heroXOffset, heroYOffset, 0);
        for (var i = 0; i < wordObjs.Length; i++)
        {
            wordObjs[i].transform.position = new Vector3(currentXOffset, currentYOffset, 0);
            currentXOffset += xOffset;
            //currentYOffset -= yOffset;
        }
    }
}