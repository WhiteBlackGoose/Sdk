// Copyright (c) 2020-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: see LICENSE for more details.

namespace Elskom.Generic.Libs.UnluacNET
{
    public sealed class Version52 : Version
    {
        public Version52()
            : base(0x52)
        {
        }

        public override bool HasHeaderTail => true;

        public override int OuterBlockScopeAdjustment => 0;

        public override Op TForTarget => Op.TFORCALL;

        public override bool UsesInlineUpvalueDeclaritions => false;

        public override bool UsesOldLoadNilEncoding => false;

        public override LFunctionType GetLFunctionType()
            => LFunctionType.TYPE52;

        public override bool IsBreakableLoopEnd(Op op)
            => op is Op.JMP or Op.FORLOOP or Op.TFORLOOP;
    }
}
