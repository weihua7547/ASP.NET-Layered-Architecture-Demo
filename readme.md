# ASP.NET Core Layered Architecture

### Subtitle：羽球場預約系統（Badminton Reservation System）

本專案採用簡單的分層式架構設計，將系統依照責任拆分，以降低耦合並提升維護性。

## Project Structure

``` text
Badminton.Contract
Badminton.DataAccess
Badminton.Model
Badminton.Service
BadmintonAPI
├── Controllers
├── Extensions
├── Handler
├── Middleware
├── Migrations
```

------------------------------------------------------------------------

## Layer Responsibilities

### BadmintonAPI

系統入口層。

負責：

-   API 路由
-   Controller
-   Middleware
-   DI 設定
-   啟動程式

------------------------------------------------------------------------

### Badminton.Service

服務邏輯層。

負責：

-   預約規則
-   場地檢查
-   時段衝突判定
-   流程控制

避免 Controller 過度肥大。

------------------------------------------------------------------------

### Badminton.DataAccess

資料存取層。

負責：

-   EF Core 操作
-   CRUD
-   Database Query
-   Repository

僅處理資料。

------------------------------------------------------------------------

### Badminton.Model

資料模型層。

負責：

-   Entity
-   DTO
-   Database Schema

範例：

-   Field
-   TimeSlot
-   Reservation
-   Member
-   Order

------------------------------------------------------------------------

### Badminton.Contract

介面與契約層。

負責：

-   Interface
-   Request/Response Contract
-   Service Interface

範例：

``` csharp
IReservationService
IArenaRepository
```

------------------------------------------------------------------------

## Request Flow

``` text
Client
   ↓
Controller
   ↓
Service
   ↓
DataAccess
   ↓
EF Core
   ↓
SQL Server
```

