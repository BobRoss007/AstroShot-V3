using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAudio : MonoBehaviour {
    [Header("Footstep audio parameters")]
    public Transform[] footBones;
    /// <summary>Reference to the MaterialAudioTypeList instance</summary>
    public MaterialAudioTypeList audioTypeList;
    /// <summary>The maximum length of the ray cast starting from the foot bone pointing down (as in its local coordinate space)</summary>
    public float footstepRayLength = 0.25f;
    public float footstepVolume = 1.0f;

    // Stores if there was a ray cast hit during the last iteration for each foot
    List<bool> lastFrameHit = new List<bool>();

    PlayerMovement movementScript;

    void Start() {
        // Filling 'lastFrameHit' with 'false' for all feet to start with
        for (int i = 0; i < footBones.Length; i++)
        {
            lastFrameHit.Add(false);
        }

        movementScript = GetComponent<PlayerMovement>();
    }

    void LateUpdate()
    {

        if(!movementScript.isRolling)
        {
            // Looping through foot bones
            for (int i = 0; i < footBones.Length; i++)
            {
                RaycastHit[] hits = Physics.RaycastAll(footBones[i].position, footBones[i].up /* "up" is really down */, footstepRayLength);
                float lastDistance = footstepRayLength * 2.0f; // Just to make sure the number starts out bigger than the distance of any possible hit
                RaycastHit obj = new RaycastHit(); // The initial value of this will never be used, I just had to give it a value here to avoid errors
                bool validHit = false;
                foreach (RaycastHit hit in hits) {
                    if (hit.collider.isTrigger) continue;
                    if (hit.distance < lastDistance) {
                        lastDistance = hit.distance;
                        obj = hit;
                        validHit = true;
                    }
                }
                bool lastFrameHitBackup = lastFrameHit[i];
                if (!validHit) {
                    lastFrameHit[i] = false;
                    continue;
                }
                lastFrameHit[i] = true;
                if (lastFrameHitBackup) continue;

                MaterialAudioTypeList.MaterialSoundEntry sampleList = null;
                MaterialAudioTypeList.MaterialSoundEntry defaultFallback = null;

                foreach (MaterialAudioTypeList.MaterialSoundEntry _sampleList in audioTypeList.MaterialAudioTypes) {
                    if (_sampleList.physicsMaterial == null) {
                        defaultFallback = _sampleList;
                    }
                    if (_sampleList.physicsMaterial == obj.collider.sharedMaterial) {
                        sampleList = _sampleList;
                    }
                }

                if (defaultFallback == null) {
                    Debug.LogError("Cannot find default surface type in the material audio type list");
                    continue;
                }

                // If the current surface type doesn't have an entry in the list, use the default surface sounds
                if (sampleList == null) sampleList = defaultFallback;

                AudioClip sampleToPlay;

                PlayerMovement pMovement = gameObject.GetComponent<PlayerMovement>();

                if (!pMovement.isRunning || pMovement.isCrouching) {
                    if (sampleList.walkSamples.Length < 1) {
                        // In case there are no sound samples available, fall back to the default surface sounds
                        sampleList = defaultFallback;

                        // In case even that is empty...
                        if (sampleList.walkSamples.Length < 1) {
                            Debug.LogError("No walking sound samples are available for the default surface type");
                            continue;
                        }
                    }
                    sampleToPlay = sampleList.walkSamples[Random.Range(0, sampleList.walkSamples.Length)];
                } else {
                    if (sampleList.runSamples.Length < 1) {
                        sampleList = defaultFallback;

                        if (sampleList.runSamples.Length < 1) {
                            Debug.LogError("No running sound samples are available for the default surface type");
                            continue;
                        }
                    }
                    sampleToPlay = sampleList.runSamples[Random.Range(0, sampleList.runSamples.Length)];
                }

                AudioSource.PlayClipAtPoint(sampleToPlay, obj.point, footstepVolume);
            }
        }


    }
}