# Módulo de Seguridad - API Documentation

## Descripción
Este módulo proporciona un CRUD completo para la gestión de **Roles**, **Usuarios** y **Permisos** del sistema, con endpoints especializados para listar usuarios con sus roles y roles con sus permisos. **Incluye autenticación con BCrypt**.

## Estructura de Base de Datos

### Esquema: `security`

#### Tablas:
- **tblRol**: Almacena los roles del sistema
- **tblUsuario**: Almacena los usuarios del sistema (contraseñas hasheadas con BCrypt)
- **tblPermiso**: Almacena los permisos disponibles
- **tblRolPermiso**: Relación N:N entre roles y permisos

---

## Endpoints del Controlador de Seguridad

Base URL: `/api/Seguridad`

---

## ?? AUTENTICACIÓN

### 1. Login - Autenticar Usuario
```http
POST /api/Seguridad/login
```

**Descripción:** Autentica un usuario verificando su nombre de usuario y contraseña (hasheada con BCrypt en la base de datos).

**Request Body:**
```json
{
  "nombreUsuario": "admin",
  "contrasena": "Admin123!"
}
```

**Response Exitosa:** `200 OK`
```json
{
  "success": true,
  "message": "Autenticación exitosa",
  "usuario": {
    "idUsuario": 1,
    "nombreUsuario": "admin",
    "idRol": 1,
    "nombreRol": "Administrador",
    "estaActivo": true,
    "ultimoIngreso": "2024-01-15T10:30:00",
    "usuarioCreacion": "SYSTEM",
    "fechaCreacion": "2024-01-01T00:00:00",
    "usuarioModificacion": null,
    "fechaModificacion": null
  }
}
```

**Response Error - Usuario no encontrado:** `401 Unauthorized`
```json
{
  "success": false,
  "message": "Usuario no encontrado",
  "usuario": null
}
```

**Response Error - Contraseña incorrecta:** `401 Unauthorized`
```json
{
  "success": false,
  "message": "Contraseña incorrecta",
  "usuario": null
}
```

**Response Error - Usuario inactivo:** `401 Unauthorized`
```json
{
  "success": false,
  "message": "Usuario inactivo",
  "usuario": null
}
```

**Response Error - Datos faltantes:** `400 Bad Request`
```json
{
  "success": false,
  "message": "Usuario y contraseña son requeridos",
  "usuario": null
}
```

---

## ?? ROLES

### 1. Listar todos los roles
```http
GET /api/Seguridad/roles
```

**Response:**
```json
[
  {
    "idRol": 1,
    "nombre": "Administrador",
    "descripcion": "Acceso total al sistema",
    "estaActivo": true,
    "usuarioCreacion": "SYSTEM",
    "fechaCreacion": "2024-01-15T10:30:00",
    "usuarioModificacion": null,
    "fechaModificacion": null
  }
]
```

### 2. Obtener rol por ID
```http
GET /api/Seguridad/roles/{id}
```

**Response:**
```json
{
  "idRol": 1,
  "nombre": "Administrador",
  "descripcion": "Acceso total al sistema",
  "estaActivo": true,
  "usuarioCreacion": "SYSTEM",
  "fechaCreacion": "2024-01-15T10:30:00"
}
```

### 3. Crear nuevo rol
```http
POST /api/Seguridad/roles
```

**Request Body:**
```json
{
  "nombre": "Gerente",
  "descripcion": "Acceso a gestión administrativa",
  "estaActivo": true,
  "usuarioCreacion": "admin"
}
```

**Response:** `201 Created`

### 4. Actualizar rol
```http
PUT /api/Seguridad/roles/{id}
```

**Request Body:**
```json
{
  "nombre": "Gerente General",
  "descripcion": "Acceso ampliado a gestión",
  "estaActivo": true,
  "usuarioModificacion": "admin"
}
```

**Response:** `200 OK`

### 5. Eliminar rol
```http
DELETE /api/Seguridad/roles/{id}
```

**Response:** `204 No Content`

