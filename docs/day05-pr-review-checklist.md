# Checklist review Pull Request - Week 5 Day 5

Checklist này dùng để review chéo Product API trước khi merge Pull Request vào `main`.

## 1. Phạm vi và Git

- [ ] Pull Request chỉ chứa thay đổi thuộc phạm vi Week 5 Day 5.
- [ ] Branch có tên đúng quy ước `feature/week5-day05-<tenhocvien>`.
- [ ] Commit nhỏ, rõ nghĩa và tuân theo Conventional Commits.
- [ ] Không commit password, connection string chứa password, User Secrets hoặc dữ liệu nhạy cảm.
- [ ] Không commit file build như `bin/`, `obj/` hoặc file tạm của IDE.
- [ ] Working tree sạch trước khi push.
- [ ] GitHub Actions chạy thành công trước khi merge.

## 2. REST API và HTTP

- [ ] Route Product dùng tiền tố `/api/products` nhất quán.
- [ ] `GET /api/products` hỗ trợ danh sách, tìm kiếm và phân trang.
- [ ] `GET /api/products/{id}` trả `200 OK` khi tồn tại và `404 Not Found` khi không tồn tại.
- [ ] `POST /api/products` trả `201 Created` và có `Location` header.
- [ ] `PUT /api/products/{id}` trả `200 OK`, `400 Bad Request`, `404 Not Found` hoặc `409 Conflict` đúng trường hợp.
- [ ] `DELETE /api/products/{id}` trả `204 No Content` khi xóa thành công và `404 Not Found` khi ID không tồn tại.
- [ ] Validation lỗi trả `400 Bad Request` với Problem Details.
- [ ] Duplicate name trong cùng category trả `409 Conflict`.
- [ ] Response status và body khớp với tài liệu API.

## 3. Controller

- [ ] Controller có `[ApiController]` và route rõ ràng.
- [ ] Controller chỉ xử lý HTTP concern: binding, status code và gọi Service.
- [ ] Controller không truy cập trực tiếp `AppDbContext` hoặc Repository.
- [ ] Controller không chứa mapping Entity-DTO hoặc business rule.
- [ ] Action database dùng `async`/`await` và nhận `CancellationToken`.
- [ ] Swagger response metadata phản ánh đúng các status có thể trả về.

## 4. DTO và validation

- [ ] Create DTO không chứa `Id`.
- [ ] Update DTO không lấy `Id` từ body; ID được lấy từ route.
- [ ] Response DTO có các trường cần thiết cho client.
- [ ] Name bắt buộc và dài từ 2 đến 100 ký tự.
- [ ] `CategoryId` lớn hơn `0`.
- [ ] Price lớn hơn `0` và validation decimal không phụ thuộc culture.
- [ ] Quantity lớn hơn hoặc bằng `0`.
- [ ] API không trả trực tiếp Entity từ Controller.

## 5. Service

- [ ] Service chịu trách nhiệm mapping Entity sang Response DTO.
- [ ] Service chuẩn hóa tên Product trước khi lưu.
- [ ] Service kiểm tra duplicate name trong cùng category khi create và update.
- [ ] Update loại trừ chính Product đang cập nhật khỏi kiểm tra duplicate.
- [ ] Service xử lý race condition từ PostgreSQL unique violation.
- [ ] Service không chứa HTTP-specific result như `IActionResult`.

## 6. Repository và Entity Framework Core

- [ ] Repository chịu trách nhiệm query và persistence qua `AppDbContext`.
- [ ] Query chỉ đọc danh sách dùng `AsNoTracking()`.
- [ ] Tìm kiếm dùng truy vấn database, không tải toàn bộ dữ liệu về memory trước khi lọc.
- [ ] Phân trang dùng `OrderBy`, `Skip` và `Take`.
- [ ] Toàn bộ I/O database dùng API bất đồng bộ.
- [ ] Không có `.Result`, `.Wait()`, `async void` hoặc `Task.Run()` để bọc database I/O.
- [ ] Migration được lưu trong repository.
- [ ] EF Core không báo pending model changes.

## 7. Dependency Injection và cấu hình

- [ ] `AppDbContext` được đăng ký bằng `AddDbContext`.
- [ ] Repository và Service được đăng ký bằng `AddScoped`.
- [ ] Controller nhận dependency qua constructor.
- [ ] Không tự tạo Service, Repository hoặc DbContext bằng `new`.
- [ ] Connection string được đọc từ configuration.
- [ ] Password local được lưu bằng .NET User Secrets và không xuất hiện trong source/README.

## 8. PostgreSQL và business rule

- [ ] Product được lưu trong PostgreSQL thật, không dùng danh sách mock.
- [ ] Price có precision phù hợp.
- [ ] Seed data không xung đột với identity sequence.
- [ ] Cột Name hỗ trợ so sánh không phân biệt hoa/thường.
- [ ] Unique index `(Name, CategoryId)` tồn tại trong schema/migration.
- [ ] Cùng tên trong category khác vẫn được phép tạo.
- [ ] Dữ liệu vẫn tồn tại đúng sau khi restart API.

## 9. Kiểm thử và tài liệu

- [ ] Build Release đạt `0 Warning(s)` và `0 Error(s)`.
- [ ] Không có NuGet package vulnerable theo nguồn package hiện tại.
- [ ] Postman collection export theo schema v2.1.
- [ ] Collection kiểm tra đủ list, search, detail, create, read-back, update, validation, duplicate, delete và not found.
- [ ] Collection dùng dữ liệu động và tự dọn Product thử.
- [ ] README có hướng dẫn PostgreSQL, User Secrets, migration và chạy API.
- [ ] README liệt kê đầy đủ endpoint, request contract, response và status code.
- [ ] Có daily report: công việc đã làm, lỗi gặp, cách xử lý và phần cần tiếp tục.
- [ ] Có kịch bản hoặc video demo CRUD khoảng 5 phút.

## Kết luận review

- [ ] Approve: đáp ứng đầy đủ yêu cầu và có thể merge.
- [ ] Request changes: ghi rõ file, dòng, vấn đề, lý do và hướng sửa đề xuất.
