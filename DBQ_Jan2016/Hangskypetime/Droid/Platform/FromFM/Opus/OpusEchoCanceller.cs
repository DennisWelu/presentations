using System;
using Android.OS;
using FM;
using FM.IceLink.WebRTC;

using Xamarin.Conference.WebRTC.AudioProcessing;

namespace Xamarin.Conference.WebRTC.Opus
{
	public class OpusEchoCanceller
    {
        private static bool x86 = false;
        static OpusEchoCanceller()
        {
            if (Build.CpuAbi.ToLower().Contains("x86"))
            {
                x86 = true;
            }
        }

		public AcousticEchoCanceller AcousticEchoCanceller { get; set; }
		public AudioMixer AudioMixer { get; set; }

        private bool Enabled;

		public OpusEchoCanceller(int clockRate, int channels)
		{
            Enabled = !x86;
            if (Enabled)
            {
                AcousticEchoCanceller = new AcousticEchoCanceller(clockRate, channels, 300);
                AudioMixer = new AudioMixer(clockRate, channels, 20);
                AudioMixer.OnFrame += OnAudioMixerFrame;
            }
		}

		public void Start()
        {
            if (Enabled)
            {
                AudioMixer.Start();
            }
		}

		public void Stop()
        {
            if (Enabled)
            {
                AudioMixer.Stop();
            }
		}

		public byte[] capture(AudioBuffer input)
        {
            if (Enabled)
            {
                return AcousticEchoCanceller.Capture(input.Data, input.Index, input.Length);
            }
            else
            {
                return BitAssistant.SubArray(input.Data, input.Index, input.Length);
            }
		}

		public void render(String peerId, AudioBuffer echo)
        {
            if (Enabled)
            {
                AudioMixer.AddSourceFrame(peerId, new AudioBuffer(echo.Data, echo.Index, echo.Length));
            }
		}

		private void OnAudioMixerFrame(AudioBuffer echoMixed)
        {
            if (Enabled)
            {
                AcousticEchoCanceller.Render(echoMixed.Data, echoMixed.Index, echoMixed.Length);
            }
		}
	}
}

