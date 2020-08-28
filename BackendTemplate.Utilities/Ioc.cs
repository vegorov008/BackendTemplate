using System;
using System.Reflection;

namespace BackendTemplate.Utilities
{
    public static class Ioc
    {
        static IIocContainer _instance;
        public static IIocContainer Instance
        {
            get
            {
                //if (_instance == null)
                //    _instance = new IocContainer();
                return _instance;
            }
        }

        public static TClass RegisterSingleton<TClass>(TClass singleton)
            where TClass : class
        {
            return Instance.RegisterSingleton<TClass>(singleton);
        }

        public static TClass RegisterSingleton<TClass>()
            where TClass : class
        {
            return Instance.RegisterSingleton<TClass>();
        }

        public static void RegisterSingleton<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : TInterface
        {
            Instance.RegisterSingleton<TInterface, TImplementation>();
        }

        public static void RegisterSingleton<TInterface, TImplementation>(TImplementation singleton) where TInterface : class where TImplementation : TInterface
        {
            Instance.RegisterSingleton<TInterface, TImplementation>(singleton);
        }

        public static void Register<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : TInterface
        {
            Instance.Register<TInterface, TImplementation>();
        }

        public static void Register<TInterface, TImplementation>(InstantiateFlag instantiateFlag)
            where TInterface : class
            where TImplementation : TInterface
        {
            Instance.Register<TInterface, TImplementation>(instantiateFlag);
        }

        public static void Register(Type typeInterface, Type typeImplementation)
        {
            Instance.Register(typeInterface, typeImplementation);
        }

        public static void Register(Type typeInterface, Type typeImplementation, InstantiateFlag instantiateFlag)
        {
            Instance.Register(typeInterface, typeImplementation, instantiateFlag);
        }



        public static void RegisterAll(Type typeInterface)
        {
            Instance.RegisterAll(typeInterface, Assembly.GetCallingAssembly());
        }

        public static void RegisterAll(Type typeInterface, Assembly assembly)
        {
            Instance.RegisterAll(typeInterface, assembly);
        }

        public static void RegisterAll(Type typeInterface, InstantiateFlag instantiateFlag)
        {
            Instance.RegisterAll(typeInterface, Assembly.GetCallingAssembly(), instantiateFlag);
        }

        public static void RegisterAll(Type typeInterface, Assembly assembly, InstantiateFlag instantiateFlag)
        {
            Instance.RegisterAll(typeInterface, assembly, instantiateFlag);
        }



        public static TInterface GetInstance<TInterface>() where TInterface : class
        {
            return Instance.GetInstance<TInterface>();
        }

        public static TClass Instantiate<TClass>()
        {
            return Instance.Instantiate<TClass>();
        }
    }
}
