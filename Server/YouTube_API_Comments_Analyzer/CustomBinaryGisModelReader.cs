using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SharpEntropy.IO
{
    public class BinaryGisModelReader : GisModelReader
    {
        private readonly Stream _input;
        private readonly byte[] _buffer;
        private int _stringLength;
        private readonly Encoding _encoding = Encoding.UTF8;

        public BinaryGisModelReader(Stream dataInputStream)
        {
            _input = dataInputStream;
            _buffer = new byte[256];
            base.ReadModel();
        }

        public BinaryGisModelReader(string fileName)
        {
            _input = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            _buffer = new byte[256];
            base.ReadModel();
        }

        protected override int ReadInt32()
        {
            _input.Read(_buffer, 0, 4);
            return BitConverter.ToInt32(_buffer, 0);
        }

        protected override double ReadDouble()
        {
            _input.Read(_buffer, 0, 8);
            return BitConverter.ToDouble(_buffer, 0);
        }

        protected override string ReadString()
        {
            _stringLength = _input.ReadByte();
            _input.Read(_buffer, 0, _stringLength);
            return _encoding.GetString(_buffer, 0, _stringLength);
        }

        protected void ReadPredicates(out int[][] outcomePatterns, out Dictionary<string, CustomPatternPredicate> predicates)
        {
            int num = ReadInt32();
            outcomePatterns = new int[num][];
            predicates = new Dictionary<string, CustomPatternPredicate>(ReadInt32());
            for (int i = 0; i < num; i++)
            {
                int num2 = ReadInt32();
                outcomePatterns[i] = new int[num2];
                for (int j = 0; j < num2; j++)
                {
                    outcomePatterns[i][j] = ReadInt32();
                }

                for (int k = 0; k < outcomePatterns[i][0]; k++)
                {
                    string key = ReadString();
                    double[] array = new double[num2 - 1];
                    for (int l = 0; l < num2 - 1; l++)
                    {
                        array[l] = ReadDouble();
                    }

                    predicates.Add(key, new CustomPatternPredicate(i, array));
                }
            }
        }

        public void Dispose()
        {
            _input?.Dispose();
        }
    }
}
