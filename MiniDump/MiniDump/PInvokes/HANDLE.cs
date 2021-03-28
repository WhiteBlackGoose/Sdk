// Copyright (c) 2018-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Microsoft.Windows.Sdk
{
    using global::System;
    using global::System.Diagnostics;

    [DebuggerDisplay("{Value}")]
    internal readonly struct HANDLE : IEquatable<HANDLE>
    {
        internal readonly IntPtr Value;

        internal HANDLE(IntPtr value)
            => this.Value = value;

        public static implicit operator IntPtr(HANDLE value)
            => value.Value;

        public static explicit operator HANDLE(IntPtr value)
            => new(value);

        public bool Equals(HANDLE other)
            => this.Value == other.Value;

        public override bool Equals(object obj)
            => obj is HANDLE other && this.Equals(other);

        public override int GetHashCode()
            => this.Value.GetHashCode();
    }
}
