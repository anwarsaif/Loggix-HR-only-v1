using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Logix.MVC.Filters
{

    public class ModifyDecimalInputsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            /* CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

             foreach (CultureInfo culture in cultures)
             {
                 NumberFormatInfo numberFormat = culture.NumberFormat;
                 string decimalSeparator = numberFormat.NumberDecimalSeparator;

                 Console.WriteLine($"{culture.Name}: {decimalSeparator}");
             }*/
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument != null)
                {
                    var argumentType = argument.GetType();
                    var properties = argumentType.GetProperties();

                    foreach (var property in properties)
                    {
                        if (property.PropertyType == typeof(decimal))
                        {
                            string propertyName = property.Name;
                            var valid = context.ModelState.GetValidationState(propertyName);
                            if (valid != ModelValidationState.Valid)
                            {
                                var raw = context.ModelState?.GetValueOrDefault(propertyName)?.RawValue;
                                if (raw != null && !string.IsNullOrEmpty(raw.ToString()))
                                {
                                    var chk = IsValidDecimalFormat(raw.ToString());

                                    if (!chk)
                                    {
                                        var correct = CorrectDecimalFormat(raw.ToString());
                                        try
                                        {
                                            var corrDecimal = decimal.Parse(correct, CultureInfo.CurrentCulture);
                                            property.SetValue(argument, corrDecimal);
                                            context.ModelState.SetModelValue(propertyName, new ValueProviderResult(correct));

                                            // Clear the error message for the property
                                            context.ModelState.Remove(propertyName);
                                            // context.ModelState.SetModelValue("najmi",corrDecimal, correct);
                                        }
                                        catch { }
                                    }

                                }
                            }

                            // Modify the decimal value here as per your requirements
                            // decimal modifiedValue = propertyValue * 2;

                            // property.SetValue(argument, decimal.Parse(raw1.ToString(), CultureInfo.CurrentCulture));

                            // Log the modification if needed
                            //  Console.WriteLine($"!!!!!!!!!!!!!! Modified decimal property '{propertyName}' from {propertyValue} to {modifiedValue}");
                        }
                    }
                }
            }

            base.OnActionExecuting(context);
        }

        private bool IsValidDecimalNumbers(string value)
        {
            var regexPattern = @"^[0-9.]+$";
            var regex = new Regex(regexPattern);

            return regex.IsMatch(value);
        }
        private bool IsValidDecimalFormat(string value)
        {
            decimal result;
            return decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out result);
        }

        private string CorrectDecimalFormat(string value)
        {
            decimal result;

            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                return result.ToString(CultureInfo.CurrentCulture);
            }
            else
            {
                // If the value cannot be parsed, return the original value
                return value;
            }
        }
    }
}
