#pragma once

#include <stdint.h>

#ifdef  WIN32
#pragma pack(push)
#pragma pack(8)
#define Vst2Handler __cdecl
typedef int32_t Vst2IntPtr;
#else
#define Vst2Handler
typedef int64_t Vst2IntPtr;
#endif // WIN32


constexpr int32_t Vst2Version = 2400;
constexpr int32_t Vst2FourCharacterCode = 'V' << 24 | 's' << 16 | 't' << 8 | 'P';

const int32_t Vst2MaxProgNameLen = 24;
const int32_t Vst2MaxParamStrLen = 8;
const int32_t Vst2MaxVendorStrLen = 64;
const int32_t Vst2MaxProductStrLen = 64;
const int32_t Vst2MaxEffectNameLen = 32;
const int32_t Vst2MaxNameLen = 64;
const int32_t Vst2MaxLabelLen = 64;
const int32_t Vst2MaxShortLabelLen = 8;
const int32_t Vst2MaxCategLabelLen = 24;
const int32_t Vst2MaxFileNameLen = 100;

enum class Vst2PluginFlags
{
    HasEditor =         1 << 0,
    HasClip =           1 << 1,
    HasVu =             1 << 2,
    CanMono =           1 << 3,
    CanReplace =        1 << 4,
    Programs =          1 << 5,
    IsSynth =           1 << 8,
    NoSoundInStop =     1 << 9,
    ExtIsAsync =        1 << 10,
    ExtHasBuffer =      1 << 11,
    CanReplaceDouble =  1 << 12
};

enum class Vst2PluginCommands
{
    Open,
    Close,
    ProgramSet,
    ProgramGet,
    ProgramSetName,
    ProgramGetName,
    ParameterGetLabel,
    ParameterGetDisplay,
    ParameterGetName,
    VuGet,
    SampleRateSet,
    BlockSizeSet,
    OnOff,
    EditorGetRectangle,
    EditorOpen,
    EditorClose,
    EditorDraw,
    EditorMouse,
    EditorKey,
    EditorIdle,
    EditorTop,
    EditorSleep,
    Identify,
    ChunkGet,
    ChunkSet,
    ProcessEvents,
    ParameterCanBeAutomated,
    ParameterFromString,
    ProgramGetCategoriesCount,
    ProgramGetNameByIndex,
    ProgramCopy,
    ConnectInput,
    ConnectOutput,
    GetInputProperties,
    GetOutputProperties,
    PluginGetCategory,
    GetCurrentPosition,
    GetDestinationBuffer,
    OfflineNotify,
    OfflinePrepare,
    OfflineRun,
    ProcessVariableIo,
    SetSpeakerArrangement,
    SetBlockSizeAndSampleRate,
    SetBypass,
    PluginGetName,
    GetErrorText,
    VendorGetString,
    ProductGetString,
    VendorGetVersion,
    VendorSpecific,
    CanDo,
    GetTailSizeInSamples,
    Idle,
    GetIcon,
    SetViewPosition,
    ParameterGetProperties,
    KeysRequired,
    GetVstVersion,
    EditorKeyDown,
    EditorKeyUp,
    SetKnobMode,
    MidiProgramGetName,
    MidiProgramGetCurrent,
    MidiProgramGetCategory,
    MidiProgramsChanged,
    MidiKeyGetName,
    BeginSetProgram,
    EndSetProgram,
    GetSpeakerArrangement,
    GetNextPlugin,
    ProcessStart,
    ProcessStop,
    SetTotalFramesToProcess,
    SetPanLaw,
    BeginLoadBank,
    BeginLoadProgram,
    SetProcessPrecision,
    MidiGetInputChannelCount,
    MidiGetOutputChannelCount,
};


enum class Vst2HostCommands
{
    Automate,
    Version,
    CurrentId,
    Idle,
    PinConnected,
    Reserved1,
    WantMidi,
    GetTime,
    ProcessEvents,
    SetTime,
    TempoAt,
    GetAutomatableParameterCount,
    GetParameterQuantization,
    IoChanged,
    NeedIdle,
    SizeWindow,
    GetSampleRate,
    GetBlockSize,
    GetInputLatency,
    GetOutputLatency,
    PluginGetPrevious,
    PluginGetNext,
    WillReplace,
    GetCurrentProcessLevel,
    GetAutomationState,
    OfflineStart,
    OfflineRead,
    OfflineWrite,
    OfflineGetCurrentPass,
    OfflineGetCurrentMetaPass,
    SetOutputSampleRate,
    GetOutputSpeakerArrangement,
    VendorGetString,
    ProductGetString,
    VendorGetVersion,
    VendorSpecific,
    SetIcon,
    CanDo,
    GetLanguage,
    WindowOpen,
    WindowClose,
    GetDirectory,
    UpdateDisplay,
    EditBegin,
    EditEnd,
    FileSelectorOpen,
    FileSelectorClose,
    EditFile,
    GetChunkFile,
    GetInputSpeakerArrangement,
};

