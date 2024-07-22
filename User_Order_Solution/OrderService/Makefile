.PHONY: build run stop migrate

build:
	docker-compose build

run:
	docker-compose up -d

stop:
	docker-compose down

migrate:
	docker-compose run --rm orderservice dotnet ef database update

tests:
	docker-compose run --rm orderservice dotnet test
