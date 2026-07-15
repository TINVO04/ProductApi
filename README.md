# ProductApi

Dự án thực hành **ASP.NET Core Web API** trong lộ trình .NET tuần 5.

Mục tiêu của dự án là xây dựng Product API theo từng ngày, bắt đầu từ kiến thức HTTP, REST API, Controller và Swagger, sau đó phát triển thành API CRUD có DTO, Service, Repository và Entity Framework Core.

## Mục tiêu Day 1

- Hiểu request và response trong HTTP.
- Hiểu các HTTP method cơ bản: GET, POST, PUT và DELETE.
- Hiểu các status code phổ biến: 200, 201, 400, 401, 404 và 500.
- Tạo project ASP.NET Core Web API bằng .NET 8 LTS.
- Cấu hình Swagger/OpenAPI.
- Tạo endpoint kiểm tra trạng thái API: `GET /api/health`.
- Hoàn thành mini challenge: `GET /api/info`.
- Test API bằng Swagger, file `.http` và Postman.

## Mục tiêu Day 2

- Biết tạo Controller và route đúng chuẩn.
- Biết nhận dữ liệu qua query, path và request body.
- Biết trả `ActionResult` phù hợp với từng tình huống.
- Tạo mock Product API với endpoint danh sách, chi tiết và tạo mới.
- Kiểm thử các response `200`, `201`, `400` và `404` bằng Swagger, file `.http` và Postman.

## Công nghệ sử dụng

- .NET 8 LTS
- ASP.NET Core Web API
- C#
- Swashbuckle.AspNetCore
- Swagger/OpenAPI
- Postman
- Git và GitHub

## Yêu cầu môi trường

Cài đặt các công cụ sau trước khi chạy dự án:

- .NET SDK 8.x (khuyến nghị để build và chạy đúng môi trường LTS của dự án)
- Git
- Postman (để import và chạy collection kiểm thử)
- Visual Studio 2022, JetBrains Rider hoặc Visual Studio Code

Kiểm tra phiên bản .NET SDK:

```powershell
dotnet --version
```

Dự án target `net8.0` và không khóa một patch SDK cụ thể bằng `global.json`, giúp hạn chế lỗi khi máy khác không có đúng một phiên bản SDK cố định.

## Cấu trúc dự án

```text
ProductApi/
├── docs/
│   └── images/
│       ├── day01-health-endpoint.png
│       ├── day01-postman-runner.png
│       ├── day02-postman-runner-part1.png
│       └── day02-postman-runner-part2.png
├── postman/
│   ├── ProductApi-Week5-Day1.postman_collection.json
│   └── ProductApi-Week5-Day2.postman_collection.json
├── src/
│   └── ProductApi/
│       ├── Controllers/
│       │   ├── HealthController.cs
│       │   ├── InfoController.cs
│       │   └── ProductsController.cs
│       ├── Models/
│       │   └── Product.cs
│       ├── Properties/
│       │   └── launchSettings.json
│       ├── appsettings.Development.json
│       ├── appsettings.json
│       ├── ProductApi.csproj
│       ├── ProductApi.http
│       └── Program.cs
├── .gitignore
├── ProductApi.sln
└── README.md
```

## Cài đặt và chạy dự án

### 1. Clone repository

```powershell
git clone https://github.com/TINVO04/ProductApi.git
cd ProductApi
```

### 2. Restore dependency

```powershell
dotnet restore .\ProductApi.sln
```

`restore` tải các package mà project khai báo trong file project.

### 3. Build solution

```powershell
dotnet build .\ProductApi.sln
```

Kết quả đạt chuẩn:

```text
Build succeeded.
0 Warning(s)
0 Error(s)
```

### 4. Tin cậy HTTPS development certificate

Chỉ cần thực hiện khi máy chưa tin cậy chứng chỉ HTTPS local:

```powershell
dotnet dev-certs https --trust
```

### 5. Chạy API

```powershell
dotnet run --project .\src\ProductApi\ProductApi.csproj --launch-profile https
```

API chạy tại:

```text
https://localhost:7005
http://localhost:5291
```

