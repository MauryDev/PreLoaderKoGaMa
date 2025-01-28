
using PreLoaderKoGaMa.Helpers;

namespace PreLoaderKoGaMa.Services
{
    public class ServiceManager
    {
        Dictionary<Type, Type[]> Runners { get; set; }
        Dictionary<Type,object> Instances { get; set; }
        List<object> InstancesList { get; set; }
        public ServiceManager()
        {
            Runners = new();
            Instances = new();
            InstancesList = new();
        }
        public void Register(Type serviceType)
        {
            var InitInfo = serviceType.GetMethod("Init");
            var InitAsyncInfo = serviceType.GetMethod("InitAsync");
            if (InitInfo == null && InitAsyncInfo == null)
            {
                Runners.Add(serviceType, Array.Empty<Type>());
                return;
            }
            if (InitAsyncInfo != null)
            {
                var parameters = InitAsyncInfo.GetParameters();
                Runners.Add(serviceType, parameters.Select(x => x.ParameterType).ToArray());
            }
            else
            {
                var parameters = InitInfo.GetParameters();
                Runners.Add(serviceType, parameters.Select(x => x.ParameterType).ToArray());
            }
        }
        public void Register<T>() where T : new()
        {
            Register(typeof(T));

        }
        async Task OnInit()
        {
            foreach (var instance in InstancesList)
            {
                var type = instance.GetType();
                var method = type.GetMethod("Init");
                var method2 = type.GetMethod("InitAsync");

                var args = Runners[type].Select(x => Instances[x]).ToArray();
                if (method != null)
                {
                    method.Invoke(instance, args);
                }
                else if (method2 != null)
                {
                    await (Task)method2.Invoke(instance, args);
                }
            }
        }
        async Task Run()
        {
            foreach (var instance in InstancesList)
            {
                var type = instance.GetType();
                var method = type.GetMethod("Run");
                var method2 = type.GetMethod("RunAsync");

                if (method != null)
                {
                    method.Invoke(instance,null);
                } else if (method2 != null)
                {
                    await (Task)method2.Invoke(instance, null);
                }
            }
        }
        async Task OnDestroy()
        {
            foreach (var instance in InstancesList)
            {
                var type = instance.GetType();
                var method = type.GetMethod("Destroy");
                var method2 = type.GetMethod("DestroyAsync");

                var args = Runners[type].Select(x => Instances[x]).ToArray();
                if (method != null)
                {
                    method.Invoke(instance, args);
                }
                else if (method2 != null)
                {
                    await (Task)method2.Invoke(instance, args);
                }
            }
        }
        public async Task Build()
        {
            
            foreach (var runner in Runners)
            {
                var type = runner.Key;
                var instance = Activator.CreateInstance(type);

                Instances.Add(type, instance);
                InstancesList.Add(instance);
            }
            ConsoleHelper.Log("ServiceManager", "OK");
            await OnInit();
            await Run();
            await OnDestroy();
            Runners.Clear();
            Instances.Clear();
            InstancesList.Clear();
        }
    }
}
