using ProduceTrafvuFilesLibrary;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace TSI_and_TS0_FileFormats
{
    public partial class TSDreader : Form
    {
        public TSDreader()
        {
            InitializeComponent();
        }
        public enum OpenPurposes { DisplayAll, DisplayVehicleType, WriteTest, TrackVehicle };
        public OpenPurposes OpenPurpose = OpenPurposes.DisplayAll;
        public uint VehicleID = 0;
        //private Boolean OpenedForWriteTest = false; // Otherwise opened for display

        private void openToTrackAVehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnterVehicleIDForm MyForm = new EnterVehicleIDForm();
            MyForm.ShowDialog();
            if (!uint.TryParse(MyForm.textBox_VehicleID.Text, out VehicleID))
            {
                MessageBox.Show("Something was wrong with your input");
                return;
            }
            OpenPurpose = OpenPurposes.TrackVehicle;
            DoTheWork();
        }
        private void openToShowVehicleTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPurpose = OpenPurposes.DisplayVehicleType;
            DoTheWork();
        }
        private void openForWriteTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPurpose = OpenPurposes.WriteTest;
            DoTheWork();
        }
        private void openForDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPurpose = OpenPurposes.DisplayAll;
            DoTheWork();
        }
        private void DoTheWork()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "tsi files (*.tsi)|*.tsi";
            DialogResult result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            String TsiFileName = openFileDialog.FileName;
            if (!File.Exists(TsiFileName))
            {
                MessageBox.Show("File does not exist", TsiFileName);
                return;
            }
            int NumberOfTs0Files = UtilityCalculations.CountTs0FilesFromTsiFile(TsiFileName);
            if (NumberOfTs0Files != 1)
            {
                MessageBox.Show("This program cannot handle more than one ts0 file", "Too many ts0 files");
                return;
            }
            // Now verify that the ts0 files exists
            String Ts0FileName = Path.ChangeExtension(TsiFileName, "ts0");
            if (!File.Exists(Ts0FileName))
            {
                MessageBox.Show("File does not exist", Ts0FileName);
                return;
            }
            TrafvuFilesBuilder OutputFiles = null;
            if (OpenPurpose == OpenPurposes.WriteTest)
                OutputFiles = new ProduceTrafvuFilesLibrary.TrafvuFilesBuilder();
            TsiAndTsoReader tsiAndTsoReader = new TsiAndTsoReader(TsiFileName);
            BaseMessage Message = null;
            //treeView_Messages.
            Cursor PreviousCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            if (treeView_Messages != null)
                treeView_Messages.Nodes.Clear(); // Clear out the tree node
            uint CurrentTimeStep = 0;
            StringBuilder AllVehicleTypes = new StringBuilder();
            StringBuilder TrackVehicleInfo = new StringBuilder();
            while (tsiAndTsoReader.GetNextMessage(ref Message))
            {
                if (OpenPurpose == OpenPurposes.WriteTest && Message.SimulationTime != CurrentTimeStep)
                    CurrentTimeStep = OutputFiles.BeginNewTimeStep();
                TreeNode RequestTypeNode = null;
                if (OpenPurpose == OpenPurposes.DisplayAll)
                {
                    RequestTypeNode = new TreeNode(Enum.GetName(typeof(RequestTypes), Message.RequestType) + " Time = " + Message.SimulationTime);
                    treeView_Messages.Nodes.Add(RequestTypeNode);
                    RequestTypeNode.Nodes.Add("File Index = " + Message.FileIndex);
                    RequestTypeNode.Nodes.Add("Message Name = " + Enum.GetName(typeof(MessageNames), Message.MessageName));
                    RequestTypeNode.Nodes.Add("Message Length = " + Message.MessageLength);
                    RequestTypeNode.Nodes.Add("Simulation Time = " + Message.SimulationTime);
                    RequestTypeNode.Nodes.Add("Request Type = " + Enum.GetName(typeof(RequestTypes), Message.RequestType));
                }
                switch ((MessageNames)Message.MessageName)
                {
                    case MessageNames.LG_Complete_GP:
                        CompleteMessage completeMessage = ((CompleteMessage)Message);
                        if (OpenPurpose == OpenPurposes.DisplayAll)
                            RequestTypeNode.Nodes.Add("Request Handle = " + completeMessage.RequestHandle);
                        break;
                    case MessageNames.LG_Data_GP:
                        switch ((RequestTypes)Message.RequestType)
                        {
                            case RequestTypes.DR_TS_VEHICLE:
                                VehicleMessage vehicleMessage = ((VehicleMessage)Message);
                                TreeNode VehiclesNode = null;
                                if (OpenPurpose == OpenPurposes.DisplayAll)
                                {
                                    RequestTypeNode.Nodes.Add("Request Handle = " + vehicleMessage.RequestHandle);
                                    RequestTypeNode.Nodes.Add("Class ID = " + Enum.GetName(typeof(ClassIDs), vehicleMessage.ClassID_1));
                                    RequestTypeNode.Nodes.Add("Action ID = " + Enum.GetName(typeof(ActionIDs), vehicleMessage.ActionID_1));
                                    RequestTypeNode.Nodes.Add("Attribute ID Count = " + vehicleMessage.AttributeIDCount_1);
                                    RequestTypeNode.Nodes.Add("Number of Aggregate Classes = " + vehicleMessage.NumberOfAggregateClasses_1);
                                    RequestTypeNode.Nodes.Add("Class ID = " + Enum.GetName(typeof(ClassIDs), vehicleMessage.ClassID_2));
                                    RequestTypeNode.Nodes.Add("Action ID = " + Enum.GetName(typeof(ActionIDs), vehicleMessage.ActionID_2));
                                    RequestTypeNode.Nodes.Add("Attribute ID Count = " + vehicleMessage.AttributeIDCount_2);
                                    RequestTypeNode.Nodes.Add("Attribute ID = " + vehicleMessage.AttributeID);
                                    RequestTypeNode.Nodes.Add("Number of Aggregate Classes = " + vehicleMessage.NumberOfAggregateClasses_2);
                                    RequestTypeNode.Nodes.Add("Instance ID Count = " + vehicleMessage.InstanceIDCount_1);
                                    RequestTypeNode.Nodes.Add("Instance ID = " + vehicleMessage.InstanceID);
                                    //RequestTypeNode.Nodes.Add("Instance ID Count = " + vehicleMessage.InstanceIDCount_2);
                                    VehiclesNode = new TreeNode("Instance ID Count (Number of vehicles) = " + vehicleMessage.InstanceIDCount_2);
                                    RequestTypeNode.Nodes.Add(VehiclesNode);
                                }
                                for (int i = 0; i < vehicleMessage.InstanceIDCount_2; i++)
                                {
                                    TreeNode VehicleNode = null;
                                    if (OpenPurpose == OpenPurposes.DisplayAll)
                                    {
                                        VehicleNode = new TreeNode("Global Vehicle ID = " + vehicleMessage.Vehicles[i].GlobalVehicleID);
                                        VehiclesNode.Nodes.Add(VehicleNode);
                                        VehicleNode.Nodes.Add("Fleet = " + Enum.GetName(typeof(FleetTypes), vehicleMessage.Vehicles[i].Fleet));
                                        VehicleNode.Nodes.Add("Vehicle Type = " + vehicleMessage.Vehicles[i].VehicleType);
                                        VehicleNode.Nodes.Add("Vehicle Length = " + vehicleMessage.Vehicles[i].VehicleLength);
                                        VehicleNode.Nodes.Add("Driver Type = " + vehicleMessage.Vehicles[i].DriverType);
                                        VehicleNode.Nodes.Add("Lane ID = " + vehicleMessage.Vehicles[i].LaneID);
                                        VehicleNode.Nodes.Add("Vehicle Position = " + vehicleMessage.Vehicles[i].VehiclePosition);
                                        VehicleNode.Nodes.Add("Previous USN = " + vehicleMessage.Vehicles[i].PreviousUSN);
                                        VehicleNode.Nodes.Add("Turn Code = " + Enum.GetName(typeof(VehicleTurnCodes), vehicleMessage.Vehicles[i].TurnCode));
                                        VehicleNode.Nodes.Add("Queue Status = " + Enum.GetName(typeof(QueueStatuses), vehicleMessage.Vehicles[i].QueueStatus));
                                        VehicleNode.Nodes.Add("Acceleration = " + vehicleMessage.Vehicles[i].Acceleration);
                                        VehicleNode.Nodes.Add("Velocity = " + vehicleMessage.Vehicles[i].Velocity);
                                        VehicleNode.Nodes.Add("Lane Change Status = " + Enum.GetName(typeof(LaneChangeStatuses), vehicleMessage.Vehicles[i].LaneChangeStatus));
                                        VehicleNode.Nodes.Add("Target Lane = " + vehicleMessage.Vehicles[i].TargetLane);
                                        VehicleNode.Nodes.Add("Destination Node = " + vehicleMessage.Vehicles[i].DestinationNode);
                                        VehicleNode.Nodes.Add("Leader Vehicle ID = " + vehicleMessage.Vehicles[i].LeaderVehicleID);
                                        VehicleNode.Nodes.Add("Follower Vehicle ID = " + vehicleMessage.Vehicles[i].FollowerVehicleID);
                                        VehicleNode.Nodes.Add("Previous Lane ID = " + vehicleMessage.Vehicles[i].PreviousLaneID);
                                    }
                                    else if (OpenPurpose == OpenPurposes.WriteTest)
                                    {
                                        OutputFiles.AddVehicle(vehicleMessage.InstanceID, vehicleMessage.Vehicles[i].GlobalVehicleID,
                                            (ProduceTrafvuFilesLibrary.FleetTypes) vehicleMessage.Vehicles[i].Fleet, vehicleMessage.Vehicles[i].VehicleType,
                                            vehicleMessage.Vehicles[i].VehicleLength, vehicleMessage.Vehicles[i].DriverType,
                                            vehicleMessage.Vehicles[i].LaneID, vehicleMessage.Vehicles[i].VehiclePosition,
                                            vehicleMessage.Vehicles[i].PreviousUSN, (ProduceTrafvuFilesLibrary.VehicleTurnCodes)vehicleMessage.Vehicles[i].TurnCode,
                                            (ProduceTrafvuFilesLibrary.QueueStatuses)vehicleMessage.Vehicles[i].QueueStatus, vehicleMessage.Vehicles[i].Acceleration,
                                            vehicleMessage.Vehicles[i].Velocity, (ProduceTrafvuFilesLibrary.LaneChangeStatuses)vehicleMessage.Vehicles[i].LaneChangeStatus,
                                            vehicleMessage.Vehicles[i].TargetLane, vehicleMessage.Vehicles[i].DestinationNode,
                                            vehicleMessage.Vehicles[i].LeaderVehicleID, vehicleMessage.Vehicles[i].FollowerVehicleID,
                                            vehicleMessage.Vehicles[i].PreviousLaneID);
                                    }
                                    else if (OpenPurpose == OpenPurposes.DisplayVehicleType)
                                    {
                                        AllVehicleTypes.Append(vehicleMessage.Vehicles[i].VehicleType.ToString() + " ");
                                    }
                                    else if (OpenPurpose == OpenPurposes.TrackVehicle)
                                    {
                                        if (vehicleMessage.Vehicles[i].GlobalVehicleID == VehicleID)
                                        {
                                            TrackVehicleInfo.Append("Time = " + vehicleMessage.SimulationTime.ToString("D3"));
                                            TrackVehicleInfo.Append(", Link Id = " + vehicleMessage.InstanceID.ToString("D5"));
                                            TrackVehicleInfo.Append(", Position = " + vehicleMessage.Vehicles[i].VehiclePosition.ToString("D5"));
                                            TrackVehicleInfo.Append(", Acceleration = " + (vehicleMessage.Vehicles[i].Acceleration >= 0 ? " " : ""));
                                            TrackVehicleInfo.Append(vehicleMessage.Vehicles[i].Acceleration.ToString("D2"));
                                            TrackVehicleInfo.Append(", Velocity = " + vehicleMessage.Vehicles[i].Velocity.ToString("D2"));
                                            TrackVehicleInfo.Append(", Upstream Node ID = " + vehicleMessage.Vehicles[i].PreviousUSN.ToString("D5"));
                                            TrackVehicleInfo.Append(", Destination Node ID = " + vehicleMessage.Vehicles[i].DestinationNode.ToString("D5"));
                                            TrackVehicleInfo.Append(", Turn Code = " + vehicleMessage.Vehicles[i].TurnCode.ToString());
                                            TrackVehicleInfo.Append(", Que Status = " + vehicleMessage.Vehicles[i].QueueStatus.ToString());
                                            TrackVehicleInfo.Append(", Lane Change Status = " + vehicleMessage.Vehicles[i].LaneChangeStatus.ToString());
                                            TrackVehicleInfo.Append(", Lane ID = " + vehicleMessage.Vehicles[i].LaneID.ToString("D2"));
                                            TrackVehicleInfo.Append(", Target Lane = " + vehicleMessage.Vehicles[i].TargetLane.ToString("D2"));
                                            TrackVehicleInfo.Append(", Previous Lane = " + vehicleMessage.Vehicles[i].PreviousLaneID.ToString("D2"));
                                            TrackVehicleInfo.Append(", Follower Vehicle = " + vehicleMessage.Vehicles[i].FollowerVehicleID.ToString("D5"));
                                            TrackVehicleInfo.Append(", Leader Vehicle = " + vehicleMessage.Vehicles[i].LeaderVehicleID.ToString("D5") + "\r\n");
                                        }
                                    }

                                }
                                break;
                            case RequestTypes.DR_TS_SIGNAL:
                                SignalMessage signalMessage = ((SignalMessage)Message);
                                TreeNode SignalsNode = null;;
                                if (OpenPurpose == OpenPurposes.DisplayAll)
                                {
                                    RequestTypeNode.Nodes.Add("Request Handle = " + signalMessage.RequestHandle);
                                    RequestTypeNode.Nodes.Add("Class ID = " + Enum.GetName(typeof(ClassIDs), signalMessage.ClassID));
                                    RequestTypeNode.Nodes.Add("Action ID = " + Enum.GetName(typeof(ActionIDs), signalMessage.ActionID));
                                    RequestTypeNode.Nodes.Add("Attribute ID Count = " + signalMessage.AttributeIDCount);
                                    RequestTypeNode.Nodes.Add("Attribute ID = " + signalMessage.AttributeID);
                                    RequestTypeNode.Nodes.Add("Number of Aggregate Classes = " + signalMessage.NumberOfAggregateClasses);
                                    //RequestTypeNode.Nodes.Add("Instance ID Count = " + signalMessage.InstanceIDCount);
                                    SignalsNode = new TreeNode("Instance ID Count (Number of signals) = " + signalMessage.InstanceIDCount);
                                    RequestTypeNode.Nodes.Add(SignalsNode);
                                }
                                for (int i = 0; i < signalMessage.InstanceIDCount; i++)
                                {
                                    if (OpenPurpose == OpenPurposes.DisplayAll)
                                    {
                                        TreeNode SignalNode = new TreeNode("Link ID = " + signalMessage.Signals[i].LinkID);
                                        SignalsNode.Nodes.Add(SignalNode);
                                        SignalNode.Nodes.Add("Left Turn Code = " + Enum.GetName(typeof(SignalCodes), signalMessage.Signals[i].LeftTurnCode));
                                        SignalNode.Nodes.Add("Left Diagonal Turn Code = " + Enum.GetName(typeof(SignalCodes), signalMessage.Signals[i].LeftDiagonalTurnCode));
                                        SignalNode.Nodes.Add("Through Code = " + Enum.GetName(typeof(SignalCodes), signalMessage.Signals[i].ThroughCode));
                                        SignalNode.Nodes.Add("Right Diagonal Turn Code = " + Enum.GetName(typeof(SignalCodes), signalMessage.Signals[i].RightDiagonalTurnCode));
                                        SignalNode.Nodes.Add("Right Turn Code = " + Enum.GetName(typeof(SignalCodes), signalMessage.Signals[i].RightTurnCode));
                                    }
                                    else
                                        OutputFiles.AddSignal(signalMessage.Signals[i].LinkID,
                                            (ProduceTrafvuFilesLibrary.SignalCodes)signalMessage.Signals[i].LeftTurnCode,
                                            (ProduceTrafvuFilesLibrary.SignalCodes)signalMessage.Signals[i].LeftDiagonalTurnCode,
                                            (ProduceTrafvuFilesLibrary.SignalCodes)signalMessage.Signals[i].ThroughCode,
                                            (ProduceTrafvuFilesLibrary.SignalCodes)signalMessage.Signals[i].RightDiagonalTurnCode,
                                            (ProduceTrafvuFilesLibrary.SignalCodes)signalMessage.Signals[i].RightTurnCode);
                                }
                                break;
                            case RequestTypes.DR_TS_RAMPMETER:
                                break;
                            case RequestTypes.DR_TS_INCIDENT:
                                IncidentMessage incidentMessage = ((IncidentMessage)Message);
                                break;
                            case RequestTypes.DR_TI_LINK:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            if (OpenPurpose == OpenPurposes.WriteTest)
            {
                string TsiFile = null, Ts0File = null;
                OutputFiles.FinishAndClose(ref TsiFile, ref Ts0File);
                TreeNode treeNode = new TreeNode(TsiFile);
                treeView_Messages.Nodes.Add(treeNode);
                treeNode = new TreeNode(Ts0File);
                treeView_Messages.Nodes.Add(treeNode);
            }
            Cursor.Current = PreviousCursor;
            if (OpenPurpose == OpenPurposes.DisplayVehicleType)
            {
                DisplayForm MyDisplayForm = new DisplayForm();
                MyDisplayForm.textBox1.ScrollBars = ScrollBars.Vertical;
                MyDisplayForm.textBox1.Text = AllVehicleTypes.ToString();
                MyDisplayForm.Show();
                //MessageBox.Show(AllVehicleTypes.ToString());
            }
            if (OpenPurpose == OpenPurposes.TrackVehicle)
            {
                DisplayForm MyDisplayForm = new DisplayForm();
                MyDisplayForm.textBox1.ScrollBars = ScrollBars.Both;
                MyDisplayForm.textBox1.WordWrap = false;
                MyDisplayForm.textBox1.Text = TrackVehicleInfo.ToString();
                MyDisplayForm.Show();
                //MessageBox.Show(AllVehicleTypes.ToString());
            }
        }
    }
}
