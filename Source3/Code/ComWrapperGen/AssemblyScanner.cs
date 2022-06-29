using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using ClassInfo = ComWrapperGen.Meta.ClassInfo;
using InterfaceInfo = ComWrapperGen.Meta.InterfaceInfo;
using MethodInfo = ComWrapperGen.Meta.MethodInfo;
using ParameterInfo = ComWrapperGen.Meta.ParameterInfo;
using TypeInfo = ComWrapperGen.Meta.TypeInfo;
using NameInfo = ComWrapperGen.Meta.NameInfo;
using System.Runtime.InteropServices;

namespace ComWrapperGen;

internal class AssemblyScanner
{
    private readonly List<InterfaceInfo> _interfaces = new();
    private readonly List<ClassInfo> _objects = new();

    public IEnumerable<InterfaceInfo> Interfaces => _interfaces;
    public IEnumerable<ClassInfo> Objects => _objects;

    public void Scan(string assemblyPath)
        => Scan(Assembly.Load(assemblyPath));

    public void Scan(Assembly assembly)
    {
        var objectsToScan = new List<Type>();

        foreach (var type in assembly.GetTypes())
        {
            if (!type.IsPublic)
                continue;

            if (type.IsInterface &&
                type.GetCustomAttribute<ComInterfaceAttribute>() is not null)
            {
                var intf = new InterfaceInfo
                {
                    IID = type.GUID,
                    Name = new NameInfo {
                        FullName = type.FullName,
                        Name = type.Name,
                        Namespace = type.Namespace
                    }
                };

                _interfaces.Add(intf);

                foreach (var method in type.GetMethods())
                {
                    var mthd = new MethodInfo
                    {
                        Name = method.Name,
                        ReturnType = new TypeInfo
                        {
                            Name = new NameInfo {
                                FullName = method.ReturnType.FullName,
                                Name = method.ReturnType.Name,
                                Namespace = method.ReturnType.Namespace
                            },
                            IsInterface = method.ReturnType.IsInterface,
                            IsValueType = method.ReturnType.IsValueType,
                            IsBlittable = IsBlittable(method.ReturnType)
                        },
                        PreserveSignature = PreserveSignature(method)
                    };

                    intf.Methods.Add(mthd);

                    foreach (var param in method.GetParameters())
                    {
                        var parameter = new ParameterInfo
                        {
                            Name = param.Name,
                            TypeInfo = new TypeInfo
                            {
                                Name = new NameInfo {
                                    FullName = param.ParameterType.FullName,
                                    Name = param.ParameterType.Name,
                                    Namespace = param.ParameterType.Namespace
                                },
                                IsInterface = param.ParameterType.IsInterface,
                                IsValueType = param.ParameterType.IsValueType,
                                IsBlittable = IsBlittable(param.ParameterType)
                            }
                        };

                        mthd.Parameters.Add(parameter);
                    }
                }
            }

            if (type.IsClass)
            {
                objectsToScan.Add(type);
            }
        }

        foreach (var obj in objectsToScan)
        {
            var objIntfs = obj.GetInterfaces();
            var objectInterfaces = new List<InterfaceInfo>();

            foreach (var intf in objIntfs)
            {
                var intfInfo = FindInterface(intf.FullName);
                if (intfInfo is not null)
                    objectInterfaces.Add(intfInfo);
            }
            
            if (objectInterfaces.Count > 0)
            {
                _objects.Add(new ClassInfo
                {
                    Name = new NameInfo {
                        FullName = obj.FullName,
                        Name = obj.Name,
                        Namespace = obj.Namespace
                    },
                    Interfaces = objectInterfaces
                });
            }
        }
    }

    private bool PreserveSignature(System.Reflection.MethodInfo method)
        => method.GetCustomAttribute<PreserveSigAttribute>() != null;

    private InterfaceInfo FindInterface(string fullName)
        => _interfaces.FirstOrDefault(i => i.Name.FullName == fullName);

    private static bool IsBlittable(Type type)
    {
        return type.IsValueType;
    }
}

