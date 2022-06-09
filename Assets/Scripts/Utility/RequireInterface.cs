using System;
using UnityEngine;

namespace Utility
{
    public class RequireInterface : PropertyAttribute
    {
        public readonly Type[] RequireTypes;

        public RequireInterface(params Type[] requireTypes)
        {
            RequireTypes = requireTypes;
        }
    }
}