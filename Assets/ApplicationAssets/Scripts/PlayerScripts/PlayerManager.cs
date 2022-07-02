using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovements))]
public class PlayerManager : MonoBehaviour {


    public int playerLevel = 1;

    [SerializeField]
    private int score = 0;
    [SerializeField]
    private float animationTimes = 1;
    [SerializeField]
    CameraEffects cameraEffects;
    [SerializeField]
    GameObject playerScannerBody;
    [SerializeField]
    GameObject playerCamera;
    [SerializeField]
    List<int> levelsEdgaes;
    



    PlayerMovements playerMovements;
    int levelIncresStacker = 0;
    void Start()
    {
        playerMovements = GetComponent<PlayerMovements>();
        score = 0;
        levelIncresStacker = 0;
    }

	// Update is called once per frame
	void Update () {
        if (levelsEdgaes.Count > 0)
        {
            if (score >= levelsEdgaes[0])
            {
                    LevelUP();
            }
        }
	}
    void LevelUP()
    {
        playerLevel++;
        levelsEdgaes.Remove(levelsEdgaes[0]);
        StartCoroutine(AdjustCameraAndPlayerBody());
    }
    IEnumerator AdjustCameraAndPlayerBody()
    {
      
            Vector3 newPosition = playerCamera.transform.localPosition;
            newPosition.y += (5 * playerLevel);
            newPosition.z -= (5 * playerLevel);
            float newScale = playerScannerBody.transform.localScale.x + playerLevel;
            Vector3 newScaleVector = new Vector3(newScale, newScale, newScale);

            for (float t = 0.0f; t < animationTimes; t += Time.deltaTime / animationTimes)
            {
                playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, newPosition, t);
                playerScannerBody.transform.localScale = Vector3.Lerp(playerScannerBody.transform.localScale, newScaleVector, t);
                yield return null;
            }
            levelIncresStacker--;
       
    }

    public void AddScore(int value)
    {
        score += value;
    }

    public void Die()
    {
        if(cameraEffects!=null)
            cameraEffects.ApplyDieEffect();
        playerScannerBody.SetActive(false);
        Invoke("Awaken", 5);
    }
    void Awaken()
    {
        if (cameraEffects != null)
            cameraEffects.ApplyAwakeEffect();
        playerScannerBody.SetActive(true);
    }
}
