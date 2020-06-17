using System;

namespace Jacobi.Vst3.Core
{
    [Flags]
    public enum Speakers : ulong
    {
        SpeakerL = 1 << 0,
        SpeakerR = 1 << 1,
        SpeakerC = 1 << 2,
        SpeakerLfe = 1 << 3,
        SpeakerLs = 1 << 4,
        SpeakerRs = 1 << 5,
        SpeakerLc = 1 << 6,
        SpeakerRc = 1 << 7,
        SpeakerS = 1 << 8,
        SpeakerCs = SpeakerS,
        SpeakerSl = 1 << 9,
        SpeakerSr = 1 << 10,
        SpeakerTm = 1 << 11,
        SpeakerTfl = 1 << 12,
        SpeakerTfc = 1 << 13,
        SpeakerTfr = 1 << 14,
        SpeakerTrl = 1 << 15,
        SpeakerTrc = 1 << 16,
        SpeakerTrr = 1 << 17,
        SpeakerLfe2 = 1 << 18,
        SpeakerM = 1 << 19,

        SpeakerW = 1 << 20,
        SpeakerX = 1 << 21,
        SpeakerY = 1 << 22,
        SpeakerZ = 1 << 23,

        SpeakerTsl = 1 << 24,
        SpeakerTsr = 1 << 25,
        SpeakerLcs = 1 << 26,
        SpeakerRcs = 1 << 27,
    }
}