## Swagger

Mở Swagger UI tại:

```text
https://localhost:7005/swagger
```

Swagger UI dùng để xem tài liệu API và gửi request thử trực tiếp bằng nút **Try it out** và **Execute**.

OpenAPI JSON được sinh tại:

```text
https://localhost:7005/swagger/v1/swagger.json
```

## API endpoints Day 1

| Method | Endpoint        | Mô tả                                                       | Status thành công |
| ------ | --------------- | ------------------------------------------------------------- | ------------------- |
| GET    | `/api/health` | Kiểm tra API có đang hoạt động hay không               | `200 OK`          |
| GET    | `/api/info`   | Trả thông tin cơ bản của project và môi trường chạy | `200 OK`          |

## API endpoints Day 2

| Method | Endpoint | Mô tả | Status thành công |
| ------ | -------- | ----- | ----------------- |
| GET | `/api/products?page=1` | Lấy danh sách Product có phân trang | `200 OK` |
| GET | `/api/products?search=smart&page=1` | Tìm Product theo tên và phân trang | `200 OK` |
| GET | `/api/products/{id}` | Lấy Product theo ID | `200 OK` hoặc `404 Not Found` |
| POST | `/api/products` | Tạo Product mới từ request body | `201 Created` hoặc `400 Bad Request` |

### GET `/api/health`

Response mẫu:

```json
{
  "status": "Healthy",
  "message": "Product API is running.",
  "timestampUtc": "2026-07-14T03:42:45.2155935Z"
}
```

Ý nghĩa:

- `status`: trạng thái hiện tại của API.
- `message`: thông báo mô tả ngắn.
- `timestampUtc`: thời điểm server xử lý request theo múi giờ UTC.

### GET `/api/info`

Response mẫu:

```json
{
  "projectName": "ProductApi",
  "version": "1.0.0",
  "framework": ".NET 8.x",
  "environment": "Development",
  "description": "ASP.NET Core Web API for product management."
}
```

Giá trị `framework` được đọc từ runtime thực tế thay vì ghi cứng trong source code.

## Test bằng file HTTP

File `src/ProductApi/ProductApi.http` chứa request mẫu cho Day 1 và Day 2:

- `GET /api/health` và `GET /api/info`.
- `GET /api/products?page=1`.
- `GET /api/products?search=smart&page=1`.
- `GET /api/products/2`.
- `GET /api/products/999` để kiểm tra `404 Not Found`.
- `POST /api/products` với body JSON để kiểm tra `201 Created`.

Có thể gửi từng request trực tiếp trong IDE hỗ trợ file `.http`. Kết quả đã xác minh gồm `200`, `201` và `404`; trường hợp `page=0` được kiểm thử riêng bằng Swagger và Postman với `400 Bad Request`.

## Test Day 1 bằng Postman

Collection kiểm thử được lưu tại `postman/ProductApi-Week5-Day1.postman_collection.json` theo schema Postman Collection v2.1.

Cách chạy collection:

1. Khởi động API bằng HTTPS tại `https://localhost:7005`.
2. Mở Postman và chọn **Import**.
3. Import file `postman/ProductApi-Week5-Day1.postman_collection.json`.
4. Mở collection **ProductApi - Week 5 Day 1** và chọn **Run**.
5. Chạy toàn bộ hai request trong Collection Runner.

Collection sử dụng biến dùng chung `baseUrl` với giá trị mặc định `https://localhost:7005` và kiểm tra:

- HTTP status code là `200`.
- Response có content type JSON.
- Response chứa các trường bắt buộc của từng endpoint.

Kết quả Collection Runner: `2` request, `6` test passed, `0` failed và `0` error.

![Kết quả Postman Collection Runner](docs/images/day01-postman-runner.png)

## Test Day 2 bằng Postman

Collection kiểm thử Day 2 được lưu tại `postman/ProductApi-Week5-Day2.postman_collection.json` theo schema Postman Collection v2.1.

Collection gồm 6 request:

