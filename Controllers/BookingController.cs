using EventstoreBasics.Application;
using EventstoreBasics.Domain;
using EventstoreBasics.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EventstoreBasics.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
  private readonly ILogger<BookingController> _logger;

  private readonly IAggregateStore _store;

  public BookingController(ILogger<BookingController> logger, IAggregateStore store)
  {
    _logger = logger;
    _store = store;
  }

  [HttpPost]
  [Route("book")]
  public async Task<string> Book([FromBody] Commands.Book command)
  {

    Booking booking = new Booking(command.BookingId);
    booking.CreateBooking(command.HotelId, command.customerId, command.From, command.To);

    await _store.Store(booking);

    return "Done";
  }

  [HttpPost]
  [Route("pay")]
  public async Task<string> ConfirmPayment([FromBody] Commands.Pay command)
  {

    Booking booking = await _store.Load<Booking>(command.BookingId);

    booking.ConfirmPayment(command.Paid);

    await _store.Store(booking);

    return "Done";
  }
}
