using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace SysVotaciones.BLL
{
    public static class Helper
    {
        private static readonly string secretKey = "JBJBJBJBJHB/T(/&/%&R%&FYYGU@GJHGJgjjgjhgYgyugyugyyugguyGYUGYUGYggHGHGJGJHJG";
        public static bool AllPropertiesHaveValue(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] propertyInfos = type.GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                object? value = propertyInfo.GetValue(obj);
                Console.WriteLine(propertyInfo);

                if (value is int && (int)value == default) return false;

                if (value == null || (value is string && string.IsNullOrWhiteSpace((string)value))) 
                    return false;
            }

            return true;
        }

        public static string GenerateToken(string user = "", string role = "")
        {

            byte[] byteKey = Encoding.UTF8.GetBytes(secretKey); 
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey),
                                                            SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }

        public static Claim? ValidateToken(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var validationParameters = new TokenValidationParameters
            {
                // Define la clave secreta utilizada para firmar el token
                IssuerSigningKey = securityKey,

                // Opcional: establece la validez de la fecha de expiración
                ValidateLifetime = true,

                // Opcional: establece la validez del emisor (issuer)
                ValidateIssuer = false, // Establece a true si deseas validar el emisor
                ValidIssuer = "your_issuer_here", // Establece el emisor válido

                // Opcional: establece la validez del destinatario (audience)
                ValidateAudience = false, // Establece a true si deseas validar el destinatario
                ValidAudience = "your_audience_here", // Establece el destinatario válido
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                // Acceder a una reclamación específica por su tipo
                var claim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
                
                return claim;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static string GenerateToken2()
        {
            // Crear las reclamaciones del token
            var claims = new[]
            {
            new Claim("sub", "1234567890"),  // Sujeto del token (ID del usuario)
            new Claim("iss", "auth_server")  // Emisor del token (identifica al servidor de autenticación)
        };

            // Configurar la clave de seguridad utilizada para firmar el token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Crear el token JWT
            var token = new JwtSecurityToken(
                issuer: "auth_server",      // Emisor del token
                audience: "api_client",     // Destinatario del token (aplicación cliente)
                claims: claims,             // Reclamaciones del token
                expires: DateTime.Now.AddHours(1),  // Fecha de expiración del token (1 hora desde ahora)
                signingCredentials: credentials  // Credenciales de firma del token
            );

            // Generar el token como una cadena JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
