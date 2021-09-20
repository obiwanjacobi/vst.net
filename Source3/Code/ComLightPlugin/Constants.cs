namespace ComLightPlugin
{
    public static class Constants
    {
        // string lengths
        public const int MaxSizeVendor = 64;
        public const int MaxSizeEmail = 128;
        public const int MaxSizeUrl = 256;
        public const int MaxSizeVersion = 64;
        public const int MaxSizeCategory = 32;
        public const int MaxSizeName = 64;
        public const int MaxSizeSubCategories = 128;
        public const int MaxSizeBusName = 128;
        public const int Fixed128 = 128;

        // IAudioProcessor.GetTailSamples()
        public const uint NoTailSamples = 0;
        public const uint InfiniteTailSamples = 0xFFFFFFFF;

        // IEditController.CreateView
        public const string EditorViewType = "editor";

        // IUnitHandler.NotifyProgramListChange
        public const int AllProgramInvalid = -1;		// all program information is invalid
    }
}
