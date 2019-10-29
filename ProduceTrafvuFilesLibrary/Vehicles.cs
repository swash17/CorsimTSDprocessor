using System.Collections.Generic;
using System.IO;


namespace ProduceTrafvuFilesLibrary
{
    class Vehicles
    {
        public uint InstanceID;
        public List<Vehicle> vehicles;
        public Vehicles()
        {
            vehicles = new List<Vehicle>();
        }
        public void WriteToTs0File(BinaryWriter Ts0OutputFile, uint CurrentTimeStep)
        {
            Ts0OutputFile.Write((uint)MessageNames.LG_Data_GP); // message name
            Ts0OutputFile.Write((uint)((vehicles.Count * 32) + 38)); // message length
            Ts0OutputFile.Write(CurrentTimeStep);
            Ts0OutputFile.Write((uint)RequestTypes.DR_TS_VEHICLE);
            Ts0OutputFile.Write((uint)1); // Request Handle
            Ts0OutputFile.Write((uint)ClassIDs.Link);
            Ts0OutputFile.Write((ushort)ActionIDs.UPDATE);
            Ts0OutputFile.Write((ushort)0); // Attribute ID Count
            Ts0OutputFile.Write((ushort)1); // Number of aggregate classes
            Ts0OutputFile.Write((uint)ClassIDs.Vehicle);
            Ts0OutputFile.Write((ushort)ActionIDs.SEARCH);
            Ts0OutputFile.Write((ushort)1); // Attribute ID Count
            Ts0OutputFile.Write((ushort)AttributeIDs.V_InputAndAnimate);
            Ts0OutputFile.Write((ushort)0); // Number of aggregate classes
            Ts0OutputFile.Write((ushort)1); // Instance ID Count
            Ts0OutputFile.Write(InstanceID);
            Ts0OutputFile.Write((ushort)vehicles.Count); // Instance ID Count
            foreach (Vehicle vehicle in vehicles)
            {
                Ts0OutputFile.Write(vehicle.GlobalVehicleID);
                Ts0OutputFile.Write(vehicle.Fleet);
                Ts0OutputFile.Write(vehicle.VehicleType);
                Ts0OutputFile.Write(vehicle.VehicleLength);
                Ts0OutputFile.Write(vehicle.DriverType);
                Ts0OutputFile.Write(vehicle.LaneID);
                Ts0OutputFile.Write(vehicle.VehiclePosition);
                Ts0OutputFile.Write(vehicle.PreviousUSN);
                Ts0OutputFile.Write(vehicle.TurnCode);
                Ts0OutputFile.Write(vehicle.QueueStatus);
                Ts0OutputFile.Write(vehicle.Acceleration);
                Ts0OutputFile.Write(vehicle.Velocity);
                Ts0OutputFile.Write(vehicle.LaneChangeStatus);
                Ts0OutputFile.Write(vehicle.TargetLane);
                Ts0OutputFile.Write(vehicle.DestinationNode);
                Ts0OutputFile.Write(vehicle.LeaderVehicleID);
                Ts0OutputFile.Write(vehicle.FollowerVehicleID);
                Ts0OutputFile.Write(vehicle.PreviousLaneID);
            }

        }
    }

    class Vehicle
    {
        public uint GlobalVehicleID; //global vehicle ID of first vehicle in this message
        public byte Fleet; //0 = Auto, 1 = truck, 2 = carpool, 3 = bus
        public byte VehicleType; //CORSIM vehicle type
        public byte VehicleLength; //vehicle length in feet
        public byte DriverType; //CORSIM driver type
        public byte LaneID; //CORSIM ID of lane in which vehicle is traveling
        public int VehiclePosition; //vehicle’s distance from the upstream end of the link in feet
        public ushort PreviousUSN; //upstream node ID of the previous link the vehicle traveled
        public byte TurnCode; //vehicle turn code: 0 = left, 1 = through, 2 = right, 3 = left diagonal, 4 = right diagonal, 5 = source emission
        public byte QueueStatus; //0 = vehicle is currently not in queue, 1 = vehicle is currently in queue
        public sbyte Acceleration; //vehicle’s instantaneous acceleration in feet/second/second
        public byte Velocity; //vehicle’s instantaneous velocity in feet/second
        public byte LaneChangeStatus; //0 = vehicle does not want to change lanes, 1 = vehicle wants to change lanes
        public byte TargetLane; //CORSIM ID of lane vehicle would like to occupy
        public ushort DestinationNode; //node ID of the vehicles destination node
        public uint LeaderVehicleID; //global ID of vehicle’s leader vehicle
        public uint FollowerVehicleID; //global ID of vehicle’s follower vehicle
        public byte PreviousLaneID; //lane ID of lane the lane that the vehicle was previously in
    }
}
