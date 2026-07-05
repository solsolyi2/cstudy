# cStudy

C# ASP.NET Core Minimal API와 PostgreSQL Docker DB로 만든 게시판 CRUD 예제입니다.

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
