#pragma checksum "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1b5c01cfda9f4f80a6304999a91304ebe1e655a8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Product_ListImage), @"mvc.1.0.view", @"/Areas/Admin/Views/Product/ListImage.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1b5c01cfda9f4f80a6304999a91304ebe1e655a8", @"/Areas/Admin/Views/Product/ListImage.cshtml")]
    #nullable restore
    public class Areas_Admin_Views_Product_ListImage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
  
    Layout = "~/Areas/Admin/Views/Shared/_Layout.cshtml";
    ViewData["Title"] = "Ảnh sản phẩm";
    int pageCount = (int)ViewBag.pageCount;
    int pageNumber = (int)ViewBag.pageNumber;
    int pageSize = (int)ViewBag.pageSize;
    int stt = pageSize * pageNumber - (pageSize - 1);
    int id = (int)ViewBag.Id;
    int count = (int)ViewBag.Count;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<section class=""content-header"">
    <h1>
        &nbsp;
    </h1>
    <ol class=""breadcrumb"">
        <li><a href=""/Admin/Home""><i class=""fa fa-dashboard""></i> Home</a></li>
        <li><a href=""#"">Sản phẩm</a></li>
        <li><a href=""/Admin/Product"">Quản lý sản phẩm</a></li>
        <li class=""active"">Quản lý ảnh</li>
    </ol>
</section>

<div class=""container"">
    <div class=""row"">
        <div class=""col-md-8"">
            <h3>Danh sách ảnh</h3>
            <table class=""table table-hover table-bordered text-center"" style=""background-color:white"">
                <thead>
                    <tr>
                        <th>STT</th>
                        <th>Mã sản phẩm</th>
                        <th>Mã ảnh</th>
                        <th>Loại ảnh</th>
                        <th>Ảnh</th>
                        <th>Thao tác</th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 40 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
                      
                        foreach (var item in Model)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr>\r\n                                <td style=\"vertical-align: middle!important\">");
#nullable restore
#line 44 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
                                                                        Write(stt);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td style=\"vertical-align: middle!important\">");
#nullable restore
#line 45 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
                                                                        Write(item.ProductId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td style=\"vertical-align: middle!important\">");
#nullable restore
#line 46 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
                                                                        Write(item.FileId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 47 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
                                  
                                    if (item.IsAvatar)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <td style=\"vertical-align: middle!important\">Ảnh đại diện</td>\r\n");
#nullable restore
#line 51 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
                                    }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <td style=\"vertical-align: middle!important\">Ảnh thường</td>\r\n");
#nullable restore
#line 55 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
                                    }
                                

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <td style=\"vertical-align: middle!important\"><img");
            BeginWriteAttribute("src", " src=\"", 2357, "\"", 2377, 1);
#nullable restore
#line 57 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
WriteAttributeValue("", 2363, item.FilePath, 2363, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"width:100px\"></td>\r\n                                <td style=\"vertical-align: middle!important\">\r\n");
#nullable restore
#line 59 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
                                      
                                        if (!item.IsAvatar)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <a");
            BeginWriteAttribute("href", " href=\"", 2675, "\"", 2718, 2);
            WriteAttributeValue("", 2682, "/Admin/Product/SetAvatar?id=", 2682, 28, true);
#nullable restore
#line 62 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
WriteAttributeValue("", 2710, item.Id, 2710, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-primary\"\r\n                                        style=\"color: white\">Set ảnh đại diện</a>\r\n");
#nullable restore
#line 64 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
                                            if (count > 1)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                <a");
            BeginWriteAttribute("href", " href=\"", 2985, "\"", 3030, 2);
            WriteAttributeValue("", 2992, "/Admin/Product/DeleteImage?id=", 2992, 30, true);
#nullable restore
#line 66 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
WriteAttributeValue("", 3022, item.Id, 3022, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-danger\"\r\n                                        style=\"color: white\">Xóa ảnh</a>\r\n");
#nullable restore
#line 68 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
                                            }
                                        }
                                        else 
                                        {
                                            

#line default
#line hidden
#nullable disable
            WriteLiteral("Ảnh đại diện không thể xóa");
#nullable restore
#line 72 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
                                                                                   
                                        }
                                    

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                            </tr>\r\n");
#nullable restore
#line 78 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
                            stt++;
                        }
                    

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\r\n            </table>\r\n            <div class=\" clearfix\">\r\n                <ul class=\"pagination pagination-sm no-margin pull-right\">\r\n                    <li><a");
            BeginWriteAttribute("href", " href=\"", 3826, "\"", 3901, 4);
            WriteAttributeValue("", 3833, "/Admin/Product/ListImage?id=", 3833, 28, true);
