# PagOnline-API
API de pago simulado con .NET Core y Oracle 18cXE

## About
### Librerías utilizadas:
 - SwaggerUI
 - EntityFramework Oracle
 - Automapper
 - FluentEmail
 
### Importante:
 - Cambiar la ConnectionString en `AppSettings.json`
 - Puerto de la API: `localhost:7000` (agregar `/swagger.json` a la ruta en caso de no aparecer interfaz visual.)

### Integracion con Mindicador.cl (cambio de divisa USD, UTM, UF)
 - Documentacion API: https://mindicador.cl/

### Integracion con envío de correos vía SMTP (ambiente desarrollo)
 - link del software `PaperCut-SMTP Server`: https://github.com/ChangemakerStudios/Papercut-SMTP
 - puerto: `25`
