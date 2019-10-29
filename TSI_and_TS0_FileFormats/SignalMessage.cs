using System.IO;

namespace TSI_and_TS0_FileFormats
{
    public class SignalMessage : BaseMessage
    {
                // Read the message starting after the first 16 bytes
        public SignalMessage(BaseMessage baseMessage, BinaryReader FileToReadFrom)
        {
            // Copy the first 4 fields that are common with every message
            MessageLength = baseMessage.MessageLength;
            SimulationTime = baseMessage.SimulationTime;
            MessageName = baseMessage.MessageName;
            RequestType = baseMessage.RequestType;
            FileIndex = baseMessage.FileIndex;

            // Read the rest of the fields
            RequestHandle = FileToReadFrom.ReadUInt32();
            ClassID = FileToReadFrom.ReadUInt32();
            ActionID = FileToReadFrom.ReadUInt16();
            AttributeIDCount = FileToReadFrom.ReadUInt16();
            AttributeID = FileToReadFrom.ReadUInt16();
            NumberOfAggregateClasses = FileToReadFrom.ReadUInt16();
            InstanceIDCount = FileToReadFrom.ReadUInt16();

            // Now read in the signals
            Signals = new Signal[InstanceIDCount];
            for (int i = 0; i < InstanceIDCount; i++)
            {
                Signals[i] = new Signal(FileToReadFrom);
            }
        }
        public uint RequestHandle;
        public uint ClassID;
        public ushort ActionID;
        public ushort AttributeIDCount;
        public ushort AttributeID;
        public ushort NumberOfAggregateClasses;
        public ushort InstanceIDCount;
        public Signal[] Signals;
    }
    public class Signal
    {
        public uint LinkID;
        public ushort LeftTurnCode;
        public ushort LeftDiagonalTurnCode;
        public ushort ThroughCode;
        public ushort RightDiagonalTurnCode;
        public ushort RightTurnCode;

        public Signal(BinaryReader FileToReadFrom)
        {
            LinkID = FileToReadFrom.ReadUInt32();
            LeftTurnCode = FileToReadFrom.ReadUInt16();
            LeftDiagonalTurnCode = FileToReadFrom.ReadUInt16();
            ThroughCode = FileToReadFrom.ReadUInt16();
            RightDiagonalTurnCode = FileToReadFrom.ReadUInt16();
            RightTurnCode = FileToReadFrom.ReadUInt16();
        }

    }
}
