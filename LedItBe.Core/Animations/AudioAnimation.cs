using LedItBe.Core.Common;
using LedItBe.Core.Devices;
using NAudio.CoreAudioApi;
using System;

namespace LedItBe.Core.Animations
{
    internal class AudioAnimation : Animation
    {
        private MMDevice _audioDevice;
        private int _leftPeak;
        private int _rightPeak;
        private double _maxPeakRatio;
        private int _maxPeak;

        private LedColor[] _colors = new LedColor[]
            {
                LedColor.FromExactRgb(0xFF, 0x00, 0x00),
                LedColor.FromExactRgb(0xFF, 0x80, 0x15),
                LedColor.FromExactRgb(0xff, 0xff, 0xff)
            };

        public AudioAnimation(Device device, int fps = 50)
            : base(device, fps)
        {}

        protected override void Init()
        {
            var devEnum = new MMDeviceEnumerator();
            _audioDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            _maxPeak = _device.Infos.LedCount;
            _maxPeak = 360;
        }

        protected override void ProcessFrame(Frame frame)
        {
            _leftPeak = (int)(_audioDevice.AudioMeterInformation.PeakValues[0] * _maxPeak);
            _rightPeak = (int)(_audioDevice.AudioMeterInformation.PeakValues[1] * _maxPeak);

            _maxPeakRatio = Math.Max(Math.Max(_leftPeak, _rightPeak) / (double)_maxPeak, 0.5);

            int m = (_leftPeak + _rightPeak) / 2;
            int step = (m / 3) * 2;

            for (int i = 0, u = 0; i < m; i++)
            {
                frame.Data[i].SetColor
                (
                    (byte)(_colors[u].R * _maxPeakRatio),
                    (byte)(_colors[u].G * _maxPeakRatio),
                    (byte)(_colors[u].B * _maxPeakRatio)
                );

                if (i == step
                    && u < _colors.Length - 1)
                {
                    ++u;
                    step = (m / 10) * 9;
                }
            }
        }
    }
}
