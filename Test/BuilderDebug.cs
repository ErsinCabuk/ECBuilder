using System;
using System.Diagnostics;

namespace ECBuilder.Test
{
    /// <summary>
    /// Internal debug for ECBuilder.
    /// </summary>
    internal class BuilderDebug
    {
        /// <summary>
        /// Writes an error message to Output.
        /// </summary>
        /// <param name="message">Message</param>
        public static void Error(string message)
        {
            Debug.WriteLine(message);
        }

        /// <summary>
        /// If <paramref name="designMode"/> is <see langword="true"/> (that is, if it is an error that occurs during the design phase), it shows the error to the developer as a <see cref="ECBuilder.Helpers.MessageBoxes.Error(string, string)">MessageBox</see>; otherwise, writes to the console.
        /// </summary>
        /// <param name="designMode"></param>
        /// <param name="message"></param>
        public static void Error(bool designMode, string message)
        {
            if (designMode)
            {
                Debug.Fail(message);
            }
            else
            {
                Debug.WriteLine(message);
            }
        }

        public static void Warn(string message)
        {
            Console.WriteLine(message);
        }

        public static void Warn(bool designMode, string message)
        {
            if (designMode)
            {
                Debug.Fail(message);
            }
            else
            {
                Console.WriteLine(message);
            }
        }
    }
}
