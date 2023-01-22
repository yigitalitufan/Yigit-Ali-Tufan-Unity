using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.AccessControl;

public class EnterPlane : MonoBehaviour
{
    public GameObject EnterText;
    public GameObject ExitText;
    public Movement PlayerScript;
    public GameObject PlayerModel;
    public GameObject Plane;
    public bool InPlane;
    public Stash playerStash;


    private void OnTriggerEnter(Collider other)
    {
        if (InPlane == false)
        {
            if (other.gameObject.name == "Player")
            {
                EnterText.transform.parent.gameObject.SetActive(true);
                Quaternion target = Quaternion.Euler(0, 50, 0);
            }
        }
        EnterText.GetComponentInParent<Button>().onClick.AddListener(GetInPlane);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player" && InPlane == false)
        {
            EnterText.transform.parent.gameObject.SetActive(false);
            Quaternion target = Quaternion.Euler(0, 50, 0);
        }
    }
    private void Update()
    {
        if (InPlane == true)
        {
            EnterText.gameObject.SetActive(false);
            ExitText.gameObject.SetActive(true);
        }
    }
    public void GetInPlane()
    {
        if (InPlane == false)
        {
            PlayerScript.transform.position = this.transform.position;
            PlayerScript.transform.rotation = this.transform.rotation;
            this.gameObject.SetActive(false);
            EnterText.gameObject.SetActive(false);
            ExitText.gameObject.SetActive(true);
            PlayerModel.gameObject.SetActive(false);
            Plane.gameObject.SetActive(true);
            PlayerScript.Speed = 60f;
            InPlane = true;
            playerStash.maxCollectableCount = 35;
            EnterText.GetComponentInParent<Button>().onClick.RemoveListener(GetInPlane);
            EnterText.GetComponentInParent<Button>().onClick.AddListener(GetOutOfPlane);
        }
    }

    public void GetOutOfPlane()
    {
        if (InPlane)
        {
            this.transform.position = PlayerScript.transform.position;
            this.transform.rotation = PlayerScript.transform.rotation;
            this.gameObject.SetActive(true);
            PlayerModel.gameObject.SetActive(true);
            Plane.gameObject.SetActive(false);
            PlayerScript.Speed = 20f;
            InPlane = false;
            ExitText.gameObject.SetActive(false);
            EnterText.gameObject.SetActive(true);
            playerStash.maxCollectableCount = 5;
            for (int i = 5; i < 10; i++)
            {
                playerStash.RemoveStash();
            }
            EnterText.GetComponentInParent<Button>().onClick.RemoveListener(GetOutOfPlane);
            EnterText.GetComponentInParent<Button>().onClick.AddListener(GetInPlane);
        }
    }
}
