using System.IO;


namespace TSI_and_TS0_FileFormats
{
    public static class UtilityCalculations
    {
        public static int CountTs0FilesFromTsiFile(string TsiFile)
        {
            int NumberOfTs0Files = 1;
            uint index;
            try
            {
                using (BinaryReader binaryReader = new BinaryReader(File.Open(TsiFile, FileMode.Open)))
                {
                    long length = binaryReader.BaseStream.Length;
                    while (binaryReader.BaseStream.Position < length)
                    {
                        index = binaryReader.ReadUInt32(); // Read a ts0 index
                        if (((int)index + 1) > NumberOfTs0Files)
                            NumberOfTs0Files = (int)index + 1;
                        binaryReader.BaseStream.Seek((long)8, SeekOrigin.Current); // Skip the next 8 bytes
                    }
                }
            }
            catch
            {
                return 0; // Returning zero indicates error
            }
            return NumberOfTs0Files;
        }
    }
}
