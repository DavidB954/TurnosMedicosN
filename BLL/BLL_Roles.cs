using BE;
using BE.Composite;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Roles
    {
        DAL_Roles dal_roles = new DAL_Roles();
        DAL_Usuario dal_usuario = new DAL_Usuario();
        BLL_Bitacora bll_bitacora = new BLL_Bitacora();

        private string ActorActual()
        {
            return Sesion.Instancia().UsuarioActual?.Nombre ?? "SISTEMA";
        }

        public void GuardarArbol(RolComposite raiz, string descripcionRaiz)
        {
            try
            {
                // La raíz es el nodo "Roles" (virtual) — sus hijos son los roles reales
                foreach (var hijo in raiz.Hijos())
                {
                    if (hijo is RolComposite rol)
                        GuardarRolRecursivo(rol, idPadre: null, descripcion: descripcionRaiz);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void GuardarRolRecursivo(RolComposite rol, int? idPadre, string descripcion)
        {
            try
            {
                int idRol;

                // Si ya tiene ID, existe en BD — no insertar, solo registrar jerarquía si corresponde
                if (rol.Id > 0)
                {
                    idRol = rol.Id;
                }
                else
                {
                    // Es nuevo — insertar
                    idRol = dal_roles.InsertarRol(rol.Nombre, descripcion);
                    rol.Id = idRol;

                    bll_bitacora.RegistrarEvento(
                        Sesion.Instancia().UsuarioActual?.IdUsuario,
                        AccionBitacora.ROL_ALTA,
                        "ROLES",
                        $"Usuario {ActorActual()} crea el rol {rol.Nombre}"
                    );
                }


                // 2. Si tiene padre, registrar la jerarquía
                if (idPadre.HasValue)
                    dal_roles.AsignarRolHijo(idPadre.Value, idRol);

                // 3. Recorrer hijos
                foreach (var hijo in rol.Hijos())
                {
                    if (hijo is RolComposite subRol)
                    {
                        GuardarRolRecursivo(subRol, idPadre: idRol, descripcion: null);
                    }
                    else if (hijo is Permiso permiso)
                    {
                        int idPermiso;

                        if (permiso.Id > 0)
                        {
                            // Ya existe en BD — solo asignar al rol
                            idPermiso = permiso.Id;
                        }
                        else
                        {
                            // Es nuevo — insertar
                            idPermiso = dal_roles.InsertarPermiso(permiso.Nombre);
                            permiso.Id = idPermiso;
                            permiso.IdPermiso = idPermiso;

                            bll_bitacora.RegistrarEvento(
                                Sesion.Instancia().UsuarioActual?.IdUsuario,
                                AccionBitacora.PERMISO_ALTA,
                                "ROLES",
                                $"Usuario {ActorActual()} crea el permiso {permiso.Nombre}"
                            );
                        }

                        dal_roles.AsignarPermisoARol(idRol, idPermiso);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }          
        }

        public RolComposite CargarArbol()
        {
            try
            {
                var (dtRoles, dtPermisos, dtRolRol, dtRolPermiso) = dal_roles.CargarArbol();

                var roles = new Dictionary<int, RolComposite>();
                var permisos = new Dictionary<int, Permiso>();

                // Crear todos los roles
                foreach (DataRow row in dtRoles.Rows)
                {
                    roles[Convert.ToInt32(row["IdRol"])] = new RolComposite
                    {
                        Id = Convert.ToInt32(row["IdRol"]),
                        Nombre = row["Nombre"].ToString()
                    };
                }

                // Crear todos los permisos
                foreach (DataRow row in dtPermisos.Rows)
                {
                    int id = Convert.ToInt32(row["IdPermisos"]);
                    permisos[id] = new Permiso
                    {
                        Id = id,
                        IdPermiso = id,
                        Nombre = row["Nombre"].ToString()
                    };
                }

                // Asignar permisos a sus roles
                foreach (DataRow row in dtRolPermiso.Rows)
                {
                    int idRol = Convert.ToInt32(row["IdRol"]);
                    int idPerm = Convert.ToInt32(row["IdPermisos"]);
                    if (roles.ContainsKey(idRol) && permisos.ContainsKey(idPerm))
                        roles[idRol].Agregar(permisos[idPerm]);
                }

                // Construir jerarquía rol-rol
                var rolesConPadre = new HashSet<int>();
                foreach (DataRow row in dtRolRol.Rows)
                {
                    int idPadre = Convert.ToInt32(row["IdRolPadre"]);
                    int idHijo = Convert.ToInt32(row["IdRolHijo"]);
                    if (roles.ContainsKey(idPadre) && roles.ContainsKey(idHijo))
                    {
                        //roles[idPadre].Agregar(roles[idHijo]);
                        //rolesConPadre.Add(idHijo);

                        roles[idPadre].Agregar(ClonarRol(roles[idHijo]));
                    }
                }

                // Raíz virtual: roles sin padre
                var raiz = new RolComposite { Id = 0, Nombre = "Roles" };
                foreach (var kvp in roles)
                {
                    if (!rolesConPadre.Contains(kvp.Key))
                        raiz.Agregar(kvp.Value);
                }

                return raiz;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        // Clonar dentro de la BLL
        private RolComposite ClonarRol(RolComposite original)
        {
            try
            {
                var clon = new RolComposite
                {
                    Id = original.Id,
                    Nombre = original.Nombre,
                    EsReferencia = true
                };

                foreach (var hijo in original.Hijos())
                {
                    if (hijo is RolComposite subRol)
                        clon.Agregar(ClonarRol(subRol));
                    else if (hijo is Permiso p)
                        clon.Agregar(new Permiso { Id = p.Id, IdPermiso = p.IdPermiso, Nombre = p.Nombre });
                }

                return clon;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }


        public List<RolComposite> ObtenerRoles()
        {
            try
            {
                DataTable dtRoles = dal_roles.ObtenerTodosLosRoles();

                List<RolComposite> lista = new List<RolComposite>();


                foreach (DataRow row in dtRoles.Rows)
                {
                    lista.Add(new RolComposite
                    {
                        Id = Convert.ToInt32(row["IdRol"]),
                        Nombre = row["Nombre"].ToString()
                    });
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }


        public List<Permiso> ObtenerPermisos()
        {
            try
            {
                var dtPermisos = dal_roles.ObtenerTodosLosPermisos();

                var permisos = new List<Permiso>();

                foreach (DataRow row in dtPermisos.Rows)
                {
                    int id = Convert.ToInt32(row["IdPermisos"]);
                    permisos.Add(new Permiso
                    {
                        Id = id,
                        IdPermiso = id,
                        Nombre = row["Nombre"].ToString()
                    });
                }
                return permisos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        public void EliminarComponente(RolComposite padre, Componente componente)
        {
            try
            {
                if (padre.Id == 0)
                {
                    return;
                    //La raiz no tiene padre real, nada que desasignar.
                }
                if (componente is RolComposite rolHijo)
                {
                    dal_roles.DesasignarRolHijo(padre.Id, rolHijo.Id);
                }

                if (componente is Permiso permiso)
                {
                    dal_roles.DesasignarPermisoDeRol(padre.Id, permiso.Id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public void EliminarRol(int id)
        {
            try
            {
                string nombreRol = dal_roles.ObtenerNombreRol(id) ?? $"ID {id}";

                dal_roles.EliminarRol(id);

                bll_bitacora.RegistrarEvento(
                    Sesion.Instancia().UsuarioActual?.IdUsuario,
                    AccionBitacora.ROL_BAJA,
                    "ROLES",
                    $"Usuario {ActorActual()} elimina el rol {nombreRol}"
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public void ModificarPermiso(int idPermiso, string nuevoNombre)
        {
            try
            {
                dal_roles.ModificarPermiso(idPermiso, nuevoNombre);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public void EliminarPermiso(int id)
        {
            try
            {
                string nombrePermiso = dal_roles.ObtenerNombrePermiso(id) ?? $"ID {id}";

                dal_roles.EliminarPermisos(id);

                bll_bitacora.RegistrarEvento(
                    Sesion.Instancia().UsuarioActual?.IdUsuario,
                    AccionBitacora.PERMISO_BAJA,
                    "ROLES",
                    $"Usuario {ActorActual()} elimina el permiso {nombrePermiso}"
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }





        //// USUARIOS
        ///
        public void AgregarRolUsuario(int idUsuario, int idRol)
        {
            try
            {
                dal_roles.AgregarRolUsuario(idUsuario, idRol);

                string nombreRol = dal_roles.ObtenerNombreRol(idRol) ?? $"ID {idRol}";
                BE_Usuario usuarioDestino = dal_usuario.ObtenerUsuarioPorId(idUsuario);
                string nombreDestino = usuarioDestino != null ? usuarioDestino.NombreApellido : $"ID {idUsuario}";

                bll_bitacora.RegistrarEvento(
                    Sesion.Instancia().UsuarioActual?.IdUsuario,
                    AccionBitacora.ROL_ASIGNADO_USUARIO,
                    "ROLES",
                    $"Usuario {ActorActual()} asigna el rol {nombreRol} al usuario {nombreDestino}, ID: {idUsuario}"
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<RolComposite> ObtenerRolesDeUsuario(int idUsuario)
        {
            try
            {
                // Reutilizamos el árbol completo ya construido
                RolComposite arbolCompleto = CargarArbol();

                // Obtenemos los IDs de roles asignados al usuario
                DataTable dtRoles = dal_roles.ObtenerRolesPorUsuario(idUsuario);

                var idsRolesUsuario = new HashSet<int>();

                foreach (DataRow row in dtRoles.Rows)
                    idsRolesUsuario.Add(Convert.ToInt32(row["IdRol"]));

                // Se busca en el arbol y los devuelve en una lista
                var rolesUsuario = new List<RolComposite>();

                foreach (var hijo in arbolCompleto.Hijos())
                {
                    if (hijo is RolComposite rol && idsRolesUsuario.Contains(rol.Id))
                        rolesUsuario.Add(rol);
                }

                return rolesUsuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public void QuitarRolUsuario(int idUsuario, int idRol)
        {
            try
            {
                string nombreRol = dal_roles.ObtenerNombreRol(idRol) ?? $"ID {idRol}";
                BE_Usuario usuarioDestino = dal_usuario.ObtenerUsuarioPorId(idUsuario);
                string nombreDestino = usuarioDestino != null ? usuarioDestino.NombreApellido : $"ID {idUsuario}";

                dal_roles.QuitarRolUsuario(idUsuario, idRol);

                bll_bitacora.RegistrarEvento(
                    Sesion.Instancia().UsuarioActual?.IdUsuario,
                    AccionBitacora.ROL_QUITADO_USUARIO,
                    "ROLES",
                    $"Usuario {ActorActual()} quita el rol {nombreRol} al usuario {nombreDestino}, ID: {idUsuario}"
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
