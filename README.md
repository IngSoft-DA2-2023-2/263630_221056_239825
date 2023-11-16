# 263630_221056_239825
Obligatorio de Diseño de Aplicaciones 2 de Martín Edelman, Tatiana Poznanski y Tomas Bañales. 

Con el fin de iniciar sesión, se debe restaurar la base de datos con el .bak. Para iniciar sesión en la UI, con un usuario con rol "administrador", el mail a ingresar es tatipozna@gmail.com y la contraseña 12345678. 

La URL del endpoint se encuentra en cada archivo .service y es la variable "urlGeneral". La misma corresponde a 'https://merely-loved-gibbon.ngrok-free.app/api/v1'.

Asimismo, se debe hacer un appsettings.json en el backend con la siguiente información:
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "ECommerceDB": "Server=127.0.0.1,1433; Database=dbname; User=sa; Password=password; TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "Martin263630Tatiana221056Tomas239825",
    "Issuer": "http://localhost:7061/",
    "Audience": "http://localhost:7061/",
    "Subject": "baseWebApiSubject"
  }
}
