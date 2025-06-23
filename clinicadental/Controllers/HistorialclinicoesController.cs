using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clinicadental.Models;
using clinicadental.dbcontext;
using static clinicadental.Enums.EnumClinicaDental;
using clinicadental.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace clinicadental.Controllers
{
    [Authorize]
    public class HistorialclinicoesController : Controller
    {
        private readonly ClinicadentalContext _context;
        // private HistorialclinicoDto ReportePdfhistorialClinico;      

        public HistorialclinicoesController(ClinicadentalContext context)
        {
            _context = context;
        }

        // GET: Historialclinicoes
        public async Task<IActionResult> Index()
        {
            var clinicadentalContext = _context.Historialclinicos.AsNoTracking().Include(h => h.IdPacienteNavigation);
            return View(await clinicadentalContext.ToListAsync());
        }

        // GET: Historialclinicoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var historialclinicoDto = await _context.Historialclinicos.AsNoTracking().AsSplitQuery()
                                        .Where(h => h.IdHistorialClinico == id)
                                        .Select(h => new HistorialclinicoDto
                                        {
                                            Paciente = new PacienteReportDto
                                            {
                                                IdPacienteDto = h.IdPacienteNavigation.IdPaciente,
                                                SexoDto = h.IdPacienteNavigation.Sexo,
                                                IdLugarNacimientoDto = h.IdPacienteNavigation.IdLugarNacimiento,
                                                IdEstadoCivilDto = h.IdPacienteNavigation.IdEstadoCivil,
                                                IdGradoInstruccionDto = h.IdPacienteNavigation.IdGradoInstruccion,
                                                ApellidoMaternoDto = h.IdPacienteNavigation.ApellidoMaterno,
                                                ApellidoPaternoDto = h.IdPacienteNavigation.ApellidoPaterno,
                                                CelularDto = h.IdPacienteNavigation.Celular,
                                                DireccionDto = h.IdPacienteNavigation.Direccion,
                                                FechaNacimientoDto = h.IdPacienteNavigation.FechaNacimientoPaciente,
                                                NombresDto = h.IdPacienteNavigation.Nombres,
                                                OcupacionDto = h.IdPacienteNavigation.Ocupacion,
                                                CodigoPacienteDto = h.IdPacienteNavigation.CodigoPaciente
                                            },
                                            ExamenExtraOral = new ExamenextraoralDto
                                            {
                                                IdExamenExtraOralDto = h.IdExamenExtraOralNavigation.IdExamenExtraOral,
                                                AtmDto = h.IdExamenExtraOralNavigation.Atm,
                                                GanglioLinfaticoDto = h.IdExamenExtraOralNavigation.GanglioLinfatico,
                                                OtroDto = h.IdExamenExtraOralNavigation.Otro,
                                                RespiracionDto = new RespiracionDto
                                                {
                                                    IdRespiracionDto = h.IdExamenExtraOralNavigation.IdRespiracionNavigation.IdRespiracion,
                                                    TipoRespiracionDto = h.IdExamenExtraOralNavigation.IdRespiracionNavigation.TipoRespiracion
                                                }
                                            },
                                            ExamenIntraoral = new ExamenintraoralDto
                                            {
                                                IdExamenIntraoralDto = h.IdExamenIntraoralNavigation.IdExamenIntraoral,
                                                EnciaDto = h.IdExamenIntraoralNavigation.Encia,
                                                LabioDto = h.IdExamenIntraoralNavigation.Labio,
                                                LenguaDto = h.IdExamenIntraoralNavigation.Lengua,
                                                MucosaYugalDto = h.IdExamenIntraoralNavigation.MucosaYugal,
                                                PaladarDto = h.IdExamenIntraoralNavigation.Paladar,
                                                PisoDeBocaDto = h.IdExamenIntraoralNavigation.PisoDeBoca,
                                                ProtesisDentalDto = h.IdExamenIntraoralNavigation.ProtesisDental,
                                            },
                                            AntecedenteBucoDental = new AntecedentebucodentalDto
                                            {
                                                IdAntecedenteBucoDentalDto = h.IdAntecedenteBucoDentalNavigation.IdAntecedenteBucoDental,
                                                UltimaVisitaDentalDto = h.IdAntecedenteBucoDentalNavigation.UltimaVisitaDental,
                                                OtroDto = h.IdAntecedenteBucoDentalNavigation.Otro,
                                                FumaDto = h.IdAntecedenteBucoDentalNavigation.Fuma,
                                                BebeDto = h.IdAntecedenteBucoDentalNavigation.Bebe,
                                            },
                                            AntecedenteHigieneOral = new AntecedentehigieneoralDto
                                            {
                                                IdAntecedenteHigieneOralDto = h.IdAntecedenteHigieneOralNavigation.IdAntecedenteHigieneOral,
                                                IdHigieneBucalDto = h.IdAntecedenteHigieneOralNavigation.IdHigieneBucal,
                                                FrecuenciaCepilladoDentalDto = h.IdAntecedenteHigieneOralNavigation.FrecuenciaCepilladoDental,
                                                CepilloDentalDto = h.IdAntecedenteHigieneOralNavigation.CepilloDental,
                                                EnjuagueBucalDto = h.IdAntecedenteHigieneOralNavigation.EnjuagueBucal,
                                                IdHiloDentalDto = h.IdAntecedenteHigieneOralNavigation.IdHiloDental,
                                                SangradoEnciasDto = h.IdAntecedenteHigieneOralNavigation.SangradoEncias,
                                            },
                                            AntecedentePatologico = new AntecedentepatologicoDto
                                            {
                                                IdAntecedentePatologicoDto = h.IdAntecedentePatologicoNavigation.IdAntecedentePatologico,
                                                OtroDto = h.IdAntecedentePatologicoNavigation.Otro,
                                                AntecedentePatologicoFamiliarDto = h.IdAntecedentePatologicoNavigation.AntecedentePatologicoFamiliar,
                                                TratamientoMedicoDto = h.IdAntecedentePatologicoNavigation.TratamientoMedico,
                                                RecibeMedicacionDto = h.IdAntecedentePatologicoNavigation.RecibeMedicacion,
                                                AntecedenteenfermedadDto = h.IdAntecedentePatologicoNavigation.Antecedenteenfermedads.Select(ae => new AntecedenteenfermedadDto
                                                {
                                                    IdAntecedenteEnfermedadDto = ae.IdAntecedenteEnfermedad,
                                                    IdAntecedentePatologicoDto = ae.IdAntecedentePatologico,
                                                    IdEnfermerdadDto = ae.IdEnfermerdad,
                                                    EnfermerdadDto = new EnfermerdadDto
                                                    {
                                                        IdEnfermerdadDto = ae.IdEnfermerdadNavigation.IdEnfermerdad,
                                                        EnfermedadDto = ae.IdEnfermerdadNavigation.Enfermedad
                                                    }
                                                }).ToList()
                                            },
                                            AntecedenteGeneral = new AntecedentegeneralDto
                                            {
                                                IdAntecedenteGeneralDto = h.IdAntecedenteGeneralNavigation.IdAntecedenteGeneral,
                                                AlergiaDto = h.IdAntecedenteGeneralNavigation.Alergia,
                                                TipoAlergiaDto = h.IdAntecedenteGeneralNavigation.TipoAlergia,
                                                HemorragiaDentalDto = h.IdAntecedenteGeneralNavigation.HemorragiaDental,
                                                EspecificacionHemorragiaDto = h.IdAntecedenteGeneralNavigation.EspecificacionHemorragia,
                                                EmbarazoDto = h.IdAntecedenteGeneralNavigation.Embarazo,
                                                SemanaEmbarazoDto = h.IdAntecedenteGeneralNavigation.SemanaEmbarazo
                                            },
                                            Odontogramas = h.Odontogramas.Select(o => new OdontogramaReportDto
                                            {
                                                Id = o.IdOdontograma,
                                                NroPiezaDental = o.NroPiezaDental,
                                                CaraPiezaDental = o.CaraPiezaDental,
                                                IdHistorialClinico = o.IdHistorialClinico,
                                                IdAfeccion = o.IdAfeccion,
                                            }).ToList(),
                                            Tratamientos = h.Tratamientos.Select(t => new TratamientoDto
                                            {
                                                IdTratamientoDto = t.IdTratamiento,
                                                SubjetivoDto = t.Subjetivo,
                                                ObjetivoDto = t.Objetivo,
                                                AnalisisDto = t.Analisis,
                                                PlanAccionDto = t.PlanAccion,
                                                FechaTratamientoDto = t.FechaTratamiento,
                                                TratamientoConcluidoDto = t.TratamientoConcluido,
                                                IdHistorialClinicoDto = t.IdHistorialClinico,
                                                PresupuestoTotalDto = t.PresupuestoTotal,
                                                ACuentaDto = t.ACuenta,
                                                SaldoDto = t.Saldo,
                                                AvanceTratamientoDtos = t.Avancetratamientos.Select(at => new AvancetratamientoDto
                                                {
                                                    IdAvanceTratamientoDto = at.IdAvanceTratamiento,
                                                    FechaInicioDto = at.FechaInicio,
                                                    FechaConclusionDto = at.FechaConclusion,
                                                    PiezaDentalDto = at.PiezaDental,
                                                    IdTratamientoDto = at.IdTratamiento,
                                                    AvanceDto = at.Avance
                                                }).ToList(),
                                                PagosTratamientoDtos = t.PagoTratamiento.Select(pt => new PagosTratamientoDto
                                                {
                                                    IdPagoTratamientoDto = pt.IdPagoTratamiento,
                                                    MontoDto = pt.Monto,
                                                    FechaPagoDto = pt.FechaPago,
                                                    IdTratamientoDto = pt.IdTratamiento
                                                }).ToList()
                                            }).ToList()
                                        }).FirstOrDefaultAsync();

            if (historialclinicoDto == null)
                return NotFound();

            ViewData["IdHistorialClinico"] = id;
            return View(historialclinicoDto);
        }

        // GET: Historialclinicoes/Create
        public IActionResult Create(int id)
        {
            //antecedentepatologico
            ViewData["IdEnfermedad"] = new SelectList(_context.Enfermerdads, "IdEnfermerdad", "Enfermedad");
            //antecedente general
            ViewData["IdAlergia"] = new SelectList(Enum.GetValues(typeof(Alergia)));
            ViewData["IdEspecificacionHemorragia"] = new SelectList(Enum.GetValues(typeof(EspecificacionHemorragia)));
            ViewData["IdHemorragia"] = new SelectList(Enum.GetValues(typeof(HemorragiaDen)));
            ViewData["IdEmbarazo"] = new SelectList(Enum.GetValues(typeof(Embarazo)));
            //examenextraoral
            ViewData["IdRespiracion"] = new SelectList(_context.Respiracions, "IdRespiracion", "TipoRespiracion");
            //examenintraoral
            ViewData["Protesis"] = new SelectList(Enum.GetValues(typeof(ProtesisDental)));
            //antecedentebucodental
            ViewData["Fuma"] = new SelectList(Enum.GetValues(typeof(Fuma)));
            ViewData["Bebe"] = new SelectList(Enum.GetValues(typeof(Bebe)));
            //antecentehigieneoral
            ViewData["CepilloDental"] = new SelectList(Enum.GetValues(typeof(CepilloDental)));
            ViewData["HiloDental"] = new SelectList(Enum.GetValues(typeof(HiloDental)));
            ViewData["EnjuagueBucal"] = new SelectList(Enum.GetValues(typeof(EnjuagueBucal)));
            ViewData["SangradoEncias"] = new SelectList(Enum.GetValues(typeof(SangradoEncias)));
            ViewData["HigieneBucal"] = new SelectList(Enum.GetValues(typeof(HigieneBucal)));

            var model = new Historialclinico { IdPaciente = id };
            return View(model);
        }

        // POST: Historialclinicoes/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHistorialClinico,IdPaciente,IdExamenExtraOral,IdExamenIntraoral,IdAntecedenteBucoDental,IdAntecedenteHigieneOral,IdAntecedentePatologico,Observacion,IdUsuario,MotivoCita,Ci")] Historialclinico historialclinico,
                                               Antecedentepatologico antecedentepatologico,
                                               Antecedentegeneral antecedentegeneral,
                                               List<int> enfermedadSeleccionada,
                                               Antecedentebucodental antecedentebucodendal,
                                               Examenextraoral examenextraoral,
                                               Examenintraoral examenintraoral,
                                               Antecedentehigieneoral antecedentehigieneoral)
        {
            if (!ModelState.IsValid)
            {
                ViewData["IdAntecedenteBucoDental"] = new SelectList(_context.Antecedentebucodentals, "IdAntecedenteBucoDental", "IdAntecedenteBucoDental", historialclinico.IdAntecedenteBucoDental);
                ViewData["IdAntecedenteHigieneOral"] = new SelectList(_context.Antecedentehigieneorals, "IdAntecedenteHigieneOral", "IdAntecedenteHigieneOral", historialclinico.IdAntecedenteHigieneOral);
                ViewData["IdAntecedentePatologico"] = new SelectList(_context.Antecedentepatologicos, "IdAntecedentePatologico", "IdAntecedentePatologico", historialclinico.IdAntecedentePatologico);
                ViewData["IdExamenExtraOral"] = new SelectList(_context.Examenextraorals, "IdExamenExtraOral", "IdExamenExtraOral", historialclinico.IdExamenExtraOral);
                ViewData["IdExamenIntraoral"] = new SelectList(_context.Examenintraorals, "IdExamenIntraoral", "IdExamenIntraoral", historialclinico.IdExamenIntraoral);
                ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente", historialclinico.IdPaciente);
                ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", historialclinico.IdUsuario);
                return View(historialclinico);
            }

            using (var transaccion = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    historialclinico.FechaCreacionHistorial = DateTime.UtcNow;
                    historialclinico.FechaModificacionHistorial = DateTime.UtcNow;
                    historialclinico.IdUsuario = Convert.ToInt32(User.FindFirst("IdNumUsuario")?.Value);
                    historialclinico.FechaHistorial = DateOnly.FromDateTime(DateTime.Now);
                    historialclinico.CodigoHistorial = Guid.NewGuid().ToString();
                    _context.Add(antecedentegeneral);
                    _context.Add(examenextraoral);
                    _context.Add(examenintraoral);
                    _context.Add(antecedentebucodendal);
                    _context.Add(antecedentehigieneoral);
                    _context.Add(antecedentepatologico);
                    await _context.SaveChangesAsync();

                    var idAntePatologico = antecedentepatologico.IdAntecedentePatologico;
                    foreach (var item in enfermedadSeleccionada)
                    {
                        var addAntecedenteEnfermedad = new Antecedenteenfermedad
                        {
                            IdEnfermerdad = item,
                            IdAntecedentePatologico = idAntePatologico
                        };
                        _context.Add(addAntecedenteEnfermedad);
                    }

                    historialclinico.IdExamenExtraOral = examenextraoral.IdExamenExtraOral;
                    historialclinico.IdExamenIntraoral = examenintraoral.IdExamenIntraoral;
                    historialclinico.IdAntecedenteBucoDental = antecedentebucodendal.IdAntecedenteBucoDental;
                    historialclinico.IdAntecedenteHigieneOral = antecedentehigieneoral.IdAntecedenteHigieneOral;
                    historialclinico.IdAntecedentePatologico = antecedentepatologico.IdAntecedentePatologico;
                    historialclinico.IdAntecedenteGeneral = antecedentegeneral.IdAntecedenteGeneral;

                    _context.Add(historialclinico);
                    await _context.SaveChangesAsync();

                    await transaccion.CommitAsync();
                    var codigoPaciente = await _context.Pacientes.Where(p => p.IdPaciente == historialclinico.IdPaciente).Select(p => p.CodigoPaciente).FirstOrDefaultAsync();

                    return RedirectToAction("Details", "Pacientes", new { codigoPaciente });                   
                }
                catch (Exception)
                {
                    await transaccion.RollbackAsync();
                    throw;
                }
            }
        }

        // GET: Historialclinicoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var historialclinico = await _context.Historialclinicos.AsNoTracking().Where(h => h.IdHistorialClinico == id)
                                                               .Include(h => h.IdPacienteNavigation)
                                                                .Include(h => h.IdExamenExtraOralNavigation)
                                                               .ThenInclude(eeo => eeo.IdRespiracionNavigation)
                                                               .Include(h => h.IdExamenIntraoralNavigation)
                                                               .Include(h => h.IdAntecedenteBucoDentalNavigation)
                                                               .Include(h => h.IdAntecedenteHigieneOralNavigation)
                                                               .Include(h => h.IdAntecedentePatologicoNavigation)
                                                                     .ThenInclude(ap => ap.Antecedenteenfermedads)
                                                                         .ThenInclude(ae => ae.IdEnfermerdadNavigation)
                                                               .Include(h => h.IdAntecedenteGeneralNavigation)
                                                              .FirstOrDefaultAsync();

            if (historialclinico == null) return NotFound();

            var codigoPaciente = historialclinico.IdPacienteNavigation.CodigoPaciente;

            var enfermedades = await _context.Enfermerdads.AsNoTracking().ToListAsync();
            var enfermedadesPaciente = historialclinico.IdAntecedentePatologicoNavigation.Antecedenteenfermedads;

            var lista = enfermedades.Select(e => new AnteEnfermedadDto
            {
                IdEnfermerdad = e.IdEnfermerdad,
                NombreEnfermedad = e.Enfermedad,
                IdAntecedenteEnfermedad = enfermedadesPaciente.FirstOrDefault(p => p.IdEnfermerdad == e.IdEnfermerdad)?.IdAntecedenteEnfermedad,
                Seleccionada = enfermedadesPaciente.Any(p => p.IdEnfermerdad == e.IdEnfermerdad)
            }).ToList();

            ViewBag.Enfermedades = lista;

            //antecedente general
            ViewData["IdAlergia"] = new SelectList(Enum.GetValues(typeof(Alergia)), historialclinico.IdAntecedenteGeneralNavigation.Alergia);
            ViewData["IdHemorragia"] = new SelectList(Enum.GetValues(typeof(HemorragiaDen)), historialclinico.IdAntecedenteGeneralNavigation.HemorragiaDental);
            ViewData["IdEspecificacionHemorragia"] = new SelectList(Enum.GetValues(typeof(EspecificacionHemorragia)), historialclinico.IdAntecedenteGeneralNavigation.EspecificacionHemorragia);
            ViewData["IdEmbarazo"] = new SelectList(Enum.GetValues(typeof(Embarazo)), historialclinico.IdAntecedenteGeneralNavigation.Embarazo);
            //examenextraoral
            ViewData["IdRespiracion"] = new SelectList(_context.Respiracions, "IdRespiracion", "TipoRespiracion", historialclinico.IdExamenExtraOralNavigation.IdRespiracionNavigation.TipoRespiracion);
            //examenintraoral
            ViewData["Protesis"] = new SelectList(Enum.GetValues(typeof(ProtesisDental)), historialclinico.IdExamenIntraoralNavigation.ProtesisDental);
            //antecedentebucodental
            ViewData["Fuma"] = new SelectList(Enum.GetValues(typeof(Fuma)), historialclinico.IdAntecedenteBucoDentalNavigation.Fuma);
            ViewData["Bebe"] = new SelectList(Enum.GetValues(typeof(Bebe)), historialclinico.IdAntecedenteBucoDentalNavigation.Bebe);
            //antecentehigieneoral
            ViewData["CepilloDental"] = new SelectList(Enum.GetValues(typeof(CepilloDental)), historialclinico.IdAntecedenteHigieneOralNavigation.CepilloDental);
            ViewData["HiloDental"] = new SelectList(Enum.GetValues(typeof(HiloDental)), historialclinico.IdAntecedenteHigieneOralNavigation.IdHiloDental);
            ViewData["EnjuagueBucal"] = new SelectList(Enum.GetValues(typeof(EnjuagueBucal)), historialclinico.IdAntecedenteHigieneOralNavigation.EnjuagueBucal);
            ViewData["SangradoEncias"] = new SelectList(Enum.GetValues(typeof(SangradoEncias)), historialclinico.IdAntecedenteHigieneOralNavigation.SangradoEncias);
            ViewData["HigieneBucal"] = new SelectList(Enum.GetValues(typeof(HigieneBucal)), historialclinico.IdAntecedenteHigieneOralNavigation.IdHigieneBucal);
            ViewData["CodigoPaciente"] = codigoPaciente;
            return View(historialclinico);
        }

        // POST: Historialclinicoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Historialclinico historialclinico,
                                               Antecedentepatologico antecedentepatologico,
                                               Antecedentegeneral antecedentegeneral,
                                               List<int> enfermedadSeleccionada,
                                               Antecedentebucodental antecedentebucodendal,
                                               Examenextraoral examenextraoral,
                                               Examenintraoral examenintraoral,
                                               Antecedentehigieneoral antecedentehigieneoral)
        {
            if (id != historialclinico.IdHistorialClinico) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["IdAntecedenteBucoDental"] = new SelectList(_context.Antecedentebucodentals, "IdAntecedenteBucoDental", "IdAntecedenteBucoDental", historialclinico.IdAntecedenteBucoDental);
                ViewData["IdAntecedenteHigieneOral"] = new SelectList(_context.Antecedentehigieneorals, "IdAntecedenteHigieneOral", "IdAntecedenteHigieneOral", historialclinico.IdAntecedenteHigieneOral);
                ViewData["IdAntecedentePatologico"] = new SelectList(_context.Antecedentepatologicos, "IdAntecedentePatologico", "IdAntecedentePatologico", historialclinico.IdAntecedentePatologico);
                ViewData["IdExamenExtraOral"] = new SelectList(_context.Examenextraorals, "IdExamenExtraOral", "IdExamenExtraOral", historialclinico.IdExamenExtraOral);
                ViewData["IdExamenIntraoral"] = new SelectList(_context.Examenintraorals, "IdExamenIntraoral", "IdExamenIntraoral", historialclinico.IdExamenIntraoral);
                ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente", historialclinico.IdPaciente);
                ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", historialclinico.IdUsuario);
                return View(historialclinico);
            }

            using (var transaccion = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    historialclinico.FechaModificacionHistorial = DateTime.UtcNow;
                    historialclinico.FechaHistorial = DateOnly.FromDateTime(DateTime.Now);
                    _context.Update(antecedentepatologico);
                    _context.Update(antecedentegeneral);
                    _context.Update(antecedentebucodendal);
                    _context.Update(examenextraoral);
                    _context.Update(examenintraoral);
                    _context.Update(antecedentehigieneoral);

                    _context.Entry(historialclinico).Property(h => h.Observacion).IsModified = true;
                    _context.Entry(historialclinico).Property(h => h.FechaHistorial).IsModified = true;
                    _context.Entry(historialclinico).Property(h => h.Ci).IsModified = true;
                    _context.Entry(historialclinico).Property(h => h.MotivoCita).IsModified = true;
                    _context.Entry(historialclinico).Property(h => h.FechaModificacionHistorial).IsModified = true;
                    await _context.SaveChangesAsync();

                    var idAntecedentePatologico = antecedentepatologico.IdAntecedentePatologico;
                    var enfermedadesRegistradas = await _context.Antecedenteenfermedads
                        .Where(a => a.IdAntecedentePatologico == idAntecedentePatologico)
                        .ToListAsync();

                    var aEliminar = enfermedadesRegistradas
                        .Where(e => !enfermedadSeleccionada.Contains(e.IdEnfermerdad))
                        .ToList();
                    _context.Antecedenteenfermedads.RemoveRange(aEliminar);

                    var idsExistentes = enfermedadesRegistradas.Select(e => e.IdEnfermerdad).ToList();
                    var aAgregar = enfermedadSeleccionada
                        .Where(id => !idsExistentes.Contains(id))
                        .Select(id => new Antecedenteenfermedad
                        {
                            IdAntecedentePatologico = idAntecedentePatologico,
                            IdEnfermerdad = id
                        })
                        .ToList();

                    _context.Antecedenteenfermedads.AddRange(aAgregar);

                    await _context.SaveChangesAsync();
                    await transaccion.CommitAsync();

                    var codigoPaciente = await _context.Pacientes.Where(p => p.IdPaciente == historialclinico.IdPaciente).Select(p => p.CodigoPaciente).FirstOrDefaultAsync();

                    return RedirectToAction("Details", "Pacientes", new { codigoPaciente });
                }
                catch (Exception)
                {
                    await transaccion.RollbackAsync();
                    return NotFound();
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> CopiarHistorial(int? id)
        {
            if(id == null) return NotFound();

            var historialClinico = await _context.Historialclinicos.Where(x => x.IdHistorialClinico == id)
                                                                                                           .Include(h => h.IdPacienteNavigation)
                                                                                                            .Include(h => h.IdExamenExtraOralNavigation)
                                                                                                           .ThenInclude(eeo => eeo.IdRespiracionNavigation)
                                                                                                           .Include(h => h.IdExamenIntraoralNavigation)
                                                                                                           .Include(h => h.IdAntecedenteBucoDentalNavigation)
                                                                                                           .Include(h => h.IdAntecedenteHigieneOralNavigation)
                                                                                                           .Include(h => h.IdAntecedentePatologicoNavigation)
                                                                                                                 .ThenInclude(ap => ap.Antecedenteenfermedads)
                                                                                                                     .ThenInclude(ae => ae.IdEnfermerdadNavigation)
                                                                                                           .Include(h => h.IdAntecedenteGeneralNavigation)
                                                                                                           .AsNoTracking()
                                                                                                           .FirstOrDefaultAsync();

            if (historialClinico == null) return NotFound();

            using var transaccion = await _context.Database.BeginTransactionAsync();
            try
            {
                var nuevoHistorialClinico = new Historialclinico
                {
                    IdPaciente = historialClinico.IdPaciente,
                    CodigoHistorial = Guid.NewGuid().ToString(),
                    FechaCreacionHistorial = DateTime.UtcNow,
                    FechaModificacionHistorial = DateTime.UtcNow,
                    IdExamenExtraOralNavigation = new Examenextraoral
                    {
                        Atm = historialClinico.IdExamenExtraOralNavigation.Atm,
                        GanglioLinfatico = historialClinico.IdExamenExtraOralNavigation.GanglioLinfatico,
                        Otro = historialClinico.IdExamenExtraOralNavigation.Otro,
                        IdRespiracion = historialClinico.IdExamenExtraOralNavigation.IdRespiracion
                    },
                    IdExamenIntraoralNavigation = new Examenintraoral
                    {
                        Encia = historialClinico.IdExamenIntraoralNavigation.Encia,
                        Labio = historialClinico.IdExamenIntraoralNavigation.Labio,
                        Lengua = historialClinico.IdExamenIntraoralNavigation.Lengua,
                        MucosaYugal = historialClinico.IdExamenIntraoralNavigation.MucosaYugal,
                        Paladar = historialClinico.IdExamenIntraoralNavigation.Paladar,
                        PisoDeBoca = historialClinico.IdExamenIntraoralNavigation.PisoDeBoca,
                        ProtesisDental = historialClinico.IdExamenIntraoralNavigation.ProtesisDental
                    },
                    IdAntecedenteBucoDentalNavigation = new Antecedentebucodental
                    {
                        UltimaVisitaDental = historialClinico.IdAntecedenteBucoDentalNavigation.UltimaVisitaDental,
                        Otro = historialClinico.IdAntecedenteBucoDentalNavigation.Otro,
                        Fuma = historialClinico.IdAntecedenteBucoDentalNavigation.Fuma,
                        Bebe = historialClinico.IdAntecedenteBucoDentalNavigation.Bebe
                    },
                    IdAntecedenteHigieneOralNavigation = new Antecedentehigieneoral
                    {
                        IdHigieneBucal = historialClinico.IdAntecedenteHigieneOralNavigation.IdHigieneBucal,
                        FrecuenciaCepilladoDental = historialClinico.IdAntecedenteHigieneOralNavigation.FrecuenciaCepilladoDental,
                        CepilloDental = historialClinico.IdAntecedenteHigieneOralNavigation.CepilloDental,
                        EnjuagueBucal = historialClinico.IdAntecedenteHigieneOralNavigation.EnjuagueBucal,
                        IdHiloDental = historialClinico.IdAntecedenteHigieneOralNavigation.IdHiloDental,
                        SangradoEncias = historialClinico.IdAntecedenteHigieneOralNavigation.SangradoEncias
                    },
                    IdAntecedentePatologicoNavigation = new Antecedentepatologico
                    {
                        Otro = historialClinico.IdAntecedentePatologicoNavigation.Otro,
                        AntecedentePatologicoFamiliar = historialClinico.IdAntecedentePatologicoNavigation.AntecedentePatologicoFamiliar,
                        TratamientoMedico = historialClinico.IdAntecedentePatologicoNavigation.TratamientoMedico,
                        RecibeMedicacion = historialClinico.IdAntecedentePatologicoNavigation.RecibeMedicacion
                    },
                    IdAntecedenteGeneralNavigation = new Antecedentegeneral
                    {
                        Alergia = historialClinico.IdAntecedenteGeneralNavigation.Alergia,
                        TipoAlergia = historialClinico.IdAntecedenteGeneralNavigation.TipoAlergia,
                        HemorragiaDental = historialClinico.IdAntecedenteGeneralNavigation.HemorragiaDental,
                        EspecificacionHemorragia = historialClinico.IdAntecedenteGeneralNavigation.EspecificacionHemorragia,
                        Embarazo = historialClinico.IdAntecedenteGeneralNavigation.Embarazo,
                        SemanaEmbarazo = historialClinico.IdAntecedenteGeneralNavigation.SemanaEmbarazo
                    },
                    Observacion = historialClinico.Observacion,
                    MotivoCita = historialClinico.MotivoCita,
                    Ci = historialClinico.Ci,
                    IdUsuario = Convert.ToInt32(User.FindFirst("IdNumUsuario")?.Value),
                    FechaHistorial = DateOnly.FromDateTime(DateTime.Now)
                };
                _context.Add(nuevoHistorialClinico);
                await _context.SaveChangesAsync();

                // Copiar las enfermedades asociadas
                if (historialClinico.IdAntecedentePatologicoNavigation?.Antecedenteenfermedads != null)
                {
                    var nuevasEnfermedades = historialClinico.IdAntecedentePatologicoNavigation.Antecedenteenfermedads
                        .Select(enfermedad => new Antecedenteenfermedad
                        {
                            IdAntecedentePatologico = nuevoHistorialClinico.IdAntecedentePatologico,
                            IdEnfermerdad = enfermedad.IdEnfermerdad
                        })
                        .ToList();

                    await _context.AddRangeAsync(nuevasEnfermedades);
                    await _context.SaveChangesAsync();
                }

                // Confirmar la transacción
                await transaccion.CommitAsync();

                return Json(new
                {
                    success = true,
                    id = nuevoHistorialClinico.IdHistorialClinico,
                    fecha = nuevoHistorialClinico.FechaHistorial,
                    motivo = nuevoHistorialClinico.MotivoCita
                });
            }
            catch (Exception)
            {
                await transaccion.RollbackAsync();              
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }

        // POST: Historialclinicoes/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var historialclinico = await _context.Historialclinicos.Where(h => h.IdHistorialClinico == id)
                                                                                                         .Include(h => h.IdExamenExtraOralNavigation)
                                                                                                        .Include(h => h.IdExamenIntraoralNavigation)
                                                                                                        .Include(h => h.IdAntecedenteBucoDentalNavigation)
                                                                                                        .Include(h => h.IdAntecedenteHigieneOralNavigation)
                                                                                                        .Include(h => h.IdAntecedentePatologicoNavigation)
                                                                                                              .ThenInclude(ap => ap.Antecedenteenfermedads)
                                                                                                        .Include(h => h.IdAntecedenteGeneralNavigation)
                                                                                                       .FirstOrDefaultAsync();

            if (historialclinico == null) return NotFound();

            using var transaccion = await _context.Database.BeginTransactionAsync();
            try
            {
                var examenExtraOral = historialclinico.IdExamenExtraOralNavigation;
                _context.Examenextraorals.Remove(examenExtraOral);

                var examenIntraoral = historialclinico.IdExamenIntraoralNavigation;
                _context.Examenintraorals.Remove(examenIntraoral);

                var antecedenteBucoDental = historialclinico.IdAntecedenteBucoDentalNavigation;
                _context.Antecedentebucodentals.Remove(antecedenteBucoDental);

                var antecedenteHigieneOral = historialclinico.IdAntecedenteHigieneOralNavigation;
                _context.Antecedentehigieneorals.Remove(antecedenteHigieneOral);

                var antecedenteEnfermedad = historialclinico.IdAntecedentePatologicoNavigation.Antecedenteenfermedads;
                _context.Antecedenteenfermedads.RemoveRange(antecedenteEnfermedad);

                var antecedentePatologico = historialclinico.IdAntecedentePatologicoNavigation;
                _context.Antecedentepatologicos.Remove(antecedentePatologico);

                var antecedenteGeneral = historialclinico.IdAntecedenteGeneralNavigation;
                _context.Antecedentegenerals.Remove(antecedenteGeneral);

                _context.Historialclinicos.Remove(historialclinico);

                await _context.SaveChangesAsync();
                await transaccion.CommitAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                await transaccion.RollbackAsync();
                ModelState.AddModelError("", "Ocurrió un error inesperado. Inténtelo de nuevo.");
                return StatusCode(500, $"Error al eliminar: {ex.Message}");
            }
        }

        private bool HistorialclinicoExists(int id)
        {
            return _context.Historialclinicos.Any(e => e.IdHistorialClinico == id);
        }
    }
}
