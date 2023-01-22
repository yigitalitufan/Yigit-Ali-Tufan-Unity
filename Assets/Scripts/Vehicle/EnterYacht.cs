using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.AccessControl;

public class EnterYacht : MonoBehaviour
{
    public GameObject EnterText;
    public GameObject ExitText;
    public Movement PlayerScript;
    public GameObject PlayerModel;
    public GameObject Yacht;
    public bool InYacht;
    public Stash playerStash;
    private Vector3 _playerPos,_yachtPos;

        private void Start()
        {
        _yachtPos = this.transform.position;
        //EnterText.GetComponentInParent<Button>().onClick.AddListener(GetInYacht);
        }
    private void OnTriggerEnter(Collider other)
    {
        if (InYacht == false)
        {
            if (other.gameObject.name == "Player")
            {
                EnterText.transform.parent.gameObject.SetActive(true);
                EnterText.GetComponentInParent<Button>().onClick.AddListener(GetInYacht);
                Quaternion target = Quaternion.Euler(0, 50, 0);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player" && InYacht == false)
        {
            EnterText.transform.parent.gameObject.SetActive(false);
            Quaternion target = Quaternion.Euler(0, 50, 0);
        }
    }
    private void Update()
    {
        if (InYacht == true)
        {
            EnterText.gameObject.SetActive(false);
            ExitText.gameObject.SetActive(true);
        }
    }
    public void GetInYacht()
    {
        _playerPos = PlayerModel.transform.position;

        if (InYacht == false)
        {
            PlayerScript.transform.position = this.transform.position;
            PlayerScript.transform.rotation = this.transform.rotation;
            this.gameObject.SetActive(false);
            EnterText.gameObject.SetActive(false);
            ExitText.gameObject.SetActive(true);
            PlayerModel.gameObject.SetActive(false);
            Yacht.gameObject.SetActive(true);
            PlayerScript.Speed = 60f;
            InYacht = true;
            playerStash.maxCollectableCount = 30;
            EnterText.GetComponentInParent<Button>().onClick.RemoveListener(GetInYacht);
            EnterText.GetComponentInParent<Button>().onClick.AddListener(GetOutOfYacht);
        }
    }

    public void GetOutOfYacht()
    {
        if (InYacht)
        {
            this.transform.position = PlayerScript.transform.position;
            this.transform.rotation = PlayerScript.transform.rotation;
            this.gameObject.SetActive(true);
            PlayerModel.gameObject.SetActive(true);
            Yacht.gameObject.SetActive(false);
            PlayerScript.Speed = 20f;
            InYacht = false;
            ExitText.gameObject.SetActive(false);
            EnterText.gameObject.SetActive(true);
            playerStash.maxCollectableCount = 5;
            for (int i = 5; i < 10; i++)
            {
                playerStash.RemoveStash();
            }
            EnterText.GetComponentInParent<Button>().onClick.RemoveListener(GetOutOfYacht);
            EnterText.GetComponentInParent<Button>().onClick.AddListener(GetInYacht);
            PlayerModel.transform.parent.transform.position = _playerPos;
            this.transform.position = _yachtPos;
        }
    }
}
