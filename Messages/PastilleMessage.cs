#nullable disable
using BigBazar.Controls;

namespace BigBazar.Messages
{
    public class PastilleMessage(object sender, object pload, PayloadKind kind = PayloadKind.Delete)
        : IDisposable
    {
        public object Sender { get; set; } = sender;
        public object Payload { get; set; } = pload;
        public PayloadKind Kind { get; set; } = kind;

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                  Payload = null;
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PastilleMessage()
        {
            Dispose(false);
        }
    }

}
