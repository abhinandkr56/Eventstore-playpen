namespace EventstoreBasics.Application;

public static class Commands
{

  public class Book
  {

    public string BookingId { get; set; }
    public string HotelId { get; set; }

    public string customerId { get; set; }

    public DateTimeOffset From { get; set; }

    public DateTimeOffset To { get; set; }

  }

  public class Pay
  {
    public string BookingId { get; set; }
    public bool Paid { get; set; }
  }
}