### 6. ? Listar roles con sus permisos (ESPECIAL)
```http
GET /api/Seguridad/roles-con-permisos
```

**Response:**
```json
[
  {
    "idRol": 1,
    "nombre": "Administrador",
    "descripcion": "Acceso total al sistema",
    "estaActivo": true,
    "permisos": [
      {
        "idPermiso": 1,
        "nombre": "Crear Usuario",
        "descripcion": "Permite crear nuevos usuarios",
        "codigo": "USUARIO_CREAR"
      },
      {
        "idPermiso": 2,
        "nombre": "Editar Usuario",
        "descripcion": "Permite editar usuarios existentes",
        "codigo": "USUARIO_EDITAR"
      }
    ]
  }
]
```

### 7. Asignar permisos a un rol
```http
POST /api/Seguridad/roles/asignar-permisos
```

**Request Body:**
```json
{
  "idRol": 2,
  "idsPermisos": [1, 2, 4, 10, 11]
}
```

**Response:** `200 OK`

---

## ?? USUARIOS

### 1. Listar todos los usuarios
```http
GET /api/Seguridad/usuarios
```

**Response:**
```json
[
  {
    "idUsuario": 1,
    "nombreUsuario": "admin",
    "idRol": 1,
    "nombreRol": "Administrador",
    "estaActivo": true,
    "ultimoIngreso": "2024-01-15T09:00:00",
    "usuarioCreacion": "SYSTEM",
    "fechaCreacion": "2024-01-01T00:00:00"
  }
]
```

### 2. Obtener usuario por ID
```http
GET /api/Seguridad/usuarios/{id}
```

### 3. Crear nuevo usuario
```http
POST /api/Seguridad/usuarios
```

**Request Body:**
```json
{
  "nombreUsuario": "jperez",
  "contrasena": "Password123!",
  "idRol": 2,
  "estaActivo": true,
  "usuarioCreacion": "admin"
}
```

**NOTA:** La contraseña será automáticamente hasheada con BCrypt antes de almacenarse.

**Response:** `201 Created`

### 4. Actualizar usuario
```http
PUT /api/Seguridad/usuarios/{id}
```

**Request Body:**
```json
{
  "nombreUsuario": "jperez",
  "contrasena": "NewPassword123!",
  "idRol": 2,
  "estaActivo": true,
  "usuarioModificacion": "admin"
}
```

**NOTA:** Si se proporciona una nueva contraseña, será automáticamente hasheada con BCrypt.

**Response:** `200 OK`

### 5. Eliminar usuario
```http
DELETE /api/Seguridad/usuarios/{id}
```

**Response:** `204 No Content`

### 6. ? Listar usuarios con su rol asociado (ESPECIAL)
```http
GET /api/Seguridad/usuarios-con-rol
```

**Response:**
```json
[
  {
    "idUsuario": 1,
    "nombreUsuario": "admin",
    "estaActivo": true,
    "ultimoIngreso": "2024-01-15T09:00:00",
    "rol": {
      "idRol": 1,
      "nombre": "Administrador",
      "descripcion": "Acceso total al sistema",
      "estaActivo": true
    }
  }
]
```

---

## ?? PERMISOS

### 1. Listar todos los permisos
```http
GET /api/Seguridad/permisos
```

### 2. Obtener permiso por ID
```http
GET /api/Seguridad/permisos/{id}
```

### 3. Crear nuevo permiso
```http
POST /api/Seguridad/permisos
```

### 4. Actualizar permiso
```http
PUT /api/Seguridad/permisos/{id}
```

### 5. Eliminar permiso
```http
DELETE /api/Seguridad/permisos/{id}
```

---

## ?? Instrucciones de Implementación

### 1. Ejecutar el Script SQL
```sql
-- Ejecutar el archivo: Database/Scripts/CreateSecurityTables.sql
-- Esto creará las tablas y usuarios con contraseñas BCrypt
```

