# Warehouse Management

Весь проект (Frontend + Backend) можно запустить через Docker Compose.

---

## Полный запуск (Frontend + Backend)

Для запуска всей системы перейдите в папку с `docker-compose.yml` и выполните команду:

```bash
cd backend/WarehouseManagement
docker-compose up --build
```

- Backend (API + Swagger) будет доступен по адресу: http://localhost:8080
- Frontend (Angular) — http://localhost:4200

## Запуск только Backend

Если необходимо поднять только Backend, используйте отдельный docker-compose:

```bash
cd backend/WarehouseManagement/docker-compose-only-backend
docker-compose up --build
```

- Backend (API + Swagger) будет доступен по адресу: http://localhost:8080

## Запуск только Frontend

```bash
cd frontend/warehouse-management
npm install
npm start
```

- Frontend (Angular) — http://localhost:4200