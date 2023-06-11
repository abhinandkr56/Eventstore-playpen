namespace EventstoreBasics.Domain;

public static class Events
{
  public class BookingCreated
  {
    public string BookingId { get; set; }
    public string HotelId { get; set; }

    public string customerId { get; set; }

    public DateTimeOffset From { get; set; }

    public DateTimeOffset To { get; set; }
  }

  public class BookingPaid
  {
    public string BookingId { get; set; }

    public bool PaidStatus { get; set; }
  }
}