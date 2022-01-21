#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Assets.TM.Script
{
    [CustomEditor(typeof(CityGenerator))]
    public class CityGeneratorEditor : OdinEditor
    {
        private bool _generated;

        public override void OnInspectorGUI()
        {
            //元のInspector部分を表示
            base.OnInspectorGUI();

            //targetを変換して対象を取得
            var generator = target as CityGenerator;

            var generateText = _generated
                ? "ReGenerate" 
                : "Generate";

            if (GUILayout.Button(generateText))
            {
                generator.Generate();
                _generated = true;
            }

            if (GUILayout.Button(generateText + " with new seed"))
            {
                generator.SetSeedFromTime();
                generator.Generate();
                _generated = true;
            }

            if (GUILayout.Button("Clear"))
            {
                generator.Clear();
                _generated = false;
            }
        }
    }
}
#endif