#pragma checksum "D:\Test\TestNetCore31\OganiShop\AppManager\Views\ShoppingCart\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "809f5b4329b2e0cf6dbcb5723b391300dfc6138a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ShoppingCart_Index), @"mvc.1.0.view", @"/Views/ShoppingCart/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"809f5b4329b2e0cf6dbcb5723b391300dfc6138a", @"/Views/ShoppingCart/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"39bdf4e2eeb9182a14600fe5d339bdb2f9540938", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ShoppingCart_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("#"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\ShoppingCart\Index.cshtml"
  
    ViewData["Title"] = "Shopping Cart";
    dynamic total = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<section class=""hero hero-normal"">
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "809f5b4329b2e0cf6dbcb5723b391300dfc6138a4488", async() => {
                WriteLiteral(@"
                            <div class=""hero__search__categories"">
                                All Categories
                                <span class=""arrow_carrot-down""></span>
                            </div>
                            <input type=""text"" placeholder=""What do yo u need?"">
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


<section class=""breadcrumb-section set-bg"" data-setbg=""/img/breadcrumb.jpg"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-lg-12 text-center"">
                <div class=""breadcrumb__text"">
                    <h2>Shopping Cart</h2>
                    <div class=""breadcrumb__option"">
                        <a href=""/Home/Index"">Home</a>
                        <span>Shopping Cart</span>
                    </");
            WriteLiteral(@"div>
                </div>
            </div>
        </div>
    </div>
</section>
<!-- Breadcrumb Section End -->
<!-- Shoping Cart Section Begin -->
<section class=""shoping-cart spad"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-lg-12"">
                <div class=""shoping__cart__table"">
                    <table id=""cart-table"">
                        <thead>
                            <tr>
                                <th class=""shoping__product"">Products</th>
                                <th>Price</th>
                                <th>Quantity</th>
                                <th>Total</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
");
#nullable restore
#line 79 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\ShoppingCart\Index.cshtml"
                              
                                foreach (var item in Model)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr");
            BeginWriteAttribute("id", " id=\"", 3165, "\"", 3178, 1);
#nullable restore
#line 82 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\ShoppingCart\Index.cshtml"
WriteAttributeValue("", 3170, item.Id, 3170, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"shopping__cart__product\">\r\n                                        <td class=\"shoping__cart__item\">\r\n                                            <img");
            BeginWriteAttribute("src", " src=\"", 3336, "\"", 3354, 1);
#nullable restore
#line 84 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\ShoppingCart\Index.cshtml"
WriteAttributeValue("", 3342, item.Avatar, 3342, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"100px\"");
            BeginWriteAttribute("alt", " alt=\"", 3369, "\"", 3375, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                            <h5>");
#nullable restore
#line 85 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\ShoppingCart\Index.cshtml"
                                           Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                                        </td>\r\n                                        <td class=\"shoping__cart__price\">\r\n                                            $");
#nullable restore
#line 88 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\ShoppingCart\Index.cshtml"
                                        Write(item.Price.ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                        </td>
                                        <td class=""shoping__cart__quantity"">
                                            <div class=""quantity"">
                                                <div class=""pro-qty"">
                                                    <input type=""text"" min=""1"" class=""product-quantity""");
            BeginWriteAttribute("value", " value=\"", 4008, "\"", 4030, 1);
#nullable restore
#line 93 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\ShoppingCart\Index.cshtml"
WriteAttributeValue("", 4016, item.Quantity, 4016, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                                                </div>
                                            </div>
                                        </td>
                                        <td class=""shoping__cart__total"">
                                            $");
#nullable restore
#line 98 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\ShoppingCart\Index.cshtml"
                                         Write((item.Price * item.Quantity).ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td class=\"shoping__cart__item__close\">\r\n                                            <a");
            BeginWriteAttribute("onclick", " onclick=\"", 4533, "\"", 4567, 3);
            WriteAttributeValue("", 4543, "RemoveItem(", 4543, 11, true);
#nullable restore
#line 101 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\ShoppingCart\Index.cshtml"
WriteAttributeValue("", 4554, item.CartId, 4554, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4566, ")", 4566, 1, true);
            EndWriteAttribute();
            BeginWriteAttribute("href", " href=\"", 4568, "\"", 4575, 0);
            EndWriteAttribute();
            WriteLiteral("><span id=\"icon-close\"\r\n                                                class=\"icon_close\"></span></a>\r\n                                        </td>\r\n                                    </tr>\r\n");
#nullable restore
#line 105 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\ShoppingCart\Index.cshtml"
                                    total += item.Price * item.Quantity;
                                }
                            

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class=""row"">
            <div class=""col-lg-12"">
                <div class=""shoping__cart__btns"">
                    <a href=""/ShoppingGrid/Index"" class=""primary-btn cart-btn"">CONTINUE SHOPPING</a>
                    <a href=""#"" onclick=(UpdateCart(event)) class=""primary-btn cart-btn cart-btn-right"">
                        <span class=""icon_loading""></span>
                        Upadate Cart
                    </a>
                </div>
            </div>
            <div class=""col-lg-6"">
                <div class=""shoping__continue"">
                    <div class=""shoping__discount"">
                        <h5>Discount Codes</h5>
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "809f5b4329b2e0cf6dbcb5723b391300dfc6138a13754", async() => {
                WriteLiteral("\r\n                            <input type=\"text\" placeholder=\"Enter your coupon code\">\r\n                            <button type=\"submit\" class=\"site-btn\">APPLY COUPON</button>\r\n                        ");
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
                </div>
            </div>
            <div class=""col-lg-6"">
                <div class=""shoping__checkout"">
                    <h5>Cart Total</h5>
                    <ul>
                        <li>Subtotal <span id=""subtotal"">$");
#nullable restore
#line 138 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\ShoppingCart\Index.cshtml"
                                                     Write(total.ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></li>\r\n                        <li>Total <span id=\"total\">$");
#nullable restore
#line 139 "D:\Test\TestNetCore31\OganiShop\AppManager\Views\ShoppingCart\Index.cshtml"
                                               Write(total.ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></li>\r\n                    </ul>\r\n                    <a href=\"/CheckOut/Index\" class=\"primary-btn\">PROCEED TO CHECKOUT</a>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</section>\r\n<!-- Shoping Cart Section End -->\r\n\r\n");
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


    function UpdateQuantity(id, quantity) {
        $.ajax({
            url: ""/ShoppingCart/AddOrUpdateCartItem"",
            type: ""POST"",
            data: {
                id: id,
                quantity: quantity
            },
            dataType: ""json"",
            beforeSend: function () { },
            success: function (data)");
                WriteLiteral(@" {
            },
            error: function () { },
            complete: function () { }
        });
    }


    function RemoveItem(CartID) {
        $.ajax({
            url: ""/ShoppingCart/RemoveItem"",
            type: ""POST"",
            data: {
                id: CartID
            },
            dataType: ""json"",
            beforeSend: function () {
            },
            success: function () {
            },
            error: function () {
            },
            complete: function () {
            }
        });
    }

    function UpdateCart(e) {
        e.preventDefault();
        var total = 0;
        $('.shopping__cart__product').each(function () {
            let id = $(this).attr(""id"");
            let qtt = $(this).find('.product-quantity').val();
            let price = $(this).find('.shoping__cart__price').html().replace(/\s/g,'').replace('$', '');
            let totalProductPrice = price * qtt;
            total += totalProductPrice;
       ");
                WriteLiteral(@"     UpdateQuantity(id, qtt);
            $(this).find('.shoping__cart__total').html('$' + parseFloat(qtt * price).toFixed(2));
        });
        $('#subtotal').html('$' + parseFloat(total).toFixed(2));
        $('#total').html('$' + parseFloat(total).toFixed(2));
        alert(""C???p nh???t gi??? h??ng th??nh c??ng"");
    }

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
