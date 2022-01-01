using System.Collections;
using UnityEngine;

public class FireWorkShow : MonoBehaviour
{
    [SerializeField]
    public int[] fireIDs;

    private readonly float fireDelay = 2;
    private readonly int fireWorksCount = 10;
    private readonly int maxRounds = 4;
    private AudioClip[] clips;
    private AudioSource[] fireWorkAudio;
    private ParticleSystem[] fireWorks;
    private float timer;

    // Use this for initialization
    private void Start()
    {
        var availableFireworks = Resources.LoadAll<GameObject>("Fireworks/Variety");
        var defaultFirework = Resources.Load<GameObject>("Fireworks/Variety/FireworksGold2");
        fireIDs = new int[maxRounds];
        fireWorks = new ParticleSystem[fireWorksCount];
        fireWorkAudio = new AudioSource[fireWorksCount];
        var position = transform.position;
        position.z = 10.0f;
        var leftX = fireWorksCount / 2;
        var spaceBetween = 30 / (fireWorksCount / 2);
        var left = true;
        var startPos = new Vector3(position.x, position.y, position.z);
        startPos.x -= spaceBetween;
        for (var i = 0; i < fireWorksCount; i++)
        {
            var nextFireWork = Random.Range(0, availableFireworks.Length);
            if (left)
            {
                leftX--;
                startPos.x -= spaceBetween;
            }
            else
            {
                startPos.x += spaceBetween;
            }

            if (left && leftX < 0)
            {
                startPos = position;
                left = false;
            }

            var chosenFireWork = defaultFirework; //availableFireworks[nextFireWork]
            var newFireWork = Instantiate(chosenFireWork, transform, true);
            newFireWork.transform.position = startPos;
            newFireWork.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            newFireWork.name = "FireWork_" + i;
            fireWorks[i] = newFireWork.GetComponent<ParticleSystem>();
            fireWorkAudio[i] = newFireWork.GetComponent<AudioSource>();
        }

        clips = Resources.LoadAll<AudioClip>("Audio");
    }

    // Update is called once per frame
    private void Update()
    {
        if (timer <= 0.0f)
        {
            do
            {
                for (var i = 0; i < maxRounds && fireWorks.Length > 0; i++)
                {
                    fireIDs[i] = Random.Range(0, fireWorks.Length);
                }
            } while (isEqual(fireIDs) && fireWorks.Length > 0);

            foreach (var t in fireIDs)
            {
                StartCoroutine(PlaySound(t));
            }

            timer = fireDelay;
        }

        timer -= Time.deltaTime;
    }

    private bool isEqual(int[] array)
    {
        for (var i = 0; i < array.Length - 1; i++)
        {
            for (var j = i + 1; j < array.Length; j++)
            {
                if (array[i] == array[j])
                {
                    return true;
                }
            }
        }

        return false;
    }

    private IEnumerator PlaySound(int fireworkID)
    {
        yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
        fireWorks[fireworkID].Play(true);
        yield return new WaitForSeconds(2.0f);
        fireWorkAudio[fireworkID].PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}