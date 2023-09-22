namespace MallContainer;
using System.Data;

public class Container
{
    private Dictionary<Type, ResolveInformation> _types;
    private Dictionary<Type, object> _globalStorage;
    private Dictionary<Type, object> _localStorage;

    public Container(Dictionary<Type, ResolveInformation> types) : this(types, new Dictionary<Type, object>())
    {

    }

    public Container(Dictionary<Type, ResolveInformation> types, Dictionary<Type, object> globalStorage)
    {
        _types = types;
        _globalStorage = globalStorage;
        _localStorage = new Dictionary<Type, object>();
    }

    public object GetService(Type type)
    {
        if (_types.ContainsKey(type) == false)
        {
            throw new ArgumentException();
        }

        var information = _types[type];

        if (information.LifeTime == LifeTime.Singleton && _globalStorage.TryGetValue(type, out var singletoneInstance))
        {
            return singletoneInstance;
        }

        if (information.LifeTime == LifeTime.PerScope && _localStorage.TryGetValue(type, out var perScopeInstance))
        {
            return perScopeInstance;
        }

        var resolveType = information.ResolveType;

        var constructors = resolveType.GetConstructors().OrderByDescending(x => x.GetParameters().Length);

        var args = new List<object>();

        foreach (var constructor in constructors)
        {
            bool ok = true;
            foreach (var argument in constructor.GetParameters())
            {
                if (_types.ContainsKey(argument.ParameterType) == false)
                {
                    ok = false;
                    break;
                }
            }

            if (ok == true)
            {
                foreach (var argument in constructor.GetParameters())
                {
                    args.Add(GetService(argument.ParameterType));
                }
                break;
            }
        }

        var instance = Activator.CreateInstance(resolveType, args.ToArray());

        if (information.LifeTime == LifeTime.Singleton)
        {
            _globalStorage[type] = instance;
        }

        if (information.LifeTime == LifeTime.PerScope)
        {
            _localStorage[type] = instance;
        }

        return instance;
    }

    public Container GetScope()
    {
        return new Container(_types, _globalStorage);
    }
}

public class ContainerBuilder
{
    private readonly Dictionary<Type, ResolveInformation> _types;

    public ContainerBuilder()
    {
        _types = new Dictionary<Type, ResolveInformation>();
    }

    public void AddType(Type type, LifeTime lifeTime)
    {
        _types[type] = new ResolveInformation(type, lifeTime);
    }

    public void AddType(Type requestedType, Type resolvedType, LifeTime lifeTime)
    {
        _types[requestedType] = new ResolveInformation(resolvedType, lifeTime);
    }


    public Container Build()
    {
        return new Container(_types);
    }
}

public static class ContainerBuilderExtension
{
    public static ContainerBuilder AddSingleton(this ContainerBuilder containerBuilder, Type type)
    {
        containerBuilder.AddSingleton(type, type);

        return containerBuilder;
    }

    public static ContainerBuilder AddSingleton(this ContainerBuilder containerBuilder, Type requestedType, Type resolvedType)
    {
        containerBuilder.AddType(requestedType, resolvedType, LifeTime.Singleton);

        return containerBuilder;
    }

    public static ContainerBuilder AddSingleton<T>(this ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(typeof(T));

        return containerBuilder;
    }

    public static ContainerBuilder AddSingleton<TRequested, TResolved>(this ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(typeof(TRequested), typeof(TResolved));

        return containerBuilder;
    }

    public static ContainerBuilder AddPerDependency(this ContainerBuilder containerBuilder, Type type)
    {
        containerBuilder.AddPerDependency(type, type);

        return containerBuilder;
    }

    public static ContainerBuilder AddPerDependency(this ContainerBuilder containerBuilder, Type requestedType, Type resolvedType)
    {
        containerBuilder.AddType(requestedType, resolvedType, LifeTime.PerDependency);

        return containerBuilder;
    }

    public static ContainerBuilder AddPerDependency<T>(this ContainerBuilder containerBuilder)
    {
        containerBuilder.AddPerDependency(typeof(T));

        return containerBuilder;
    }

    public static ContainerBuilder AddPerDependency<TRequested, TResolved>(this ContainerBuilder containerBuilder)
    {
        containerBuilder.AddPerDependency(typeof(TRequested), typeof(TResolved));

        return containerBuilder;
    }

    public static ContainerBuilder AddPerScope(this ContainerBuilder containerBuilder, Type type)
    {
        containerBuilder.AddPerScope(type, type);

        return containerBuilder;
    }

    public static ContainerBuilder AddPerScope(this ContainerBuilder containerBuilder, Type requestedType, Type resolvedType)
    {
        containerBuilder.AddType(requestedType, resolvedType, LifeTime.PerScope);

        return containerBuilder;
    }

    public static ContainerBuilder AddPerScope<T>(this ContainerBuilder containerBuilder)
    {
        containerBuilder.AddPerScope(typeof(T));

        return containerBuilder;
    }

    public static ContainerBuilder AddPerScope<TRequested, TResolved>(this ContainerBuilder containerBuilder)
    {
        containerBuilder.AddPerScope(typeof(TRequested), typeof(TResolved));

        return containerBuilder;
    }
}

public class ResolveInformation
{
    public ResolveInformation(Type resolveType, LifeTime lifeTime)
    {
        ResolveType = resolveType;
        LifeTime = lifeTime;
    }

    public Type ResolveType { get; }
    public LifeTime LifeTime { get; }
}

public enum LifeTime
{
    PerDependency,
    Singleton,
    PerScope
}