### 2. Credenciales por defecto
Las contraseñas están hasheadas con BCrypt:
- **Usuario:** `admin` / **Contraseña:** `Admin123!` / **Rol:** Administrador
- **Usuario:** `supervisor` / **Contraseña:** `Supervisor123!` / **Rol:** Supervisor
- **Usuario:** `usuario` / **Contraseña:** `Usuario123!` / **Rol:** Usuario

### 3. Probar el endpoint de login
```bash
curl -X POST "https://localhost:5001/api/Seguridad/login" \
  -H "Content-Type: application/json" \
  -d '{
    "nombreUsuario": "admin",
    "contrasena": "Admin123!"
  }'
```

---

## ?? Seguridad Implementada

### ? BCrypt Password Hashing
- Las contraseñas se almacenan hasheadas con BCrypt (algoritmo adaptativo)
- Factor de trabajo: 11 (configurable)
- Cada contraseña tiene un salt único generado automáticamente
- Resistente a ataques de fuerza bruta y rainbow tables

### ? Validación de Credenciales
- Verificación segura con `BCrypt.Verify()`
- No se expone información sobre la existencia de usuarios
- Registro del último ingreso
- Validación de usuario activo

### ? Paquete Utilizado
```xml
<PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />
```

---

## ?? Ejemplos de Pruebas

### Autenticar con credenciales correctas:
```bash
curl -X POST "http://localhost:5000/api/Seguridad/login" \
  -H "Content-Type: application/json" \
  -d '{
    "nombreUsuario": "admin",
    "contrasena": "Admin123!"
  }'
```

### Crear usuario (la contraseña se hasheará automáticamente):
```bash
curl -X POST "http://localhost:5000/api/Seguridad/usuarios" \
  -H "Content-Type: application/json" \
  -d '{
    "nombreUsuario": "test",
    "contrasena": "Test123!",
    "idRol": 3,
    "estaActivo": true,
    "usuarioCreacion": "admin"
  }'
```

### Actualizar contraseña (se hasheará automáticamente):
```bash
curl -X PUT "http://localhost:5000/api/Seguridad/usuarios/1" \
  -H "Content-Type: application/json" \
  -d '{
    "nombreUsuario": "admin",
    "contrasena": "NewAdmin123!",
    "idRol": 1,
    "estaActivo": true,
    "usuarioModificacion": "admin"
  }'
```

---

## ?? Flujo de Autenticación

```
1. Usuario envía credenciales ? POST /api/Seguridad/login
2. Sistema busca usuario en BD por nombreUsuario
3. Si existe:
   - Verifica si está activo
   - Compara contraseña con BCrypt.Verify()
   - Si coincide: actualiza ultimoIngreso y retorna datos
   - Si no coincide: retorna error 401
4. Si no existe: retorna error 401
```

---

## ?? Notas de Seguridad

1. ? **BCrypt implementado** - Contraseñas hasheadas de forma segura
2. ?? **JWT recomendado** - Para autenticación stateless, considera implementar JWT
3. ?? **HTTPS obligatorio** - Usar HTTPS en producción
4. ?? **Rate limiting** - Implementar limitación de intentos de login
5. ?? **Auditoría** - Registrar intentos fallidos de autenticación

---

## ? Checklist de Implementación

- [x] Entidades creadas (Rol, Usuario, Permiso, RolPermiso)
- [x] DTOs creados (incluido LoginDto y LoginResponseDto)
- [x] Repositorios implementados
- [x] Servicios implementados
- [x] Controlador creado con endpoint de login
- [x] BCrypt instalado y configurado
- [x] AutoMapper configurado
- [x] Dependencias registradas en Program.cs
- [x] Script SQL con contraseñas BCrypt
- [x] Documentación actualizada
- [ ] Ejecutar script SQL en base de datos
- [ ] Probar endpoint de login en Swagger
- [ ] Implementar JWT Authentication (recomendado)
- [ ] Implementar rate limiting (recomendado)

---

**Autor:** Sistema SGTNApi  
**Fecha:** 2024  
**Versión:** 2.0 (Con BCrypt Authentication)
