using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jacobi.Vst3.TestPlugin
{
    public class Version
    {
        private const string VersionSeparator = ".";

        public Version(int major, int minor, int? revision, int? build)
        {
            Major = major;
            Minor = minor;
            Revision = revision;
            Build = build;
        }

        public int Major { get; private set; }
        public int Minor { get; private set; }
        public int? Revision { get; private set; }
        public int? Build { get; private set; }

        private int? _version;

        public int ToInt32()
        {
            if (!_version.HasValue)
            {
                int version = Major << 8 | Minor;

                if (Revision.HasValue)
                {
                    version <<= 8;
                    version |= Revision.Value;

                    if (Build.HasValue)
                    {
                        version <<= 8;
                        version |= Build.Value;
                    }
                }

                _version = version;
            }

            return _version.GetValueOrDefault();
        }

        private string _text;

        public override string ToString()
        {
            if (_text == null)
            {
                var text = new StringBuilder();

                text.Append(Major);
                text.Append(VersionSeparator);
                text.Append(Minor);

                if (Revision.HasValue)
                {
                    text.Append(VersionSeparator);
                    text.Append(Revision.Value);

                    if (Build.HasValue)
                    {
                        text.Append(VersionSeparator);
                        text.Append(Build.Value);
                    }
                }

                _text = text.ToString();
            }

            return _text;
        }
    }
}
