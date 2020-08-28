using System;
using System.Reflection;

namespace BackendTemplate.Utilities
{
    public enum InstantiateFlag
    {
        Instance,
        Singleton
    }

    public interface IIocContainer
    {
        TClass RegisterSingleton<TClass>(TClass singleton) where TClass : class;
        TClass RegisterSingleton<TClass>() where TClass : class;
        void RegisterSingleton<TInterface, TImplementation>(TImplementation singleton) where TInterface : class where TImplementation : TInterface;
        void RegisterSingleton<TInterface, TImplementation>() where TInterface : class where TImplementation : TInterface;


        void Register<TInterface, TImplementation>() where TInterface : class where TImplementation : TInterface;
        void Register<TInterface, TImplementation>(InstantiateFlag instantiateFlag) where TInterface : class where TImplementation : TInterface;
        void Register(Type typeInterface, Type typeImplementation);
        void Register(Type typeInterface, Type typeImplementation, InstantiateFlag instantiateFlag);


        void RegisterAll(Type typeInterface);
        void RegisterAll(Type typeInterface, Assembly assembly);
        void RegisterAll(Type typeInterface, InstantiateFlag instantiateFlag);
        void RegisterAll(Type typeInterface, Assembly assembly, InstantiateFlag instantiateFlag);


        TInterface GetInstance<TInterface>() where TInterface : class;
        TClass Instantiate<TClass>();
    }
}
