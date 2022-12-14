using System.Collections.Generic;
using ComponentContainer.Container;

namespace ComponentContainer.Internal
{
    public static class Injector
    {
        public static void Inject(object instance, IContainer container, List<TargetMethodInfo> targets)
        {
            if (targets is not { Count: > 0 }) return;

            foreach (var methodInfo in targets) {
                var parameters = methodInfo.ParameterInfos;
                var values = new object[parameters.Length];
      
                for (int i = 0; i < parameters.Length; i++) {
                    var targetParameter = parameters[i];
                    values[i] = container.Resolve(targetParameter.ParameterType, 
                        targetParameter.IsDefined(typeof(NullableAttribute), true));
                }

                methodInfo.MethodInfo.Invoke(instance, values);
            }
        }
    }
}
