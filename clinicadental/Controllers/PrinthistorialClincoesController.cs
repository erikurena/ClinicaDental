using Microsoft.AspNetCore.Mvc;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Table = iText.Layout.Element.Table;
using iText.IO.Image;
using iText.Layout.Borders;
using Microsoft.AspNetCore.Hosting;
using iText.Kernel.Colors;
using clinicadental.dbcontext;
using Microsoft.EntityFrameworkCore;
using clinicadental.Dtos;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.IO.Font;


namespace clinicadental.Controllers
{
    public class PrinthistorialClincoesController : Controller
    {
        public readonly ClinicadentalContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PrinthistorialClincoesController(ClinicadentalContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> GenerateClinicalHistoryPdf([FromBody] PdfRequestData request)
        {
            var ReportePdfhistorialClinico = await _context.Historialclinicos.AsNoTracking().AsSplitQuery()
                .Where(h => h.IdHistorialClinico == request.Id)
                .Select(h => new HistorialclinicoDto
                {
                   CodigoHistorialClinicoDto = h.CodigoHistorial,
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
                        }).ToList(),
                    }).ToList(),
                    Usuario = new UsuarioDto
                    {
                        IdUsuario = h.IdUsuarioNavigation.IdUsuario,
                        PrimerNombre = h.IdUsuarioNavigation.PrimerNombre,
                        SegundoNombre = h.IdUsuarioNavigation.SegundoNombre,
                        ApellidoPaterno = h.IdUsuarioNavigation.ApellidoPaterno,
                        ApellidoMaterno = h.IdUsuarioNavigation.ApellidoMaterno,
                        Clinicas = h.IdUsuarioNavigation.Clinicas.Select(c => new ClinicaDto
                        {
                            IdClinica = c.IdClinica,
                            Nombre = c.Nombre,
                            Direccion = c.Direccion,
                            Celular = c.Celular,
                            Ujsedes = c.Ujsedes,
                            Nit = c.Nit,
                            FotoClinica = c.FotoClinica,
                            Ciudad = c.Ciudad,
                            Pais = c.Pais
                        }).ToList(),
                    }
                }).FirstOrDefaultAsync();

            if (ReportePdfhistorialClinico == null)
                return NotFound();

            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(ms);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);
                PdfFont fontCourier = PdfFontFactory.CreateFont(StandardFonts.COURIER_BOLD);

                // Cargar fuentes personalizadas
                string poppinsBoldPath = Path.Combine(_webHostEnvironment.WebRootPath, "fonts", "Poppins-ExtraBold.ttf");
                string TimesNewRomanPath = Path.Combine(_webHostEnvironment.WebRootPath, "fonts", "times.ttf");
                PdfFont fontSubtitulos = PdfFontFactory.CreateFont(TimesNewRomanPath, PdfEncodings.IDENTITY_H);
                PdfFont fontTimesRoman = PdfFontFactory.CreateFont(TimesNewRomanPath, PdfEncodings.IDENTITY_H);
                PdfFont fontPoppins = PdfFontFactory.CreateFont(poppinsBoldPath, PdfEncodings.IDENTITY_H);


                // Crear la tabla principal con 3 columnas
                Table headerTable1 = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2, 1 }))
                .UseAllAvailableWidth()
                .SetBorderCollapse(BorderCollapsePropertyValue.COLLAPSE)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetMarginBottom(3);

                // --------- COLUMNA 1:   IMAGEN ---------  
                var nombreimagen1 = ReportePdfhistorialClinico.Usuario.Clinicas.Select(x => x.FotoClinica).FirstOrDefault();

                if (string.IsNullOrEmpty(nombreimagen1))
                    nombreimagen1 = "dientedefault.png";

                string rutaFoto1 = Path.Combine(_webHostEnvironment.WebRootPath, "photos", "FotoClinicaPortada", nombreimagen1);

                if (string.IsNullOrEmpty(rutaFoto1) || !System.IO.File.Exists(rutaFoto1))
                    rutaFoto1 = Path.Combine(_webHostEnvironment.WebRootPath, "photos", "dientedefault.png"); // Ruta de imagen por defecto

                Image img1 = new Image(ImageDataFactory.Create(rutaFoto1)).SetWidth(50).SetHeight(50).SetMargins(0, 0, 0, 0).SetPadding(0);

                Cell imageCell1 = new Cell().Add(img1).SetBorder(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetPadding(0).SetMargin(0).SetWidth(50);
                headerTable1.AddCell(imageCell1);

                string nombreClinica = ReportePdfhistorialClinico.Usuario.Clinicas.Select(c => c.Nombre).FirstOrDefault() ?? "Nombre no disponible";
                string direccionClinica = ReportePdfhistorialClinico.Usuario.Clinicas.Select(c => c.Direccion).FirstOrDefault() ?? "Dirección no disponible";
                string celularClinica = ReportePdfhistorialClinico.Usuario.Clinicas.Select(c => c.Celular).FirstOrDefault() ?? "Celular no disponible";
                string pais = ReportePdfhistorialClinico.Usuario.Clinicas.Select(c => c.Pais).FirstOrDefault() ?? "País no disponible";
                string ciudad = ReportePdfhistorialClinico.Usuario.Clinicas.Select(c => c.Ciudad).FirstOrDefault() ?? "Ciudad no disponible";

                // --------- COLUMNA 2: LISTA DE 5 FILAS ---------  
                Cell listaCell1 = new Cell().SetBorder(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetPadding(0).SetMargin(0).SetFont(fontCourier);
                listaCell1.Add(new Paragraph(nombreClinica.ToUpper()).SetFont(fontTimesRoman).SetFontSize(18));
                listaCell1.Add(new Paragraph(direccionClinica).SetFontSize(8));
                listaCell1.Add(new Paragraph($"Celular: {celularClinica}").SetFontSize(8));
                listaCell1.Add(new Paragraph($"{ciudad} - {pais}").SetFontSize(8));
                headerTable1.AddCell(listaCell1);

                // --------- COLUMNA 3:  FILAS ---------
                Cell infoExtraCell1 = new Cell().SetBorder(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetPadding(0).SetMargin(0).SetWidth(50).SetHeight(50); // <- IGUAL que imagen
                headerTable1.AddCell(infoExtraCell1);

                // Agrega esta tabla al documento
                document.Add(headerTable1);



                // Crear la tabla principal con 3 columnas
                Table headerTable = new Table(new float[] { 1, 1 }).UseAllAvailableWidth();
                headerTable.SetBorderCollapse(BorderCollapsePropertyValue.COLLAPSE);
                headerTable.SetTextAlignment(TextAlignment.LEFT);
                headerTable.SetBorderTop(new SolidBorder(ColorConstants.BLACK, 1f)); // Borde superior de la tabla
                headerTable.SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1f)); // Borde inferior de la tabla


                // --------- COLUMNA 1: LISTA DE 5 FILAS ---------
                 
                Table tablaInterna = new Table(2).UseAllAvailableWidth();
                tablaInterna.SetBorder(Border.NO_BORDER);

                tablaInterna.AddCell(new Cell().Add(new Paragraph("COD. PACIENTE").SetMultipliedLeading(0.8f).SetFont(fontPoppins)).SetBorder(Border.NO_BORDER).SetPadding(0).SetFontSize(8));
                tablaInterna.AddCell(new Cell().Add(new Paragraph($": {ReportePdfhistorialClinico.Paciente.CodigoPacienteDto}").SetFont(fontCourier).SetFontSize(8)).SetBorder(Border.NO_BORDER).SetPadding(0));

                tablaInterna.AddCell(new Cell().Add(new Paragraph("PACIENTE").SetMultipliedLeading(0.8f).SetFont(fontPoppins)).SetBorder(Border.NO_BORDER).SetPadding(0).SetFontSize(8));
                tablaInterna.AddCell(new Cell().Add(new Paragraph($": {ReportePdfhistorialClinico.Paciente.NombresDto} {ReportePdfhistorialClinico.Paciente.ApellidoPaternoDto} {ReportePdfhistorialClinico.Paciente.ApellidoMaternoDto}").SetFont(fontCourier).SetFontSize(8)).SetBorder(Border.NO_BORDER).SetPadding(0));
               
                // --------- PÁRRAFO EDAD ---------
                DateOnly hoy = DateOnly.FromDateTime(DateTime.Today);
                DateOnly? fechaNacimiento = ReportePdfhistorialClinico.Paciente.FechaNacimientoDto;
                int edad = 0;
                if (fechaNacimiento.HasValue)
                {   edad = hoy.Year - fechaNacimiento.Value.Year;
                    if (hoy < fechaNacimiento.Value.AddYears(edad)) edad--;
                }
                tablaInterna.AddCell(new Cell().Add(new Paragraph("EDAD").SetMultipliedLeading(0.8f).SetFont(fontPoppins)).SetBorder(Border.NO_BORDER).SetPadding(0).SetFontSize(8));
                tablaInterna.AddCell(new Cell().Add(new Paragraph($": {edad} años").SetFont(fontCourier).SetFontSize(8)).SetBorder(Border.NO_BORDER).SetPadding(0));

                tablaInterna.AddCell(new Cell().Add(new Paragraph("CELULAR").SetMultipliedLeading(0.8f).SetFont(fontPoppins)).SetBorder(Border.NO_BORDER).SetPadding(0).SetFontSize(8));
                tablaInterna.AddCell(new Cell().Add(new Paragraph($": {ReportePdfhistorialClinico.Paciente.CelularDto}").SetFont(fontCourier).SetFontSize(8)).SetBorder(Border.NO_BORDER).SetPadding(0));

                // --------- CELDA ---------
                Cell listaCellCol1 = new Cell().SetBorder(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetPaddingLeft(5f).SetMargin(0);

                listaCellCol1.Add(tablaInterna);
                headerTable.AddCell(listaCellCol1);


                // --------- COLUMNA 2: LISTA DE 5 FILAS ---------  

                Table tablaInterna2= new Table(2).UseAllAvailableWidth();
                tablaInterna2.SetBorder(Border.NO_BORDER);

                //añadir codigo historial
                tablaInterna2.AddCell(new Cell().Add(new Paragraph("COD. HISTORIAL").SetMultipliedLeading(0.8f).SetFont(fontPoppins)).SetBorder(Border.NO_BORDER).SetPadding(0).SetFontSize(8));
                tablaInterna2.AddCell(new Cell().Add(new Paragraph($": {ReportePdfhistorialClinico.CodigoHistorialClinicoDto}").SetFont(fontCourier).SetFontSize(8)).SetBorder(Border.NO_BORDER).SetPadding(0));

                //añadir sexo
                tablaInterna2.AddCell(new Cell().Add(new Paragraph("SEXO").SetMultipliedLeading(0.8f).SetFont(fontPoppins)).SetBorder(Border.NO_BORDER).SetPadding(0).SetFontSize(8));
                tablaInterna2.AddCell(new Cell().Add(new Paragraph($": {ReportePdfhistorialClinico.Paciente.SexoDto}").SetFont(fontCourier).SetFontSize(8)).SetBorder(Border.NO_BORDER).SetPadding(0));

                //añadir fecha
                tablaInterna2.AddCell(new Cell().Add(new Paragraph("FECHA").SetMultipliedLeading(0.8f).SetFont(fontPoppins)).SetBorder(Border.NO_BORDER).SetPadding(0).SetFontSize(8));
                tablaInterna2.AddCell(new Cell().Add(new Paragraph($": {DateTime.Now:dd/MM/yyyy}").SetFont(fontCourier).SetFontSize(8)).SetBorder(Border.NO_BORDER).SetPadding(0));

                // --------- CELDA ---------
                Cell listaCellCol2 = new Cell().SetBorder(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetPaddingLeft(5f).SetMargin(0);
                                
                listaCellCol2.Add(tablaInterna2);
                headerTable.AddCell(listaCellCol2);

                // Agrega esta tabla al documento
                document.Add(headerTable);


                // TITULO DE HISTORIAL CLINICO
                document.Add(new Paragraph("HISTORIAL CLINICO DENTAL").SetFontSize(18).SetTextAlignment(TextAlignment.CENTER).SetFont(fontSubtitulos).SetUnderline());


                // Antecedentes Patológicos
                document.Add(new Paragraph("ANTECEDENTE PATOLOGICO").SetFontSize(12).SetMarginTop(5).SetFixedLeading(12f).SetFont(fontSubtitulos).SetUnderline());

                Paragraph tratamientoParagraph = new Paragraph().SetFixedLeading(10f).SetFontSize(10);
                tratamientoParagraph.Add(new Text("Recibe Tratamiento Médico: ").SetFont(fontSubtitulos));
                tratamientoParagraph.Add(new Text($"{ReportePdfhistorialClinico.AntecedentePatologico.TratamientoMedicoDto}"));
                tratamientoParagraph.Add(new Text("\tRecibe Medicación: ").SetFont(fontSubtitulos));
                tratamientoParagraph.Add(new Text($"{ReportePdfhistorialClinico.AntecedentePatologico.RecibeMedicacionDto}"));
                document.Add(tratamientoParagraph);

                // Segundo Paragraph: Antecedentes Familiares
                Paragraph antecedentesParagraph2 = new Paragraph().SetFixedLeading(10f).SetFontSize(10);
                antecedentesParagraph2.Add(new Text("Antecedentes Familiares: ").SetFont(fontSubtitulos));
                antecedentesParagraph2.Add(new Text($"{ReportePdfhistorialClinico.AntecedentePatologico.AntecedentePatologicoFamiliarDto}"));
                document.Add(antecedentesParagraph2);


                if (ReportePdfhistorialClinico.AntecedentePatologico.AntecedenteenfermedadDto.Any())
                {
                    document.Add(new Paragraph("ENFERMEDADES:").SetFontSize(12).SetMarginTop(5).SetFixedLeading(12f).SetFont(fontSubtitulos).SetUnderline());
                    foreach (var enfermedad in ReportePdfhistorialClinico.AntecedentePatologico.AntecedenteenfermedadDto)
                    {
                        document.Add(new Paragraph($"\t- {enfermedad.EnfermerdadDto.EnfermedadDto}").SetFixedLeading(8f).SetFontSize(10));
                    }
                }

                // Antecedentes Generales
                Paragraph antecedentesGeneralParagraph = new Paragraph().SetFixedLeading(10f).SetFontSize(10);
                antecedentesGeneralParagraph.Add(new Text("Alergias: ").SetFont(fontSubtitulos));
                antecedentesGeneralParagraph.Add(new Text($"{ReportePdfhistorialClinico.AntecedenteGeneral.AlergiaDto}"));
                antecedentesGeneralParagraph.Add(new Text("\tTipo de Alergia: ").SetFont(fontSubtitulos));
                antecedentesGeneralParagraph.Add(new Text($"{ReportePdfhistorialClinico.AntecedenteGeneral.TipoAlergiaDto}"));
                antecedentesGeneralParagraph.Add(new Text("\tHemorragia Dental: ").SetFont(fontSubtitulos));
                antecedentesGeneralParagraph.Add(new Text($"{ReportePdfhistorialClinico.AntecedenteGeneral.HemorragiaDentalDto}"));
                antecedentesGeneralParagraph.Add(new Text("\tEmbarazo: ").SetFont(fontSubtitulos));
                antecedentesGeneralParagraph.Add(new Text($"{ReportePdfhistorialClinico.AntecedenteGeneral.EmbarazoDto}"));

                if (ReportePdfhistorialClinico.AntecedenteGeneral.SemanaEmbarazoDto != null)
                {
                    antecedentesGeneralParagraph.Add(new Text($"\tSemanas de Embarazo: ").SetFont(fontSubtitulos));
                    antecedentesGeneralParagraph.Add(new Text(ReportePdfhistorialClinico.AntecedenteGeneral.SemanaEmbarazoDto));
                }

                document.Add(antecedentesGeneralParagraph);

                // Examen Extraoral
                document.Add(new Paragraph("EXAMEN EXTRA ORAL").SetFontSize(12).SetMarginTop(5).SetFixedLeading(12f).SetFont(fontSubtitulos).SetUnderline());

                Paragraph examenExtraOralParagraph = new Paragraph().SetFixedLeading(10f).SetFontSize(10);
                examenExtraOralParagraph.Add(new Text("ATM: ").SetFont(fontSubtitulos));
                examenExtraOralParagraph.Add(new Text($"{ReportePdfhistorialClinico.ExamenExtraOral.AtmDto}"));
                examenExtraOralParagraph.Add(new Text("\tGanglios Linfáticos: ").SetFont(fontSubtitulos));
                examenExtraOralParagraph.Add(new Text($"{ReportePdfhistorialClinico.ExamenExtraOral.GanglioLinfaticoDto}"));
                examenExtraOralParagraph.Add(new Text("\tRespiración: ").SetFont(fontSubtitulos));
                examenExtraOralParagraph.Add(new Text($"{ReportePdfhistorialClinico.ExamenExtraOral.RespiracionDto.TipoRespiracionDto}"));
                examenExtraOralParagraph.Add(new Text("\tOtros: ").SetFont(fontSubtitulos));
                examenExtraOralParagraph.Add(new Text($"{ReportePdfhistorialClinico.ExamenExtraOral.OtroDto}"));
                document.Add(examenExtraOralParagraph);

                // Examen Intraoral
                document.Add(new Paragraph("EXAMEN INTRA ORAL").SetFontSize(12).SetMarginTop(5).SetFixedLeading(12f).SetFont(fontSubtitulos).SetUnderline());

                Paragraph paragraph1 = new Paragraph().SetFixedLeading(10f).SetFontSize(10);
                paragraph1.Add(new Text("Encia: ").SetFont(fontSubtitulos));
                paragraph1.Add(new Text($"{ReportePdfhistorialClinico.ExamenIntraoral.EnciaDto}"));
                paragraph1.Add(new Text("\tLabio: ").SetFont(fontSubtitulos));
                paragraph1.Add(new Text($"{ReportePdfhistorialClinico.ExamenIntraoral.LabioDto}"));
                paragraph1.Add(new Text("\tLengua: ").SetFont(fontSubtitulos));
                paragraph1.Add(new Text($"{ReportePdfhistorialClinico.ExamenIntraoral.LenguaDto}"));
                paragraph1.Add(new Text("\tMucosa Yugal: ").SetFont(fontSubtitulos));
                paragraph1.Add(new Text($"{ReportePdfhistorialClinico.ExamenIntraoral.MucosaYugalDto}"));
                document.Add(paragraph1);

                Paragraph paragraph2 = new Paragraph().SetFixedLeading(10f).SetFontSize(10);
                paragraph2.Add(new Text("Paladar: ").SetFont(fontSubtitulos));
                paragraph2.Add(new Text($"{ReportePdfhistorialClinico.ExamenIntraoral.PaladarDto}"));
                paragraph2.Add(new Text("\tPiso de Boca: ").SetFont(fontSubtitulos));
                paragraph2.Add(new Text($"{ReportePdfhistorialClinico.ExamenIntraoral.PisoDeBocaDto}"));
                paragraph2.Add(new Text("\tPrótesis Dental: ").SetFont(fontSubtitulos));
                paragraph2.Add(new Text($"{ReportePdfhistorialClinico.ExamenIntraoral.ProtesisDentalDto}"));
                document.Add(paragraph2);

                // Antecedentes Bucodentales
                document.Add(new Paragraph("ANTECEDENTE BUCODENTAL").SetFontSize(12).SetMarginTop(5).SetFixedLeading(12f).SetFont(fontSubtitulos).SetUnderline());

                Paragraph antecedentesParagraph = new Paragraph().SetFixedLeading(10f).SetFontSize(10);
                antecedentesParagraph.Add(new Text("Última Visita Dental: ").SetFont(fontSubtitulos));
                antecedentesParagraph.Add(new Text($"{ReportePdfhistorialClinico.AntecedenteBucoDental.UltimaVisitaDentalDto:dd/MM/yyyy}"));
                antecedentesParagraph.Add(new Text("\tFuma: ").SetFont(fontSubtitulos));
                antecedentesParagraph.Add(new Text($"{ReportePdfhistorialClinico.AntecedenteBucoDental.FumaDto}"));
                antecedentesParagraph.Add(new Text("\tBebe: ").SetFont(fontSubtitulos));
                antecedentesParagraph.Add(new Text($"{ReportePdfhistorialClinico.AntecedenteBucoDental.BebeDto}"));
                antecedentesParagraph.Add(new Text("\tOtros: ").SetFont(fontSubtitulos));
                antecedentesParagraph.Add(new Text($"{ReportePdfhistorialClinico.AntecedenteBucoDental.OtroDto}"));
                document.Add(antecedentesParagraph);

                // Antecedentes de Higiene Oral
                document.Add(new Paragraph("ANTECEDENTE DE HIGIENE ORAL").SetFontSize(12).SetMarginTop(5).SetFixedLeading(12f).SetFont(fontSubtitulos).SetUnderline());

                Paragraph higieneOralParagraph = new Paragraph().SetFixedLeading(10f).SetFontSize(10);
                higieneOralParagraph.Add(new Text("Frecuencia de Cepillado: ").SetFont(fontSubtitulos));
                higieneOralParagraph.Add(new Text($"{ReportePdfhistorialClinico.AntecedenteHigieneOral.FrecuenciaCepilladoDentalDto}"));
                higieneOralParagraph.Add(new Text("\tUsa Cepillo Dental: ").SetFont(fontSubtitulos));
                higieneOralParagraph.Add(new Text($"{ReportePdfhistorialClinico.AntecedenteHigieneOral.CepilloDentalDto}"));
                higieneOralParagraph.Add(new Text("\tUsa Enjuague Bucal: ").SetFont(fontSubtitulos));
                higieneOralParagraph.Add(new Text($"{ReportePdfhistorialClinico.AntecedenteHigieneOral.EnjuagueBucalDto}"));
                higieneOralParagraph.Add(new Text("\tSangrado de Encías: ").SetFont(fontSubtitulos));
                higieneOralParagraph.Add(new Text($"{ReportePdfhistorialClinico.AntecedenteHigieneOral.SangradoEnciasDto}"));
                document.Add(higieneOralParagraph);

              

              

                // Odontogramas
                document.Add(new Paragraph("ODONTOGRAMA").SetPaddingBottom(0).SetMarginBottom(0).SetFont(fontSubtitulos).SetUnderline());

                // Añadir imágenes combinadas de odontogramas
                if (!string.IsNullOrEmpty(request.Images?.Adultos))
                {
                    document.Add(new Paragraph("ODONTOGRAMA - PIEZAS PERMANENTES").SetMarginTop(0).SetFont(fontSubtitulos).SetUnderline());
                    var adultosImage = ConvertBase64ToImage(request.Images.Adultos);
                    document.Add(new Image(adultosImage).ScaleToFit(450, 350).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER));
                }

                if (!string.IsNullOrEmpty(request.Images?.Ninos))
                {
                    document.Add(new Paragraph("ODONTOGRAMA - PIEZAS TEMPORALES").SetFont(fontSubtitulos).SetUnderline());
                    var ninosImage = ConvertBase64ToImage(request.Images.Ninos);
                    document.Add(new Image(ninosImage).ScaleToFit(300, 200).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER));
                }

                // Odontogramas Table
                if (ReportePdfhistorialClinico.Odontogramas.Any())
                {
                    document.Add(new Paragraph("PIEZAS DENTALES").SetFontSize(12).SetMarginTop(5).SetFont(fontSubtitulos).SetUnderline());
                    Table odontogramaTable = new Table(3).SetTextAlignment(TextAlignment.CENTER);
                    odontogramaTable.SetWidth(UnitValue.CreatePercentValue(100));

                    odontogramaTable.AddHeaderCell("Nº Pieza Dental");
                    odontogramaTable.AddHeaderCell("Cara Pieza Dental");
                    odontogramaTable.AddHeaderCell("Afección");

                    foreach (var odontograma in ReportePdfhistorialClinico.Odontogramas)
                    {
                        odontogramaTable.AddCell(odontograma.NroPiezaDental.ToString()).SetFontSize(10);
                        odontogramaTable.AddCell(odontograma.CaraPiezaDental).SetFontSize(10);

                        switch (odontograma.IdAfeccion)
                        {
                            case 1:
                                odontogramaTable.AddCell(new Paragraph("Lesión de Caries Activa"));
                                break;
                            case 2:
                                odontogramaTable.AddCell(new Paragraph("Obturado y Cariado"));
                                break;
                            case 3:
                                odontogramaTable.AddCell(new Paragraph("Obturado sin Caries"));
                                break;
                            case 4:
                                odontogramaTable.AddCell(new Paragraph("Perdido por Caries"));
                                break;
                            case 5:
                                odontogramaTable.AddCell(new Paragraph("Perdido por otra Razón"));
                                break;
                            case 6:
                                odontogramaTable.AddCell(new Paragraph("Soporte de Puente Corono Especial o Funda"));
                                break;
                            case 7:
                                odontogramaTable.AddCell(new Paragraph("No Erupcionado"));
                                break;
                            case 8:
                                odontogramaTable.AddCell(new Paragraph("Traumatismos"));
                                break;
                            case 9:
                                odontogramaTable.AddCell(new Paragraph("No Registrado"));
                                break;
                            default:
                                odontogramaTable.AddCell(new Paragraph("Otro"));
                                break;
                        }
                    }

                    document.Add(odontogramaTable).SetFontSize(10);
                }

                // Tratamientos
                if (ReportePdfhistorialClinico.Tratamientos.Any())
                {
                    document.Add(new Paragraph("TRATAMIENTO").SetFontSize(12).SetMarginTop(5).SetFont(fontSubtitulos).SetUnderline());
                    foreach (var tratamiento in ReportePdfhistorialClinico.Tratamientos)
                    {
                        // Tratamiento ID
                        Paragraph tratamientoIdParagraph = new Paragraph().SetFontSize(10).SetFixedLeading(10f);
                        tratamientoIdParagraph.Add(new Text("Tratamiento ID: ").SetFont(fontSubtitulos));
                        tratamientoIdParagraph.Add(new Text($"{tratamiento.IdTratamientoDto}"));
                        document.Add(tratamientoIdParagraph);

                        // Fecha
                        Paragraph fechaParagraph = new Paragraph().SetFontSize(10).SetFixedLeading(10f);
                        fechaParagraph.Add(new Text("Fecha: ").SetFont(fontSubtitulos));
                        fechaParagraph.Add(new Text($"{tratamiento.FechaTratamientoDto:dd/MM/yyyy}"));
                        document.Add(fechaParagraph);

                        // Subjetivo
                        Paragraph subjetivoParagraph = new Paragraph().SetFontSize(10).SetFixedLeading(10f);
                        subjetivoParagraph.Add(new Text("Subjetivo: ").SetFont(fontSubtitulos));
                        subjetivoParagraph.Add(new Text($"{tratamiento.SubjetivoDto}"));
                        document.Add(subjetivoParagraph);

                        // Objetivo
                        Paragraph objetivoParagraph = new Paragraph().SetFontSize(10).SetFixedLeading(10f);
                        objetivoParagraph.Add(new Text("Objetivo: ").SetFont(fontSubtitulos));
                        objetivoParagraph.Add(new Text($"{tratamiento.ObjetivoDto}"));
                        document.Add(objetivoParagraph);

                        // Análisis
                        Paragraph analisisParagraph = new Paragraph().SetFontSize(10).SetFixedLeading(10f);
                        analisisParagraph.Add(new Text("Análisis: ").SetFont(fontSubtitulos));
                        analisisParagraph.Add(new Text($"{tratamiento.AnalisisDto}"));
                        document.Add(analisisParagraph);

                        // Plan de Acción
                        Paragraph planAccionParagraph = new Paragraph().SetFontSize(10).SetFixedLeading(10f);
                        planAccionParagraph.Add(new Text("Plan de Acción: ").SetFont(fontSubtitulos));
                        planAccionParagraph.Add(new Text($"{tratamiento.PlanAccionDto}"));
                        document.Add(planAccionParagraph);

                        // Concluido
                        //   Paragraph concluidoParagraph = new Paragraph().SetFontSize(10).SetFixedLeading(10f);
                        //    concluidoParagraph.Add(new Text("Concluido: ").SetFont(fontSubtitulos));
                        //    concluidoParagraph.Add(new Text($"{tratamiento.TratamientoConcluidoDto}"));
                        //    document.Add(concluidoParagraph);

                        if (tratamiento.AvanceTratamientoDtos.Any())
                        {
                            document.Add(new Paragraph("Avances:").SetFont(fontSubtitulos).SetUnderline());
                            foreach (var avance in tratamiento.AvanceTratamientoDtos)
                            {
                                document.Add(new Paragraph($"- Fecha Inicio: {avance.FechaInicioDto:dd/MM/yyyy} - Fecha de Finalización: {avance.FechaConclusionDto:dd/MM/yyyy}, Pieza Dental: {avance.PiezaDentalDto}, Avance: {avance.AvanceDto}"));
                            }
                        }
                        // Presupuesto Total
                        Paragraph presupuestoParagraph = new Paragraph().SetFontSize(10).SetFixedLeading(10f);
                        presupuestoParagraph.Add(new Text("Presupuesto Total: ").SetFont(fontSubtitulos));
                        presupuestoParagraph.Add(new Text($"{tratamiento.PresupuestoTotalDto:C}"));
                        document.Add(presupuestoParagraph);

                        // A Cuenta
                        Paragraph cuentaParagraph = new Paragraph().SetFontSize(10).SetFixedLeading(10f);
                        cuentaParagraph.Add(new Text("A Cuenta: ").SetFont(fontSubtitulos));
                        cuentaParagraph.Add(new Text($"{tratamiento.ACuentaDto:C}"));
                        document.Add(cuentaParagraph);

                        // Saldo
                        Paragraph saldoParagraph = new Paragraph().SetFontSize(10).SetFixedLeading(10f);
                        saldoParagraph.Add(new Text("Saldo: ").SetFont(fontSubtitulos));
                        saldoParagraph.Add(new Text($"{tratamiento.SaldoDto:C}"));
                        document.Add(saldoParagraph);

                        if (tratamiento.PagosTratamientoDtos.Any())
                        {
                            document.Add(new Paragraph("PAGOS REALIZADOS").SetFont(fontSubtitulos).SetUnderline());
                            foreach (var pago in tratamiento.PagosTratamientoDtos)
                            {
                                document.Add(new Paragraph($"- Fecha: {pago.FechaPagoDto:dd/MM/yyyy}, Monto: {pago.MontoDto:C}"));
                            }
                        }
                    }
                }

                document.Close();

                // Return the PDF as a downloadable file
                return File(ms.ToArray(), "application/pdf", $"HistorialClinico-{ReportePdfhistorialClinico.Paciente.CodigoPacienteDto}.pdf");
            }
        }
        private iText.Kernel.Pdf.Xobject.PdfImageXObject ConvertBase64ToImage(string base64String)
        {
            // Eliminar el prefijo "data:image/png;base64,"
            var base64Data = base64String.Replace("data:image/png;base64,", "");
            var imageBytes = Convert.FromBase64String(base64Data);
            return new iText.Kernel.Pdf.Xobject.PdfImageXObject(iText.IO.Image.ImageDataFactory.Create(imageBytes));
        }
    }
    public class PdfRequestData
    {
        public int Id { get; set; }
        public PdfImageData Images { get; set; }
    }
    public class PdfImageData
    {
        public string Adultos { get; set; }
        public string Ninos { get; set; }
    }
}
