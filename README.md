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
- Test API bằng Swagger và file `.http`.

## Công nghệ sử dụng

- .NET 8 LTS
- ASP.NET Core Web API
- C#
- Swashbuckle.AspNetCore
- Swagger/OpenAPI
- Git và GitHub

## Yêu cầu môi trường

Cài đặt các công cụ sau trước khi chạy dự án:

- .NET SDK 8.x (khuyến nghị để build và chạy đúng môi trường LTS của dự án)
- Git
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
│       └── day01-health-endpoint.png
├── src/
│   └── ProductApi/
│       ├── Controllers/
│       │   ├── HealthController.cs
│       │   └── InfoController.cs
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

File `src/ProductApi/ProductApi.http` chứa hai request mẫu:

```http
GET https://localhost:7005/api/health
Accept: application/json
```

```http
GET https://localhost:7005/api/info
Accept: application/json
```

Có thể gửi request trực tiếp trong IDE hỗ trợ file `.http` hoặc sử dụng Postman với cùng method và URL.

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
- [x] Lưu ảnh kết quả Health endpoint.

## Báo cáo Day 1

### Công việc đã hoàn thành

- Khởi tạo solution và ASP.NET Core Web API project.
- Chuyển project về .NET 8 LTS để tăng khả năng tương thích.
- Cấu hình Swagger/OpenAPI bằng Swashbuckle.AspNetCore.
- Tạo và test endpoint `GET /api/health`.
- Hoàn thành mini challenge `GET /api/info`.
- Tạo file `.http` để test hai endpoint.
- Build bằng SDK 8 và SDK 10 đều đạt `0 Warning(s)`, `0 Error(s)`.
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
