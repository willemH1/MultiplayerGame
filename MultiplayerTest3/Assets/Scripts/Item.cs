using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    
    public int giftNumber;
    [SerializeField]
    private AudioSource giftGlowingSound;
    [SerializeField]
    private ParticleSystem giftParticles;
    [SerializeField]
    private Light pointLightGift;
    [SerializeField]
    private AudioSource pickUpGiftSound;
    [SerializeField]
    private ParticleSystem pickUpGiftParticles;
    [SerializeField]
    private GameObject gift1;
    [SerializeField]
    private GameObject gift2;
    [SerializeField]
    private ParticleSystem gift1Particles;
    [SerializeField]
    private ParticleSystem gift2Particles;
    [SerializeField]
    private AudioSource gift1Audio;
    [SerializeField]
    private AudioSource gift2Audio;
    private string strGiftName;
    private void Awake()
    {
        PlayerPrefs.DeleteKey("Gift01");
        PlayerPrefs.DeleteKey("Gift02");

        /*  if (PlayerPrefs.GetString("Gift0" + giftNumber) == "Y")
          {
              Debug.Log("it has been picked up buddy");
              giftGlowingSound.Stop();
              giftParticles.Stop();
              pointLightGift.gameObject.SetActive(false);
          } */
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pickUpGift()
    {
        
        strGiftName = FindObjectOfType<HoldToPickup>().GetGiftNumber();
        switch (strGiftName)
        {
            case "GreenGift2":
                Debug.Log("the name of this gameobject is GreenGift2");
                giftNumber = 2;
                break;
            case "GreenGift1":
                Debug.Log("the name of this gameobject is GreenGift1");
                giftNumber = 1;
                break;
        }
        Debug.Log("Gift0" + giftNumber);
        switch (giftNumber)
        {
            case 1:
                gift1.GetComponentInChildren<Light>().gameObject.SetActive(false);
                gift1.GetComponentInChildren<ParticleSystem>().Stop();
                gift1.GetComponentInChildren<AudioSource>().Stop();
                gift1Particles.Play();
                gift1Audio.Play();
                break;
            case 2:
                gift2.GetComponentInChildren<Light>().gameObject.SetActive(false);
                gift2.GetComponentInChildren<ParticleSystem>().Stop();
                gift2.GetComponentInChildren<AudioSource>().Stop();
                gift2Particles.Play();
                gift2Audio.Play();
                break;
        }
       
    }
}
