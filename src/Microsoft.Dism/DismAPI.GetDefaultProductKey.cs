// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Dism
{
    public static partial class DismApi
    {
        /// <summary>
        /// Gets the default product key for the Windows edition in the image.
        /// </summary>
        /// <param name="session">A valid DismSession. The DismSession must be associated with an image. You can associate a session with an image by using the <see cref="OpenOfflineSession(string)" /> method.</param>
        /// <returns>Returns the default product key if found, <see langword="null"/> otherwise.</returns>
        public static string GetDefaultProductKey(DismSession session)
        {
            string currentEdition = GetCurrentEdition(session);

            int hresult = NativeMethods.GetEditionIdFromName(currentEdition, out uint editionId);
            DismUtilities.ThrowIfFail(hresult);

            hresult = NativeMethods.SkuGetProductKeyForEdition(editionId, null, out IntPtr productKeyPtr, out IntPtr productPfnPtr);
            try
            {
                DismUtilities.ThrowIfFail(hresult);

                return Marshal.PtrToStringUni(productKeyPtr);
            }
            finally
            {
                Marshal.FreeHGlobal(productKeyPtr);
                Marshal.FreeHGlobal(productPfnPtr);
            }
        }

        internal static partial class NativeMethods
        {
            /// <summary>
            /// Returns the Windows edition ID for the given edition name.
            /// </summary>
            /// <param name="editionName">The name of the Windows edition.</param>
            /// <param name="editionId">The Windows edition ID.</param>
            /// <returns>Returns <c>S_OK</c> on success.</returns>
            [DllImport("PKeyHelper", CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int GetEditionIdFromName(string editionName, out uint editionId);

            /// <summary>
            /// Get the default key for the given Windows edition.
            /// </summary>
            /// <param name="editionId">The ID of the Windows edition, as returned by <see cref="GetEditionIdFromName"/>.</param>
            /// <param name="channelName">Optional. The channel of the Windows edition.</param>
            /// <param name="productKeyPtr">Pointer that will receive the default key.</param>
            /// <param name="productPfnPtr">Pointer that will receive the package family name (PFN).</param>
            /// <returns>Returns S_OK on success.</returns>
            [DllImport("PKeyHelper", CharSet = DismCharacterSet)]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int SkuGetProductKeyForEdition(uint editionId, string? channelName, out IntPtr productKeyPtr, out IntPtr productPfnPtr);
        }
    }
}
