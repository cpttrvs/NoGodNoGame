using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AillieoUtils.EasyBehaviorTree.Creator
{
    [Serializable]
    internal struct NodeParamConfigEntry
    {
        public string typeName;
        public string paramTypeName;
        public bool includeArrayType;
        public bool willGenerate;

        internal NodeParamConfigEntry(string type)
        {
            type = type.Trim(' ', '\n', '\r', '\t');
            typeName = type;
            paramTypeName = GetDefaultParamTypeName(type);
            includeArrayType = true;
            willGenerate = true;
        }

        internal static string GetDefaultParamTypeName(string typeName)
        {
            return typeName.Replace(".","_").Replace("[]","Array");
        }

        internal NodeParamConfigEntry MakeArrayTypeEntry()
        {
            NodeParamConfigEntry ret = new NodeParamConfigEntry(typeName + "[]");
            ret.paramTypeName = safeParamTypeName + "Array";
            return ret;
        }

        internal string safeParamTypeName
        {
            get
            {
                if (string.IsNullOrEmpty(paramTypeName))
                {
                    paramTypeName = GetDefaultParamTypeName(typeName);
                }
                return paramTypeName;
            }
        }
    }

    internal class NodeParamConfig : ScriptableSingleton<NodeParamConfig>
    {
        [SerializeField]
        internal DefaultAsset folder = null;

        [SerializeField]
        [HideInInspector]
        internal NodeParamConfigEntry[] extendedTypes = Array.Empty<NodeParamConfigEntry>();

        [SerializeField]
        [HideInInspector]
        internal NodeParamConfigEntry[] defaultTypes = Array.Empty<NodeParamConfigEntry>();
    }

}
