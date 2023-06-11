using EventstoreBasics.Infrastructure;

namespace EventstoreBasics.Domain;

public class Booking : Entity
{
  string _customerId;
  string _hotelId;

  BookingStatus _status;

  DateTimeOffset _from;
  DateTimeOffset _to;
  private string _id;
  private bool _paid;

  public Booking(string id) => _id = id;
  public void CreateBooking(string hotelId, string customerId, DateTimeOffset from, DateTimeOffset to)
  {
    Apply(new Events.BookingCreated()
    {
      BookingId = _id,
      HotelId = hotelId,
      customerId = customerId,
      From = from,
      To = to
    });

  }

  public void ConfirmPayment(bool paid)
  {
    Apply(new Events.BookingPaid()
    {
      PaidStatus = paid
    });
  }

  protected override void When(object @event)
  {
    switch (@event)
    {
      case Events.BookingCreated e:
        _hotelId = e.HotelId;
        _customerId = e.customerId;
        _to = e.To;
        _from = e.From;
        break;
      case Events.BookingPaid e:
        _paid = e.PaidStatus;
        _id = e.BookingId;
        break;

    }
  }

  public override string GetId()
  {
    return _id;
  }
}

public enum BookingStatus
{
  Booked,
  Confirmed,
  Paid,
  Cancelled
}