struct Vst2Plugin;

typedef	Vst2IntPtr (Vst2Handler* Vst2HostCallback) (Vst2Plugin* plugin, int32_t opcode, int32_t index, Vst2IntPtr value, void* ptr, float opt);
typedef	Vst2IntPtr (Vst2Handler* Vst2HostCommand) (Vst2Plugin* plugin, Vst2HostCommands command, int32_t index, Vst2IntPtr value, void* ptr, float opt);
typedef Vst2IntPtr (Vst2Handler* Vst2PluginCommand) (Vst2Plugin* plugin, Vst2PluginCommands command, int32_t index, Vst2IntPtr value, void* ptr, float opt);
typedef void (Vst2Handler* Vst2PluginProcess) (Vst2Plugin* plugin, float** inputs, float** outputs, int32_t sampleFrames);
typedef void (Vst2Handler* Vst2PluginProcessDouble) (Vst2Plugin* plugin, double** inputs, double** outputs, int32_t sampleFrames);
typedef void (Vst2Handler* Vst2PluginSetParameter) (Vst2Plugin* plugin, int32_t paramIndex, float paramValue);
typedef float (Vst2Handler* Vst2PluginGetParameter) (Vst2Plugin* plugin, int32_t paramIndex);


enum class Vst2PlugCategory
{
    Unknown,
    Effect,
    Synth,
    Analysis,
    Mastering,
    Spacializer,
    RoomFx,
    SurroundFx,
    Restoration,
    OfflineProcess,
    Shell,
    Generator,
};


struct Vst2Plugin
{
    int32_t VstP;
    Vst2PluginCommand command;
    Vst2PluginProcess process;
    Vst2PluginSetParameter parameterSet;
    Vst2PluginGetParameter parameterGet;

    int32_t programCount;
    int32_t parameterCount;
    int32_t inputCount;
    int32_t outputCount;

    Vst2PluginFlags flags;

    Vst2IntPtr reserved1;
    Vst2IntPtr reserved2;

    int32_t startupDelay;
    int32_t realQualities;
    int32_t offQualities;
    float ioRatio;

    void* object;
    void* user;

    int32_t id;
    int32_t version;

    Vst2PluginProcess replace;
    Vst2PluginProcessDouble replaceDouble;

    uint8_t filler[56];
};


struct Vst2Rectangle
{
    int16_t top;
    int16_t left;
    int16_t bottom;
    int16_t right;
};


enum class Vst2EventKind
{
    Midi,
    Audio,
    Video,
    Parameter,
    Trigger,
    SystemExclusive
};

struct Vst2Event
{
    Vst2EventKind kind;
    int32_t sizeInBytes;
    int32_t deltaFrames;
    int32_t flags;
    uint8_t data[16];
};

struct Vst2Events
{
    int32_t eventCount;
    Vst2IntPtr reserved;
    Vst2Event* events[2];
};


enum class Vst2MidiEventFlags
{
    None = 0,
    IsRealTime = 1 << 0,
};

struct Vst2MidiEvent
{
    Vst2EventKind kind;
    int32_t sizeInBytes;
    int32_t deltaFrames;
    Vst2MidiEventFlags flags;
    int32_t noteLength;
    int32_t noteOffset;
    uint8_t midiData[4];
    int8_t detune;
    uint8_t noteOffVelocity;
    uint8_t reserved1;
    uint8_t reserved2;
};

struct Vst2MidiSysExEvent
{
    Vst2EventKind kind;
    int32_t sizeInBytes;
    int32_t deltaFrames;
    int32_t flags;
    int32_t dumpInBytes;
    Vst2IntPtr reserved1;
    char* dump;
    Vst2IntPtr reserved2;
};

