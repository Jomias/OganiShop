# OganiShop

Web bán nguyên liệu và thực phẩm sử dụng Net Core 3.1

Frontend người dùng: https://themewagon.com/themes/free-bootstrap-4-html5-responsive-ecommerce-website-template-ogani/

Frontend admin: https://adminlte.io/themes/AdminLTE/index2.html

## Các chức năng

### Trang người dùng
#### Thanh điều hướng
- Đăng nhập và đăng xuất.
- Điều hướng đến các trang.
- Tìm kiếm sản phẩm sẽ chuyển về trang ShoppingGrid.
- Ấn vào biểu tượng giỏ hàng sẽ chuyển về trang Shopping Cart.
- Ấn vào các danh mục sẽ chuyển về trang ShoppingGrid với loại sản phẩm tương ứng.
#### Home
- Hiển thị các sản phẩm nổi bật, các blog.
- Hiển thị các banner
- Click vào sản phẩm redirect đến trang chi tiết sản phẩm.
- Click vào blog redirect đến bài viết chi tiết.
#### Các sản phẩm
- Hiển thị hình đại diện và giá tiền của sản phẩm.
- Có thể thêm vào giỏ hàng thông qua nút cart.
#### ShoppingGrid
- Tìm kiếm sản phẩm theo tên
- Render các sản phẩm được giảm giá.
- Render các sản phẩm thuộc danh mục khác nhau.
- Filter theo danh mục, theo giá.
- Sắp xếp sản phẩm theo giá, theo tên, theo ngày tạo.
- Paging.
#### ProductDetail
- Render sản phẩm tương ứng.
- Render các sản phẩm liên quan.
- Sản phẩm có thể thêm vào giỏ hàng 
- Bao gồm 1 ảnh đại diện và các ảnh khác của sản phẩm.
- Các thông tin khác
#### ShoppingCart
- Hiển thị sản phẩm có trong giỏ. 
- Cho phép người dùng ẩn danh với cookies.
- Lưu trữ giỏ hàng khi khách đăng nhập.
- Có thể xóa và cập nhật số lượng sản phẩm.
#### CheckOut
- Nếu khách đăng nhập thì sẽ lưu thông tin địa chỉ của khách.
- Không thể thanh toán nếu ko có hàng hóa trong giỏ.
- Thanh toán thì sẽ chuyển các thông tin trong giỏ hàng thành order của shop.
#### Blog
- Render các bài viết.
- Click vào bài viết sẽ chuyển đến trang chi tiết tương ứng
- Filter theo tag.
- Filter theo thư mục.
- Tìm kiếm theo tên.
- Paging.
#### BlogDetail
- Hiển thị bài viết tương ứng.
- Hiển thị blog liên quan.
#### Contact
- Nhận thư liên lạc từ khách hàng.
### Trang admin
#### Account
- Có phân quyền
- Có thể đăng nhập, đăng ký, đăng xuất
- Khi đăng ký thành công sẽ chuyển về trang đăng nhập.
- Mã hóa mật khẩu.
- Hiển thị tài khoản tương ứng (cho cả admin/khách hàng)
#### User
- Xem, sửa thông tin cá nhân + ảnh đại diện.
- Admin có thể sửa thông tin người khác.
- Các role khác có thể tự cập nhật thông tin cá nhân nhưng không sửa được tài khoản người khác.
- Hiển thị danh sách các user.
#### Trang chủ
- Thống kê, báo cáo.
- Hiển thị các tin nhắn mới nhất từ khách hàng
#### Product
- Hiển thị danh sách sản phẩm.
- Thêm, sửa, xóa các thông tin cơ bản của sản phẩm.
- Thêm, xóa ảnh / đặt lại ảnh đại diện cho sản phẩm.
- Thêm, sửa, xóa khuyến mại của sản phẩm
- Paging + Search.
#### Category
- Hiển thị danh sách thư mục. 
- Thêm/Sửa/Xóa danh mục
- Paging + Search.
#### Blog
- Admin có thể xem tất cả các bài viết và thêm, sửa, xóa.
- Hiển thị các bài viết công khai và bài viết cá nhân(các chức vụ khác).
- Xem + Sửa các bài viết của cá nhân cho các chức vụ khác.
- Thêm/Xóa danh mục Blog.
- Lọc theo danh mục.
- Paging + Search.
- Xem bài viết chi tiết.
#### ShopOrder
- Hiển thị các đơn hàng và chi tiết đơn hàng.
- Có thể hủy đơn hàng.
- Xác nhận đơn hàng sẽ tự động trừ số lượng vào trong số lượng của sản phẩm .
