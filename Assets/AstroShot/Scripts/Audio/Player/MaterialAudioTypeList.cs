using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Material Audio Type List")]
public class MaterialAudioTypeList : ScriptableObject {
    [System.Serializable]
    public class MaterialSoundEntry {
        /// <summary>Name of the surface type</summary>
        public string name;
        /// <summary>PhysicMaterial associated with the surface</summary>
        public PhysicMaterial physicsMaterial;
        /// <summary>Audio clips for walking sounds on this surface</summary>
        public AudioClip[] walkSamples;
        /// <summary>Audio clips for running sounds on this surface</summary>
        public AudioClip[] runSamples;
    }

    public List<MaterialSoundEntry> MaterialAudioTypes;
}