using System;
using System.IO;

namespace TSI_and_TS0_FileFormats
{
    public class IncidentMessage : BaseMessage
    {
        public IncidentMessage(BaseMessage baseMessage, BinaryReader FileToReadFrom)
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
            AttributeID_1 = FileToReadFrom.ReadUInt16();
            AttributeID_2= FileToReadFrom.ReadUInt16();
            AttributeID_3 = FileToReadFrom.ReadUInt16();
            AttributeID_4 = FileToReadFrom.ReadUInt16();
            AttributeID_5 = FileToReadFrom.ReadUInt16();
            AttributeID_6 = FileToReadFrom.ReadUInt16();
            AttributeID_7 = FileToReadFrom.ReadUInt16();
            AttributeID_8 = FileToReadFrom.ReadUInt16();
            AttributeID_9 = FileToReadFrom.ReadUInt16();
            AttributeID_10 = FileToReadFrom.ReadUInt16();
            AttributeID_11 = FileToReadFrom.ReadUInt16();
            AttributeID_12 = FileToReadFrom.ReadUInt16();
            NumberOfAggregateClasses = FileToReadFrom.ReadUInt16();
            InstanceIDCount = FileToReadFrom.ReadUInt16();
            Incidents = new Incident[InstanceIDCount];
            for (int i = 0; i < InstanceIDCount; i++)
            {
                Incidents[i] = new Incident(FileToReadFrom);
            }
        }
        public uint RequestHandle;
        public uint ClassID;
        public ushort ActionID;
        public ushort AttributeIDCount;
        public ushort AttributeID_1;
        public ushort AttributeID_2;
        public ushort AttributeID_3;
        public ushort AttributeID_4;
        public ushort AttributeID_5;
        public ushort AttributeID_6;
        public ushort AttributeID_7;
        public ushort AttributeID_8;
        public ushort AttributeID_9;
        public ushort AttributeID_10;
        public ushort AttributeID_11;
        public ushort AttributeID_12;
        public ushort NumberOfAggregateClasses;
        public ushort InstanceIDCount;
        public Incident[] Incidents;
    }
    public class Incident
    {
        public Incident(BinaryReader FileToReadFrom)
        {
            InstanceID = FileToReadFrom.ReadUInt32();
            InstanceID_Repeated = FileToReadFrom.ReadUInt32();
            LinkID = FileToReadFrom.ReadUInt32();
            Type = FileToReadFrom.ReadUInt16();
            Position = FileToReadFrom.ReadSingle();
            Length = FileToReadFrom.ReadSingle();
            OccurrenceTime = FileToReadFrom.ReadUInt32();
            Duration = FileToReadFrom.ReadUInt32();
            ReactionPointPosition = FileToReadFrom.ReadSingle();
            RubberneckingFactor = FileToReadFrom.ReadSingle();
            ModelType = FileToReadFrom.ReadUInt16();
            State = FileToReadFrom.ReadUInt16();
            NumberOfAffectedLanes = FileToReadFrom.ReadUInt16();
            AffectedLanes = new AffectedLane[NumberOfAffectedLanes];
            for (int i = 0; i < NumberOfAffectedLanes; i++)
            {
                AffectedLanes[i] = new AffectedLane(FileToReadFrom);
            }
        }
        public uint InstanceID;
        public uint InstanceID_Repeated;
        public uint LinkID;
        public ushort Type;
        public Single Position;
        public Single Length;
        public uint OccurrenceTime;
        public uint Duration;
        public Single ReactionPointPosition;
        public Single RubberneckingFactor;
        public ushort ModelType;
        public ushort State;
        public ushort NumberOfAffectedLanes;
        public AffectedLane[] AffectedLanes;
    }
    public class AffectedLane
    {
        public AffectedLane(BinaryReader FileToReadFrom)
        {
            AffectedLaneID = FileToReadFrom.ReadUInt32();
            StatusCode = FileToReadFrom.ReadUInt16();
        }
        public uint AffectedLaneID;
        public ushort StatusCode;
    }
}
