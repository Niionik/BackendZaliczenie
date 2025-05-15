#!/bin/bash

# Logowanie jako admin
ADMIN_TOKEN=$(curl -s -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"admin"}' | jq -r .token)
echo "Admin JWT: $ADMIN_TOKEN"

# Logowanie jako user
USER_TOKEN=$(curl -s -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"user","password":"user"}' | jq -r .token)
echo "User JWT: $USER_TOKEN"

# Dodanie studenta (admin)
curl -X POST http://localhost:5000/api/students \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $ADMIN_TOKEN" \
  -d '{"firstName":"Jan","lastName":"Kowalski","email":"jan.kowalski@example.com"}'
echo -e "\nDodano studenta."

# Pobranie wszystkich studentów
curl -X GET http://localhost:5000/api/students

echo -e "\nLista studentów."

# Dodanie kursu (admin)
curl -X POST http://localhost:5000/api/courses \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $ADMIN_TOKEN" \
  -d '{"name":"Matematyka","description":"Podstawy matematyki"}'
echo -e "\nDodano kurs."

# Pobranie wszystkich kursów
curl -X GET http://localhost:5000/api/courses

echo -e "\nLista kursów."

# Dodanie zapisu (admin)
curl -X POST http://localhost:5000/api/enrollments \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $ADMIN_TOKEN" \
  -d '{"studentId":1,"courseId":1}'
echo -e "\nDodano zapis."

# Pobranie wszystkich zapisów
curl -X GET http://localhost:5000/api/enrollments

echo -e "\nLista zapisów." 