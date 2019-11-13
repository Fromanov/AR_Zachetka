using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

namespace Michsky.UI.Hexart
{
	public class UIElementSound : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
	{
		[Header("RESOURCES")]
		public AudioClip hoverSound;
		public AudioClip clickSound;
		public AudioClip notificationSound;



		private AudioMixer mixer;
		private string MasterGroup = "Master";
		private string SoundFXGroup = "SoundFx";
		private string MusicGroup = "Music";

		[Header("SETTINGS")]
		public bool enableHoverSound = true;
		public bool enableClickSound = true;
		public bool isNotification = false;

		private AudioSource HoverSource { get { return GetComponent<AudioSource>(); } }
		private AudioSource ClickSource { get { return GetComponent<AudioSource>(); } }
		private AudioSource NotificationSource { get { return GetComponent<AudioSource>(); } }

		void Start()
		{
			mixer = Resources.Load<AudioMixer>("MasterMixer");
			if (mixer == null)
			{
				Debug.Log("Mixer is NULL");
			}
			gameObject.AddComponent<AudioSource>();
			HoverSource.clip = hoverSound;
			HoverSource.outputAudioMixerGroup = mixer.FindMatchingGroups(SoundFXGroup)[0];
			ClickSource.clip = clickSound;
			ClickSource.outputAudioMixerGroup = mixer.FindMatchingGroups(SoundFXGroup)[0];

			HoverSource.playOnAwake = false;
			ClickSource.playOnAwake = false;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (enableHoverSound == true)
			{
				HoverSource.PlayOneShot(hoverSound);
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (enableClickSound == true)
			{
				ClickSource.PlayOneShot(clickSound);
			}
		}

		public void Notification()
		{
			if (isNotification == true)
			{
				NotificationSource.PlayOneShot(notificationSound);
			}
		}
	}
}