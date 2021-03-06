
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitModeBehaviour : MonoBehaviour
{
    public GameObject selectedIndicator;
    public SphereCollider RadioSphereCollider;

    //public PlayerMotherbase.TeamType teamType;

    public bool _isConnectedToMotherbase;
    public bool IsConnectedToMotherbase { get { return _isConnectedToMotherbase; }
      set { _isConnectedToMotherbase = value;
            ConnectedIndicator.SetActive(value);
        } }


    public List<HouseObject> activatedHouses;
    public GameObject ConnectedIndicator;

    public enum UnitMode
    {
        Van,
        Tower,
        Tank,
    };

    public UnitMode currentMode;
    public GameObject towerObj;
    public GameObject vanObj;

    public void Start()
    {
        currentMode = UnitMode.Van;
        IsConnectedToMotherbase = false;
        activatedHouses = new List<HouseObject>();
    }


    // Changes the unit mode from Tower to Van, or vice versa
    public void Toggle(bool isBeforeDeath = false)
    {
        if (currentMode == UnitMode.Tank)
        {
            return;
        }
		FindObjectOfType<AudioManager>().Play("antennae");
        if (currentMode == UnitMode.Van)
        {
            transform.rotation = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;
            RadioSphereCollider.enabled = false;

            currentMode = UnitMode.Tower;
            vanObj.GetComponent<Animator>().SetBool("isShrunk", true);
            towerObj.GetComponent<Animator>().SetBool("isShrunk", false);

            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<ClickToMove>().StopMovement();

            StartCoroutine(EnableTowerCr(0.75f));
        }
        if (currentMode == UnitMode.Tower && isBeforeDeath)
        {
            towerObj.GetComponent<Animator>().SetBool("isShrunk", true);
            StartCoroutine(EnableVanCr(0.5f));
        }
#if false
        else
        {
            currentMode = UnitMode.Van;
            vanObj.GetComponent<Animator>().SetBool("isShrunk", false);
            towerObj.GetComponent<Animator>().SetBool("isShrunk", true);

            StartCoroutine(EnableVanCr(0.5f));
        }
#endif
    }

    public void DisconnectAllHouses()
    {
        Debug.Log("### disconnectng all houses...");
        IsConnectedToMotherbase = false;
        Toggle(true);
        foreach (HouseObject house in activatedHouses)
        {
            house.DisconnectHouse();
        }
    }

    private IEnumerator EnableTowerCr(float delay)
    {
        yield return new WaitForSeconds(delay);
        RadioSphereCollider.enabled = true;
    }

    private IEnumerator EnableVanCr(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Collider>().enabled = true;
    }


}



