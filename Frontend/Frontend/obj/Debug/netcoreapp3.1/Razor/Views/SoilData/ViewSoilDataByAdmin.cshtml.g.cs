#pragma checksum "C:\Users\Yishmael\Documents\Visual Studio 2019\Project\IOT-Soil-Monitoring-System\Frontend\Frontend\Views\SoilData\ViewSoilDataByAdmin.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0138dafbd74da30f14a7181752e5920c10370b6e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SoilData_ViewSoilDataByAdmin), @"mvc.1.0.view", @"/Views/SoilData/ViewSoilDataByAdmin.cshtml")]
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
#line 1 "C:\Users\Yishmael\Documents\Visual Studio 2019\Project\IOT-Soil-Monitoring-System\Frontend\Frontend\Views\_ViewImports.cshtml"
using Frontend;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Yishmael\Documents\Visual Studio 2019\Project\IOT-Soil-Monitoring-System\Frontend\Frontend\Views\_ViewImports.cshtml"
using Frontend.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0138dafbd74da30f14a7181752e5920c10370b6e", @"/Views/SoilData/ViewSoilDataByAdmin.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3ae8aefa3c68973493840eb164c197d32d70fdff", @"/Views/_ViewImports.cshtml")]
    public class Views_SoilData_ViewSoilDataByAdmin : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Frontend.Models.Data.ViewModel.SoilDataViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/circularprogressbar/jquery-1.11.1.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/circularprogressbar/jquery.circle-diagram.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/circularprogressbar/MyMain.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/gauge.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Yishmael\Documents\Visual Studio 2019\Project\IOT-Soil-Monitoring-System\Frontend\Frontend\Views\SoilData\ViewSoilDataByAdmin.cshtml"
  
    ViewData["Title"] = "ViewSoilDataByAdmin";
    Layout = "~/Views/Shared/_AdminDashboardLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<style>



    body {
        background-color: white;
    }

    .diagram {
        display: inline-block;
        margin: 2em;
    }

    #diagram-id-1 {
        margin: 0;
        padding: 0;
    }

    #diagram-id-2 {
        margin: 0;
        padding: 0;
    }

    #diagram-id-3 {
        margin: 0;
        padding: 0;
    }

    #diagram-id-4 {
        margin: 0;
        padding: 0;
    }

    #diagram-id-5 {
        margin: 0;
        padding: 0;
    }
</style>




<div class=""main-content"">
    <div class=""section__content section__content--p30"">
        <div class=""container-fluid"">
            <h2 style=""color: #80d03c;"">View Soil Data as at ");
