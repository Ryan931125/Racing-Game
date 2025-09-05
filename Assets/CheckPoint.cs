using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioClip CheckPointSE;
    private LevelManager levelManager;

    private void Awake()
    {
      levelManager = GetComponentInParent<LevelManager>();  
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(CheckPointSE, other.gameObject.transform.position);
        // Debug.Log($"{other.transform} Passed CheckPoint");
        levelManager.LoadCheckPoint();
    }
}
