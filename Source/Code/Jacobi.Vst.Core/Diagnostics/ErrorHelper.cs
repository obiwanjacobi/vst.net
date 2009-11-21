namespace Jacobi.Vst.Core.Diagnostics
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Reflection;

    public static class ErrorHelper
    {
        public static string FormatException(Exception e)
        {
            StringBuilder text = new StringBuilder();

            while (e != null)
            {
                BuildExceptionText(text, e);

                //
                // Special cases for more info on exceptions
                //

                BuildReflectionTypeLoadExceptionText(text, e as ReflectionTypeLoadException);

                if (e.InnerException != null)
                {
                    text.AppendLine();
                    text.AppendLine("Inner Exception --------------------------");
                }

                e = e.InnerException;
            }

            return text.ToString();
        }

        private static void BuildExceptionText(StringBuilder text, Exception e)
        {
            text.AppendFormat("{0}: {1}", e.GetType(), e.Message);
            text.AppendLine();

            text.AppendLine(e.StackTrace.ToString());

            if (e.Data.Count > 0)
            {
                foreach(KeyValuePair<string, object> item in e.Data)
                {
                    text.AppendFormat("Key={0}, Value={1}", item.Key, item.Value);
                    text.AppendLine();
                }
            }

            if(!String.IsNullOrEmpty(e.HelpLink))
            {
                text.Append("Help link: ");
                text.AppendLine(e.HelpLink);
            }
        }

        private static void BuildReflectionTypeLoadExceptionText(StringBuilder text, ReflectionTypeLoadException rtle)
        {
            if (rtle != null)
            {
                if (rtle.LoaderExceptions != null)
                {
                    foreach (Exception le in rtle.LoaderExceptions)
                    {
                        text.AppendLine();
                        text.AppendLine("Loader Exception -----------------------");

                        BuildExceptionText(text, le);
                    }
                }

                if (rtle.Types != null && rtle.Types.Length > 0)
                {
                    text.AppendLine("Loaded Types ----------------------------");

                    foreach (Type loadedType in rtle.Types)
                    {
                        text.Append("\t");
                        text.AppendLine(loadedType.FullName);
                    }
                }
            }
        }
    }
}
