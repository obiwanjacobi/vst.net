using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jacobi.Vst3.Interop
{
    internal static class Interfaces
    {
        // factory interfaces
        public const string IPluginFactory = "7A4D811C-5211-4A1F-AED9-D2EE0B43BF9F";
        public const string IPluginFactory2 = "0007B650-F24B-4C0B-A464-EDB9F00B2ABB";
        public const string IPluginFactory3 = "4555A2AB-C123-4E57-9B12-291036878931";

        // plugin interfaces
        public const string IPluginBase = "22888DDB-156E-45AE-8358-B34808190625";
        public const string IComponent = "E831FF31-F2D5-4301-928E-BBEE25697802";
        public const string IAudioProcessor = "42043F99-B7DA-453C-A569-E79D9AAEC33D";
        public const string IEditController = "DCD7BBE3-7742-448D-A874-AACC979C759E";
        public const string IEditController2 = "7F4EFE59-F320-4967-AC27-A3AEAFB63038";
        public const string IBStream = "C3BF6EA2-3099-4752-9B6B-F9901EE33E9B";
        public const string IParameterChanges = "A4779663-0BB6-4A56-B443-84A8466FEB9D";
        public const string IParamValueQueue = "01263A18-ED07-4F6F-98C9-D3564686F9BA";
        public const string IEventList = "3A2C4214-3463-49FE-B2C4-F397B9695A44";
        
        // messaging interfaces
        public const string IConnectionPoint = "70A4156F-6E6E-4026-9891-48BFAA60D8D1";
        public const string IMessage = "936F033B-C6C0-47DB-BB08-82F813C1E613";
        public const string IAttributeList = "1E5F0AEB-CC7F-4533-A254-401138AD5EE4";

        // UI interfaces
        public const string IPlugView = "5BC32507-D060-49EA-A615-1B522B755B29";
        public const string IPlugFrame = "367FAF01-AFA9-4693-8D4D-A2A0ED0882A3";

        // host interfaces
        public const string IHostApplication = "58E595CC-DB2D-4969-8B6A-AF8C36A664E5";
        public const string IComponentHandler = "93A0BEA3-0BD0-45DB-8E89-0B0CC1E46AC6";
        public const string IComponentHandler2 = "F040B4B3-A360-45EC-ABCD-C045B4D5A2CC";
        public const string IComponentHandler3 = "69F11617-D26B-400D-A4B6-B9647B6EBBAB";

        // marker interfaces
        public const string IVst3ToVst2Wrapper = "29633AEC-1D1C-47E2-BB85-B97BD36EAC61";
        public const string IVst3ToAUWrapper = "A3B8C6C5-C095-4688-B091-6F0BB697AA44";

        // test interfaces
        public const string ITestW = "FE64FC19-9568-4F53-AAA7-8DC87228338E";
        public const string ITestResultW = "69796279-F651-418B-B24D-79B7D7C527F4";
        public const string ITestSuiteW = "5CA7106F-9878-4AA5-B4D3-0D712F5F1498";
        public const string ITestFactoryW = "AB483D3A-1526-4650-BF86-EEF69A327A93";
    }
}
