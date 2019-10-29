using System.IO;

namespace TSI_and_TS0_FileFormats
{
    public class VehicleMessage : BaseMessage
    {
        // Read the message starting after the first 16 bytes
        public VehicleMessage(BaseMessage baseMessage, BinaryReader FileToReadFrom)
        {
            // Copy the first 4 fields that are common with every message
            MessageLength = baseMessage.MessageLength;
            SimulationTime = baseMessage.SimulationTime;
            MessageName = baseMessage.MessageName;
            RequestType = baseMessage.RequestType;
            FileIndex = baseMessage.FileIndex;

            // Read the rest of the fields
            RequestHandle = FileToReadFrom.ReadUInt32();
            ClassID_1 = FileToReadFrom.ReadUInt32();
            ActionID_1 = FileToReadFrom.ReadUInt16();
            AttributeIDCount_1 = FileToReadFrom.ReadUInt16();
            NumberOfAggregateClasses_1 = FileToReadFrom.ReadUInt16();
            ClassID_2 = FileToReadFrom.ReadUInt32();
            ActionID_2 = FileToReadFrom.ReadUInt16();
            AttributeIDCount_2 = FileToReadFrom.ReadUInt16();
            AttributeID = FileToReadFrom.ReadUInt16();
            NumberOfAggregateClasses_2 = FileToReadFrom.ReadUInt16();
            InstanceIDCount_1 = FileToReadFrom.ReadUInt16();
            InstanceID = FileToReadFrom.ReadUInt32();
            InstanceIDCount_2 = FileToReadFrom.ReadUInt16();

            // Now read in the vehicles
            Vehicles = new Vehicle[InstanceIDCount_2];
            for (int i = 0; i < InstanceIDCount_2; i++)
            {
                Vehicles[i] = new Vehicle(FileToReadFrom);
            }
            //uint ActualLength = NonRepeatingLength + (uint)Vehicles.Length * RepeatingLength;
            //if (ActualLength != MessageLength + 12)
            //{
            //    throw new Exception();
            //}
        }
        //public const uint NonRepeatingLength = 50;
        //public const uint RepeatingLength = 32;
        public uint RequestHandle;
        public uint ClassID_1;
        public ushort ActionID_1;
        public ushort AttributeIDCount_1;
        public ushort NumberOfAggregateClasses_1;
        public uint ClassID_2;
        public ushort ActionID_2;
        public ushort AttributeIDCount_2;
        public ushort AttributeID;
        public ushort NumberOfAggregateClasses_2;
        public ushort InstanceIDCount_1;
        public uint InstanceID; // ID of link containing vehicles for which this message is reporting
        public ushort InstanceIDCount_2; // number of vehicles on the specified link at the specified simulation time
        public Vehicle[] Vehicles;
    }

    public class Vehicle
    {
        public uint GlobalVehicleID; //global vehicle ID of first vehicle in this message
        private byte _Fleet; //0 = Auto, 1 = truck, 2 = carpool, 3 = bus
        public FleetTypes Fleet;
        public byte VehicleType; //CORSIM vehicle type
        public byte VehicleLength; //vehicle length in feet
        public byte DriverType; //CORSIM driver type
        public byte LaneID; //CORSIM ID of lane in which vehicle is traveling
        public int VehiclePosition; //vehicle’s distance from the upstream end of the link in feet
        public ushort PreviousUSN; //upstream node ID of the previous link the vehicle traveled
        private byte _TurnCode; //vehicle turn code: 0 = left, 1 = through, 2 = right, 3 = left diagonal, 4 = right diagonal, 5 = source emission
        public VehicleTurnCodes TurnCode;
        private byte _QueueStatus; //0 = vehicle is currently not in queue, 1 = vehicle is currently in queue
        public QueueStatuses QueueStatus;
        public sbyte Acceleration; //vehicle’s instantaneous acceleration in feet/second/second
        public byte Velocity; //vehicle’s instantaneous velocity in feet/second
        private byte _LaneChangeStatus; //0 = vehicle does not want to change lanes, 1 = vehicle wants to change lanes
        public LaneChangeStatuses LaneChangeStatus;
        public byte TargetLane; //CORSIM ID of lane vehicle would like to occupy
        public ushort DestinationNode; //node ID of the vehicles destination node
        public uint LeaderVehicleID; //global ID of vehicle’s leader vehicle
        public uint FollowerVehicleID; //global ID of vehicle’s follower vehicle
        public byte PreviousLaneID; //lane ID of lane the lane that the vehicle was previously in
        public Vehicle(BinaryReader FileToReadFrom)
        {
            GlobalVehicleID = FileToReadFrom.ReadUInt32();
            _Fleet = FileToReadFrom.ReadByte();
            Fleet = (FleetTypes)_Fleet;
            VehicleType = FileToReadFrom.ReadByte();
            VehicleLength = FileToReadFrom.ReadByte();
            DriverType = FileToReadFrom.ReadByte();
            LaneID = FileToReadFrom.ReadByte();
            VehiclePosition = FileToReadFrom.ReadInt32();
            PreviousUSN = FileToReadFrom.ReadUInt16();
            _TurnCode = FileToReadFrom.ReadByte();
            TurnCode = (VehicleTurnCodes)_TurnCode;
            _QueueStatus = FileToReadFrom.ReadByte();
            QueueStatus = (QueueStatuses)_QueueStatus;
            Acceleration = FileToReadFrom.ReadSByte();
            Velocity = FileToReadFrom.ReadByte();
            _LaneChangeStatus = FileToReadFrom.ReadByte();
            LaneChangeStatus = (LaneChangeStatuses)_LaneChangeStatus;
            TargetLane = FileToReadFrom.ReadByte();
            DestinationNode = FileToReadFrom.ReadUInt16();
            LeaderVehicleID = FileToReadFrom.ReadUInt32();
            FollowerVehicleID = FileToReadFrom.ReadUInt32();
            PreviousLaneID = FileToReadFrom.ReadByte();
        }

    }
}
