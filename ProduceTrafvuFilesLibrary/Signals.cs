using System.Collections.Generic;
using System.IO;


namespace ProduceTrafvuFilesLibrary
{
    class Signals
    {
        public List<signal> signals = new List<signal>();
        public void WriteToTs0File(BinaryWriter Ts0OutputFile, uint CurrentTimeStep)
        {
            Ts0OutputFile.Write((uint)MessageNames.LG_Data_GP); // message name
            Ts0OutputFile.Write((uint)((signals.Count * 14) + 22)); // message length
            Ts0OutputFile.Write(CurrentTimeStep);
            Ts0OutputFile.Write((uint)RequestTypes.DR_TS_SIGNAL);
            Ts0OutputFile.Write((uint)1); // Request Handle
            Ts0OutputFile.Write((uint)ClassIDs.Link);
            Ts0OutputFile.Write((ushort)ActionIDs.UPDATE);
            Ts0OutputFile.Write((ushort)1); // Attribute ID Count
            Ts0OutputFile.Write((ushort)AttributeIDs.LK_SignalState);
            Ts0OutputFile.Write((ushort)0); // Number of aggregate classes
            Ts0OutputFile.Write((ushort)signals.Count); // Number of links under signal control
            foreach (signal MySignal in signals)
            {
                Ts0OutputFile.Write(MySignal.LinkID);
                Ts0OutputFile.Write(MySignal.LeftTurnCode);
                Ts0OutputFile.Write(MySignal.LeftDiagonalTurnCode);
                Ts0OutputFile.Write(MySignal.ThroughCode);
                Ts0OutputFile.Write(MySignal.RightDiagonalTurnCode);
                Ts0OutputFile.Write(MySignal.RightTurnCode);
            }
        }
    }
    class signal
    {
        public uint LinkID;
        public ushort LeftTurnCode;
        public ushort LeftDiagonalTurnCode;
        public ushort ThroughCode;
        public ushort RightDiagonalTurnCode;
        public ushort RightTurnCode;
    }
}
