using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour 
{

	#region Variable Declarations
	public static AudioManager Instance;

	// Serialized Fields
	[SerializeField] AudioSource audioSourceMusic;
	[SerializeField] AudioSource audioSourceSFX;

	[Header("Music")]
	[SerializeField] List<AudioClip> musicClips = new List<AudioClip>();

	[Header("Sounds")]
	[SerializeField] AudioClip buttonSound;
	[SerializeField] AudioClip buttonSound2;
	[SerializeField] AudioClip confirmSound;
	[SerializeField] AudioClip buildingDragSound;
	[SerializeField] AudioClip buildingDragSound2;

	// Private
	int currentTrack;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Awake()
    {
		if (Instance == null) Instance = this;
		else if (Instance != this) Destroy(gameObject);
    }

    private void Start () 
	{
		if (musicClips.Count > 0) StartCoroutine(PlayBackgroundMusic());
	}
	#endregion



	#region Public Functions
	public void PlaySound(AudioClip clip)
	{
		audioSourceSFX.PlayOneShot(clip);
	}

	public void PlayButtonClick()
    {
		audioSourceSFX.PlayOneShot(buttonSound);
    }

	public void PlayButtonClickVariant()
	{
		audioSourceSFX.PlayOneShot(buttonSound2);
	}

	public void PlayConfirmSound()
	{
		audioSourceSFX.PlayOneShot(confirmSound);
	}

	public void PlayBuildingDragSound()
    {
		audioSourceSFX.PlayOneShot(buildingDragSound);
    }

	public void PlayBuildingDragSoundVariant()
	{
		audioSourceSFX.PlayOneShot(buildingDragSound2);
	}
	#endregion



	#region Private Functions

	#endregion



	#region Coroutines
	IEnumerator PlayBackgroundMusic()
    {
		while (true)
		{
			currentTrack = Random.Range(0, musicClips.Count);
			audioSourceMusic.clip = musicClips[currentTrack];
			audioSourceMusic.Play();
			audioSourceMusic.DOFade(1f, 2f).SetEase(Ease.OutExpo);

			yield return new WaitForSecondsRealtime(musicClips[currentTrack].length - 2f);

			audioSourceMusic.DOFade(0f, 2f).SetEase(Ease.InExpo);

			yield return new WaitForSecondsRealtime(2f);
		}
    }
	#endregion
}