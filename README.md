# Projekt zaliczeniowy .NET – Clean Architecture

## Jak uruchomić?

1. Otwórz solucję w Visual Studio lub VS Code.
2. Uruchom projekty:
   - Presentation.RestApi (domyślnie na http://localhost:5000)
   - Presentation.GraphQLApi (domyślnie na http://localhost:5001)
   - Presentation.RazorPages (panel admina, domyślnie na http://localhost:5002)
3. Zaloguj się do panelu:
   - login: admin, hasło: admin (rola Admin)
   - login: user, hasło: user (rola User)
4. Testuj API przez Swagger lub plik test_api.sh

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
- Formularze Razor Pages sprawdzają poprawność danych (np. wymagane pola, poprawny email).
- API zwraca odpowiednie kody błędów HTTP (400, 401, 404, 500).
- W panelu admina pojawiają się komunikaty o błędach.

## Autor: Patrycja Opałacz 14968