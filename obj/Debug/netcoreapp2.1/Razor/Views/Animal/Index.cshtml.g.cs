#pragma checksum "C:\Alan\asp_net_projects\AnimalCatalog\AnimalCatalogSqLite\Views\Animal\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "09b2417029a6a60120b8fde5cecd7abe9f23f999"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AnimalCatalogSqLite.Animal.Views_Animal_Index), @"mvc.1.0.view", @"/Views/Animal/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Animal/Index.cshtml", typeof(AnimalCatalogSqLite.Animal.Views_Animal_Index))]
namespace AnimalCatalogSqLite.Animal
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"09b2417029a6a60120b8fde5cecd7abe9f23f999", @"/Views/Animal/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"28f380611c5c4fb95b747f2953fac75de328c22a", @"/Views/_ViewImports.cshtml")]
    public class Views_Animal_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Models.Animal>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "C:\Alan\asp_net_projects\AnimalCatalog\AnimalCatalogSqLite\Views\Animal\Index.cshtml"
  
    Layout = "_Layout";
    ViewData["Title"] = "Animals";

#line default
#line hidden
            BeginContext(70, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(100, 1675, true);
            WriteLiteral(@"
<section class=""list-container"">
    <div class=""modal fade"" id=""userRemoveModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
        <div class=""modal-dialog"">
            <div class=""modal-content"">
                <div class=""modal-header"">
                <h5 class=""modal-title"" id=""exampleModalLabel"">Remove user</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
                </div>
                <div class=""modal-body"">
                    <span>Confirm exclusion?</span>
                </div>
                <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"">Cancel</button>
                <button type=""button"" class=""btn btn-danger"">Confirm</button>
                </div>
            </div>
        </div>
    </div>
    <h2>Animal</h2>
    <ul c");
            WriteLiteral(@"lass=""list"">
        <li class=""list-row list-header"">
            <div class=""list-item"">
                <span class=""list-data"">Id</span>
            </div>
            <div class=""list-item"">
                <span class=""list-data"">Name</span>
            </div>
            <div class=""list-item"">
                <span class=""list-data"">Scientific Name</span>
            </div>
            <div class=""list-item"">
                <span class=""list-data list-btn"">Edit</span>
            </div>
            <div class=""list-item"">
                <span class=""list-data list-btn"">Remove</span>
            </div>
        </li>
");
            EndContext();
#line 48 "C:\Alan\asp_net_projects\AnimalCatalog\AnimalCatalogSqLite\Views\Animal\Index.cshtml"
         foreach (var animal in Model)
        {
            

#line default
#line hidden
            BeginContext(1839, 45, false);
#line 50 "C:\Alan\asp_net_projects\AnimalCatalog\AnimalCatalogSqLite\Views\Animal\Index.cshtml"
       Write(await Component.InvokeAsync("Animal", animal));

#line default
#line hidden
            EndContext();
#line 50 "C:\Alan\asp_net_projects\AnimalCatalog\AnimalCatalogSqLite\Views\Animal\Index.cshtml"
                                                          
        }

#line default
#line hidden
            BeginContext(1897, 21, true);
            WriteLiteral("    </ul>\r\n</section>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Models.Animal>> Html { get; private set; }
    }
}
#pragma warning restore 1591
