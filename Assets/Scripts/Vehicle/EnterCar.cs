using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterCar : MonoBehaviour
{
    public GameObject EnterText;
    public GameObject ExitText;
    public GameObject LeftCarDoor;
    public Movement PlayerScript;
    public GameObject PlayerModel;
    public GameObject Car;
    public GameObject CarTire;
    public GameObject CarTire2;
    public GameObject CarTire3;
    public GameObject CarTire4;
    public bool InCar;
    public Stash playerStash;


    private void OnTriggerEnter(Collider other)
    {
        if (InCar == false)
        {
            if (other.gameObject.name == "Player")
            {
                EnterText.transform.parent.gameObject.SetActive(true);
                Quaternion target = Quaternion.Euler(0, 50, 0);
                LeftCarDoor.transform.rotation = Quaternion.Slerp(transform.rotation, target, 1);
            }
        }
        EnterText.GetComponentInParent<Button>().onClick.AddListener(GetInCar);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player" && InCar == false)
        {
            EnterText.transform.parent.gameObject.SetActive(false);
            Quaternion target = Quaternion.Euler(0, 50, 0);
            LeftCarDoor.transform.rotation = Quaternion.Slerp(target, transform.rotation, 1);
        }
    }
    private void Update()
    {
        if (InCar == true)
        {
            EnterText.gameObject.SetActive(false);
            ExitText.gameObject.SetActive(true);
        }
    }
    public void GetInCar()
    {
        if (InCar == false)
        {
            PlayerScript.transform.position = this.transform.position;
            PlayerScript.transform.rotation = this.transform.rotation;
            this.gameObject.SetActive(false);
            EnterText.gameObject.SetActive(false);
            ExitText.gameObject.SetActive(true);
            PlayerModel.gameObject.SetActive(false);
            Car.gameObject.SetActive(true);
            PlayerScript.Speed = 60f;
            InCar = true;
            playerStash.maxCollectableCount = 10;
            EnterText.GetComponentInParent<Button>().onClick.RemoveListener(GetInCar);
            EnterText.GetComponentInParent<Button>().onClick.AddListener(GetOutOfCar);
        }
    }

    public void GetOutOfCar()
    {
        if (InCar)
        {
            this.transform.position = PlayerScript.transform.position;
            this.transform.rotation = PlayerScript.transform.rotation;
            this.gameObject.SetActive(true);
            PlayerModel.gameObject.SetActive(true);
            Car.gameObject.SetActive(false);
            PlayerScript.Speed = 20f;
            InCar = false;
            ExitText.gameObject.SetActive(false);
            EnterText.gameObject.SetActive(true);
            playerStash.maxCollectableCount = 5;
            for (int i = 5; i < 10; i++)
            {
                playerStash.RemoveStash();
            }
            EnterText.GetComponentInParent<Button>().onClick.RemoveListener(GetOutOfCar);
            EnterText.GetComponentInParent<Button>().onClick.AddListener(GetInCar);
        }
    }
}
