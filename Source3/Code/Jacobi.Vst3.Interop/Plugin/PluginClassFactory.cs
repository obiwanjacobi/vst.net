using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace Jacobi.Vst3.Interop.Plugin
{
    [ClassInterface(ClassInterfaceType.None)]
    public class PluginClassFactory : IPluginFactory, IPluginFactory2, IPluginFactory3, IServiceContainerSite
    {
        private List<ClassRegistration> _registrations = new List<ClassRegistration>();

        public const string AudioModuleClassCategory = "Audio Module Class";
        public const string ComponentControllerClassCategory = "Component Controller Class";

        public PluginClassFactory(string vendor, string email, string url)
            : this(vendor, email, url, PFactoryInfo.FactoryFlags.NoFlags)
        { }


        public PluginClassFactory(string vendor, string email, string url, PFactoryInfo.FactoryFlags flags)
            : this(vendor, email, url, flags, new Version(3, 5, 2))
        { }

        public PluginClassFactory(string vendor, string email, string url, PFactoryInfo.FactoryFlags flags, Version sdkVersion)
        {
            Vendor = vendor;
            Email = email;
            Url = url;
            Flags = flags | PFactoryInfo.FactoryFlags.Unicode;
            SdkVersion = sdkVersion;

            DefaultVersion = ReflectionExtensions.GetExportAssembly().GetAssemblyVersion();

            ServiceContainer = new ServiceContainer();
        }

        public string Vendor { get; private set; }

        public string Url { get; private set; }

        public string Email { get; private set; }

        public PFactoryInfo.FactoryFlags Flags { get; private set; }

        public Version SdkVersion { get; private set; }

        public ServiceContainer ServiceContainer { get; protected set; }

        public void Register(Type classType, string category)
        {
            Register(new ClassRegistration
                {
                     ClassType = classType,
                     Category = category,
                });
        }

        public void Register(ClassRegistration registration)
        {
            if (registration.ClassType.GetClassGuid() == null)
                throw new ArgumentException("The specified type does not have a GuidAttribute set.", "ClassType");
            if (registration.ClassType.GetDisplayName() == null)
                throw new ArgumentException("The specified type does not have a DisplayNameAttribute set.", "ClassType");

            EnrichRegistration(registration);

            _registrations.Add(registration);
        }

        public bool Unregister(Type classType)
        {
            foreach (var reg in _registrations)
            {
                if (reg.ClassType.FullName == classType.FullName)
                {
                    _registrations.Remove(reg);
                    return true;
                }
            }

            return false;
        }

        public ClassRegistration Find(Guid classId)
        {
            var guid = classId.ToString().ToUpperInvariant();

            return (from reg in _registrations
                    where reg.ClassType.GetClassGuid() == guid
                    select reg).FirstOrDefault();
        }

        protected virtual object CreateInstance(ClassRegistration registration)
        {
            object instance = null;

            if (registration.CreatorCallback != null)
            {
                instance = registration.CreatorCallback(ServiceContainer, registration.ClassType);
            }
            else
            {
                instance = Activator.CreateInstance(registration.ClassType);
            }

            // link-up service container hierarchy
            var site = instance as IServiceContainerSite;

            if (site != null && site.ServiceContainer != null)
            {
                site.ServiceContainer.ParentContainer = this.ServiceContainer;
            }

            return instance;
        }

        protected virtual void EnrichRegistration(ClassRegistration registration)
        {
            if (String.IsNullOrEmpty(registration.Vendor))
            {
                registration.Vendor = this.Vendor;
            }

            if (registration.Version == null)
            {
                registration.Version = DefaultVersion;
            }
        }

        public Version DefaultVersion { get; private set; }

        #region IPluginFactory Members

        public int GetFactoryInfo(ref PFactoryInfo info)
        {
            info.Email = Email;
            info.Flags = Flags;
            info.Url = Url;
            info.Vendor = Vendor;

            return TResult.S_OK;
        }

        public int CountClasses()
        {
            return _registrations.Count;
        }

        public int GetClassInfo(int index, ref PClassInfo info)
        {
            if (index < 0 || index > _registrations.Count)
            {
                return TResult.E_InvalidArg;
            }

            var reg = _registrations[index];
            info.Cardinality = Constants.ClassCardinalityManyInstances;
            info.Category = reg.Category;
            info.ClassId = new Guid(reg.ClassType.GetClassGuid());
            info.Name = reg.ClassType.GetDisplayName();

            return TResult.S_OK;
        }

        public int CreateInstance(ref Guid classId, ref Guid interfaceId, ref IntPtr instance)
        {
            // seems not every host is programmed defensively...
            //if (instance != IntPtr.Zero)
            //{
            //    return TResult.E_Pointer;
            //}

            var reg = Find(classId);

            if (reg != null)
            {
                object obj = CreateInstance(reg);
                IntPtr unk = Marshal.GetIUnknownForObject(obj);

                try
                {
                    return Marshal.QueryInterface(unk, ref interfaceId, out instance);
                }
                finally
                {
                    Marshal.Release(unk);
                }
            }

            instance = IntPtr.Zero;
            return TResult.E_ClassNotReg;
        }

        #endregion

        #region IPluginFactory2 Members

        public int GetClassInfo2(int index, ref PClassInfo2 info)
        {
            if (index < 0 || index > _registrations.Count)
            {
                return TResult.E_InvalidArg;
            }

            var reg = _registrations[index];
            info.Cardinality = Constants.ClassCardinalityManyInstances;
            info.Category = reg.Category;
            info.ClassFlags = reg.ClassFlags;
            info.ClassId = new Guid(reg.ClassType.GetClassGuid());
            info.Name = reg.ClassType.GetDisplayName();
            info.SdkVersion = "VST " + SdkVersion.ToString();
            info.SubCategories = string.Join("|", reg.SubCategories.ToArray());
            info.Vendor = reg.Vendor ?? Vendor;
            info.Version = reg.Version.ToString();

            return TResult.S_OK;
        }

        #endregion

        #region IPluginFactory3 Members

        public int GetClassInfoUnicode(int index, ref PClassInfoW info)
        {
            if (index < 0 || index > _registrations.Count)
            {
                return TResult.E_InvalidArg;
            }

            var reg = _registrations[index];
            info.Cardinality = Constants.ClassCardinalityManyInstances;
            info.Category.Value = reg.Category;
            info.ClassFlags = reg.ClassFlags;
            info.ClassId = new Guid(reg.ClassType.GetClassGuid());
            info.Name = reg.ClassType.GetDisplayName();
            info.SdkVersion = "VST " + SdkVersion.ToString();
            info.SubCategories.Value = string.Join("|", reg.SubCategories.ToArray());
            info.Vendor = reg.Vendor ?? Vendor;
            info.Version = reg.Version.ToString();

            return TResult.S_OK;
        }

        public int SetHostContext(object context)
        {
            ServiceContainer.Unknown = context;
            return TResult.S_OK;
        }

        #endregion
    }
}