#nullable restore
#line 53 "C:\Users\Yishmael\Documents\Visual Studio 2019\Project\IOT-Soil-Monitoring-System\Frontend\Frontend\Views\SoilData\ViewSoilDataByAdmin.cshtml"
                                                        Write(Html.DisplayFor(model => model.Date));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 53 "C:\Users\Yishmael\Documents\Visual Studio 2019\Project\IOT-Soil-Monitoring-System\Frontend\Frontend\Views\SoilData\ViewSoilDataByAdmin.cshtml"
                                                                                              Write(Html.DisplayFor(model => model.Time));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h2>
            <hr />
            <div class=""row"">
                <div class=""col-md-2"">
                    <div class=""card shadow-lg"" style=""height: 40rem; border-radius: 10px; margin-top: 5px; border-color:red;"">
                        <div class=""card-body"">
                            <h5 class=""card-title text-primary"" style=""font-weight:bold;font-size:medium"">Temperature</h5>

                            <canvas data-type=""linear-gauge""
                                    data-width=""120""
                                    data-height=""400""
                                    data-min-value=""0""
                                    data-start-angle=""90""
                                    data-ticks-angle=""180""
                                    data-value-box=""false""
                                    data-max-value=""220""
                                    data-major-ticks=""0,20,40,60,80,100,120,140,160,180,200,220""
                                    data-minor-ticks=""2""
  ");
            WriteLiteral(@"                                  data-stroke-ticks=""true""
                                    data-highlights='[ {""from"": 100, ""to"": 220, ""color"": ""rgba(200, 50, 50, .75)""} ]'
                                    data-color-plate=""#fff""
                                    data-border-shadow-width=""0""
                                    data-borders=""false""
                                    data-needle-type=""arrow""
                                    data-needle-width=""2""
                                    data-needle-circle-size=""7""
                                    data-needle-circle-outer=""true""
                                    data-needle-circle-inner=""false""
                                    data-animation-duration=""1500""
                                    data-animation-rule=""linear""
                                    data-bar-width=""10""
                                    data-value=""");
#nullable restore
#line 84 "C:\Users\Yishmael\Documents\Visual Studio 2019\Project\IOT-Soil-Monitoring-System\Frontend\Frontend\Views\SoilData\ViewSoilDataByAdmin.cshtml"
                                           Write(Html.DisplayFor(model => model.Temperature));

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n                            </canvas>\r\n                            <br />\r\n                            <h2 class=\"text-primary\" style=\"margin:0;padding:0;\">");
#nullable restore
#line 87 "C:\Users\Yishmael\Documents\Visual Studio 2019\Project\IOT-Soil-Monitoring-System\Frontend\Frontend\Views\SoilData\ViewSoilDataByAdmin.cshtml"
                                                                            Write(Html.DisplayFor(model => model.Temperature));

#line default
#line hidden
#nullable disable
            WriteLiteral(@" &#8451;</h2>



                        </div>
                    </div>
                </div>
                <div class=""col-md-10"">
                    <div class=""row"">
                        <div class=""col-md-5"">
                            <div class=""card shadow-lg"" style=""height: 13rem; border-radius: 20px; margin-top: 10px; border-color: #80d03c; "">
                                <div class=""card-body"">
                                    <h5 class=""card-title text-primary"" style=""font-weight:bold""><span class=""fa fa-cloud-rain""></span> Humidity</h5>
                                    <div class=""row"">
                                        <div class=""col-md-6 offset-3"">
                                            <div id=""diagram-id-1""
                                                 class=""diagram""
                                                 data-circle-diagram='{
			                        ""percent"": """);
#nullable restore
#line 105 "C:\Users\Yishmael\Documents\Visual Studio 2019\Project\IOT-Soil-Monitoring-System\Frontend\Frontend\Views\SoilData\ViewSoilDataByAdmin.cshtml"
                                           Write(Html.DisplayFor(model => model.Humidity));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"%"",
			                        ""size"": ""110"",
			                        ""borderWidth"": ""4"",
			                        ""bgFill"": ""#cacaca"",
			                        ""frFill"": ""#80d03c"",
			                        ""textSize"": ""30"",
			                        ""textColor"": ""#585858""
			                        }'>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class=""col-md-5"">
                            <div class=""card shadow-lg"" style=""height: 13rem; border-radius: 20px; margin-top: 10px; border-color: #ff6a00; "">
                                <div class=""card-body"">
                                    <h5 class=""card-title text-primary"" style=""font-weight:bold""><span class=""fa fa-water""></span> Soil Moisture</h5>
                                    <div clas");
            WriteLiteral(@"s=""row"">
                                        <div class=""col-md-6 offset-3"">
                                            <div id=""diagram-id-2""
                                                 class=""diagram""
                                                 data-circle-diagram='{
			                        ""percent"": """);
#nullable restore
#line 130 "C:\Users\Yishmael\Documents\Visual Studio 2019\Project\IOT-Soil-Monitoring-System\Frontend\Frontend\Views\SoilData\ViewSoilDataByAdmin.cshtml"
                                           Write(Html.DisplayFor(model => model.SoilMoisture));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"%"",
			                        ""size"": ""110"",
			                        ""borderWidth"": ""4"",
			                        ""bgFill"": ""#cacaca"",
			                        ""frFill"": ""#ff6a00"",
			                        ""textSize"": ""30"",
			                        ""textColor"": ""#585858""
			                        }'>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-md-5"">
                            <div class=""card shadow-lg"" style=""height: 13rem; border-radius: 20px; margin-top: 10px; border-color: #7D2AE8;"">
                                <div class=""card-body"">
                                    <h5 class=""card-title text-primary"" style=""font-weight:bold""><span class=""fa fa-wind""></span> ");
            WriteLiteral(@"Nitrogen</h5>
                                    <div class=""row"">
                                        <div class=""col-md-6 offset-3"">
                                            <div id=""diagram-id-3""
                                                 class=""diagram""
                                                 data-circle-diagram='{
			                        ""percent"": """);
