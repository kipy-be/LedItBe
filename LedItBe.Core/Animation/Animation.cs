using LedItBe.Core.Common;
using LedItBe.Core.Devices;
using LedItBe.Core.Utils.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace LedItBe.Core.Animation
{
    internal abstract class Animation
    {
        protected Device _device;
        private int _fps;
        private int _delay;

        private bool _frameFlip = false;
        private Frame _frameA;
        private Frame _frameB;

        private bool _started;
        private ManualResetEvent _terminate = new ManualResetEvent(false);

        public Animation(Device device, int fps = 25)
        {
            _device = device;
            Fps = fps;

            _frameA = Frame.Create(_device.Infos.LedCount, _device.Infos.LedProfile);
            _frameB = Frame.Create(_device.Infos.LedCount, _device.Infos.LedProfile);
        }


        public int Fps
        {
            get => _fps;
            set
            {
                _fps = value.RestrictTo(1, 60);
                _delay = (int)(1000.0 / _fps);
            }
        }

        public void Start()
        {
            if (_started)
            {
                return;
            }

            _terminate.Reset();

            Task.Run(() =>
            {
                _started = true;
                Process();
                _started = false;
            });

        }

        public void Stop()
        {
            if (!_started)
            {
                return;
            }

            _terminate.Set();
        }

        public void Dispose() => Stop();

        private async Task Prepare()
        {
            if (_device.LedOperationMode != LedOperationMode.Color)
            {
                await _device.ToStaticColorMode(LedColor.Black);
            }
            else
            {
                await _device.SetColor(LedColor.Black);
            }

            await _device.ToDirectMode();
        }

        private async void Process()
        {
            try
            {
                await Prepare();
                Frame frame;
                do
                {
                    frame = _frameFlip ? _frameA : _frameB;
                    frame.Clear();

                    _device.SendFrame(frame);
                    _frameFlip = !_frameFlip;

                } while (!_terminate.WaitOne(_delay));
            }
            catch
            { }
        }

        protected abstract void ProcessFrame(Frame frame);
    }
}
