using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace TourverseToolkit.Editor
{
    internal sealed class TourverseSettingsWindow : SettingsProvider
    {
        private SerializedObject tourverseSettings;
        private SerializedProperty toursList;
        private SerializedProperty generateHybrid;
        private SerializedProperty generateOnceObject;
        private SerializedProperty onceObjectName;

        public TourverseSettingsWindow(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path, scopes, keywords)
        {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            tourverseSettings?.Dispose();
        }

        private void InitGUI()
        {
            TourverseSettings settings = TourverseSettingsUtils.LoadOrCreateSettings();
            tourverseSettings = new(settings);
            toursList = tourverseSettings.FindProperty("toursList");
            generateHybrid = tourverseSettings.FindProperty("generateHybrid");
            generateOnceObject = tourverseSettings.FindProperty("generateOnceObject");
            onceObjectName = tourverseSettings.FindProperty("onceObjectName");
        }

        public override void OnGUI(string searchContext)
        {
            if (tourverseSettings == null || !tourverseSettings.targetObject)
            {
                InitGUI();
            }

            tourverseSettings.Update();

            EditorGUILayout.PropertyField(toursList);
            EditorGUILayout.PropertyField(generateHybrid);
            EditorGUILayout.PropertyField(generateOnceObject);

            if (GUILayout.Button("Generate"))
            {
                OnGenerateClicked();
            }

            if (generateOnceObject.boolValue)
            {
                EditorGUILayout.PropertyField(onceObjectName);

                if (GUILayout.Button("Generate Once"))
                {
                    GenerateByName(onceObjectName.stringValue);
                }
            }

            tourverseSettings.ApplyModifiedProperties();
            TourverseSettingsUtils.Save();
        }

        public override void OnDeactivate()
        {
            base.OnDeactivate();
            TourverseSettingsUtils.Save();
        }

        private void OnGenerateClicked()
        {
            CollectTourItems();

            for (int i = 0; i < _toursData.Length; i++)
            {
                var data = _toursData[i];
                GenerateData(data.tourName, data.tourAddressablesUuid, data.scene, data.dllPath);
            }
        }

        private void GenerateByName(string tourName)
        {
            if (string.IsNullOrWhiteSpace(tourName))
            {
                Debug.LogError("Tour name is empty");
                return;
            }

            if (_toursData == null || _toursData.Length == 0)
            {
                CollectTourItems();
            }

            for (int i = 0; i < _toursData.Length; i++)
            {
                var data = _toursData[i];

                if (!string.Equals(data.tourName, tourName, System.StringComparison.Ordinal))
                    continue;

                Debug.Log(data.tourName);
                GenerateData(data.tourName, data.tourAddressablesUuid, data.scene, data.dllPath);
                return;
            }

            Debug.LogError($"Tour with name '{tourName}' not found");
        }

        private void CollectTourItems()
        {
            int count = toursList.arraySize;

            _toursData = new (string tourName, string tourAddressablesUuid, SceneAsset scene, string dllPath)[count];

            BuildDllService.BuildDlls();

            for (int i = 0; i < count; i++)
            {
                var tour = toursList.GetArrayElementAtIndex(i);

                string tourName = tour.FindPropertyRelative("tourName").stringValue;
                SceneAsset scene = tour.FindPropertyRelative("tourScene").objectReferenceValue as SceneAsset;
                string dllPath = BuildDllService.GenerateDllForAndroid(tourName);
                string tourAddressablesUuid = tour.FindPropertyRelative("tourAddressablesUuid").stringValue;

                _toursData[i] = (tourName, tourAddressablesUuid, scene, dllPath);
            }
        }

        private void GenerateData(string tourName, string tourAddressablesUuid, SceneAsset scene, string dllPath)
        {
            BuildAddressablesService.GenerateOrUseAddressablesGroups(tourName);
            BuildAddressablesService.BuildGroups(tourName, tourAddressablesUuid, scene, dllPath);
        }

        private (string tourName, string tourAddressablesUuid, SceneAsset scene, string dllPath)[] _toursData;
    }
}