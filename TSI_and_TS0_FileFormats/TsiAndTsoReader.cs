using System;
using System.IO;


namespace TSI_and_TS0_FileFormats
{
    public class TsiAndTsoReader
    {
        private string TsiFileName;
        private BinaryReader Ts0Reader;
        public TsiAndTsoReader(string TsiFileName)
        {
            this.TsiFileName = TsiFileName;
            Ts0Reader = new BinaryReader(File.Open(Path.ChangeExtension(TsiFileName, "ts0"), FileMode.Open));
            Ts0Reader.BaseStream.Seek(16, SeekOrigin.Begin); // Skip the file header
        }
        public Boolean GetNextMessage(ref BaseMessage baseMessage)
        {
            if (Ts0Reader.BaseStream.Position >= Ts0Reader.BaseStream.Length)
                return false;
            Boolean HaveMessage = false;
            BaseMessage TempBaseMessage = new BaseMessage(Ts0Reader);
            while (!HaveMessage)
            {
                if (Ts0Reader.BaseStream.Position >= Ts0Reader.BaseStream.Length)
                    return false;
                switch ((MessageNames)TempBaseMessage.MessageName)
                {
                    case MessageNames.LG_Complete_GP:
                        baseMessage = new CompleteMessage(TempBaseMessage, Ts0Reader);
                        HaveMessage = true;
                        break;
                    case MessageNames.LG_Data_GP:
                        switch ((RequestTypes)TempBaseMessage.RequestType)
                        {
                            case RequestTypes.DR_TS_VEHICLE:
                                baseMessage = new VehicleMessage(TempBaseMessage, Ts0Reader);
                                HaveMessage = true;
                                break;
                            case RequestTypes.DR_TS_SIGNAL:
                                baseMessage = new SignalMessage(TempBaseMessage, Ts0Reader);
                                HaveMessage = true;
                                break;
                            case RequestTypes.DR_TS_RAMPMETER:
                                Ts0Reader.BaseStream.Seek(TempBaseMessage.MessageLength - 4, SeekOrigin.Current); // Skip this message
                                break;
                            case RequestTypes.DR_TS_INCIDENT:
                                Ts0Reader.BaseStream.Seek(TempBaseMessage.MessageLength - 4, SeekOrigin.Current); // Skip this message
                                //baseMessage = new IncidentMessage(TempBaseMessage, Ts0Reader);
                                break;
                            case RequestTypes.DR_TI_LINK:
                                Ts0Reader.BaseStream.Seek(TempBaseMessage.MessageLength - 4, SeekOrigin.Current); // Skip this message
                                break;
                            default:
                                return false;
                        }
                        break;
                    default:
                        return false;
                }


            }
            return true;
        }
    }
}
