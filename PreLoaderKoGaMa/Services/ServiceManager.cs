
using PreLoaderKoGaMa.Helpers;
using PreLoaderKoGaMa.Services.Tools;

namespace PreLoaderKoGaMa.Services
{
    public class ServiceManager
    {
        Dictionary<Type, Type[]> Runners { get; set; }
        Dictionary<Type, object> Instances { get; set; }
        List<object> InstancesList { get; set; }
        Dictionary<Type, object> ToolList { get; set; }

        public ServiceManager()
        {
            Runners = new();
            Instances = new();
            InstancesList = new();
            ToolList = new()
            {
                [typeof(IServiceCurrent)] = new ServiceCurrent()
            };
        }
        void SetCurrent(object current)
        {
            var service = ToolList[typeof(IServiceCurrent)];
            ((ServiceCurrent)service).SetService(current);
        }
        public void Register(Type serviceType)
        {
            if (Runners.ContainsKey(serviceType))
                return;

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
        object? GetValueFromType(Type x)
        {
            if (ToolList.TryGetValue(x, out var value))
            {
                return value;
            }
            if (Instances.TryGetValue(x, out var value2))
            {
                return value2;
            }
            return null;
        }
        async Task OnInit()
        {
            foreach (var instance in InstancesList)
            {
                var type = instance.GetType();
                var method = type.GetMethod("Init");
                var method2 = type.GetMethod("InitAsync");

                var args = Runners[type].Select(GetValueFromType).ToArray();
                if (method != null)
                {
                    SetCurrent(instance);
                    method.Invoke(instance, args);
                }
                else if (method2 != null)
                {
                    SetCurrent(instance);
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
                    SetCurrent(instance);
                    method.Invoke(instance, null);
                }
                else if (method2 != null)
                {
                    SetCurrent(instance);
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

                if (method != null)
                {
                    SetCurrent(instance);
                    method.Invoke(instance, null);
                }
                else if (method2 != null)
                {
                    SetCurrent(instance);
                    await (Task)method2.Invoke(instance, null);
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

        public void Unregister(Type serviceType)
        {
            if (Runners.ContainsKey(serviceType))
            {
                Instances.Remove(serviceType);
                var instance = InstancesList.FirstOrDefault(i => i.GetType() == serviceType);
                if (instance != null)
                {
                    InstancesList.Remove(instance);
                }
                Runners.Remove(serviceType);
            }
        }

        public void Unregister<T>() where T : new()
        {
            Unregister(typeof(T));
        }

        public T? GetService<T>() where T : class
        {
            Instances.TryGetValue(typeof(T), out var service);
            return service as T;
        }

        public void AddTool<T>(T tool) where T : class
        {
            ToolList[typeof(T)] = tool;
        }

        public T? GetTool<T>() where T : class
        {
            ToolList.TryGetValue(typeof(T), out var tool);
            return tool as T;
        }
    }
}
