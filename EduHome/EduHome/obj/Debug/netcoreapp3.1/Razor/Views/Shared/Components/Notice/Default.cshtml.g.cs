#pragma checksum "C:\Users\user\Desktop\EduHomeBackEnd\EduHome\EduHome\Views\Shared\Components\Notice\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "de3e1f01d0e215d2248cf62d181cce25852c0e29"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_Notice_Default), @"mvc.1.0.view", @"/Views/Shared/Components/Notice/Default.cshtml")]
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
#line 1 "C:\Users\user\Desktop\EduHomeBackEnd\EduHome\EduHome\Views\_ViewImports.cshtml"
using EduHome;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\user\Desktop\EduHomeBackEnd\EduHome\EduHome\Views\_ViewImports.cshtml"
using EduHome.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\user\Desktop\EduHomeBackEnd\EduHome\EduHome\Views\_ViewImports.cshtml"
using EduHome.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\user\Desktop\EduHomeBackEnd\EduHome\EduHome\Views\_ViewImports.cshtml"
using EduHome.ViewModels.Admin;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"de3e1f01d0e215d2248cf62d181cce25852c0e29", @"/Views/Shared/Components/Notice/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"283aed79e8566cd429071b68f27f7af875ccf7d1", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_Notice_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Notice>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<section class=""notice-area two pt-140"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-md-6 col-sm-6 col-xs-12"">
                <div class=""notice-right-wrapper mb-25 pb-25"">
                    <h3>TAKE A VIDEO TOUR</h3>
                    <div class=""notice-video"">
                        <div class=""video-icon video-hover"">
                            <a class=""video-popup"" href=""https://www.youtube.com/watch?v=to6Ghf8UL7o"">
                                <i class=""zmdi zmdi-play""></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""col-md-6 col-sm-6 col-xs-12"">
                <div class=""notice-left-wrapper"">
                    <h3>notice board</h3>
                    <div class=""notice-left"">
");
#nullable restore
#line 21 "C:\Users\user\Desktop\EduHomeBackEnd\EduHome\EduHome\Views\Shared\Components\Notice\Default.cshtml"
                         foreach (var notice in Model)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"single-notice-left mb-23 pb-20\">\r\n                                <h4>");
#nullable restore
#line 24 "C:\Users\user\Desktop\EduHomeBackEnd\EduHome\EduHome\Views\Shared\Components\Notice\Default.cshtml"
                               Write(String.Format("{0:MM/dd/yyyy}", notice.Date));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                                <p>");
#nullable restore
#line 25 "C:\Users\user\Desktop\EduHomeBackEnd\EduHome\EduHome\Views\Shared\Components\Notice\Default.cshtml"
                              Write(notice.Desc);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            </div>\r\n");
#nullable restore
#line 27 "C:\Users\user\Desktop\EduHomeBackEnd\EduHome\EduHome\Views\Shared\Components\Notice\Default.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        \r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</section>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Notice>> Html { get; private set; }
    }
}
#pragma warning restore 1591
