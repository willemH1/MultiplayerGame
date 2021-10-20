using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class HoldToPickup : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Probably just the main camera.. but referenced here to avoid Camera.main calls")]
	private Camera camera;
	[SerializeField]
	[Tooltip("The layers the items to pickup will be on")]
	private LayerMask layerMask;
	[SerializeField]
	[Tooltip("How long it takes to pick up an item.")]
	private float pickupTime = 2f;
	[SerializeField]
	[Tooltip("The root of the images (progress image should be a child of this too)")]
	private RectTransform pickupImageRoot;
	[SerializeField]
	[Tooltip("The ring around the button that fills")]
	private Image pickupProgressImage;
	[SerializeField]
	private AudioSource pickUpItemSound;
	[SerializeField]
	private ParticleSystem pickUpParticle;
	private bool isGiftPickedUp;
	private Item itemBeingPickedUp;
	private float currentPickupTimerElapsed;

	private void Update()
	{
		SelectItemBeingPickedUpFromRay();

		if (HasItemTargetted())
		{
			pickupImageRoot.gameObject.SetActive(true);

			if (Input.GetKey(KeyCode.E))
			{
				IncrementPickupProgressAndTryComplete();
			}
			else
			{
				currentPickupTimerElapsed = 0f;
			}

			UpdatePickupProgressImage();
		}
		else
		{
			pickupImageRoot.gameObject.SetActive(false);
			currentPickupTimerElapsed = 0f;
		}
	}

	private bool HasItemTargetted()
	{
		return itemBeingPickedUp != null;
	}

	private void IncrementPickupProgressAndTryComplete()
	{
		currentPickupTimerElapsed += Time.deltaTime;
		if (currentPickupTimerElapsed >= pickupTime)
		{
			if(itemBeingPickedUp.gameObject.layer == 10)
            {
				playGrampophone();
			}
			else if(itemBeingPickedUp.gameObject.layer == 11)
            {
				
				OpenMultiplayerLobbySystem();

			}
            else if(itemBeingPickedUp.gameObject.layer == 12 && isGiftPickedUp == false)
            {
				if(isGiftPickedUp == false)
                {
					pickUpGift();
                }
            }
		}
	}
	private void pickUpGift()
    {
		isGiftPickedUp = true;
		pickUpParticle.Play();
		pickUpItemSound.Play();
		itemBeingPickedUp.GetComponentInChildren<ParticleSystem>().Stop();
		itemBeingPickedUp.GetComponentInChildren<Light>().gameObject.SetActive(false);
		itemBeingPickedUp.GetComponentInChildren<AudioSource>().Stop();
        switch (itemBeingPickedUp.GetComponent<Item>().giftNumber)
        {
			case 1:
				PlayerPrefs.SetInt("Gift01", 1);
				Debug.Log("gift 1 picked up !!");
				break;
        }
	    
		currentPickupTimerElapsed = 0f;
		pickupProgressImage.fillAmount = 0f;
	}
	private void UpdatePickupProgressImage()
	{
		float pct = currentPickupTimerElapsed / pickupTime;
		pickupProgressImage.fillAmount = pct;
	}

	private void SelectItemBeingPickedUpFromRay()
	{
		Ray ray = camera.ViewportPointToRay(Vector3.one / 2f);
		Debug.DrawRay(ray.origin, ray.direction * 2f, Color.red);

		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo, 4f, layerMask))
		{
			
			var hitItem = hitInfo.collider.GetComponent<Item>();

			if (hitItem == null)
			{
				itemBeingPickedUp = null;
			}
			else if (hitItem != null && hitItem != itemBeingPickedUp)
			{
				
				itemBeingPickedUp = hitItem;

			}
		}
		else
		{
			itemBeingPickedUp = null;
		}
	}

	private void OpenMultiplayerLobbySystem()
	{
		SceneManager.LoadScene(1);
	}
	private void playGrampophone()
    {
		
		currentPickupTimerElapsed = 0f ;
		pickupProgressImage.fillAmount = 0f;
		FindObjectOfType<AudioManager>().playGramophone();
	}
}