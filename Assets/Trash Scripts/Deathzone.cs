using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deathzone : MonoBehaviour
{
    Animator animator;
    GameObject player;
    private void Start()
    {
        player = GameObject.Find("Hero");
        animator = player.GetComponent<Animator>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DropItems(player);
            animator.SetBool("isDead", true);
            StartCoroutine(WaitForSceneLoad(other));
        }
    }

    private void DropItems(GameObject _player)
    {
        var holdItem = _player.GetComponent<HoldItem>();
        var heroController = _player.GetComponent<HeroController>();
        if (holdItem != null && holdItem.heldItem != null && heroController != null)
        {
            heroController.DropItem(holdItem);
        }
    }

    private IEnumerator WaitForSceneLoad(Collider other)
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}