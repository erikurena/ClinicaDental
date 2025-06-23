using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace clinicadental.Enums
{
    public class EnumClinicaDental
    {
        public enum Sexo
        {
            Masculino,Femenino
        }
        public enum Alergia
        {
            Si,No
        }
        public enum Embarazo
        {
            Si,No
        }
        public enum HemorragiaDen
        {
            Si,No
        }
        public enum EspecificacionHemorragia
        {
            Ninguna,Inmediata, Mediata
        }
       public enum ProtesisDental
        {
            Si,No
        }
        public enum Fuma
        {
            Si, No
        }
        public enum Bebe
        {
            Si, No
        }
        public enum HigieneBucal
        {
            Buena,Regular,Mala
        }
        public enum CepilloDental
        {
            Si,No
        }
        public enum HiloDental
        {
            Si,No
        }
        public enum EnjuagueBucal
        {
            Si,No
        }
        public enum SangradoEncias
        {
            Si, No
        }
        public enum EstadoCivil
        {             Soltero, Casado, Divorciado, Viudo, Concubino, Otro
        }
        public enum GradoInstruccion
        {
            Inicial,Primaria, Secundaria, Universidad, Tecnico, Profesional, Otro
        }
        
    }
}