enum class Vst2TimeInfoFlags
{
    TransportChanged =      1 << 0,
    TransportPlaying =      1 << 1,
    TransportCycleActive =  1 << 2,
    TransportRecording =    1 << 3,
    AutomationWriting =     1 << 6,
    AutomationReading =     1 << 7,
    NanosValid =            1 << 8,
    PpqPosValid =           1 << 9,
    TempoValid =            1 << 10,
    BarsValid =             1 << 11,
    CyclePosValid =         1 << 12,
    TimeSigValid =          1 << 13,
    SmpteValid =            1 << 14,
    ClockValid =            1 << 15,
};

enum class Vst2SmpteFrameRate
{
    Smpte24fps = 0,
    Smpte25fps = 1,
    Smpte2997fps = 2,
    Smpte30fps = 3,
    Smpte2997dfps = 4,
    Smpte30dfps = 5,
    SmpteFilm16mm = 6,
    SmpteFilm35mm = 7,
    Smpte239fps = 10,
    Smpte249fps = 11,
    Smpte599fps = 12,
    Smpte60fps = 13,
};


struct Vst2TimeInfo
{
    double samplePosition;
    double sampleRate;
    double nanoSeconds;
    double ppqPosition;
    double tempo;
    double barStartPosition;
    double cycleStartPosition;
    double cycleEndPosition;
    int32_t timeSigNumerator;
    int32_t timeSigDenominator;
    int32_t smpteOffset;
    Vst2SmpteFrameRate smpteFrameRate;
    int32_t sampleCountToNextClock;
    Vst2TimeInfoFlags flags;
};


struct Vst2VariableIo
{
    float** inputs;
    float** outputs;
    int32_t sampleInputCount;
    int32_t sampleOutputCount;
    int32_t* sampleInputProcessedCount;
    int32_t* sampleOutputProcessedCount;
};


enum class Vst2HostLanguage
{
    kVstLangEnglish = 1,
    kVstLangGerman,
    kVstLangFrench,
    kVstLangItalian,
    kVstLangSpanish,
    kVstLangJapanese,
};


enum class Vst2ProcessPrecision
{
    kVstProcessPrecision32,
    kVstProcessPrecision64
};

enum Vst2SpeakerArrangementKind
{
    ArrUserDefined = -2,
    ArrEmpty = -1,
    ArrMono = 0,
    ArrStereo,
    ArrStereoSurround,
    ArrStereoCenter,
    ArrStereoSide,
    ArrStereoCLfe,
    Arr30Cine,
    Arr30Music,
    Arr31Cine,
    Arr31Music,
    Arr40Cine,
    Arr40Music,
    Arr41Cine,
    Arr41Music,
    Arr50,
    Arr51,
    Arr60Cine,
    Arr60Music,
    Arr61Cine,
    Arr61Music,
    Arr70Cine,
    Arr70Music,
    Arr71Cine,
    Arr71Music,
    Arr80Cine,
    Arr80Music,
    Arr81Cine,
    Arr81Music,
    Arr102,
};


enum class Vst2ParameterFlags
{
    IsSwitch =                 1 << 0,
    UsesIntegerMinMax =        1 << 1,
    UsesFloatStep =            1 << 2,
    UsesIntStep =              1 << 3,
    SupportsDisplayIndex =     1 << 4,
    SupportsDisplayCategory =  1 << 5,
    CanRamp =                  1 << 6,
};

struct Vst2ParameterProperties
{
    float stepFloat;
    float smallStepFloat;
    float largeStepFloat;
    char label[Vst2MaxLabelLen];
    Vst2ParameterFlags flags;
    int32_t minInteger;
    int32_t maxInteger;
    int32_t stepInteger;
    int32_t largeStepInteger;
    char shortLabel[Vst2MaxShortLabelLen];
    int16_t displayIndex;
    int16_t category;
    int16_t numParametersInCategory;
    int16_t reserved;
    char categoryLabel[Vst2MaxCategLabelLen];
    uint8_t filler[16];
};


enum class Vst2MidiProgramNameFlags
{
    None,
    IsOmni,
};

struct Vst2MidiProgramName
{
    int32_t thisProgramIndex;
    char name[Vst2MaxNameLen];
    char midiProgram;
    char midiBankMsb;
    char midiBankLsb;
    uint8_t reserved;
    int32_t parentCategoryIndex;
    Vst2MidiProgramNameFlags flags;
};


struct Vst2MidiProgramCategory
{
    int32_t thisCategoryIndex;
    char name[Vst2MaxNameLen];
    int32_t parentCategoryIndex;
    uint32_t flags;
};

