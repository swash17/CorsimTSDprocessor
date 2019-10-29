using System;
using System.Collections.Generic;
using System.IO;


namespace ProduceTrafvuFilesLibrary
{
    /// <summary>
    /// This class builds the "tsi" and "ts0" files that you will need for TRAFVU.
    /// </summary>
    public class TrafvuFilesBuilder
    {
        private Boolean Finished = false;
        private string TsiFilePath, Ts0FilePath;
        private BinaryWriter Ts0Writer, TsiWriter;
        /// <summary>
        /// Create an object that can build the "tsi" and "ts0" TRAFVU files.
        /// </summary>
        /// <param name="FilePath">File path of output tsi and tso file. This should not contain a file name</param>
        /// <param name="Filename">Name to be used for your output "tsi" and "ts0" TRAFVU files. Do not place a file extension on the filename</param>
        public TrafvuFilesBuilder(string FilePath, string Filename)
        {
            string TempFilePath = FilePath;
            if (TempFilePath.Substring(TempFilePath.Length - 1, 1) != @"/") TempFilePath += @"/"; // Make sure a slash is at the end
            TempFilePath += Filename;
            if (Path.HasExtension(TempFilePath)) TempFilePath = Path.GetFileNameWithoutExtension(TempFilePath); // Remove any file extension
            TsiFilePath = TempFilePath + ".tsi";
            Ts0FilePath = TempFilePath + ".ts0";
            OpenOutputFiles();
        }
        /// <summary>
        /// Create an object that can build the "tsi" and "ts0" TRAFVU files. The files will be placed in your temporary folder.
        /// </summary>
        public TrafvuFilesBuilder()
        {
            string FilePath = Path.GetTempPath();
            for (int i = 0; i < int.MaxValue; i++)
            {
                TsiFilePath = FilePath + "ForTrafvu" + i + ".tsi";
                Ts0FilePath = FilePath + "ForTrafvu" + i + ".ts0";
                if (!File.Exists(TsiFilePath) && !File.Exists(Ts0FilePath))
                    break;
            }
            OpenOutputFiles();
        }
        /// <summary>
        /// destructor
        /// </summary>
        ~TrafvuFilesBuilder() // Just in case the user forgets to call FinishAndClose()
        {
            if (Finished) return;
            FinishAndClose();
        }
        private void OpenOutputFiles()
        {
            TsiWriter = new BinaryWriter(File.Open(TsiFilePath, FileMode.Create)); // Open the tsi file
            Ts0Writer = new BinaryWriter(File.Open(Ts0FilePath, FileMode.Create)); // Open the ts0 file
            // Write out the file header to the ts0 file
            string Ts0FileHeader = "5.01_01-NOV-04";
            for (int i = 0; i < Ts0FileHeader.Length; i++)
                Ts0Writer.Write((byte)Ts0FileHeader[i]);
            Ts0Writer.Write((byte)0);
            Ts0Writer.Write((byte)'L');
        }
        private uint CurrentTimeStep = 0; // valid values are 0 to uint.MaxValue;
        private List<Vehicles> vehicles = new List<Vehicles>();
        private Signals signals = null;
        /// <summary>
        /// Begins a new timestep and terminates the current timestep. It is optional to call this method prior to the first time step, but you MUST call this method to begin every subsequent time step after the first time step.
        /// </summary>
        /// <returns>The new timestep in seconds</returns>
        public uint BeginNewTimeStep()
        {
            if (Finished) return CurrentTimeStep;
            if (vehicles.Count == 0 && signals == null)
                return CurrentTimeStep;
            uint TsiVehicleIndex = (uint)Ts0Writer.BaseStream.Position; // Save the index of the first vehicle message
            TsiWriter.Write((uint)0); // Write out the ts0 file number (which will always be zero for us)
            if (vehicles.Count == 0)// Write out the index of the first vehicle record
                TsiWriter.Write((uint)0);
            else
            {
                TsiWriter.Write(TsiVehicleIndex);
                foreach (Vehicles Vehicle in vehicles)
                    Vehicle.WriteToTs0File(Ts0Writer, CurrentTimeStep);
                // Write out the "complete" message
                Ts0Writer.Write((uint)MessageNames.LG_Complete_GP);
                Ts0Writer.Write((uint)8); // Message length
                Ts0Writer.Write(CurrentTimeStep);
                Ts0Writer.Write((uint)RequestTypes.DR_TS_VEHICLE);
                Ts0Writer.Write((uint)1); // Request handle

            }
            if (signals == null) // Write out the index of the signal record
                TsiWriter.Write((uint)0);
            else
            {
                TsiWriter.Write((uint)Ts0Writer.BaseStream.Position);
                signals.WriteToTs0File(Ts0Writer, CurrentTimeStep);
                // Write out the "complete" message
                Ts0Writer.Write((uint)MessageNames.LG_Complete_GP);
                Ts0Writer.Write((uint)8); // Message length
                Ts0Writer.Write(CurrentTimeStep);
                Ts0Writer.Write((uint)RequestTypes.DR_TS_SIGNAL);
                Ts0Writer.Write((uint)1); // Request handle
            }
            vehicles.Clear();
            signals = null;
            return ++CurrentTimeStep;
        }
        /// <summary>
        /// You must call this when you are finished with this object so the files can be fully written and properly closed
        /// </summary>
        /// <param name="TsiFilePath">Returns the file path of the tsi file</param>
        /// <param name="Ts0FilePath">Returns the file path of the ts0 file</param>
        public void FinishAndClose(ref string TsiFilePath, ref string Ts0FilePath)
        {
            if (Finished) return;
            FinishAndClose();
            TsiFilePath = this.TsiFilePath;
            Ts0FilePath = this.Ts0FilePath;
        }
        /// <summary>
        /// You must call this when you are finished with this object so the files can be fully written and properly closed
        /// </summary>
        public void FinishAndClose()
        {
            if (Finished) return;
            BeginNewTimeStep();
            Ts0Writer.Close();
            TsiWriter.Close();
            Finished = true;
        }
        /// <summary>
        /// Add the information about a signal for the current time step
        /// </summary>
        /// <param name="LinkID">ID of the link under signal control</param>
        /// <param name="LeftTurnCode">Signal code for left turns: red, yellow, protected green, green, none</param>
        /// <param name="LeftDiagonalTurnCode">Signal code for left diagonal turns: red, yellow, protected green, green, none</param>
        /// <param name="ThroughCode">Signal code for through movements: red, yellow, protected green, green, none</param>
        /// <param name="RightDiagonalTurnCode">Signal code for right diagonal turns: red, yellow, protected green, green, none</param>
        /// <param name="RightTurnCode">Signal code for right turns: red, yellow, protected green, green, none</param>
        public void AddSignal(uint LinkID, SignalCodes LeftTurnCode, SignalCodes LeftDiagonalTurnCode, SignalCodes ThroughCode,
            SignalCodes RightDiagonalTurnCode, SignalCodes RightTurnCode)
        {
            if (Finished) return;
            if (signals == null)
                signals = new Signals();
            signal MySignal = new signal();
            signals.signals.Add(MySignal);
            MySignal.LinkID = LinkID;
            MySignal.LeftTurnCode = (ushort)LeftTurnCode;
            MySignal.LeftDiagonalTurnCode = (ushort)LeftDiagonalTurnCode;
            MySignal.ThroughCode = (ushort)ThroughCode;
            MySignal.RightDiagonalTurnCode = (ushort)RightDiagonalTurnCode;
            MySignal.RightTurnCode = (ushort)RightTurnCode;
       }
        /// <summary>
        /// Add the information about a vehicle for the current time step
        /// </summary>
        /// <param name="InstanceID">ID of link containing vehicles for which this message is reporting</param>
        /// <param name="VehicleID">ID of vehicle</param>
        /// <param name="Fleet">Auto, truck, carpool, bus</param>
        /// <param name="VehicleType">CORSIM vehicle type, 1 to 9, see page 213 & 257 of CORSIM Reference Manual</param>
        /// <param name="VehicleLength">Vehicle length in feet</param>
        /// <param name="DriverType">CORSIM driver type, 1 to 10</param>
        /// <param name="LaneID">CORSIM ID of lane in which vehicle is traveling</param>
        /// <param name="VehiclePosition">Vehicle’s distance from the upstream end of the link in feet</param>
        /// <param name="PreviousUSN">Upstream node ID of the previous link the vehicle traveled</param>
        /// <param name="TurnCode">vehicle turn code: left, through, right, left diagonal, right diagonal, source emission</param>
        /// <param name="QueueStatus">Vehicle is currently not in queue -OR- Vehicle is currently in queue</param>
        /// <param name="Acceleration">Vehicle’s instantaneous acceleration in feet/second/second</param>
        /// <param name="Velocity">vehicle’s instantaneous velocity in feet/second</param>
        /// <param name="LaneChangeStatus">Vehicle does not want to change lanes -OR- Vehicle wants to change lanes</param>
        /// <param name="TargetLane">CORSIM ID of lane vehicle would like to occupy</param>
        /// <param name="DestinationNode">Node ID of the vehicles destination node</param>
        /// <param name="LeaderVehicleID">Global ID of vehicle’s leader vehicle</param>
        /// <param name="FollowerVehicleID">Global ID of vehicle’s follower vehicle</param>
        /// <param name="PreviousLaneID">Lane ID of the lane that the vehicle was previously in</param>
        public void AddVehicle(uint InstanceID, uint VehicleID, FleetTypes Fleet, byte VehicleType, byte VehicleLength, byte DriverType,
            byte LaneID, int VehiclePosition, ushort PreviousUSN, VehicleTurnCodes TurnCode, QueueStatuses QueueStatus, sbyte Acceleration, byte Velocity,
            LaneChangeStatuses LaneChangeStatus, byte TargetLane, ushort DestinationNode, uint LeaderVehicleID, uint FollowerVehicleID, byte PreviousLaneID)
        {
            Vehicles vehicle = null;
            for (int i = 0; i < vehicles.Count; i++)
            {
                if (vehicles[i].InstanceID == InstanceID)
                {
                    vehicle = vehicles[i];
                    break;
                }
            }
            if (vehicle == null)
            {
                vehicle = new Vehicles();
                vehicle.InstanceID = InstanceID;
                vehicles.Add(vehicle);
            }
            Vehicle newVehicle = new Vehicle();
            vehicle.vehicles.Add(newVehicle);
            newVehicle.GlobalVehicleID = VehicleID;
            newVehicle.Fleet = (byte)Fleet;
            newVehicle.VehicleType = VehicleType;
            newVehicle.VehicleLength = VehicleLength;
            newVehicle.DriverType = DriverType;
            newVehicle.LaneID = LaneID;
            newVehicle.VehiclePosition = VehiclePosition;
            newVehicle.PreviousUSN = PreviousUSN;
            newVehicle.TurnCode = (byte)TurnCode;
            newVehicle.QueueStatus = (byte)QueueStatus;
            newVehicle.Acceleration = Acceleration;
            newVehicle.Velocity = Velocity;
            newVehicle.LaneChangeStatus = (byte)LaneChangeStatus;
            newVehicle.TargetLane = TargetLane;
            newVehicle.DestinationNode = DestinationNode;
            newVehicle.LeaderVehicleID = LeaderVehicleID;
            newVehicle.FollowerVehicleID = FollowerVehicleID;
            newVehicle.PreviousLaneID = PreviousLaneID;
        }
    }
}
