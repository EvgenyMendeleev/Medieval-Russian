using System.Collections;
using TourverseToolkit.Runtime;
using UnityEngine;

public class TourEntryPoint : MonoBehaviour
{
    [SerializeField]
    private PlayerCamera _playerCamera;

    private IEnumerator Start()
    {
        TourController.TourStart(_playerCamera);
        yield return new WaitForSeconds(30.0f);
        TourController.CheckPoint(0);
    }
}
