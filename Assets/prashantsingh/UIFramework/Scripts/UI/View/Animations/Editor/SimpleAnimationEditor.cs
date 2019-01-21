using UnityEditor;

namespace Prashant
{
    [CustomEditor(typeof(SimpleAnimation))]
    public class SimpleAnimationEditor : Editor
    {
        SimpleAnimation _target;
        public override void OnInspectorGUI()
        {
            _target = (SimpleAnimation)target;
            if (_target.GetComponent<BaseAnimation>() == null)
            {
                EditorGUILayout.HelpBox("Add base animation script", MessageType.Warning);
            }
            else
            {
                base.OnInspectorGUI();
            }
        }
    }
}