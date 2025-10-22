using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Logix.MVC.Filters
{
    public class TrackingModelBinder : IModelBinder
    {
        private readonly IModelBinder _innerBinder;

        public TrackingModelBinder(IModelBinder innerBinder)
        {
            _innerBinder = innerBinder;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // Perform pre-binding tracking logic here
            Console.WriteLine("Executing pre-binding tracking logic...");

            // Call the inner binder to perform the actual binding
            await _innerBinder.BindModelAsync(bindingContext);

            // Perform post-binding tracking logic here
            Console.WriteLine("Executing post-binding tracking logic...");
        }
    }
}