struct Vst2MidiKeyName
{
    int32_t thisProgramIndex;
    int32_t thisKeyNumber;
    char keyName[Vst2MaxNameLen];
    uint32_t reserved;
    uint32_t flags;
};


enum class Vst2PinPropertiesFlags
{
    IsActive =   1 << 0,
    IsStereo =   1 << 1,
    UseSpeaker = 1 << 2,
};


struct Vst2PinProperties
{
    char label[Vst2MaxLabelLen];
    Vst2PinPropertiesFlags flags;
    Vst2SpeakerArrangementKind arrangementKind;
    char shortLabel[Vst2MaxShortLabelLen];

    uint8_t filler[48];
};

enum class Vst2SpeakerType
{
    U32 = -32,
    U31,
    U30,
    U29,
    U28,
    U27,
    U26,
    U25,
    U24,
    U23,
    U22,
    U21,
    U20,
    U19,
    U18,
    U17,
    U16,
    U15,
    U14,
    U13,
    U12,
    U11,
    U10,
    U9,
    U8,
    U7,
    U6,
    U5,
    U4,
    U3,
    U2,
    U1,

    Undefined = 0x7FFFFFFF,
    M = 0,
    L,
    R,
    C,
    Lfe,
    Ls,
    Rs,
    Lc,
    Rc,
    S,
    Cs = S,
    Sl,
    Sr,
    Tm,
    Tfl,
    Tfc,
    Tfr,
    Trl,
    Trc,
    Trr,
    Lfe2,
};


struct Vst2SpeakerProperties
{
    float azimuth;
    float elevation;
    float radius;
    float reserved;
    char name[Vst2MaxNameLen];
    Vst2SpeakerType type;

    char filler[28];
};

struct Vst2SpeakerArrangement
{
    Vst2SpeakerArrangementKind kind;
    int32_t channelCount;
    Vst2SpeakerProperties speakers[8];
};



struct Vst2KeyCode
{
    int32_t character;
    uint8_t virt;
    uint8_t modifier;
};


enum class Vst2VirtualKey
{
    None,
    BACK,
    TAB,
    CLEAR,
    RETURN,
    PAUSE,
    ESCAPE,
    SPACE,
    NEXT,
    END,
    HOME,
    LEFT,
    UP,
    RIGHT,
    DOWN,
    PAGEUP,
    PAGEDOWN,
    SELECT,
    PRINT,
    ENTER,
    SNAPSHOT,
    INSERT,
    keyDELETE,
    HELP,
    NUMPAD0,
    NUMPAD1,
    NUMPAD2,
    NUMPAD3,
    NUMPAD4,
    NUMPAD5,
    NUMPAD6,
    NUMPAD7,
    NUMPAD8,
    NUMPAD9,
    MULTIPLY,
    ADD,
    SEPARATOR,
    SUBTRACT,
    DECIMAL,
    DIVIDE,
    F1,
    F2,
    F3,
    F4,
    F5,
    F6,
    F7,
    F8,
    F9,
    F10,
    F11,
    F12,
    NUMLOCK,
    SCROLL,
    SHIFT,
    CONTROL,
    ALT,
    EQUALS
};


enum class Vst2ModifierKey
{
    Shift =     1 << 0,
    Alternate = 1 << 1,
    Command =   1 << 2,
    Control =   1 << 3,
};


struct Vst2PatchChunkInfo
{
    int32_t version;
    int32_t pluginUniqueID;
    int32_t pluginVersion;
    int32_t numElements;
    uint8_t filler[48];
};


struct Vst2FileType
{
    char name[128];
    char macType[8];
    char dosType[8];
    char unixType[8];
    char mimeType1[128];
    char mimeType2[128];
};

enum Vst2FileSelectCommand
{
    FileLoad,
    FileSave,
    MultipleFilesLoad,
    DirectorySelect,
};

struct Vst2FileSelect
{
    Vst2FileSelectCommand command;
    int32_t type;
    int32_t macCreator;
    int32_t nbFileTypes;
    Vst2FileType* fileTypes;
    char title[1024];
    char* initialPath;
    char* returnPath;
    int32_t sizeReturnPath;
    char** returnMultiplePaths;
    int32_t nbReturnPath;
    Vst2IntPtr reserved;
    uint8_t filler[116];
};










#ifdef WIN32
#pragma pack(pop)
#endif // WIN32