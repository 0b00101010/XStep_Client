using UnityEngine;

namespace Slash.Unity.DataBind.Examples.ScriptableObjectDataProvider
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TestScriptableObject", order = 1)]
    public class TestScriptableObject : ScriptableObject
    {
        public string StringData;
    }
}
