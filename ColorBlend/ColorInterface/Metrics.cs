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
        /// <summary>
        /// Contains the color/count pair for counting colors.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct ColorCountPair
        {
            /// <summary>
            /// The color (in UInt32 form) with Alpha at 0x0.
            /// </summary>
            public UInt32 Color;
            /// <summary>
            /// The quantity of color found.
            /// </summary>
            public UInt32 Count;
            /// <summary>
            /// Structure initialized.
            /// </summary>
            public bool Initialized;
        }

        /// <summary>
        /// Count the number of unique colors in the image.
        /// </summary>
        /// <param name="Source">Image whose colors will be counted..</param>
        /// <param name="Width">Width of the image.</param>
        /// <param name="Height">Height of the image.</param>
        /// <param name="UniqueColorCount">Number of unique colors found.</param>
        /// <param name="Keys">List of colors.</param>
        /// <param name="Values">List of counts associated (in order) with <seealso cref="Keys"/>.</param>
        /// <returns>Value indicating operational results.</returns>
        [DllImport("ColorBlender.dll", EntryPoint = "_ReturnUniqueColors@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ReturnUniqueColors(void* Source, Int32 Width, Int32 Height, UInt32* UniqueColorCount,
              void* Results, Int32 ColorsToReturn);

        /// <summary>
        /// Get the number of unique colors in the passed image.
        /// </summary>
        /// <param name="Source">Image whose color count will be returned.</param>
        /// <param name="SourceWidth">Width of the image.</param>
        /// <param name="SourceHeight">Height of the image.</param>
        /// <param name="UniqueColorCount">Number of unique colors.</param>
        /// <param name="Result">Value indicating operational results.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool GetCommonColors(WriteableBitmap Source, int SourceWidth, int SourceHeight,
          int TopColorCount, out UInt32 UniqueColorCount, out List<ColorCountPair> CommonColors, out ReturnCode Result)
        {
            UniqueColorCount = 0;
            CommonColors = new List<ColorCountPair>();
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;
            if (TopColorCount < 1)
                return false;
            ColorCountPair[] Pairs = new ColorCountPair[TopColorCount];
            for (int i = 0; i < TopColorCount; i++)
                Pairs[i].Initialized = false;

            unsafe
            {
                UInt32 Count = 0;
                Source.Lock();
                fixed (void* PairArray = Pairs)
                {
                    OpReturn = (ReturnCode)ReturnUniqueColors(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                         &Count, PairArray, TopColorCount);
                }
                Source.Unlock();
                UniqueColorCount = Count;
                for (int i = 0; i < TopColorCount; i++)
                    if (Pairs[i].Initialized)
                        CommonColors.Add(Pairs[i]);
            }

            LastReturnCode = OpReturn;
            Result = OpReturn;
            return OpReturn == ReturnCode.Success;
        }

        /// <summary>
        /// Count the number of unique colors in the image.
        /// </summary>
        /// <param name="Source">Image whose colors will be counted..</param>
        /// <param name="Width">Width of the image.</param>
        /// <param name="Height">Height of the image.</param>
        /// <param name="UniqueColorCount">Number of unique colors found.</param>
        /// <returns>Value indicating operational results.</returns>
        [DllImport("ColorBlender.dll", EntryPoint = "_CountUniqueColors@16", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int CountUniqueColors(void* Source, Int32 Width, Int32 Height,
            UInt32* UniqueColorCount);

        /// <summary>
        /// Get the number of unique colors in the passed image.
        /// </summary>
        /// <param name="Source">Image whose color count will be returned.</param>
        /// <param name="SourceWidth">Width of the image.</param>
        /// <param name="SourceHeight">Height of the image.</param>
        /// <param name="UniqueColorCount">Number of unique colors.</param>
        /// <param name="Result">Value indicating operational results.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool CountColors(WriteableBitmap Source, int SourceWidth, int SourceHeight,
          out UInt32 UniqueColorCount, out ReturnCode Result)
        {
            UniqueColorCount = 0;
            ReturnCode OpReturn = ReturnCode.NotSet;
            Result = ReturnCode.NotSet;

            unsafe
            {
                UInt32 Count = 0;
                Source.Lock();
                OpReturn = (ReturnCode)CountUniqueColors(Source.BackBuffer.ToPointer(), SourceWidth, SourceHeight,
                     &Count);
                Source.Unlock();
                UniqueColorCount = Count;
            }

            LastReturnCode = OpReturn;
            Result = OpReturn;
            return OpReturn == ReturnCode.Success;
        }
    }
}
