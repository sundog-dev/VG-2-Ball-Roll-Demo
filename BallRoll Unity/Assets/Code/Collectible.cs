using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectible : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        SoundManager.Instance.PlayCollectSound();
        CollectibleManager.Instance.IncrementCollectedCoinCount();
        print("collect coin!");
    }
}
