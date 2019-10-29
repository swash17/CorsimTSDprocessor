using System.IO;

namespace TSI_and_TS0_FileFormats
{
    public class CompleteMessage : BaseMessage
    {
        public CompleteMessage(BaseMessage baseMessage, BinaryReader FileToReadFrom)
        {
            // Copy the first 4 fields that are common with every message
            MessageLength = baseMessage.MessageLength;
            SimulationTime = baseMessage.SimulationTime;
            MessageName = baseMessage.MessageName;
            RequestType = baseMessage.RequestType;
            FileIndex = baseMessage.FileIndex;

            // Read the rest of the fields
            RequestHandle = FileToReadFrom.ReadUInt32();
        }
        public uint RequestHandle;
    }
}
