using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

namespace BLL
{
    public class BLL_Usuario
    {
        DAL_Usuario dal_usuario = new DAL_Usuario();
        BLL_Bitacora bll_bitacora = new BLL_Bitacora();
        BLL_DVV bll_dvv = new BLL_DVV();
        BLL_HistorialCambios bll_historialCambios = new BLL_HistorialCambios();

        public BE_Usuario ObtenerUsuarioPorId(int IdUsuario)
        {
            try
            {
                return dal_usuario.ObtenerUsuarioPorId(IdUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
         
        }

        public BE_LoginResultado ObtenerUsuarioPorEmail(string Email, string Password)
        {
            try
            {
                Password = HashHelper.GenerarHash(Password);

                BE_Usuario Usuario = dal_usuario.ObtenerUsuarioPorEmail(Email);

                if (Usuario == null)
                {
                    bll_bitacora.RegistrarEvento(null, AccionBitacora.LOGIN_INTENTO, "LOGIN", $"Intento de Login usando el email: {Email}");

                    return new BE_LoginResultado { ExitoLogin = false, Mensaje = "Usuario o Contraseña Incorrecto" };
                }

                if (!Usuario.Activo)
                {
                    bll_bitacora.RegistrarEvento(Usuario.IdUsuario, AccionBitacora.LOGIN_INTENTO, "LOGIN", $"Intento de sesion del usuario bloqueado: {Usuario.Nombre}, con IdUsuario = {Usuario.IdUsuario}");

                    return new BE_LoginResultado { ExitoLogin = false, Mensaje = "Usuario bloqueado. Contactese con el Administrador" };
                }

                if (Usuario.HashPassword != Password)
                {
                    Usuario.IntentosFallidos++;

                    if (Usuario.IntentosFallidos > 3)
                    {
                        CalcularDVH(Usuario);

                        dal_usuario.BloquearUsuario(Usuario.IdUsuario, Usuario.DVH);

                        bll_dvv.ActualizarDVV("Usuario");

                        bll_bitacora.RegistrarEvento(Usuario.IdUsuario, AccionBitacora.LOGIN_BLOQUEADO, "LOGIN", $"Se bloquea al usuario: {Usuario.Nombre}, ID: {Usuario.IdUsuario}, por superar la cantidad de intentos permitidos");

                        return new BE_LoginResultado { ExitoLogin = false, Mensaje = "Usuario Bloqueado. Contacte Administrador" };
                    }
                    else
                    {
                        CalcularDVH(Usuario);

                        dal_usuario.ActualizarIntentosFallidos(Usuario.IntentosFallidos, Usuario.IdUsuario, Usuario.DVH);

                        bll_dvv.ActualizarDVV("Usuario");

                        bll_bitacora.RegistrarEvento(Usuario.IdUsuario, AccionBitacora.LOGIN_INCORRECTO, "LOGIN", $"Intento de inicio de sesion con el usuario: {Usuario.Nombre}, ID: {Usuario.IdUsuario} con contraseña incorrecta");

                        return new BE_LoginResultado { ExitoLogin = false, Mensaje = $"Contraseña Incorrecta. Intentos fallidos: {Usuario.IntentosFallidos}" };
                    }

                }
                else
                {
                    Usuario.IntentosFallidos = 0;

                    CalcularDVH(Usuario);

                    dal_usuario.ActualizarIntentosFallidos(Usuario.IntentosFallidos, Usuario.IdUsuario, Usuario.DVH);

                    bll_dvv.ActualizarDVV("Usuario");

                    bll_bitacora.RegistrarEvento(Usuario.IdUsuario, AccionBitacora.LOGIN_OK, "LOGIN", $"Login correcto del usuario: {Usuario.Nombre}, ID: {Usuario.IdUsuario}");

                    return new BE_LoginResultado { ExitoLogin = true, Usuario = Usuario, Mensaje = "Login exitoso" };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        public List<BE_Usuario> ListaUsuarios()
        {
            try
            {
                return dal_usuario.ListaUsuario();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public void AgregarUsuario(BE_Usuario usuario)
        {
            try
            {
                usuario.HashPassword = HashHelper.GenerarHash(usuario.HashPassword);

                CalcularDVH(usuario);

                dal_usuario.AgregarUsuario(usuario);

                bll_dvv.ActualizarDVV("Usuario");
                bll_historialCambios.RegistrarAlta("Usuario", usuario.IdUsuario, Sesion.Instancia().UsuarioActual.IdUsuario);

                bll_bitacora.RegistrarEvento(Sesion.Instancia().UsuarioActual.IdUsuario, AccionBitacora.USUARIO_ALTA, "USUARIO", $"Se crea un nuevo usuario: {usuario.Nombre}, {usuario.Apellido}, creado por {Sesion.Instancia().UsuarioActual.Nombre}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public void ModificarUsuario(BE_Usuario usuarioNuevo)
        {
            try
            {
                BE_Usuario usuarioAnterior = dal_usuario.ObtenerUsuarioPorId(usuarioNuevo.IdUsuario);

                usuarioNuevo.HashPassword = HashHelper.GenerarHash(usuarioNuevo.HashPassword);  

                CalcularDVH(usuarioNuevo);

                dal_usuario.ModificarUsuario(usuarioNuevo);

                bll_dvv.ActualizarDVV("Usuario");

                int? idEditor = Sesion.Instancia().UsuarioActual?.IdUsuario;
            
                bll_historialCambios.RegistrarCambio(
                    usuarioAnterior,
                    usuarioNuevo,
                   idEditor ?? 0
                );

                string modificadoPor = Sesion.Instancia().UsuarioActual?.Nombre ?? "SISTEMA";

                bll_bitacora.RegistrarEvento(
                    idEditor,
                    AccionBitacora.USUARIO_MODIFICACION,
                    "USUARIO",
                    $"Se modifica al usuario: {usuarioNuevo.Nombre}, ID: {usuarioNuevo.IdUsuario}. Modificado por: {modificadoPor}"
                );
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error en ModificarUsuario: " + ex.Message, ex);
            }
          
        }

        public void RestaurarUsuario(BE_Usuario usuarioRestaurado, DateTime fechaRestaurada)
        {
            try
            {
                BE_Usuario usuarioAnterior = dal_usuario.ObtenerUsuarioPorId(usuarioRestaurado.IdUsuario);

                CalcularDVH(usuarioRestaurado);

                dal_usuario.ModificarUsuario(usuarioRestaurado);

                bll_dvv.ActualizarDVV("Usuario");

                int? idEditor = Sesion.Instancia().UsuarioActual?.IdUsuario;

                bll_historialCambios.RegistrarCambio(usuarioAnterior, usuarioRestaurado, idEditor ?? 0);

                string restauradoPor = Sesion.Instancia().UsuarioActual?.Nombre ?? "SISTEMA";

                bll_bitacora.RegistrarEvento(
                    idEditor,
                    AccionBitacora.USUARIO_RESTAURACION,
                    "USUARIO",
                    $"Se restaura al usuario: {usuarioRestaurado.Nombre}, ID: {usuarioRestaurado.IdUsuario} al estado del {fechaRestaurada:dd/MM/yyyy HH:mm}. Restaurado por: {restauradoPor}"
                );
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error en RestaurarUsuario: " + ex.Message, ex);
            }
        }

        public void CerrarSesion()
        {
            try
            {
                BE_Usuario usuario = Sesion.Instancia().UsuarioActual;

                if (usuario != null)
                {
                    bll_bitacora.RegistrarEvento(usuario.IdUsuario, AccionBitacora.LOGOUT, "LOGIN", $"Logout del usuario: {usuario.Nombre}, ID: {usuario.IdUsuario}");
                }

                Sesion.Instancia().CerrarSesion();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EliminarUsuario(int id)
        {
            try
            {
                dal_usuario.EliminarUsuario(id);

                bll_dvv.ActualizarDVV("Usuario");

                bll_historialCambios.RegistrarBaja("Usuario", id, Sesion.Instancia().UsuarioActual.IdUsuario);

                bll_bitacora.RegistrarEvento(Sesion.Instancia().UsuarioActual.IdUsuario, AccionBitacora.USUARIO_BAJA, "USUARIO", $"El Usuario {Sesion.Instancia().UsuarioActual.Nombre}, da de baja al Usuario con ID: {id}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        public void CalcularDVH(BE_Usuario usuario)
        {
            try
            {
                string activo = usuario.Activo ? "1" : "0";

                string DVH = $"{usuario.Nombre}|{usuario.Apellido}|{usuario.Email}|{usuario.HashPassword}|{usuario.IntentosFallidos}|{activo}";

                usuario.DVH = HashHelper.GenerarHash(DVH);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BE_RegistrosCorruptos> VerificarUsuarios()
        {
            try
            {
                List<BE_RegistrosCorruptos> ListaCorruptos = new List<BE_RegistrosCorruptos>();

                List<BE_Usuario> ListaUsuarios = dal_usuario.ListaUsuario();

                foreach (var usuario in ListaUsuarios)
                {
                    string activo = usuario.Activo ? "1" : "0";

                    string dvhRecalculado = HashHelper.GenerarHash($"{usuario.Nombre}|{usuario.Apellido}|{usuario.Email}|{usuario.HashPassword}|{usuario.IntentosFallidos}|{activo}");

                    if (usuario.DVH != dvhRecalculado)
                    {
                        ListaCorruptos.Add(new BE_RegistrosCorruptos
                        {
                            IdUsuario = usuario.IdUsuario,
                            DVH_Almacenado = usuario.DVH,
                            DVH_Recalculado = dvhRecalculado
                        });
                    }
                }

                if (ListaCorruptos.Count > 0)
                {
                    bll_bitacora.RegistrarEvento(
                        Sesion.Instancia().UsuarioActual?.IdUsuario,
                        AccionBitacora.INTEGRIDAD_ERROR,
                        "USUARIO",
                        $"Se detectaron {ListaCorruptos.Count} registro(s) de Usuario con DVH inválido (posible alteración fuera del sistema)."
                    );
                }

                return ListaCorruptos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public BE_Usuario ValidarCredencialesEmergencia(string email, string password)
        {
            try
            {
                string passwordHash = HashHelper.GenerarHash(password);

                BE_Usuario usuario = dal_usuario.ObtenerUsuarioPorEmail(email);

                if (usuario == null)
                    return null;
                if (usuario.HashPassword != passwordHash)
                    return null;

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
