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
        /// Defines a region in an image or an entire image depending on usage.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        struct ImageDefintionStruct
        {
            /// <summary>
            /// Pointer to the bits in the region.
            /// </summary>
            public IntPtr Buffer;
            /// <summary>
            /// Left of the upper-left corner.
            /// </summary>
            public UInt32 X;
            /// <summary>
            /// Upper of the upper-left corner.
            /// </summary>
            public UInt32 Y;
            /// <summary>
            /// Width of the region.
            /// </summary>
            public UInt32 Width;
            /// <summary>
            /// Height of the region.
            /// </summary>
            public UInt32 Height;
            /// <summary>
            /// Stride of the region.
            /// </summary>
            public UInt32 Stride;
        };

        [DllImport("ColorBlender.dll", EntryPoint = "_ImageCombine@24", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ImageCombine (void* Destination, Int32 Width, Int32 Height, void* Sources, int SubCount,
                 UInt32 BGColor);

        public bool CombineImages (WriteableBitmap Destination, List<SubImageDescription> Components, Color BGColor)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                Destination.Lock();

                List<GCHandle> Handles = new List<GCHandle>();
                ImageDefintionStruct[] LocalImages = new ImageDefintionStruct[Components.Count];
                for (int i = 0; i < Components.Count; i++)
                {
                    LocalImages[i].Width = (uint)Components[i].Width;
                    LocalImages[i].Height = (uint)Components[i].Height;
                    LocalImages[i].X = (uint)Components[i].X;
                    LocalImages[i].Y = (uint)Components[i].Y;
                    //Do some Garbage Collector/memory management trickery to get things to work.
                    LocalImages[i].Buffer = Marshal.UnsafeAddrOfPinnedArrayElement(Components[i].Bits, 0);
                    Handles.Add(GCHandle.Alloc(Components[i].Bits, GCHandleType.Pinned));
                    LocalImages[i].Buffer = Handles.Last().AddrOfPinnedObject();
                }

                fixed (void* ComponentImages = LocalImages)
                {
                    OpReturn = (ReturnCode)ImageCombine(Destination.BackBuffer.ToPointer(), Destination.PixelWidth,
                        Destination.PixelHeight, ComponentImages, Components.Count, BGColor.ToARGB());
                }

                //Return the buffers to the control of the Garbage Collector.
                foreach (GCHandle Handle in Handles)
                    Handle.Free();
                System.Windows.Int32Rect DirtyRect = new Int32Rect(0, 0, Destination.PixelWidth, Destination.PixelHeight);
                Destination.AddDirtyRect(DirtyRect);
                Destination.Unlock();
            }

            return OpReturn == ReturnCode.Success;
        }

        [DllImport("ColorBlender.dll", EntryPoint = "_ImageSplit@20", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern unsafe int ImageSplit (void* Source, Int32 Width, Int32 Height, void* Results, int SubCount);

        public bool SplitImage (WriteableBitmap Source, ref List<SubImageDescription> Components)
        {
            ReturnCode OpReturn = ReturnCode.NotSet;
            unsafe
            {
                Source.Lock();
                List<GCHandle> Handles = new List<GCHandle>();
                ImageDefintionStruct[] LocalImages = new ImageDefintionStruct[Components.Count];
                for (int i = 0; i < Components.Count; i++)
                {
                    LocalImages[i].Width = (uint)Components[i].Width;
                    LocalImages[i].Height = (uint)Components[i].Height;
                    LocalImages[i].X = (uint)Components[i].X;
                    LocalImages[i].Y = (uint)Components[i].Y;
                    //Do some Garbage Collector/memory management trickery to get things to work.
                    LocalImages[i].Buffer = Marshal.UnsafeAddrOfPinnedArrayElement(Components[i].Bits, 0);
                    Handles.Add(GCHandle.Alloc(Components[i].Bits, GCHandleType.Pinned));
                    LocalImages[i].Buffer = Handles.Last().AddrOfPinnedObject();
                }

                fixed (void* ComponentImages = LocalImages)
                {
                    fixed (void* Targets = LocalImages)
                    {
                        OpReturn = (ReturnCode)ImageSplit(Source.BackBuffer.ToPointer(), Source.PixelWidth, Source.PixelHeight,
                            Targets, Components.Count);
                    }
                }
                //Return the buffers to the control of the Garbage Collector.
                foreach (GCHandle Handle in Handles)
                    Handle.Free();
                Source.Unlock();
            }

            return OpReturn == ReturnCode.Success;
            //MERGEBLOBS3
        }

        public class SubImageDescription
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public byte[] Bits { get; set; }
        }
    }
}