1. `GET /api/products?page=1` — danh sách và phân trang.
2. `GET /api/products?search=smart&page=1` — tìm kiếm theo tên.
3. `GET /api/products/2` — lấy chi tiết sản phẩm tồn tại.
4. `GET /api/products/999` — kiểm tra `404 Not Found`.
5. `GET /api/products?page=0` — kiểm tra `400 Bad Request`.
6. `POST /api/products` — tạo sản phẩm và kiểm tra `201 Created`.

Collection sử dụng biến dùng chung `baseUrl` với giá trị `https://localhost:7005`. Collection Runner đã chạy 1 iteration với kết quả `19` test passed, `0` failed, `0` skipped và `0` error.

![Kết quả Postman Day 2 - phần 1](docs/images/day02-postman-runner-part1.png)

![Kết quả Postman Day 2 - phần 2](docs/images/day02-postman-runner-part2.png)

## Continuous Integration

Dự án sử dụng GitHub Actions để tự động kiểm tra source code khi push lên `main`, các branch `feature/**` hoặc khi tạo Pull Request vào `main`.

Workflow CI được khai báo tại `.github/workflows/ci.yml` và thực hiện các bước:

1. Checkout source code.
2. Cài đặt .NET 8 SDK.
3. Restore dependency từ `ProductApi.sln`.
4. Build solution với cấu hình Release.

CI giúp xác nhận dự án có thể restore và build trên một máy Linux sạch, không chỉ trên máy phát triển local.

## Kết quả chạy Health endpoint

![Kết quả GET health endpoint](docs/images/day01-health-endpoint.png)

Ảnh trên xác nhận Health endpoint trả status `200 OK` và response body ở định dạng JSON.

## Kiến thức HTTP trọng tâm

### HTTP method

| Method | Mục đích thường dùng     |
| ------ | ------------------------------ |
| GET    | Đọc dữ liệu                |
| POST   | Tạo dữ liệu mới            |
| PUT    | Cập nhật toàn bộ dữ liệu |
| DELETE | Xóa dữ liệu                 |

### HTTP status code

| Status code               | Ý nghĩa                            |
| ------------------------- | ------------------------------------ |
| 200 OK                    | Request được xử lý thành công |
| 201 Created               | Tạo tài nguyên thành công       |
| 400 Bad Request           | Request không hợp lệ              |
| 401 Unauthorized          | Chưa được xác thực             |
| 404 Not Found             | Không tìm thấy tài nguyên       |
| 500 Internal Server Error | Server xảy ra lỗi ngoài dự kiến |

## Kết quả Day 1

- [x] Tạo solution và Web API project.
- [x] Chuyển project về .NET 8 LTS để tăng khả năng tương thích.
- [x] Build thành công với `0 Warning(s)` và `0 Error(s)`.
- [x] Cấu hình Swagger/OpenAPI.
- [x] Tạo `GET /api/health`.
- [x] Tạo `GET /api/info`.
- [x] Test endpoint bằng Swagger.
- [x] Test endpoint bằng file `.http`.
- [x] Tạo và chạy Postman collection với `6/6` test passed.
- [x] Lưu ảnh kết quả Health endpoint và Postman Collection Runner.
- [x] Cấu hình GitHub Actions CI để restore và build dự án.

## Kết quả Day 2

- [x] Tạo `Product` model và danh sách dữ liệu mock.
- [x] Tạo `ProductsController` với route `/api/products`.
- [x] Tạo `GET /api/products` hỗ trợ `search` và `page`.
- [x] Tạo `GET /api/products/{id}` và xử lý `404 Not Found` khi không tìm thấy ID.
- [x] Tạo `POST /api/products` nhận JSON body và trả `201 Created`.
- [x] Test response `200`, `201`, `400` và `404` bằng Swagger.
- [x] Mở rộng file `.http` với request Product API và xác minh status response.
- [x] Tạo Postman collection Day 2 gồm 6 request và 19 assertions.
- [x] Collection Runner đạt `19/19` test passed, `0` failed và `0` error.
- [x] Thực hành breakpoint/debug cho flow tìm thấy Product và flow `404`.
- [x] Lưu hai ảnh bằng chứng kết quả Postman Runner.

