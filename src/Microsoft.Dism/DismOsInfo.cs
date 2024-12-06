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
        /// This struct is currently undocumented.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        internal struct DismOsInfo_
        {
            /// <summary>
            /// The state of the OS.
            /// </summary>
            public DismOsState OsState;

            /// <summary>
            /// The architecture of the image.
            /// </summary>
            public DismProcessorArchitecture Architecture;

            /// <summary>
            /// The major version of the operating system.
            /// </summary>
            public uint MajorVersion;

            /// <summary>
            /// The minor version of the operating system.
            /// </summary>
            public uint MinorVersion;

            /// <summary>
            /// The build number, for example, "7600".
            /// </summary>
            public uint BuildNumber;

            /// <summary>
            /// The revision number.
            /// </summary>
            public uint RevisionNumber;

            /// <summary>
            /// The Windows directory.
            /// </summary>
            public string WindowsDirectory;

            /// <summary>
            /// The boot drive.
            /// </summary>
            public string BootDrive;
        }
    }

    /// <summary>
    /// Represents OS information of the current image.
    /// </summary>
    public sealed class DismOsInfo : IEquatable<DismOsInfo>
    {
        private readonly DismApi.DismOsInfo_ _osInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="DismOsInfo" /> class.
        /// </summary>
        /// <param name="osInfo">A <see cref="DismApi.DismOsInfo_" /> structure.</param>
        internal DismOsInfo(DismApi.DismOsInfo_ osInfo)
        {
            _osInfo = osInfo;

            ProductVersion = new Version((int)osInfo.MajorVersion, (int)osInfo.MinorVersion, (int)osInfo.BuildNumber, (int)osInfo.RevisionNumber);
        }

        /// <summary>
        /// Gets the <see cref="DismOsState"/> of the current image.
        /// </summary>
        public DismOsState OsState => _osInfo.OsState;

        /// <summary>
        /// Gets the OS <see cref="DismProcessorArchitecture"/>.
        /// </summary>
        public DismProcessorArchitecture Architecture => _osInfo.Architecture;

        /// <summary>
        /// Gets the OS product version.
        /// </summary>
        public Version ProductVersion { get; }

        /// <summary>
        /// Gets the OS <c>Windows</c> directory path.
        /// </summary>
        public string WindowsDirectory => _osInfo.WindowsDirectory;

        /// <summary>
        /// Gets the OS boot drive.
        /// </summary>
        public string BootDrive => _osInfo.BootDrive;

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />, otherwise <see langword="false" />.</returns>
        public override bool Equals(object? obj)
        {
            return obj != null && Equals(obj as DismOsInfo);
        }

        /// <summary>
        /// Determines whether the specified <see cref="DismOsInfo" /> is equal to the current <see cref="DismOsInfo" />.
        /// </summary>
        /// <param name="other">The <see cref="DismOsInfo" /> object to compare with the current object.</param>
        /// <returns><see langword="true" /> if the specified <see cref="DismOsInfo" /> is equal to the current <see cref="DismOsInfo" />, otherwise <see langword="false" />.</returns>
        public bool Equals(DismOsInfo? other)
        {
            return other != null
                && OsState == other.OsState
                && Architecture == other.Architecture
                && ProductVersion == other.ProductVersion
                && WindowsDirectory == other.WindowsDirectory
                && BootDrive == other.BootDrive;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
        public override int GetHashCode()
        {
            return OsState.GetHashCode()
                ^ Architecture.GetHashCode()
                ^ ProductVersion.GetHashCode()
                ^ WindowsDirectory.GetHashCode()
                ^ BootDrive.GetHashCode();
        }
    }
}
