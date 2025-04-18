namespace BoardMate.Views
{
    public class RoomItem
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public decimal RoomPrice { get; set; }

        public override string ToString()
        {
            return RoomNumber; 
        }
    }
}