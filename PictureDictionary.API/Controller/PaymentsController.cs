using Microsoft.AspNetCore.Mvc;
using Stripe;
//using Stripe.BillingPortal;
using Stripe.Checkout;

using Stripe.Forwarding;
using System.Threading.Tasks;

namespace PictureDictionary.API.Controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        public PaymentsController() { }

        //create session
        [HttpPost("createsession")]
        public async Task<IActionResult> CreateSession()
        {
            //StripeConfiguration.ApiKey = "pk_test_51QkmrhINR5l65ygWdQCyAT1mHw4fM8tbwy8A0Sq6NnwnftL7oEcUDMvUFiLTBlsFWzfiMTB8JXUM8D0jeqKnabC200vWIxys4u";
            StripeConfiguration.ApiKey = "sk_test_51QkmrhINR5l65ygWybPGBG43dLKrfi9LTtCuWQxXVeQwyP4HqmHRJoQVJeOgKlAL7rIQYsWHLznORtfoFL7QTEZK00Hpnu4qGl";
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = "https://example.com/success",
                LineItems = new List<Stripe.Checkout.SessionLineItemOptions>
                {
                    new Stripe.Checkout.SessionLineItemOptions
                    {
                        Price = "price_1Qnel0INR5l65ygWIDOMScnG",
                        Quantity = 1,
                    },
                },
                Mode = "payment",
            };
            var service = new Stripe.Checkout.SessionService();
            return Ok(await service.CreateAsync(options));
        }

        // Set your secret key. Remember to switch to your live secret key in production.
        // See your keys here: https://dashboard.stripe.com/apikeys
        //StripeConfiguration.ApiKey = "sk_test_51QkmrhINR5l65ygWybPGBG43dLKrfi9LTtCuWQxXVeQwyP4HqmHRJoQVJeOgKlAL7rIQYsWHLznORtfoFL7QTEZK00Hpnu4qGl";

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            Event stripeEvent;
            try
            {
                var webhookSecret = "{{STRIPE_WEBHOOK_SECRET}}";
                stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    webhookSecret
                );
                Console.WriteLine($"Webhook notification with type: {stripeEvent.Type} found for {stripeEvent.Id}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something failed {e}");
                return BadRequest();
            }

            switch (stripeEvent.Type)
            {
                case "checkout.session.completed":
                    // Payment is successful and the subscription is created.
                    // You should provision the subscription and save the customer ID to your database.
                    break;
                case "invoice.paid":
                    // Continue to provision the subscription as payments continue to be made.
                    // Store the status in your database and check when a user accesses your service.
                    // This approach helps you avoid hitting rate limits.
                    break;
                case "invoice.payment_failed":
                    // The payment failed or the customer does not have a valid payment method.
                    // The subscription becomes past_due. Notify your customer and send them to the
                    // customer portal to update their payment information.
                    break;
                default:
                    // Unhandled event type
                    break;
            }

            return Ok();
        }

        [HttpGet("products")]
        public async Task<IActionResult> Products()
        {
            //StripeConfiguration.ApiKey = "pk_test_51QkmrhINR5l65ygWdQCyAT1mHw4fM8tbwy8A0Sq6NnwnftL7oEcUDMvUFiLTBlsFWzfiMTB8JXUM8D0jeqKnabC200vWIxys4u";
            StripeConfiguration.ApiKey = "sk_test_51QkmrhINR5l65ygWybPGBG43dLKrfi9LTtCuWQxXVeQwyP4HqmHRJoQVJeOgKlAL7rIQYsWHLznORtfoFL7QTEZK00Hpnu4qGl";

            var options = new ProductListOptions { Limit = 3 };
            var service = new ProductService();
            StripeList<Product> products = service.List(options);
            return Ok(products);
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut()
        {
            StripeConfiguration.ApiKey = "sk_test_51QkmrhINR5l65ygWybPGBG43dLKrfi9LTtCuWQxXVeQwyP4HqmHRJoQVJeOgKlAL7rIQYsWHLznORtfoFL7QTEZK00Hpnu4qGl";

            var options = new SessionCreateOptions
            {
                //SuccessUrl = "http://localhost:4200/StaticDictionary/payment-success",
                SuccessUrl = "https://ankitvishwaintern.github.io/StaticDictionary/docs/browser/?isSuccess=1&isfailed=0",

                //CancelUrl = "http://localhost:4200/StaticDictionary/payment-cancel",
                CancelUrl = "https://ankitvishwaintern.github.io/StaticDictionary/docs/browser/payment-cancel",
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                Mode = "payment",
            };

            var sessionListItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    //PriceData = new SessionLineItemPriceDataOptions
                    //{
                    //    Currency = "usd",
                    //    ProductData = new SessionLineItemPriceDataProductDataOptions
                    //    {
                    //        Name = "Basic Subscription",
                    //    },
                    //    UnitAmount = 1,
                    //},
                    Price = "price_1Qnel0INR5l65ygWIDOMScnG",
                    Quantity = 1,
                    
                    
                },
            };

            options.LineItems= new List<SessionLineItemOptions>();
            options.LineItems.Add(sessionListItems[0]);
            var service = new SessionService();
            Session session = service.Create(options);
            Response.Headers.Add("Location", session.Url);
            return Ok(session);
        }


    }
}