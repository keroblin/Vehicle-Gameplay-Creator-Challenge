using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipLife : MonoBehaviour
{
    public CarController carController;
    public float destructionSpeed = 100f;
    public float life = 1f;
    public ParticleSystem explosion;
    public MeshFilter mesh;
    public TextMeshProUGUI lifeText;
    public GameObject deathScreen;
    public Button restart;

    Vector3 origPos;
    Quaternion origRot;
    Mesh oldMesh;
    void Start()
    {
        explosion.gameObject.transform.SetParent(gameObject.transform, false);
        oldMesh = mesh.mesh;
        origPos = transform.position;
        origRot = transform.rotation;

        restart.onClick.AddListener(Restart);
        lifeText.text = "Full Health";
    }

    void Restart()
    {
        carController.inputEnabled = true;
        deathScreen.SetActive(false);
        carController.rb.velocity = Vector3.zero;
        lifeText.text = "Full Health";
        transform.position = origPos;
        transform.rotation = origRot;
        mesh.mesh = oldMesh;
        life = 1f;
    }

    void Die()
    {
        carController.inputEnabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        deathScreen.SetActive(true);
        Debug.Log("Died!");
        explosion.Play();
        explosion.transform.SetParent(null, true);
        mesh.mesh = null;
    }

    void Damage()
    {
        if(life - 0.25 <= 0f)
        {
            Die();
            life = 0f;
        }
        else
        {
            life -= 0.25f;
        }
        lifeText.text = "Life:" + life;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Meteor"))
        {
            if(carController.rb.velocity.magnitude > destructionSpeed)
            {
                Die();
            }
            else
            {
                Damage();
            }
        }
    }
}
