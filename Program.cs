using AutoMapper;
using m01_labMedicine.DTO.Pessoa.Enfermeiro;
using m01_labMedicine.DTO.Pessoa.Medico;
using m01_labMedicine.DTO.Pessoa.Paciente;
using m01_labMedicine.Model;
using m01_labMedicine.Services.AtendimentoMedico;
using m01_labMedicine.Services.Enfermeiro;
using m01_labMedicine.Services.Medico;
using m01_labMedicine.Services.Paciente;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo { Title = "LabMedicine API", Version = "v1" }
    );
    
    options.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});



string connectionString = "Server=DESKTOP-6HE46OL\\SQLEXPRESS;Database=labmedicinebd;Trusted_Connection=True;TrustServerCertificate=True;";

//Injeção de Dependencia do Context

builder.Services.AddDbContext<LabMedicineContext>(o => o.UseSqlServer(connectionString));
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IMedicoService, MedicoService>();
builder.Services.AddScoped<IEnfermeiroService, EnfermeiroService>();
builder.Services.AddScoped<IAtendimentoMedicoService, AtendimentoMedicoService>();


var config = new MapperConfiguration(cfg =>
{
    // Adicione seus perfis de mapeamento aqui, se necessário
    cfg.CreateMap<PacienteRequestDTO, PacienteModel>()
        .ForMember(dest => dest.NomeCompleto, opt => opt.MapFrom(src => src.Nome))
        .ForMember(dest => dest.TotalAtendimentos, opt => opt.Ignore())
        .ForMember(dest => dest.Id, opt => opt.Ignore());

    cfg.CreateMap<PacienteModel, PacienteResponseDTO>()
        .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.NomeCompleto))
        .ForMember(dest => dest.Atendimentos, opt => opt.MapFrom(src => src.TotalAtendimentos));

    cfg.CreateMap<PacienteUpdateDTO, PacienteModel>()
        .ForMember(dest => dest.NomeCompleto, opt => opt.MapFrom(src => src.Nome))
        .ForMember(dest => dest.TotalAtendimentos, opt => opt.Ignore())
        .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.CPF, opt => opt.Ignore());

    cfg.CreateMap<MedicoRequestDTO, MedicoModel>()
        .ForMember(dest => dest.NomeCompleto, opt => opt.MapFrom(src => src.Nome))
        .ForMember(dest => dest.InstituicaoEnsinoFormacao, opt => opt.MapFrom(src => src.InstituicaoEnsino))
        .ForMember(dest => dest.CrmUF, opt => opt.MapFrom(src => src.CRMUF))
        .ForMember(dest => dest.EstadoSistema, opt => opt.MapFrom(src => src.SituacaoSistema))
        .ForMember(dest => dest.TotalAtendimentosRealizados, opt => opt.MapFrom(src => src.TotalAtendimentos))
        .ForMember(dest => dest.Id, opt => opt.Ignore());

    cfg.CreateMap<MedicoModel, MedicoResponseDTO>()
        .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.NomeCompleto))
        .ForMember(dest => dest.Atendimentos, opt => opt.MapFrom(src => src.TotalAtendimentosRealizados));

    cfg.CreateMap<MedicoUpdateDTO, MedicoModel>()
        .ForMember(dest => dest.NomeCompleto, opt => opt.MapFrom(src => src.Nome))
        .ForMember(dest => dest.InstituicaoEnsinoFormacao, opt => opt.MapFrom(src => src.InstituicaoEnsino))
        .ForMember(dest => dest.CrmUF, opt => opt.MapFrom(src => src.CRMUF))
        .ForMember(dest => dest.EstadoSistema, opt => opt.MapFrom(src => src.SituacaoSistema))
        .ForMember(dest => dest.TotalAtendimentosRealizados, opt => opt.MapFrom(src => src.TotalAtendimentos))
        .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.CPF, opt => opt.Ignore());

    cfg.CreateMap<EnfermeiroRequestDTO, EnfermeiroModel>()
        .ForMember(dest => dest.NomeCompleto, opt => opt.MapFrom(src => src.Nome))
        .ForMember(dest => dest.InstituicaoEnsinoFormacao, opt => opt.MapFrom(src => src.InstituicaoEnsino))
        .ForMember(dest => dest.Id, opt => opt.Ignore());

    cfg.CreateMap<EnfermeiroModel, EnfermeiroResponseDTO>()
        .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.NomeCompleto))
        .ForMember(dest => dest.InstituicaoEnsino, opt => opt.MapFrom(src => src.InstituicaoEnsinoFormacao))
        .ForMember(dest => dest.Atendimentos, opt => opt.Ignore());

    cfg.CreateMap<EnfermeiroUpdateDTO, EnfermeiroModel>()
        .ForMember(dest => dest.NomeCompleto, opt => opt.MapFrom(src => src.Nome))
        .ForMember(dest => dest.InstituicaoEnsinoFormacao, opt => opt.MapFrom(src => src.InstituicaoEnsino))
        .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.CPF, opt => opt.Ignore());

});
config.AssertConfigurationIsValid();

var mapper = config.CreateMapper();

// Registre o Mapper como um serviço em seu contêiner de injeção de dependência
builder.Services.AddSingleton<IMapper>(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