#nullable restore
#line 85 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
WriteAttributeValue("", 3861, id, 3861, 3, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3864, "&pageNumber=", 3864, 12, true);
#nullable restore
#line 85 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
WriteAttributeValue("", 3876, Math.Max(pageNumber-1,1), 3876, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">&laquo;</a>\r\n                    </li>\r\n");
#nullable restore
#line 87 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
                     for (int i = 1; i <= pageCount; ++i)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <li><a");
            BeginWriteAttribute("href", " href=\"", 4055, "\"", 4107, 4);
            WriteAttributeValue("", 4062, "/Admin/Product/ListImage?id=", 4062, 28, true);
#nullable restore
#line 89 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
WriteAttributeValue("", 4090, id, 4090, 3, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4093, "&pageNumber=", 4093, 12, true);
#nullable restore
#line 89 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
WriteAttributeValue("", 4105, i, 4105, 2, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 89 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
                                                                               Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n");
#nullable restore
#line 90 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <li><a");
            BeginWriteAttribute("href", "\r\n                            href=\"", 4171, "\"", 4283, 4);
            WriteAttributeValue("", 4207, "/Admin/Product/ListImage?id=", 4207, 28, true);
#nullable restore
#line 92 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
WriteAttributeValue("", 4235, id, 4235, 3, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4238, "&pageNumber=", 4238, 12, true);
#nullable restore
#line 92 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
WriteAttributeValue("", 4250, Math.Min(pageNumber+1,pageCount), 4250, 33, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">&raquo;</a>
                    </li>
                </ul>
            </div>
        </div>
        <div class=""col-md-4"" style=""padding:5px"">
            <h3>Thêm mới ảnh</h3>
            <form class=""form-horizontal"" action=""/Admin/Product/AddImage"" method=""Post""
                style=""overflow: hidden; background-color:white"">
                <label class=""col-md-3 control-label"">Thêm ảnh</label>
                <div class=""col-md-12 text-center"">
                    <img id=""image-upload"" class=""rounded mx-auto d-block"" style=""padding-bottom:1rem""
                        src=""/img/default.jpg"" width=""200px"">
                </div>
                <input type=""text"" name=""productId""");
            BeginWriteAttribute("value", " value=\"", 4995, "\"", 5006, 1);
#nullable restore
#line 106 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Product\ListImage.cshtml"
WriteAttributeValue("", 5003, id, 5003, 3, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" hidden>\r\n                <input type=\"text\" name=\"filePath\" id=\"image-file-path\"");
            BeginWriteAttribute("value", " value=\"", 5088, "\"", 5096, 0);
            EndWriteAttribute();
            WriteLiteral(" hidden>\r\n                <input type=\"text\" name=\"fileId\" id=\"image-file-id\"");
            BeginWriteAttribute("value", " value=\"", 5174, "\"", 5182, 0);
            EndWriteAttribute();
            WriteLiteral(@" hidden>
                <div class=""col-md-12 text-center"">
                    <input type=""file"" id=""image-upload"" name=""fileUpload"">
                </div>
                <div class=""col-md-12"">
                    <button type=""submit"" class=""btn btn-info pull-right"" style=""margin-bottom: 5px"">Thêm ảnh</button>
                </div>
            </form>
        </div>
    </div>

</div>


");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
<script>
    $(document).on('change', 'input[name=""fileUpload""]', function () {
        let files = $(this).prop(""files"");
        let formData = new FormData();
        formData.append(""file"", files[0]);
        $.ajax({
            url: ""/Admin/Product/UploadFile"",
            type: ""POST"",
            data: formData,
            contentType: false,
            processData: false,
            beforeSend: () => {

            },
            success: res => {
                $('#image-upload').attr('src', res.fileInfo.filePath);
                $('#image-file-path').val(res.fileInfo.filePath);
                $('#image-file-id').val(res.fileInfo.id);
            },
            error: error => {
                console.log(error);
            }
        })
    })
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
