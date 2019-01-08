using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Runtime.InteropServices;

namespace ColorBlend
{
    public partial class ColorBlenderInterface
    {
        [DllImport("ColorBlender.dll", EntryPoint = "_ErrorStackClear@0", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void ErrorStackClear ();

        [DllImport("ColorBlender.dll", EntryPoint = "_SetSeparator@4", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe void SetSetparator (StringBuilder Sep);

        [DllImport("ColorBlender.dll", EntryPoint = "_GetSeparator@0", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.BStr)]
        static extern unsafe string GetSeparator ();

        [DllImport("ColorBlender.dll", EntryPoint = "_PopErrorCode@0", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int PopErrorCode ();

        [DllImport("ColorBlender.dll", EntryPoint = "ErrorStackEmpty@0", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe bool ErrorStackEmpty ();

        [DllImport("ColorBlender.dll", EntryPoint = "ErrorStackFull@0", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe bool ErrorStackFull ();

        [DllImport("ColorBlender.dll", EntryPoint = "_ErrorTop@0", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.BStr)]
        static extern unsafe string ErrorTop ();

        [DllImport("ColorBlender.dll", EntryPoint = "_ErrorTop2@4", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.BStr)]
        static extern unsafe string ErrorTop2 (StringBuilder Sep);

        [DllImport("ColorBlender.dll", EntryPoint = "_ErrorPop@0", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.BStr)]
        static extern unsafe string ErrorPop ();

        [DllImport("ColorBlender.dll", EntryPoint = "_ErrorPop2@4", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.BStr)]
        static extern unsafe string ErrorPop2 (StringBuilder Sep);

        [DllImport("ColorBlender.dll", EntryPoint = "_ErrorConstantToString@4", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.BStr)]
        static extern unsafe string ErrorConstantToString (Int32 ErrorConstant);

        /// <summary>
        /// Given the value in <paramref name="ErrorConstant"/>, return the string description.
        /// </summary>
        /// <param name="ErrorConstant">Error constant returned by ColorBlend.dll</param>
        /// <returns>String description for <paramref name="ErrorConstant"/>.</returns>
        public string ErrorMessage (int ErrorConstant)
        {
            unsafe
            {
                string ErrorMsg = "";
                ErrorMsg = ErrorConstantToString(ErrorConstant);
                return ErrorMsg;
            }
        }

        public Dictionary<int, string> ColorLibraryErrors = null;

        public void CreateErrorList()
        {
            unsafe
            {
                ColorLibraryErrors = new Dictionary<int, string>();
                int ErrorCode = -1;
                while(true)
                {
                    string ErrMsg = ErrorConstantToString(ErrorCode);
                    if (string.IsNullOrEmpty(ErrMsg))
                        break;
                    if (ErrMsg.Length < 1)
                        break;
                    ColorLibraryErrors.Add(ErrorCode, ErrMsg);
                    ErrorCode++;
                }
            }
        }

        /// <summary>
        /// Clear the error stack.
        /// </summary>
        public void ClearErrorStack ()
        {
            unsafe
            {
                ErrorStackClear();
            }
        }

        /// <summary>
        /// Get the error separator string.
        /// </summary>
        /// <returns>The error separator string.</returns>
        public string GetErrorSeparator()
        {
            unsafe
            {
                return GetSeparator();
            }
        }

        /// <summary>
        /// Set the error separator string.
        /// </summary>
        /// <param name="Sep">New error separator string.</param>
        public void SetErrorSeparator(string Sep)
        {
            if (string.IsNullOrEmpty(Sep))
                throw new ArgumentNullException("Sep");
            unsafe
            {
                SetSetparator(new StringBuilder(Sep));
            }
        }

        /// <summary>
        /// Get the empty stack status.
        /// </summary>
        /// <returns>True if the stack is empty, false if not.</returns>
        public bool ErrorStackIsEmpty()
        {
            unsafe
            {
                return ErrorStackEmpty();
            }
        }

        /// <summary>
        /// Get the full stack status.
        /// </summary>
        /// <returns>True if the stack is full, false if not.</returns>
        public bool ErrorStackIsFull()
        {
            unsafe
            {
                return ErrorStackFull();
            }
        }

        /// <summary>
        /// Pop the top of the error stack and return the error code.
        /// </summary>
        /// <returns>Error code on the top of the error stack.</returns>
        public int PopErrorStackCode()
        {
            unsafe
            {
                return PopErrorCode();
            }
        }

        /// <summary>
        /// Pop the top of the error stack and return the contents as a string.
        /// </summary>
        /// <returns>String with the contents of the top of the stack.</returns>
        public string ErrorStackPop()
        {
            unsafe
            {
                return ErrorPop();
            }
        }

        /// <summary>
        /// Pop the top of the error stack and return the contents as a string, fields
        /// separated by <paramref name="Sep"/>.
        /// </summary>
        /// <param name="Sep">The string that separates fields.</param>
        /// <returns>String with the contents of the top of the stack.</returns>
        public string ErrorStackPop2(string Sep)
        {
            unsafe
            {
                return ErrorPop2(new StringBuilder(Sep));
            }
        }

        /// <summary>
        /// Return (but don't pop) the top of the stack.
        /// </summary>
        /// <returns>Top of the stack as a string.</returns>
        public string ErrorStackTop ()
        {
            unsafe
            {
                return ErrorTop();
            }
        }

        /// <summary>
        /// Return (but don't pop) the top of the stack, using <paramref name="Sep"/> to
        /// separate fields.
        /// </summary>
        /// <param name="Sep">String used to separate fields.</param>
        /// <returns>Top of the stack as a string.</returns>
        public string ErrorStackTop2 (string Sep)
        {
            unsafe
            {
                return ErrorTop2(new StringBuilder(Sep));
            }
        }
    }
}
