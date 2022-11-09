using System;
using System.Collections.Generic;
using System.Reflection;
namespace ComponentContainer.Internal
{
    public sealed class TargetMethodInfo
    {
        public readonly MethodInfo MethodInfo;
        public readonly ParameterInfo[] ParameterInfos;

        public TargetMethodInfo(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
            ParameterInfos = methodInfo.GetParameters();
        }
    }
    
    public static class TargetSearcher
    {
        public static List<TargetMethodInfo> Search(Type type)
        {
            var targetMethods = new List<TargetMethodInfo>();
            foreach (var methodInfo in type.GetRuntimeMethods()) {
                if (methodInfo.IsDefined(typeof(InjectAttribute), true)) {
                    targetMethods.Add(new TargetMethodInfo(methodInfo));
                }
            }

            return targetMethods;
        }
    }
}
