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
        /// Gets detailed OS information for the image.
        /// </summary>
        /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
        /// <returns>A <see cref="DismOsInfo" /> object.</returns>
        public static DismOsInfo GetOsInfo(DismSession session)
        {
            int hresult = NativeMethods.DismGetOsInfo(session, out IntPtr osInfoPtr);

            try
            {
                DismUtilities.ThrowIfFail(hresult, session);

                return new DismOsInfo(osInfoPtr.ToStructure<DismApi.DismOsInfo_>());
            }
            finally
            {
                Delete(osInfoPtr);
            }
        }

        internal static partial class NativeMethods
        {
            /// <summary>
            /// Gets detailed OS information for the iamge.
            /// </summary>
            /// <param name="session">A valid DISM Session. The DISM Session must be associated with an image. You can associate a session with an image by using the DismOpenSession Function.</param>
            /// <param name="osInfoPtr">A pointer that will receive the <see cref="DismOsInfo"/> struct.</param>
            /// <returns>Returns <c>S_OK</c> on success.</returns>
            [DllImport(DismDllName, EntryPoint = "_DismGetOsInfo")]
            [return: MarshalAs(UnmanagedType.Error)]
            public static extern int DismGetOsInfo(DismSession session, out IntPtr osInfoPtr);
        }
    }
}