#nullable restore
#line 157 "C:\Users\Yishmael\Documents\Visual Studio 2019\Project\IOT-Soil-Monitoring-System\Frontend\Frontend\Views\SoilData\ViewSoilDataByAdmin.cshtml"
                                           Write(Html.DisplayFor(model => model.Nitrogen));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"%"",
			                        ""size"": ""110"",
			                        ""borderWidth"": ""4"",
			                        ""bgFill"": ""#cacaca"",
			                        ""frFill"": ""#7D2AE8"",
			                        ""textSize"": ""30"",
			                        ""textColor"": ""#585858""
			                        }'>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class=""col-md-5"">
                            <div class=""card shadow-lg"" style=""height: 13rem; border-radius: 20px; margin-top: 10px; border-color: #ff006e;"">
                                <div class=""card-body"">
                                    <h5 class=""card-title text-primary"" style=""font-weight:bold""><span class=""fa fa-cloud""></span> Phosphorus</h5>
                                    <div class=""r");
            WriteLiteral(@"ow"">
                                        <div class=""col-md-6 offset-3"">
                                            <div id=""diagram-id-4""
                                                 class=""diagram""
                                                 data-circle-diagram='{
			                        ""percent"": """);
#nullable restore
#line 182 "C:\Users\Yishmael\Documents\Visual Studio 2019\Project\IOT-Soil-Monitoring-System\Frontend\Frontend\Views\SoilData\ViewSoilDataByAdmin.cshtml"
                                           Write(Html.DisplayFor(model => model.Phosphorus));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"%"",
			                        ""size"": ""110"",
			                        ""borderWidth"": ""4"",
			                        ""bgFill"": ""#cacaca"",
			                        ""frFill"": ""#ff006e"",
			                        ""textSize"": ""30"",
			                        ""textColor"": ""#585858""
			                        }'>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-md-10"">
                            <div class=""card shadow-lg"" style=""height: 13rem; border-radius: 20px; margin-top: 10px; border-color: #80d03c;"">
                                <div class=""card-body"">
                                    <h5 class=""card-title text-primary"" style=""font-weight:bold""><span class=""fa fa-wind""></span>");
            WriteLiteral(@" Potassium</h5>
                                    <div class=""row"">
                                        <div class=""col-md-6 offset-3"">
                                            <div id=""diagram-id-5""
                                                 class=""diagram""
                                                 data-circle-diagram='{
			                        ""percent"": """);
#nullable restore
#line 209 "C:\Users\Yishmael\Documents\Visual Studio 2019\Project\IOT-Soil-Monitoring-System\Frontend\Frontend\Views\SoilData\ViewSoilDataByAdmin.cshtml"
                                           Write(Html.DisplayFor(model => model.Potassium));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"%"",
			                        ""size"": ""110"",
			                        ""borderWidth"": ""4"",
			                        ""bgFill"": ""#cacaca"",
			                        ""frFill"": ""#80d03c"",
			                        ""textSize"": ""30"",
			                        ""textColor"": ""#585858""
			                        }'>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>


                </div>

            </div>


        </div>
    </div>
</div>




");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0138dafbd74da30f14a7181752e5920c10370b6e19072", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0138dafbd74da30f14a7181752e5920c10370b6e20172", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0138dafbd74da30f14a7181752e5920c10370b6e21272", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0138dafbd74da30f14a7181752e5920c10370b6e22372", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Frontend.Models.Data.ViewModel.SoilDataViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
