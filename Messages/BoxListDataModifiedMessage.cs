#nullable disable
namespace BigBazar.Messages
{
    public class BoxListDataModifiedMessage
    {
        public string ViewModelName { get; set; }
        public object MessageData { get; set; }
    }

}
