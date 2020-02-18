# Holidays

GreenFlux Holiday Optimizer Assignment
Create an API wrapper around the Nager.Date API. This API allows you to retrieve all public holidays
for a country in a specific year (see https://date.nager.at/Home/Api for api docs).
Use this array as input for supported country codes in Nager.Date API: ["AD", "AR", "AT", "AU", "AX",
"BB", "BE", "BG", "BO", "BR", "BS", "BW", "BY", "BZ", "CA", "CH", "CL", "CN", "CO", "CR", "CU", "CY",
"CZ", "DE", "DK", "DO", "EC", "EE", "EG", "ES", "FI", "FO", "FR", "GA", "GB", "GD", "GL", "GR", "GT",
"GY", "HN", "HR", "HT", "HU", "IE", "IM", "IS", "IT", "JE", "JM", "LI", "LS", "LT", "LU", "LV", "MA",
"MC", "MD", "MG", "MK", "MT", "MX", "MZ", "NA", "NI", "NL", "NO", "NZ", "PA", "PE", "PL", "PR",
"PT", "PY", "RO", "RS", "RU", "SE", "SI", "SJ", "SK", "SM", "SR", "SV", "TN", "TR", "UA", "US", "UY",
"VA", "VE", "ZA"].
Requirements:
- be kind to API’s you are calling and don’t DDOS it, always throttle your requests,
- be able to handle garbage-in scenario’s
- implement in .NET Core,
- use Swagger,
- write unit tests.
Operations that should be exposed:
1. Which country had the most holidays this year?
2. Which month had most holidays if you compare globally?
3. Which country had the most unique holidays? E.g. days that no other country had a holiday.
4. Bonus question: what is the longest lasting sequence of holidays around the world you can
find this year if you could travel at lightspeed between countries and timezones?
o Tip: Use RestCountries.eu API to lookup timezone information per country, e.g.
https://restcountries.eu/rest/v2/alpha/nl

# Link to access the api

https://holidayoptimizationsweb20200218065439.azurewebsites.net/swagger

