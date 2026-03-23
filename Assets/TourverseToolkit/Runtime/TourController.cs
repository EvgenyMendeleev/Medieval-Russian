using System;

namespace TourverseToolkit.Runtime
{
    public static class TourController
    {
        public static event Action OnTourStart;
        public static event Action<PlayerCamera> OnStreamStart;
        public static event Action<PlayerCamera> OnCameraDataSwitch;
        public static event Action<int> OnCheckPoint;

        private static Language _language;
        public static Language Language => _language;

        public static void TourStart(PlayerCamera playerCamera)
        {
            OnTourStart?.Invoke();
            OnStreamStart?.Invoke(playerCamera);
        }

        public static void CameraSwitchData(PlayerCamera playerCamera)
        {
            OnCameraDataSwitch?.Invoke(playerCamera);
        }

        public static void CheckPoint(int pointNumber)
        {
            OnCheckPoint?.Invoke(pointNumber);
        }

        public static void SetLanguage(Language language)
        {
            _language = language;
        }
    }
}