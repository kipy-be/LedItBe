using System.Collections.Generic;

namespace LedItBe.Core.Common
{
    public class Frame
    {
        private const int FRAGMENT_SIZE = 900;

        private int _ledCount;
        private int _bytesPerLed;
        private int _dataSize;
        private int _fragmentCount;
        private int _lastFragmentSize;
        private LedProfile _colorMode;

        public LedColor[] Data { get; private set; }

        private Frame(int ledCount, LedProfile colorMode)
        {
            _ledCount = ledCount;
            _colorMode = colorMode;
            _bytesPerLed = _colorMode == LedProfile.RGB ? 3 : 4;
            _dataSize = _ledCount * _bytesPerLed;
            _fragmentCount = (_dataSize / FRAGMENT_SIZE) + 1;
            _lastFragmentSize = _dataSize - FRAGMENT_SIZE * (_fragmentCount - 1);

            InitData();
        }

        private void InitData()
        {
            Data = new LedColor[_ledCount * _bytesPerLed];

            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = LedColor.Black;
            }
        }

        public void Clear()
        {
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i].R = 0;
                Data[i].G = 0;
                Data[i].B = 0;
                Data[i].W = 0;
            }
        }

        public static Frame Create(int ledCount, LedProfile colorMode)
        {
            return new Frame(ledCount, colorMode);
        }

        internal List<byte[]> ToFragments()
        {
            var res = new List<byte[]>();

            byte[] fragment;
            int last = _fragmentCount - 1;
            int ledNum = 0;

            for (int i = 0; i < _fragmentCount; i++)
            {
                fragment = new byte[i == last ? _lastFragmentSize : FRAGMENT_SIZE];

                for (int u = 0; u < fragment.Length; u++)
                {
                    if (_bytesPerLed == 4)
                    {
                        fragment[u] = Data[ledNum].W;
                        fragment[++u] = Data[ledNum].R;
                        fragment[++u] = Data[ledNum].G;
                        fragment[++u] = Data[ledNum].B;
                    }
                    else
                    {
                        fragment[u] = Data[ledNum].R;
                        fragment[++u] = Data[ledNum].G;
                        fragment[++u] = Data[ledNum].B;
                    }
                    
                    ledNum++;
                }

                res.Add(fragment);
            }

            return res;
        }
    }
}
