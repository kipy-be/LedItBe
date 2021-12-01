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
        private LedColor[] _data;

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
            _data = new LedColor[_ledCount * _bytesPerLed];

            for (int i = 0; i < _data.Length; i++)
            {
                _data[i] = LedColor.Black;
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
                        fragment[u] = _data[ledNum].W;
                        fragment[++u] = _data[ledNum].R;
                        fragment[++u] = _data[ledNum].G;
                        fragment[++u] = _data[ledNum].B;
                    }
                    else
                    {
                        fragment[u] = _data[ledNum].R;
                        fragment[++u] = _data[ledNum].G;
                        fragment[++u] = _data[ledNum].B;
                    }
                    
                    ledNum++;
                }

                res.Add(fragment);
            }

            return res;
        }
    }
}
