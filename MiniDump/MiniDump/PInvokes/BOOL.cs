// Copyright (c) 2018-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Microsoft.Windows.Sdk
{
    internal readonly struct BOOL
    {
        internal BOOL(bool value)
            => this.Value = value ? 1 : 0;

        internal BOOL(int value)
            => this.Value = value;

        internal int Value { get; }

        public static implicit operator bool(BOOL value)
            => value.Value != 0;

        public static implicit operator BOOL(bool value)
            => new(value);

        public static explicit operator BOOL(int value)
            => new(value);
    }
}
