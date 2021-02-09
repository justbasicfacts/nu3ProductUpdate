using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;
using System.Linq;

namespace nu3ProductUpdate.Classes
{
    public class AllowedFileExtensionAttribute : ActionFilterAttribute
    {
        private readonly string[] _extensions;

        public AllowedFileExtensionAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid && context.ActionArguments.ContainsKey("productData"))
            {
                var productData = context.ActionArguments["productData"] as IFormFile;
                if (productData != null)
                {
                    var extension = Path.GetExtension(productData.FileName);
                    if (extension != null)
                    {
                        if (_extensions.Contains(extension.ToLower().Replace(".", "")))
                        {
                            return;
                        }
                    }
                }
            }

            context.Result = new BadRequestResult();
        }
    }
}