#pragma checksum "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Category\AddOrUpdate.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2dd445c2280c694da87be4ef0cd63f0fc83322f4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Category_AddOrUpdate), @"mvc.1.0.view", @"/Areas/Admin/Views/Category/AddOrUpdate.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2dd445c2280c694da87be4ef0cd63f0fc83322f4", @"/Areas/Admin/Views/Category/AddOrUpdate.cshtml")]
    #nullable restore
    public class Areas_Admin_Views_Category_AddOrUpdate : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Category\AddOrUpdate.cshtml"
  
    Layout = "~/Areas/Admin/Views/Shared/_Layout.cshtml";
    ViewData["Title"] = "Quản lý danh mục";
    string error = TempData["error"] as string;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<section class=""content-header"">
    <h1>
        &nbsp;
    </h1>
    <ol class=""breadcrumb"">
        <li><a href=""/Admin/Home""><i class=""fa fa-dashboard""></i>Home</a></li>
        <li><a href=""#"">Danh mục</a></li>
        <li class=""active"">Quản lý danh mục</li>
    </ol>
</section>

<section class=""content"">
    <div class=""box box-info"">
        <div class=""box-header with-border container"" style=""width:80%"">
            <h3 class=""box-title"">&nbsp;&nbsp;&nbsp; ");
#nullable restore
#line 21 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Category\AddOrUpdate.cshtml"
                                                Write(ViewBag.Type);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" danh mục</h3>
        </div>
        <!-- /.box-header -->
        <!-- form start -->
        <div class=""container"" style=""width:80%"">
            <form role=""form"" action=""/Admin/Category/AddOrUpdate"" method=""post"">
                <div class=""box-body"">
                    <div class=""form-group"">
                        <input type=""number"" name=""id""");
            BeginWriteAttribute("value", " value=\"", 1025, "\"", 1064, 1);
#nullable restore
#line 29 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Category\AddOrUpdate.cshtml"
WriteAttributeValue("", 1033, Model.Id == 0 ? 0 : Model.Id, 1033, 31, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" hidden>\r\n                        <input type=\"text\" name=\"filePath\"");
            BeginWriteAttribute("value", " value=\"", 1133, "\"", 1179, 1);
#nullable restore
#line 30 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Category\AddOrUpdate.cshtml"
WriteAttributeValue("", 1141, Model.Id == 0 ? "" : Model.FilePath, 1141, 38, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"file-path\" hidden>\r\n                        <input type=\"text\" name=\"fileId\"");
            BeginWriteAttribute("value", " value=\"", 1261, "\"", 1305, 1);
#nullable restore
#line 31 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Category\AddOrUpdate.cshtml"
WriteAttributeValue("", 1269, Model.Id == 0 ? "" : Model.FileId, 1269, 36, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"file-id\" hidden>\r\n                    </div>\r\n                    <div class=\"form-group\">\r\n                        <label>Tên danh mục</label>\r\n                        <input type=\"text\" class=\"form-control\" name=\"name\"");
            BeginWriteAttribute("value", " value=\"", 1531, "\"", 1573, 1);
#nullable restore
#line 35 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Category\AddOrUpdate.cshtml"
WriteAttributeValue("", 1539, Model.Id == 0 ? "" : Model.Name, 1539, 34, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                    </div>\r\n                    <div class=\"form-group\">\r\n                        <label for=\"exampleInputFile\" style=\"display:block\">Upload ảnh</label>\r\n                        <img");
            BeginWriteAttribute("src", " src=\"", 1775, "\"", 1835, 1);
#nullable restore
#line 39 "D:\Test\TestNetCore31\OganiShop\AppManager\Areas\Admin\Views\Category\AddOrUpdate.cshtml"
WriteAttributeValue("", 1781, Model.Id == 0 ? "/img/default.jpg" : Model.FilePath, 1781, 54, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" id=""image-upload"" class=""rounded mx-auto d-block"" style=""width: 300px;height: 300px"" />
                        <input type=""file"" name=""fileUpload"" id=""exampleInputFile"">
                        <i class=""help-block"">Upload ảnh đại diện của danh mục</i>
                    </div>
                </div>
                <!-- /.box-body -->
                <div class=""box-footer"">
                    <button type=""submit"" class=""btn btn-info pull-right"">Xác nhận</button>
                </div>
            </form>
        </div>
    </div>
</section>

");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>
        $(document).on('change', 'input[name=""fileUpload""]', function() {
            let files = $(this).prop(""files"");
            let formData = new FormData();
            formData.append(""file"", files[0]);
            $.ajax({
                url: ""/Admin/Category/UploadFile"",
                type: ""POST"",
                data: formData,
                contentType: false,
                processData: false,
                success: res => {
                    if (res.status == 'success'){
                        $('#image-upload').attr('src', res.fileInfo.filePath);
                        $('#file-path').val(res.fileInfo.filePath);
                        $('#file-id').val(res.fileInfo.id);
                    }
                    else {
                        alert(""Error!"");
                    }
                },
                error: error => {
                    console.log(error);
                }
            })
        });
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
