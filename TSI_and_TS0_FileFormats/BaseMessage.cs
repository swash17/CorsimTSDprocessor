using System.IO;


namespace TSI_and_TS0_FileFormats
{
    public class BaseMessage
    {
        public BaseMessage() {}
        public BaseMessage(BinaryReader FileToReadFrom)
        {
            FileIndex = (uint)FileToReadFrom.BaseStream.Position;
            MessageName = FileToReadFrom.ReadUInt32();
            MessageLength = FileToReadFrom.ReadUInt32();
            SimulationTime = FileToReadFrom.ReadUInt32();
            RequestType = FileToReadFrom.ReadUInt32();
        }
        public uint MessageName;
        public uint MessageLength;
        public uint SimulationTime; // Simulation time in seconds
        public uint RequestType;
        public uint FileIndex;
    }
}
