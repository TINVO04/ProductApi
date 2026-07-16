# Kịch bản video demo Product API - Week 5 Day 5

Thời lượng mục tiêu: khoảng 5 phút.

Tên file video đề xuất: `docs/videos/week5-product-api-demo.mp4`.

> Không hiển thị mật khẩu PostgreSQL, nội dung User Secrets hoặc connection string có password trong video.

## Chuẩn bị trước khi quay

1. Mở pgAdmin 4 và xác nhận PostgreSQL server local đang chạy.
2. Chạy migration nếu máy demo chưa có database:

   ```powershell
   dotnet ef database update --project .\src\ProductApi\ProductApi.csproj
   ```

3. Chạy API bằng HTTP profile:

   ```powershell
   dotnet run --project .\src\ProductApi\ProductApi.csproj --launch-profile http
   ```

4. Import `postman/ProductApi-Week5-Day5.postman_collection.json` vào Postman.
5. Đóng hoặc che các cửa sổ có password, User Secrets và thông tin riêng tư.
6. Kiểm tra microphone và vùng ghi màn hình.

## 0:00 - 0:30: Giới thiệu

Nội dung nói đề xuất:

- Đây là ProductApi của Week 5, xây dựng bằng ASP.NET Core Web API và .NET 8.
- API dùng PostgreSQL với Entity Framework Core.
- Kiến trúc gồm Controller, DTO, Service, Repository và AppDbContext.
- Demo sẽ đi qua các chức năng CRUD, validation, duplicate rule và Postman Runner.

Thao tác trên màn hình:

- Hiển thị nhanh cấu trúc thư mục `Controllers`, `Dtos`, `Services`, `Repositories`, `Data/Migrations`.

## 0:30 - 1:05: Giải thích flow và Dependency Injection

Nội dung nói đề xuất:

- Request đi vào `ProductsController`.
- Controller chỉ xử lý HTTP binding và status code, sau đó gọi `IProductService`.
- `ProductService` mapping DTO, chuẩn hóa tên và xử lý business rule.
- `ProductRepository` thực hiện truy vấn bất đồng bộ qua `AppDbContext`.
- Các dependency được đăng ký scoped trong `Program.cs`.

Thao tác trên màn hình:

- Mở `Program.cs`, chỉ vào `AddDbContext`, `AddScoped<IProductRepository, ProductRepository>` và `AddScoped<IProductService, ProductService>`.
- Mở nhanh constructor của `ProductsController`.

## 1:05 - 1:35: GET danh sách, tìm kiếm và chi tiết

Chạy lần lượt trong Postman:

1. `01 - Get Product List`
2. `02 - Search Products`
3. `03 - Get Product Detail`

Nội dung cần giải thích:

- GET list trả `items` và `pagination`.
- Query `search=SMART` vẫn tìm được Smartphone vì tìm kiếm không phân biệt hoa/thường.
- GET detail trả `ProductResponseDto`, không trả thẳng Entity.

Status cần hiển thị:

- `200 OK` cho cả ba request.

## 1:35 - 2:15: POST tạo Product

Chạy:

1. `04 - Create Product`
2. `05 - Read Created Product`

Nội dung cần giải thích:

- Request body dùng ProductCreateDto nên không có `Id`.
- Server sinh ID và trả `201 Created`.
- Response có `Location` header trỏ tới endpoint chi tiết.
- Request tiếp theo dùng ID động lấy từ response để đọc lại Product từ PostgreSQL.

Status cần hiển thị:

- `201 Created`.
- `200 OK` khi đọc lại.

## 2:15 - 2:50: PUT cập nhật Product

Chạy:

- `06 - Update Product`

Nội dung cần giải thích:

- ID lấy từ route, dữ liệu cập nhật lấy từ ProductUpdateDto.
- PUT là cập nhật toàn bộ các trường editable.
- Service mapping Entity sang ProductResponseDto sau khi lưu.

Status cần hiển thị:

- `200 OK` và body có tên, giá, số lượng mới.

## 2:50 - 3:35: Validation và duplicate rule

Chạy:

1. `07 - Reject Duplicate Product Name`
2. `08 - Reject Invalid Product`

Nội dung cần giải thích:

- Tên duplicate được chuyển sang chữ thường nhưng vẫn bị chặn trong cùng category.
- Service kiểm tra duplicate trước khi lưu.
- PostgreSQL `citext` và unique index `(Name, CategoryId)` bảo vệ thêm ở database.
- Duplicate trả `409 Conflict`.
- Price bằng `0` vi phạm DataAnnotations và trả `400 Bad Request` theo Problem Details.

Status cần hiển thị:

- `409 Conflict`.
- `400 Bad Request` với `application/problem+json`.

## 3:35 - 4:10: DELETE và Not Found

Chạy:

1. `09 - Delete Product`
2. `10 - Verify Product Was Deleted`
3. `11 - Delete Product Not Found`

Nội dung cần giải thích:

- DELETE thành công trả `204 No Content`, không có response body.
- GET lại ID vừa xóa trả `404 Not Found`.
- DELETE một ID không tồn tại cũng trả `404 Not Found`.
- Collection đã tự dọn Product demo nên không để lại dữ liệu rác.

## 4:10 - 4:40: Chạy Collection Runner

Thao tác:

1. Mở collection `ProductApi - Week 5 Day 5 CRUD Demo`.
2. Chọn Run collection.
3. Chạy 1 iteration, không dùng data file.
4. Hiển thị kết quả toàn bộ request và assertion pass.

Kết quả mong đợi:

- `11` request.
- `22` assertions passed.
- `0` failed.
- `0` errors.

## 4:40 - 5:00: Kết luận

Nội dung nói đề xuất:

- Product API đã hoàn thiện CRUD chuẩn với PostgreSQL, EF Core, DTO, Service, Repository và DI.
- API xử lý đúng các status `200`, `201`, `204`, `400`, `404`, `409`.
- Migration được lưu trong repository.
- Postman collection có thể chạy lặp lại và tự dọn dữ liệu.
- Build Release và GitHub Actions đều phải xanh trước khi merge Pull Request.

## Checklist video trước khi nộp

- [ ] Video dài khoảng 3-5 phút, ưu tiên gần 5 phút.
- [ ] Giọng nói nghe rõ, thao tác không quá nhanh.
- [ ] Hiển thị đủ GET, POST, PUT, DELETE.
- [ ] Hiển thị validation `400`, not found `404` và duplicate `409`.
- [ ] Hiển thị `201 Created`, `Location` header và `204 No Content`.
- [ ] Hiển thị Postman Runner pass toàn bộ.
- [ ] Không để lộ password hoặc User Secrets.
- [ ] Video mở được sau khi lưu.
- [ ] Tên file và đường dẫn video được cập nhật vào README trước khi nộp.
