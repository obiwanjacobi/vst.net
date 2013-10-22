using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jacobi.Vst3.Interop;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Jacobi.Vst3.TestPlugin
{
    [ClassInterface(ClassInterfaceType.None)]
    public class PluginClassFactory : IPluginFactory, IPluginFactory2, IPluginFactory3
    {
        private List<ClassRegistration> _registrations = new List<ClassRegistration>();
        

        public PluginClassFactory(string vendor, string email, string url, PFactoryInfo.FactoryFlags flags)
            : this(vendor, email, url, flags, new Version(3, 5, 2, null))
        { }

        public PluginClassFactory(string vendor, string email, string url, PFactoryInfo.FactoryFlags flags, Version sdkVersion)
        {
            Vendor = vendor;
            Email = email;
            Url = url;
            Flags = flags | PFactoryInfo.FactoryFlags.Unicode;
            SdkVersion = sdkVersion;
        }

        public string Vendor { get; private set; }

        public string Url { get; private set; }

        public string Email { get; private set; }

        public PFactoryInfo.FactoryFlags Flags { get; private set; }

        public Version SdkVersion { get; private set; }

        protected ServiceContainer ServiceContainer { get; private set; }

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
            if (GetGuidFromType(registration.ClassType) == null)
                throw new ArgumentException("The specified type does not have a GuidAttribute set.", "ClassType");
            if (GetNameFromType(registration.ClassType) == null)
                throw new ArgumentException("The specified type does not have a DisplayNameAttribute set.", "ClassType");

            _registrations.Add(registration);
        }

        public bool Unregister(Type classType)
        {
            var guid = GetGuidFromType(classType);

            if (guid != null && _registrations.Count > 0)
            {
                foreach (var reg in _registrations)
                {
                    if (reg.ClassType.FullName == classType.FullName)
                    {
                        _registrations.Remove(reg);
                        return true;
                    }
                }
            }

            return false;
        }

        public ClassRegistration Find(Guid classId)
        {
            var guid = classId.ToString();

            return (from reg in _registrations
                    where GetGuidFromType(reg.ClassType) == guid
                    select reg).FirstOrDefault();
        }

        protected virtual object CreateInstance(ClassRegistration registration)
        {
            if (registration.CreatorCallback != null)
            {
                return registration.CreatorCallback(ServiceContainer, registration.ClassType);
            }

            return Activator.CreateInstance(registration.ClassType);
        }

        private static string GetGuidFromType(Type classType)
        {
            var attrs = classType.GetCustomAttributes(typeof(GuidAttribute), true);

            if (attrs != null && attrs.Length > 0)
            {
                var guidAttr = (GuidAttribute)attrs[0];
                return guidAttr.Value;
            }

            return null;
        }

        private static string GetNameFromType(Type classType)
        {
            var attrs = classType.GetCustomAttributes(typeof(DisplayNameAttribute), true);

            if (attrs != null && attrs.Length > 0)
            {
                var guidAttr = (DisplayNameAttribute)attrs[0];
                return guidAttr.DisplayName;
            }

            return null;
        }

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
            //Guard.ThrowIfNotInRange(index, "index", 0, _registrations.Count);

            var reg = _registrations[index];
            info.Cardinality = Constants.ClassCardinalityManyInstances;
            info.Category = reg.Category;
            info.ClassId = new Guid(GetGuidFromType(reg.ClassType));
            info.Name = GetNameFromType(reg.ClassType);

            return TResult.S_OK;
        }

        public int CreateInstance(ref Guid classId, ref Guid interfaceId, ref IntPtr instance)
        {
            if (instance != IntPtr.Zero)
            {
                return TResult.E_Pointer;
            }

            var reg = Find(classId);

            if (reg != null)
            {
                object obj = CreateInstance(reg);
                IntPtr unk = Marshal.GetIUnknownForObject(obj);

                try
                {
                    return TResult.FromInt32(Marshal.QueryInterface(unk, ref interfaceId, out instance));
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
            //Guard.ThrowIfNotInRange(index, "index", 0, _registrations.Count);

            var reg = _registrations[index];
            info.Cardinality = Constants.ClassCardinalityManyInstances;
            info.Category = reg.Category;
            info.ClassFlags = reg.ClassFlags;
            info.ClassId = new Guid(GetGuidFromType(reg.ClassType));
            info.Name = GetNameFromType(reg.ClassType);
            info.SdkVersion = SdkVersion.ToString();
            info.SubCategories = string.Join("|", reg.SubCategories.ToArray());
            info.Vendor = reg.Vendor ?? Vendor;
            info.Version = reg.Version.ToString();

            return TResult.S_OK;
        }

        #endregion

        #region IPluginFactory3 Members

        public int GetClassInfoUnicode(int index, ref PClassInfoW info)
        {
            //Guard.ThrowIfNotInRange(index, "index", 0, _registrations.Count);

            var reg = _registrations[index];
            info.Cardinality = Constants.ClassCardinalityManyInstances;
            info.Category.Value = reg.Category;
            info.ClassFlags = reg.ClassFlags;
            info.ClassId = new Guid(GetGuidFromType(reg.ClassType));
            info.Name = GetNameFromType(reg.ClassType);
            info.SdkVersion = SdkVersion.ToString();
            info.SubCategories.Value = string.Join("|", reg.SubCategories.ToArray());
            info.Vendor = reg.Vendor ?? Vendor;
            info.Version = reg.Version.ToString();

            return TResult.S_OK;
        }

        public int SetHostContext(object context)
        {
            if (ServiceContainer == null)
            {
                ServiceContainer = new ServiceContainer(context);
                return TResult.S_OK;
            }

            return TResult.E_Fail;
        }

        #endregion
    }
}
