namespace ComLightPlugin
{
    internal static class Interfaces
    {
        // base interfaces
        public const string IPluginBase = "22888DDB-156E-45AE-8358-B34808190625";
        public const string IPluginFactory = "7A4D811C-5211-4A1F-AED9-D2EE0B43BF9F";
        public const string IPluginFactory2 = "0007B650-F24B-4C0B-A464-EDB9F00B2ABB";
        public const string IPluginFactory3 = "4555A2AB-C123-4E57-9B12-291036878931";
        public const string IBStream = "C3BF6EA2-3099-4752-9B6B-F9901EE33E9B";
        public const string ISizeableStream = "04F9549E-E02F-4E6E-87E8-6A8747F4E17F";
        public const string IErrorContext = "12BCD07B-7C69-4336-B7DA-77C3444A0CD0";
        public const string IString = "F99DB7A3-0FC1-4821-800B-0CF98E348EDF";
        public const string IStringResult = "550798BC-8720-49DB-8492-0A153B50B7A8";
        public const string IPersistent = "BA1A4637-3C9F-46D0-A65D-BA0EB85DA829";
        public const string IAttributes = "FA1E32F9-CA6D-46F5-A982-F956B1191B58";
        public const string IAttributes2 = "1382126A-FECA-4871-97D5-2A45B042AE99";
        public const string ICloneable = "D45406B9-3A2D-4443-9DAD-9BA985A1454B";

        // plugin interfaces
        public const string IComponent = "E831FF31-F2D5-4301-928E-BBEE25697802";
        public const string IAudioProcessor = "42043F99-B7DA-453C-A569-E79D9AAEC33D";
        public const string IAudioPresentationLatency = "309ECE78-EB7D-4FAE-8B22-25D909FD08B6";
        public const string IEditController = "DCD7BBE3-7742-448D-A874-AACC979C759E";
        public const string IEditController2 = "7F4EFE59-F320-4967-AC27-A3AEAFB63038";
        public const string IParameterChanges = "A4779663-0BB6-4A56-B443-84A8466FEB9D";
        public const string IParamValueQueue = "01263A18-ED07-4F6F-98C9-D3564686F9BA";
        public const string IEventList = "3A2C4214-3463-49FE-B2C4-F397B9695A44";
        public const string IMidiMapping = "DF0FF9F7-49B7-4669-B63A-B7327ADBF5E5";
        public const string IEditControllerHostEditing = "C1271208-7059-4098-B9DD-34B36BB0195E";
        public const string IUnitInfo = "3D4BD6B5-913A-4FD2-A886-E768A5EB92C1";
        public const string IProgramListData = "8683B01F-7B35-4F70-A265-1DEC353AF4FF";
        public const string IUnitData = "6C389611-D391-455D-B870-B83394A0EFDD";
        public const string IDependent = "F52B7AAE-DE72-416d-8AF1-8ACE9DD7BD5E";
        public const string INoteExpressionController = "B7F8F859-4123-4872-9116-95814F3721A3";
        public const string IKeyswitchController = "1F2F76D3-BFFB-4B96-B995-27A55EBCCEF4";
        public const string IXmlRepresentationController = "A81A0471-48C3-4DC4-AC30-C9E13C8393D5";
        public const string IAutomationState = "B4E8287F-1BB3-46AA-83A4-666768937BAB";
        public const string IInfoListener = "0F194781-8D98-4ADA-BBA0-C1EFC011D8D0";
        public const string IComponentHandlerBusActivation = "067D02C1-5B4E-274D-A92D-90FD6EAF7240";
        public const string IMidiLearn = "6B2449CC-4197-40B5-AB3C-79DAC5FE5C86";

        // messaging interfaces
        public const string IConnectionPoint = "70A4156F-6E6E-4026-9891-48BFAA60D8D1";
        public const string IMessage = "936F033B-C6C0-47DB-BB08-82F813C1E613";
        public const string IAttributeList = "1E5F0AEB-CC7F-4533-A254-401138AD5EE4";
        public const string IStreamAttributes = "D6CE2FFC-EFAF-4B8C-9E74-F1BB12DA44B4";

        // UI interfaces
        public const string IPlugView = "5BC32507-D060-49EA-A615-1B522B755B29";
        public const string IPlugFrame = "367FAF01-AFA9-4693-8D4D-A2A0ED0882A3";
        public const string IContextMenu = "2E93C863-0C9C-4588-97DB-ECF5AD17817D";
        public const string IContextMenuTarget = "3CDF2E75-85D3-4144-BF86-D36BD7C4894D";

        // host interfaces
        public const string IHostApplication = "58E595CC-DB2D-4969-8B6A-AF8C36A664E5";
        public const string IComponentHandler = "93A0BEA3-0BD0-45DB-8E89-0B0CC1E46AC6";
        public const string IComponentHandler2 = "F040B4B3-A360-45EC-ABCD-C045B4D5A2CC";
        public const string IComponentHandler3 = "69F11617-D26B-400D-A4B6-B9647B6EBBAB";
        public const string IUnitHandler = "4B5147F8-4654-486B-8DAB-30BA163A3C56";
        public const string IUpdateHandler = "F5246D56-8654-4d60-B026-AFB57B697B37";

        // wrapper interfaces
        public const string IVst3ToVst2Wrapper = "29633AEC-1D1C-47E2-BB85-B97BD36EAC61";
        public const string IVst3ToAUWrapper = "A3B8C6C5-C095-4688-B091-6F0BB697AA44";
        public const string IVst3ToAAXWrapper = "6D319DC6-60C5-6242-B32C-951B93BEF4C6";
        public const string IVst3WrapperMPESupport = "44149067-42CF-4BF9-8800-B750F7359FE3";

        // test interfaces
        public const string ITestW = "FE64FC19-9568-4F53-AAA7-8DC87228338E";
        public const string ITestResultW = "69796279-F651-418B-B24D-79B7D7C527F4";
        public const string ITestSuiteW = "5CA7106F-9878-4AA5-B4D3-0D712F5F1498";
        public const string ITestFactoryW = "AB483D3A-1526-4650-BF86-EEF69A327A93";
    }
}
