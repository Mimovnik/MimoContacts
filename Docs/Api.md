# Mimo Contacts API

- [Mimo Contacts API](#mimo-contacts-api)
  - [Create Contact](#create-contact)
    - [Create Contact Request](#create-contact-request)
    - [Create Contact Responses](#create-contact-responses)
  - [Get Contact](#get-contact)
    - [Get Contact Request](#get-contact-request)
    - [Get Contact Responses](#get-contact-responses)
    - [Get All Contacts Request](#get-all-contacts-request)
    - [Get All Contacts Responses](#get-all-contacts-responses)
  - [Update Contact](#update-contact)
    - [Update Contact Request](#update-contact-request)
    - [Update Contact Responses](#update-contact-responses)
  - [Delete Contact](#delete-contact)
    - [Delete Contact Request](#delete-contact-request)
    - [Delete Contact Responses](#delete-contact-responses)

## Create Contact

### Create Contact Request

```js
POST {host}/contacts
```

```json
{
    "firstName": "John",
    "lastName": "Cena",
    "email": "magicJohn@gmail.com",
    "password": "wHyDoIeVenHaVeYoUrPaSsWoRd?!123",
    "category": "business",
    "subcategory": "client",
    "phoneNumber": "123456789",
    "birthDate": "1977-04-23",
}
```

### Create Contact Responses

```js
201 Created
```

```yml
Location: {host}/contacts/{id}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "lastModifiedDateTime": "2022-04-06T12:00:00",
    "firstName": "John",
    "lastName": "Cena",
    "email": "magicJohn@gmail.com",
    "password": "wHyDoIeVenHaVeYoUrPaSsWoRd?!123",
    "category": "business",
    "subcategory": "client",
    "phoneNumber": "123456789",
    "birthDate": "1977-04-23",
}
```

or

```js
400 Bad Request
```

## Get Contact

### Get Contact Request

```js
GET {host}/contacts/{id}
```

### Get Contact Responses

```js
200 Ok
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "lastModifiedDateTime": "2022-04-06T12:00:00",
    "firstName": "John",
    "lastName": "Cena",
    "email": "magicJohn@gmail.com",
    "password": "wHyDoIeVenHaVeYoUrPaSsWoRd?!123",
    "category": "business",
    "subcategory": "client",
    "phoneNumber": "123456789",
    "birthDate": "1977-04-23",
}
```

or

```js
404 NotFound
```

### Get All Contacts Request

```js
GET {host}/contacts
```

### Get All Contacts Responses

```js
200 Ok
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "lastModifiedDateTime": "2022-04-06T12:00:00",
    "firstName": "John",
    "lastName": "Cena",
    "email": "magicJohn@gmail.com",
    "password": "wHyDoIeVenHaVeYoUrPaSsWoRd?!123",
    "category": "business",
    "subcategory": "client",
    "phoneNumber": "123456789",
    "birthDate": "1977-04-23",
},
{
    "id": "12332112-1232-1232-1231-123123321230",
    "lastModifiedDateTime": "2023-02-01T22:30:00",
    "firstName": "Elon",
    "lastName": "Musk",
    "email": "elonmusk@spacex.com",
    "password": "ILoveRockets123",
    "category": "private",
    "subcategory": "friend",
    "phoneNumber": "555444333",
    "birthDate": "1971-06-28",
},
```

or

```js
204 No Content
```

## Update Contact

### Update Contact Request

```js
PUT {host}/contacts/{id}
```

```json
{
    "firstName": "John",
    "lastName": "Cena",
    "email": "magicJohn@gmail.com",
    "password": "wHyDoIeVenHaVeYoUrPaSsWoRd?!123",
    "category": "other",
    "subcategory": "wrestler",
    "phoneNumber": "987654321",
    "birthDate": "1977-04-23",
}
```

### Update Contact Responses

```js
204 No Content
```

or

```js
201 Created
```

```yml
Location: {host}/contacts/{id}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "lastModifiedDateTime": "2022-04-06T12:00:00",
    "firstName": "John",
    "lastName": "Cena",
    "email": "magicJohn@gmail.com",
    "password": "wHyDoIeVenHaVeYoUrPaSsWoRd?!123",
    "category": "business",
    "subcategory": "client",
    "phoneNumber": "123456789",
    "birthDate": "1977-04-23",
}
```

## Delete Contact

### Delete Contact Request

```js
DELETE {host}/contacts/{id}
```

### Delete Contact Responses

```js
204 No Content
```

or

```js
404 NotFound
```
