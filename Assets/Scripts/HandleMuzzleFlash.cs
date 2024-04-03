using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMuzzleFlash : MonoBehaviour
{

    [SerializeField] private Light _muzzleFlash = null;
    [SerializeField][Range(0, 1)] private float _flashDuration = 0.5f;

    IEnumerator MuzzleFlash(Light flash, Transform location) {
        Light temp = flash;
        Instantiate(flash, location);
        yield return new WaitForSeconds(_flashDuration);
        Destroy(temp);
    }
}
