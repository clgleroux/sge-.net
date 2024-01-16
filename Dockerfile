# Use postgres/example user/password credentials
FROM postgres:latest

ENV POSTGRES_PASSWORD=example

EXPOSE 5432

COPY init-database.sql /docker-entrypoint-initdb.d/