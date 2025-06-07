# Projekt zaliczeniowy .NET – Clean Architecture StudentServiceMenager

## Jak uruchomić?

1. Otwórz solucję w Visual Studio lub VS Code
2. Uruchom projekt StudentManagementSystem:
   - Domyślnie na http://localhost:5000
   - Panel GraphQL dostępny pod adresem http://localhost:5000/graphql
3. Zaloguj się do systemu:
   - login: admin, hasło: admin (rola Admin)
   - login: user, hasło: user (rola User)
4. Testuj API przez Swagger (http://localhost:5000/swagger) lub plik test_api.sh

## Funkcjonalności

- REST API (CRUD, JWT, Swagger, EF InMemory)
- GraphQL (HotChocolate)
- Panel admina (Razor Pages, role, logowanie)
- Testy jednostkowe (xUnit)
- Skrypt cURL do testowania API

## Przykładowe zapytanie GraphQL

```
{
  students {
    id
    firstName
    lastName
    email
  }
}
```

## Walidacja i obsługa wyjątków
- Formularze sprawdzają poprawność danych (wymagane pola, poprawny email)
- API zwraca odpowiednie kody błędów HTTP (400, 401, 404, 500)
- W panelu admina pojawiają się komunikaty o błędach

## Autor: Opałacz Patrycja 14968