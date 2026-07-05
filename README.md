# cStudy

C# ASP.NET Core Controller-Service 구조와 PostgreSQL Docker DB로 만든 게시판 CRUD 예제입니다.

## 구조

```text
Controllers/PostsController.cs   HTTP 요청/응답 처리
Services/Create/                 게시글 생성 Service/ServiceImpl
Services/Get/                    게시글 조회 Service/ServiceImpl
Services/Update/                 게시글 수정 Service/ServiceImpl
Services/Delete/                 게시글 삭제 Service/ServiceImpl
Services/Mapping/PostMapper.cs   Entity -> Response 변환
Dao/IPostDao.cs                  게시글 DAO 인터페이스
Dao/PostDao.cs                   EF Core 기반 DAO 구현체
Data/AppDbContext.cs             EF Core DB Context
Models/Post.cs                   게시글 Entity
Dtos/Requests/                   생성/수정 Request DTO
Dtos/Responses/                  조회/생성/수정 Response DTO
docker-compose.yml               API + PostgreSQL 실행 구성
```

흐름은 `Controller -> CRUD별 Service -> DAO -> AppDbContext -> PostgreSQL` 입니다.

## 실행

```bash
cd /Users/sol/Downloads/cStudy
docker compose up --build
```

Docker Desktop이 실행 중이어야 합니다. API 주소는 `http://localhost:8080` 입니다.

## API

### 게시글 생성

```bash
curl -X POST http://localhost:8080/api/posts \
  -H "Content-Type: application/json" \
  -d '{"title":"첫 글","content":"게시판 테스트입니다.","author":"sol"}'
```

### 전체 조회

```bash
curl http://localhost:8080/api/posts
```

### 단건 조회

```bash
curl http://localhost:8080/api/posts/1
```

### 수정

```bash
curl -X PUT http://localhost:8080/api/posts/1 \
  -H "Content-Type: application/json" \
  -d '{"title":"수정된 글","content":"내용을 수정했습니다.","author":"sol"}'
```

### 삭제

```bash
curl -X DELETE http://localhost:8080/api/posts/1
```

## DB

DB는 프로젝트 폴더 안에 파일로 생성되는 방식이 아니라 Docker의 PostgreSQL 컨테이너로 실행됩니다.

- Container: `cstudy-postgres`
- Volume: `cstudy_cstudy-postgres-data`
- Table: `posts`

`Program.cs`에서 `db.Database.EnsureCreated()`를 실행하기 때문에 API가 처음 시작될 때 PostgreSQL 안에 `posts` 테이블이 자동 생성됩니다.

## DB 접속 정보

- Host: `localhost`
- Port: `5433`
- Database: `cstudy`
- Username: `cstudy`
- Password: `cstudy1234`

## 종료

```bash
docker compose down
```

DB 데이터까지 삭제하려면:

```bash
docker compose down -v
```
