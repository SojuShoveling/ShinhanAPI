using AxGIEXPERTCONTROLLib;
using System;

namespace ShinhanAPI
{
    public class Indi : IDisposable
    {
        public AxGiExpertControl I_ndi { get; internal set; }

        public Input Input { get; internal set; }
        public Output Output { get; internal set; }

        public Indi()
        {
            I_ndi = new AxGiExpertControl
            {
                OcxState = (System.Windows.Forms.AxHost.State)Ocx.OcxState
            };

            I_ndi.CreateControl();

            Input = new Input(I_ndi);
            Output = new Output(I_ndi);
        }

        public void Dispose()
        {
            if (I_ndi != null)
                I_ndi.Dispose();
        }
    }
}