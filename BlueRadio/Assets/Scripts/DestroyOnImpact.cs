using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DestroyOnImpact : NetworkBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(name + " Collided with " + other.name);
        /// TODO: add a check for other player
        if (other.tag == "Unit")
        {
            //other.gameObject.GetComponent<UnitModeBehaviour>().IsConnectedToMotherbase = false;
            other.gameObject.GetComponent<UnitModeBehaviour>().DisconnectAllHouses();
            StartCoroutine(DestroyNextFrame(other.gameObject));
        }
    }

    private IEnumerator DestroyNextFrame(GameObject go)
    {
        //go.SetActive(false);
        yield return new WaitForSeconds(1f);
        NetworkManager.Destroy(go);
    }
}
