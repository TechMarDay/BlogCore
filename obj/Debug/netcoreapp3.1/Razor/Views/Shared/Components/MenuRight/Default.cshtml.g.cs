#pragma checksum "C:\BlogCore\BlogCore\Views\Shared\Components\MenuRight\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "aec6087b3d080a44cdaf68388230e9bc9bdba489"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_MenuRight_Default), @"mvc.1.0.view", @"/Views/Shared/Components/MenuRight/Default.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\BlogCore\BlogCore\Views\_ViewImports.cshtml"
using BlogCore;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\BlogCore\BlogCore\Views\_ViewImports.cshtml"
using BlogCore.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aec6087b3d080a44cdaf68388230e9bc9bdba489", @"/Views/Shared/Components/MenuRight/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"784938c45a729b1006f16b3b5ebaebddb8e24285", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_MenuRight_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<aside class=\"col-md-2 blog-sidebar\">\r\n    <div class=\"p-4\">\r\n        <h4 class=\"font-italic\">Marketing</h4>\r\n        <ol class=\"list-unstyled mb-0\">\r\n");
#nullable restore
#line 5 "C:\BlogCore\BlogCore\Views\Shared\Components\MenuRight\Default.cshtml"
             foreach (var item in ViewBag.CategoriesMarketing)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li><a href=\"#\">");
#nullable restore
#line 7 "C:\BlogCore\BlogCore\Views\Shared\Components\MenuRight\Default.cshtml"
                           Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n");
#nullable restore
#line 8 "C:\BlogCore\BlogCore\Views\Shared\Components\MenuRight\Default.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </ol>\r\n    </div>\r\n\r\n    <div class=\"p-4\">\r\n        <h4 class=\"font-italic\">Programming</h4>\r\n        <ol class=\"list-unstyled mb-0\">\r\n");
#nullable restore
#line 15 "C:\BlogCore\BlogCore\Views\Shared\Components\MenuRight\Default.cshtml"
             foreach (var item in ViewBag.CategoriesPrograming)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li><a href=\"#\">");
#nullable restore
#line 17 "C:\BlogCore\BlogCore\Views\Shared\Components\MenuRight\Default.cshtml"
                           Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n");
#nullable restore
#line 18 "C:\BlogCore\BlogCore\Views\Shared\Components\MenuRight\Default.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </ol>\r\n    </div>\r\n</aside>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
