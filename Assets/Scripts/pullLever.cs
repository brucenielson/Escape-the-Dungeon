using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pullLever : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject wall;
    public float movementSpeed;
    private bool leverActivated = false;
    private bool top = false;
    public AudioClip audioClip;
    public bool isMovingFloor;

    public void PullLever()
    {
        this.GetComponent<Animation>().Play();
        leverActivated = true;
        AudioSource.PlayClipAtPoint(audioClip, wall.transform.position);
    }

    void FixedUpdate()
    {
            if (leverActivated)
            {
                if (isMovingFloor)
                {
                    if (!top)
                    {
                        foreach (GameObject obj in objects)
                        {
                            obj.transform.position = Vector3.MoveTowards(obj.transform.position, new Vector3(obj.transform.position.x, obj.transform.position.y + -9.5f, obj.transform.position.z + -20f), 1f * Time.fixedDeltaTime);
                        }
                        if (transform.position.y <= 42.5)
                        {
                            leverActivated = false;
                            top = true;
                        }
                    }
                    else
                    {
                        foreach (GameObject obj in objects)
                        {
                            obj.transform.position = Vector3.MoveTowards(obj.transform.position, new Vector3(obj.transform.position.x, obj.transform.position.y + 9.5f, obj.transform.position.z + 20f), 1f * Time.fixedDeltaTime);
                        }
                        if (transform.position.y >= 51.2)
                        {
                            leverActivated = false;
                            top = false;
                        }
                    }
                } else
                {
                    if (!top)
                    {
                        foreach (GameObject obj in objects)
                        {
                            obj.transform.position = Vector3.MoveTowards(obj.transform.position, new Vector3(obj.transform.position.x, obj.transform.position.y + 9.3f, obj.transform.position.z), 1f * Time.fixedDeltaTime);
                        }
                        wall.transform.position = Vector3.MoveTowards(wall.transform.position, new Vector3(38.99f, 35.5f, 1.06f), 0.8f * Time.fixedDeltaTime);

                        if (transform.position.y >= 51.51)
                        {
                            leverActivated = false;
                            top = true;
                        }
                    }
                    else
                    {
                        foreach (GameObject obj in objects)
                        {
                            obj.transform.position = Vector3.MoveTowards(obj.transform.position, new Vector3(obj.transform.position.x, obj.transform.position.y - 9.3f, obj.transform.position.z), 1f * Time.fixedDeltaTime);
                        }
                        wall.transform.position = Vector3.MoveTowards(wall.transform.position, new Vector3(38.25f, 41.84f, 3.45f), 0.8f * Time.fixedDeltaTime);
                        if (transform.position.y <= 42.23)
                        {
                            leverActivated = false;
                            top = false;
                        }
                    }
                }

            }
    }
}
