using DTO;
using System.Data.Entity.Validation;
using PTWee_SinKdnaConX.Models;
using System.Net;

namespace PTWee_SinKdnaConX.Services
{
    public interface IRegister
    {
        
    
        //Get
        List<WCompanyRegister> GetRegisters();

        Register_DTO GetRegisterbyID(int id);

        //Insert
        string InsertRegister(Register_DTO registro);

        //Update
        string UpdateRegister(Register_DTO register);

        //Delete 
        string DeleteRegister(int id);

    
    }


    public class RegisterService : IRegister
    {
        //variable para el cotxto
        private readonly WeeCompanyContext _context;

        //Constructor para inicializar el contexto 
        public RegisterService(WeeCompanyContext context)
        {
            this._context = context;
        }
        //Implementacion de métodos
        public string DeleteRegister(int id)
        {
            try
            {
               
                WCompanyRegister _register= _context.WCompanyRegister.Find(id);

                if (_register != null)
                {
                    try
                    {
                       
                        _context.WCompanyRegister.Remove(_register);
                        _context.SaveChanges();
                        return $"El registro {id} ha si eliminado";
                    }
                    catch (DbEntityValidationException ex)
                    {
                        string resp = ""; 
                        foreach (var error in ex.EntityValidationErrors)
                        {
                            //Recorro los detalles de cada error
                            foreach (var validationError in error.ValidationErrors)
                            {
                                resp = "Error en la Entidad: " + error.Entry.Entity.GetType().Name;
                                resp += validationError.PropertyName;
                                resp += validationError.ErrorMessage;
                            }
                        }
                        return resp;
                    }
                }
                else
                {
                    return $"No se encontró el objeto con identificador {id}";
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Register_DTO GetRegisterbyID(int id)
        {
            Register_DTO respuesta = new Register_DTO();
            WCompanyRegister _register = _context.WCompanyRegister.Find(id);
            if (_register != null)
            {
                respuesta.ID_Register= _register.ID_Register;
                respuesta.Nom_Company= _register.Nom_Company;
                respuesta.Nom_Contact= _register.Nom_Contact;
                respuesta.Email= _register.Email;
                respuesta.Num_Tel= _register.Num_Tel;
                
            }
            return respuesta;
        }

        public List<WCompanyRegister> GetRegisters()
        {
            List<WCompanyRegister> lista_registros = _context.WCompanyRegister.ToList(); //lleno mi lista usando LinQ
            return lista_registros; //regreso mi lista
        }

        public string InsertRegister(Register_DTO register)
        {
            try
            {
                //Creo  un camion del odelo original
                WCompanyRegister _register= new WCompanyRegister();
                //asigno los valores que provienen del parámetro
                _register.ID_Register = register.ID_Register;
                _register.Nom_Company = register.Nom_Company;
                _register.Nom_Contact = register.Nom_Contact;
                _register.Email = register.Email;
                _register.Num_Tel = register.Num_Tel;


                try
                {
                    //añadimos el bjeto al contexto
                    _context.WCompanyRegister.Add(_register);
                    _context.SaveChanges();

                }
                catch (DbEntityValidationException ex)
                {

                    string resp = "";
                    //Recorro todos los posibles errores de la entidad Referencial 
                    foreach (var error in ex.EntityValidationErrors)
                    {
                        //Recorro los detalles de cada error
                        foreach (var validationError in error.ValidationErrors)
                        {
                            resp = "Error en la Entidad: " + error.Entry.Entity.GetType().Name;
                            resp += validationError.PropertyName;
                            resp += validationError.ErrorMessage;
                        }

                    }
                    return resp;
                }
                //Retorno la respues
                return "Registro agregado con éxito";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string UpdateRegister(Register_DTO register)
        {
            try
            {
                //Creo  un camion del odelo original
                WCompanyRegister _register = new WCompanyRegister();
                //asigno los valores que provienen del parámetro
                _register.ID_Register = register.ID_Register;
                _register.Nom_Company = register.Nom_Company;
                _register.Nom_Contact = register.Nom_Contact;
                _register.Email = register.Email;
                _register.Num_Tel = register.Num_Tel;

                try
                {
                    //añadimos el bjeto al contexto
                    _context.Entry(_register).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();

                }
                catch (DbEntityValidationException ex)
                {

                    string resp = "";
                    //Recorro todos los posibles errores de la entidad Referencial 
                    foreach (var error in ex.EntityValidationErrors)
                    {
                        //Recorro los detalles de cada error
                        foreach (var validationError in error.ValidationErrors)
                        {
                            resp = "Error en la Entidad: " + error.Entry.Entity.GetType().Name;
                            resp += validationError.PropertyName;
                            resp += validationError.ErrorMessage;
                        }

                    }
                    return resp;
                }
                //Retorno la respues
                return "registro actualizado con éxito";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        //Implementacion de metodos


    }
}
