#pragma checksum "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0dc259e6c24d7d9c1971fbc533940f092a74183b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Product_Detail), @"mvc.1.0.view", @"/Views/Product/Detail.cshtml")]
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
#line 1 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\_ViewImports.cshtml"
using AppManager;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\_ViewImports.cshtml"
using AppManager.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0dc259e6c24d7d9c1971fbc533940f092a74183b", @"/Views/Product/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"39bdf4e2eeb9182a14600fe5d339bdb2f9540938", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Product_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onsubmit", new global::Microsoft.AspNetCore.Html.HtmlString("Search()"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
  
    ViewData["Title"] = "Product Detail";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<section class=""hero hero-normal"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-lg-3"">
                <div class=""hero__categories"">
                    <div class=""hero__categories__all"">
                        <i class=""fa fa-bars""></i>
                        <span>All departments</span>
                    </div>
                    <ul id=""department"">
                    </ul>
                </div>
            </div>
            <div class=""col-lg-9"">
                <div class=""hero__search"">
                    <div class=""hero__search__form"">
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0dc259e6c24d7d9c1971fbc533940f092a74183b4452", async() => {
                WriteLiteral(@"
                            <div class=""hero__search__categories"">
                                All Categories
                                <span class=""arrow_carrot-down""></span>
                            </div>
                            <input type=""text"" name=""search"" placeholder=""What do yo u need?"">
                            <button type=""submit"" class=""site-btn"">SEARCH</button>
                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                    </div>
                    <div class=""hero__search__phone"">
                        <div class=""hero__search__phone__icon"">
                            <i class=""fa fa-phone""></i>
                        </div>
                        <div class=""hero__search__phone__text"">
                            <h5>+65 11.188.888</h5>
                            <span>support 24/7 time</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>


<!-- Breadcrumb Section Begin -->
<section class=""breadcrumb-section set-bg"" data-setbg=""/img/breadcrumb.jpg"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-lg-12 text-center"">
                <div class=""breadcrumb__text"">
                    <h2>");
#nullable restore
#line 52 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
                   Write(Model.Detail.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                    <div class=\"breadcrumb__option\">\r\n                        <a href=\"/Home/Index\">Home</a>\r\n                        <a href=\"/ShoppingGrid/Index\">Shopping Grid</a>\r\n                        <span>");
#nullable restore
#line 56 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
                         Write(Model.Detail.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
<!-- Breadcrumb Section End -->
<!-- Product Details Section Begin -->
<section class=""product-details spad"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-lg-6 col-md-6"">
                <div class=""product__details__pic"">
                    <div class=""product__details__pic__item"">
                        <img class=""product__details__pic__item--large""");
            BeginWriteAttribute("src", " src=\"", 2787, "\"", 2813, 1);
#nullable restore
#line 71 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
WriteAttributeValue("", 2793, Model.Detail.Avatar, 2793, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 2814, "\"", 2820, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    </div>\r\n                    <div class=\"product__details__pic__slider owl-carousel\">\r\n");
#nullable restore
#line 74 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
                          
                            foreach (var item in Model.ListImages)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <img data-imgbigurl=\"");
#nullable restore
#line 77 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
                                                Write(item.FilePath);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"");
            BeginWriteAttribute("src", " src=\"", 3125, "\"", 3145, 1);
#nullable restore
#line 77 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
WriteAttributeValue("", 3131, item.FilePath, 3131, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 3146, "\"", 3152, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n");
#nullable restore
#line 78 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
                            }
                        

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                </div>\r\n            </div>\r\n            <div class=\"col-lg-6 col-md-6\">\r\n                <div class=\"product__details__text\">\r\n                    <h3>");
#nullable restore
#line 85 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
                   Write(Model.Detail.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>
                    <div class=""product__details__rating"">
                        <i class=""fa fa-star""></i>
                        <i class=""fa fa-star""></i>
                        <i class=""fa fa-star""></i>
                        <i class=""fa fa-star""></i>
                        <i class=""fa fa-star-half-o""></i>
                        <span>(18 reviews)</span>
                    </div>
                    <div class=""product__details__price"">$");
#nullable restore
#line 94 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
                                                      Write(String.Format("{0:0.00}", Model.Detail.Price));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                    <p>\r\n                        ");
#nullable restore
#line 96 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
                   Write(Model.Detail.Summary);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                    </p>
                    <div class=""product__details__quantity"">
                        <div class=""quantity"">
                            <div class=""pro-qty"">
                                <input type=""text"" value=""1"" id=""prd-qtt"">
                            </div>
                        </div>
                    </div>
                    <a href=""#"" class=""primary-btn"" id=""add-to-cart"">ADD TO CARD</a>
                    <a href=""#"" class=""heart-icon""><span class=""icon_heart_alt""></span></a>
                    <ul>
                        <li><b>Availability</b> <span>In Stock</span></li>
                        <li><b>Shipping</b> <span>01 day shipping. <samp>Free pickup today</samp></span></li>
                        <li><b>Weight</b> <span>");
#nullable restore
#line 110 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
                                            Write(Model.Detail.Weight + " " + Model.Detail.Unit);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span></li>
                        <li>
                            <b>Share on</b>
                            <div class=""share"">
                                <a href=""#""><i class=""fa fa-facebook""></i></a>
                                <a href=""#""><i class=""fa fa-twitter""></i></a>
                                <a href=""#""><i class=""fa fa-instagram""></i></a>
                                <a href=""#""><i class=""fa fa-pinterest""></i></a>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class=""col-lg-12"">
                <div class=""product__details__tab"">
                    <ul class=""nav nav-tabs"" role=""tablist"">
                        <li class=""nav-item"">
                            <a class=""nav-link active"" data-toggle=""tab"" href=""#tabs-1"" role=""tab""
                                aria-selected=""true"">Description</a>
                        </li>
                        <li");
            WriteLiteral(@" class=""nav-item"">
                            <a class=""nav-link"" data-toggle=""tab"" href=""#tabs-2"" role=""tab""
                                aria-selected=""false"">Information</a>
                        </li>
                        <li class=""nav-item"">
                            <a class=""nav-link"" data-toggle=""tab"" href=""#tabs-3"" role=""tab""
                                aria-selected=""false"">Reviews <span>(1)</span></a>
                        </li>
                    </ul>
                    <div class=""tab-content"">
                        <div class=""tab-pane active"" id=""tabs-1"" role=""tabpanel"">
                            <div class=""product__details__tab__desc"">
                                <h6>Products Infomation</h6>
                                <p>
                                    ");
#nullable restore
#line 144 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
                               Write(Model.Detail.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </p>
                            </div>
                        </div>
                        <div class=""tab-pane"" id=""tabs-2"" role=""tabpanel"">
                            <div class=""product__details__tab__desc"">
                                <h6>Products Infomation</h6>
                                <p>
                                    ");
#nullable restore
#line 152 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
                               Write(Model.Detail.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </p>
                            </div>
                        </div>
                        <div class=""tab-pane"" id=""tabs-3"" role=""tabpanel"">
                            <div class=""product__details__tab__desc"">
                                <h6>Products Infomation</h6>
                                <p>
                                    ");
#nullable restore
#line 160 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
                               Write(Model.Detail.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
<!-- Product Details Section End -->
<!-- Related Product Section Begin -->
<section class=""related-product"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-lg-12"">
                <div class=""section-title related__product__title"">
                    <h2>Related Product</h2>
                </div>
            </div>
        </div>
        <div class=""row"" id=""related-product"">
");
#nullable restore
#line 182 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
              
                foreach (var item in Model.RelatedProducts)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"col-lg-3 col-md-4 col-sm-6\">\r\n                        <div class=\"product__item\">\r\n                            <div class=\"product__item__pic set-bg\" data-setbg=\"");
#nullable restore
#line 187 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
                                                                          Write(item.Avatar);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""">
                                <ul class=""product__item__pic__hover"">
                                    <li><a href=""#""><i class=""fa fa-heart""></i></a></li>
                                    <li><a href=""#""><i class=""fa fa-retweet""></i></a></li>
                                    <li><a href=""#""><i class=""fa fa-shopping-cart""></i></a></li>
                                </ul>
                            </div>
                            <div class=""product__item__text"">
                                <h6><a");
            BeginWriteAttribute("href", " href=\"", 9068, "\"", 9102, 2);
            WriteAttributeValue("", 9075, "/Product/Detail?id=", 9075, 19, true);
#nullable restore
#line 195 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
WriteAttributeValue("", 9094, item.Id, 9094, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 195 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
                                                                     Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></h6>\r\n                                <h5>$");
#nullable restore
#line 196 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
                                Write(item.Price.ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n");
#nullable restore
#line 200 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\Product\Detail.cshtml"
                }
            

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</section>\r\n<!-- Related Product Section End -->\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
<script>
    // category
    $.ajax({
        url: ""/Category/GetAllCategories"",
        type: ""GET"",
        dataType: ""json"",
        beforeSend: function () {

        },
        success: function (data) {
            $('#department').append('<li><a href=""/ShoppingGrid/Index"" class=""category"">All</a></li>');
            data.forEach(function (item, index) {
                let row = `<li><a href=""/ShoppingGrid/Index?id=${item.id}"" class=""category"">${item.name}</a></li>`;
                $('#department').append(row);
            })
        },
        error: function () {

        },
        complete: function () {

        }
    });

    $(document).on('click', '.add-to-cart', function (e) {
        e.preventDefault();
        let id = $(this).closest('ul').attr('id');
        $.ajax({
            url: ""/ShoppingCart/AddOrUpdateCartItem"",
            type: ""POST"",
            data: {
                id: id,
            },
            dataType: ""json"",
            beforeSe");
                WriteLiteral(@"nd: function () { },
            success: function (data) {
                alert(""???? th??m s???n ph???m v??o gi??? h??ng"");
            },
            error: function () { },
            complete: function () { }
        });
    });

    function Search() {
        let temp = '/Product/Detail?search=' + $('input[name=""search""]').val();
        window.location.assign('temp');
    };
</script>
");
            }
            );
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