## Báo cáo Day 1

### Công việc đã hoàn thành

- Khởi tạo solution và ASP.NET Core Web API project.
- Chuyển project về .NET 8 LTS để tăng khả năng tương thích.
- Cấu hình Swagger/OpenAPI bằng Swashbuckle.AspNetCore.
- Tạo và test endpoint `GET /api/health`.
- Hoàn thành mini challenge `GET /api/info`.
- Tạo file `.http` để test hai endpoint.
- Tạo Postman collection gồm hai request và sáu test assertion.
- Chạy Collection Runner đạt `6/6` test passed và lưu ảnh kết quả.
- Build bằng SDK 8 và SDK 10 đều đạt `0 Warning(s)`, `0 Error(s)`.
- Cấu hình GitHub Actions CI để tự động restore và build Release bằng .NET 8 SDK.
- Chia thay đổi thành các commit nhỏ theo Conventional Commits.

### Lỗi đã gặp

1. Endpoint `/api/health` trả trang trắng vì chưa tạo đúng file `HealthController.cs` trong thư mục `Controllers`.
2. Package `Microsoft.OpenApi` phiên bản cũ có cảnh báo bảo mật `NU1903`.
3. SDK 8 không đọc được solution định dạng `.slnx` và báo lỗi `MSB4068`.
4. HTTPS development certificate chưa được Windows tin cậy.

### Cách xử lý

1. Tạo lại `HealthController.cs` đúng thư mục, build và khởi động lại API.
2. Chuyển dự án về .NET 8 LTS và sử dụng `Swashbuckle.AspNetCore` cho Swagger.
3. Thay solution `.slnx` bằng `ProductApi.sln` để tương thích với SDK 8.
4. Chạy `dotnet dev-certs https --trust` để tin cậy chứng chỉ HTTPS local.
5. Luôn build với tiêu chuẩn `0 Warning(s)` và `0 Error(s)` trước khi test endpoint.

## Báo cáo Day 2

### Công việc đã hoàn thành

- Tạo `Product` model với các thuộc tính `Id`, `Name`, `Price` và `Quantity`.
- Tạo `ProductsController` và dữ liệu mock trong bộ nhớ.
- Implement `GET /api/products` với query parameters `search` và `page`.
- Implement `GET /api/products/{id}` với route constraint số nguyên và xử lý `404`.
- Implement `POST /api/products` với `[FromBody]`, tự sinh ID và trả `201 Created` kèm `Location` header.
- Test API bằng Swagger, file `.http` và Postman.
- Tạo Postman collection Day 2 với 6 request, 19 assertions và export theo schema v2.1.
- Chạy Collection Runner đạt `19/19` test passed, `0` failed và `0` error.
- Thực hành breakpoint ở `GetById()`, quan sát model binding `id`, dữ liệu Product tìm thấy và nhánh `NotFound()`.

### Lỗi đã gặp

1. VS Code ban đầu cố build riêng file `ProductsController.cs` thay vì build project, dẫn đến lỗi không nhận `Product`, `StatusCodes` và `FromBody`.
2. Khởi động đồng thời hai phiên debug làm cổng HTTPS `7005` bị chiếm và phát sinh lỗi `address already in use`.

### Cách xử lý

1. Tạo cấu hình debug local `.vscode/launch.json` chạy assembly `ProductApi.dll` của project, sau đó chọn đúng cấu hình `Debug ProductApi` trong Run and Debug.
2. Dừng tất cả phiên debug cũ, chỉ khởi động một phiên rồi gửi request để breakpoint dừng đúng trong controller.

### Phần chưa hoàn thiện trong Day 2

- Endpoint POST hiện nhận trực tiếp `Product`; chưa tách `CreateDto`, `UpdateDto`, `ResponseDto` và validation bằng DataAnnotations vì đó là nội dung của Day 3.
- Dữ liệu hiện là mock in-memory nên sẽ reset khi restart API; chưa có database, Service hoặc Repository vì đó là phần kết nối EF Core của Day 4.
