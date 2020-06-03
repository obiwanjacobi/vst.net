using Jacobi.Vst3.Common;
using Jacobi.Vst3.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Plugin
{
    [ClassInterface(ClassInterfaceType.None)]
    public class PluginClassFactory : IPluginFactory, IPluginFactory2, IPluginFactory3, IServiceContainerSite, IDisposable
    {
        private readonly List<ClassRegistration> _registrations = new List<ClassRegistration>();

        public const string AudioModuleClassCategory = "Audio Module Class";
        public const string ComponentControllerClassCategory = "Component Controller Class";
        public const string TestClassCategory = "Test Class";

        public PluginClassFactory(string vendor, string email, string url)
            : this(vendor, email, url, PFactoryInfo.FactoryFlags.NoFlags)
        { }

        public PluginClassFactory(string vendor, string email, string url, PFactoryInfo.FactoryFlags flags)
            : this(vendor, email, url, flags, new Version(3, 6, 0))
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

        public Version DefaultVersion { get; set; }

        public ClassRegistration Register(Type classType, ClassRegistration.ObjectClasses objClass)
        {
            var reg = new ClassRegistration
            {
                ClassType = classType,
                ObjectClass = objClass,
            };

            Register(reg);

            return reg;
        }

        public void Register(ClassRegistration registration)
        {
            if (registration.ClassTypeId == Guid.Empty && registration.ClassType.GUID == Guid.Empty)
                throw new ArgumentException("The ClassTypeId property is not set and the ClassType (class) does not have a GuidAttribute set.", "ClassType");
            if (String.IsNullOrEmpty(registration.DisplayName) && registration.ClassType.GetDisplayName() == null)
                throw new ArgumentException("The DisplayName property is not set and the ClassType (class) does not have a DisplayNameAttribute set.", "ClassType");

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
            return (from reg in _registrations
                    where reg.ClassTypeId == classId
                    select reg).FirstOrDefault();
        }

        protected virtual object CreateObjectInstance(ClassRegistration registration)
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
            // internals

            if (registration.ClassTypeId == Guid.Empty)
            {
                registration.ClassTypeId = registration.ClassType.GUID;
            }

            if (String.IsNullOrEmpty(registration.DisplayName))
            {
                registration.DisplayName = registration.ClassType.GetDisplayName();
            }

            if (String.IsNullOrEmpty(registration.Vendor))
            {
                registration.Vendor = this.Vendor;
            }

            if (registration.Version == null)
            {
                registration.Version = DefaultVersion;
            }
        }

        #region IPluginFactory Members

        public virtual int GetFactoryInfo(ref PFactoryInfo info)
        {
            info.Email = Email;
            info.Flags = Flags;
            info.Url = Url;
            info.Vendor = Vendor;

            return TResult.S_OK;
        }

        public virtual int CountClasses()
        {
            return _registrations.Count;
        }

        public virtual int GetClassInfo(int index, ref PClassInfo info)
        {
            if (!IsValidRegIndex(index))
            {
                return TResult.E_InvalidArg;
            }

            var reg = _registrations[index];

            FillClassInfo(ref info, reg);

            return TResult.S_OK;
        }

        protected virtual void FillClassInfo(ref PClassInfo info, ClassRegistration reg)
        {
            info.Cardinality = PClassInfo.ClassCardinalityManyInstances;
            info.Category = ObjectClassToCategory(reg.ObjectClass);
            info.ClassId = reg.ClassTypeId;
            info.Name = reg.DisplayName;
        }

        public virtual int CreateInstance(ref Guid classId, ref Guid interfaceId, ref IntPtr instance)
        {
            // seems not every host is programmed defensively...
            //if (instance != IntPtr.Zero)
            //{
            //    return TResult.E_Pointer;
            //}

            var reg = Find(classId);

            if (reg != null)
            {
                object obj = CreateObjectInstance(reg);
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

        public virtual int GetClassInfo2(int index, ref PClassInfo2 info)
        {
            if (!IsValidRegIndex(index))
            {
                return TResult.E_InvalidArg;
            }

            var reg = _registrations[index];

            FillClassInfo2(ref info, reg);

            return TResult.S_OK;
        }

        protected virtual void FillClassInfo2(ref PClassInfo2 info, ClassRegistration reg)
        {
            info.Cardinality = PClassInfo2.ClassCardinalityManyInstances;
            info.Category = ObjectClassToCategory(reg.ObjectClass);
            info.ClassFlags = reg.ClassFlags;
            info.ClassId = reg.ClassTypeId;
            info.Name = reg.DisplayName;
            info.SdkVersion = FormatSdkVersionString(SdkVersion);
            info.SubCategories = reg.Categories.ToString();
            info.Vendor = reg.Vendor;
            info.Version = reg.Version.ToString();
        }

        #endregion

        #region IPluginFactory3 Members

        public virtual int GetClassInfoUnicode(int index, ref PClassInfoW info)
        {
            if (!IsValidRegIndex(index))
            {
                return TResult.E_InvalidArg;
            }

            var reg = _registrations[index];

            FillClassInfoW(ref info, reg);

            return TResult.S_OK;
        }

        protected virtual void FillClassInfoW(ref PClassInfoW info, ClassRegistration reg)
        {
            info.Cardinality = PClassInfoW.ClassCardinalityManyInstances;
            info.Category.Value = ObjectClassToCategory(reg.ObjectClass);
            info.ClassFlags = reg.ClassFlags;
            info.ClassId = reg.ClassType.GUID;
            info.Name = reg.DisplayName;
            info.SdkVersion = FormatSdkVersionString(SdkVersion);
            info.SubCategories.Value = reg.Categories.ToString();
            info.Vendor = reg.Vendor;
            info.Version = reg.Version.ToString();
        }

        public virtual int SetHostContext(object context)
        {
            ServiceContainer.Unknown = context;
            return TResult.S_OK;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposeAll)
        {
            this._registrations.Clear();

            if (this.ServiceContainer != null)
            {
                this.ServiceContainer.Dispose();
            }
        }

        #endregion

        private bool IsValidRegIndex(int index)
        {
            return (index >= 0 && index < _registrations.Count);
        }

        private static string FormatSdkVersionString(Version sdkVersion)
        {
            return "VST " + sdkVersion.ToString();
        }

        private static string ObjectClassToCategory(ClassRegistration.ObjectClasses objClass)
        {
            switch (objClass)
            {
                case ClassRegistration.ObjectClasses.AudioModuleClass:
                    return AudioModuleClassCategory;

                case ClassRegistration.ObjectClasses.ComponentControllerClass:
                    return ComponentControllerClassCategory;

                case ClassRegistration.ObjectClasses.TestClass:
                    return TestClassCategory;

                default:
                    throw new InvalidEnumArgumentException("objClass", (int)objClass, typeof(ClassRegistration.ObjectClasses));
            }
        }
    }
}
