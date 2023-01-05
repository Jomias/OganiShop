# OganiShop

Web bán nguyên liệu và thực phẩm

Frontend người dùng: https://themewagon.com/themes/free-bootstrap-4-html5-responsive-ecommerce-website-template-ogani/
Frontend admin: https://adminlte.io/themes/AdminLTE/index2.html

## Các chức năng

### Trang người dùng
#### Thanh điều hướng
- Đăng nhập và đăng xuất.
- Điều hướng đến các trang.
- Tìm kiếm sản phẩm sẽ chuyển về trang ShoppingGrid.
- Ấn vào biểu tượng giỏ hàng sẽ chuyển về trang Shopping Cart.
- Ấn vào các category sẽ chuyển về trang ShoppingGrid với loại sản phẩm tương ứng.

#### Home
- Render các sản phẩm nổi bật, các blog.
- Render banner
- Click vào sản phẩm redirect đến trang chi tiết sản phẩm.
- Click vào blog redirect đến bài viết chi tiết.
#### Các sản phẩm
- Hiển thị hình đại diện và giá tiền của sản phẩm.
- Có thể thêm vào giỏ hàng thông qua nút cart.
#### Trang ShoppingGrid
- Render các sản phẩm được giảm giá.
- Render các sản phẩm thuộc danh mục khác nhau.
- Filter theo danh mục, theo giá.
- Sắp xếp sản phẩm theo giá, theo tên, theo ngày tạo.
- Paging.
#### Chi tiết sản phẩm
- Render sản phẩm tương ứng.
- Render các sản phẩm liên quan.
- Sản phẩm có thể thêm vào giỏ hàng 
- Bao gồm 1 ảnh đại diện và các ảnh khác của sản phẩm.
- Các thông tin khác
#### ShoppingCart
- Hiển thị sản phẩm có trong giỏ. 
+ Cho phép người dùng ẩn danh với cookies.
+ Lưu trữ giỏ hàng khi khách đăng nhập.
- Có thể xóa và cập nhật số lượng sản phẩm.

#### CheckOut
- Nếu khách đăng nhập thì sẽ lưu thông tin địa chỉ của khách
- Không có gì trong giỏ hàng sẽ không thanh toán được.
#### Blog
- Render các post theo thông tin trong database.
- Click vào bài viết để đến trang tương ứng.
- Filter theo tag.
- Filter theo thư mục.
- Paging.
#### BlogDetail
- Render bài viết tương ứng.
- Có bài viết tương tự.

### Trang admin
#### Account
- Có thể đăng nhập hoặc đăng ký.
- Khi đăng ký thành công sẽ chuyển về trang đăng nhập. Tài khoản đăng ký auto tài khoản khách hàng.
- Có thể đăng xuất (cả admin lẫn người dùng)
- Hiển thị tài khoản tương ứng khi đăng nhập thành công (ở trên thanh navbar)
#### User
- Xem, sửa thông tin cá nhân + ảnh đại diện.
- Chỉ có admin mới có thể sửa vai trò.
- Chỉ có admin mới có thể xem danh sách người dùng (paging).
#### Trang chủ
- Hiển thị thống kê theo tháng và năm (gộp theo danh mục sản phẩm)
- Hiển thị những nhân viên mới và danh sách đơn hàng.
#### Product
- Hiển thị danh sách sản phẩm.
- Có thể xoá / thay đổi thông tin sản phẩm.
- Có thể thêm ảnh / đặt lại ảnh đại diện cho sản phẩm.
- Paging.
#### Category
- Hiển thị danh sách thư mục. 
- Có thể xoá / thay đổi / đặt lại ảnh cho thư mục.
- Paging.
#### Blog
- Hiển thị tất cả bài viết công khai.
- Thêm thư mục cho blog.
- Lọc theo thư mục.
- Xem + sửa + thêm bài viết của bản thân ở chế độ công khai / nháp / xoá.
- Paging.
#### Report
- Y như trang chủ nhưng cắt bỏ phần nhân viên mới + danh sách đơn hàng
